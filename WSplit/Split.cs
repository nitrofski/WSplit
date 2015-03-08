using System;
using System.Collections.Generic;

public class Split
{
    private String runTitle;
    private String runGoal;
    private int attemptsCount;
    private int startDelay;
    public CompareType CompType;
    private int liveIndex;
    public bool NewBestTime;
    private bool oldFaster;
    public List<Segment> segments = new List<Segment>();
    private String runFile;
    private bool unsavedSplits;

    public String RunTitle
    {
        set { this.runTitle = value; }
        get { return this.runTitle; }
    }

    public String RunGoal
    {
        set { this.runGoal = value; }
        get { return this.runGoal; }
    }

    public int AttemptsCount
    {
        set { this.attemptsCount = value; }
        get { return this.attemptsCount; }
    }

    public int StartDelay
    {
        set { this.startDelay = value; }
        get { return this.startDelay; }
    }

    public String RunFile
    {
        set { this.runFile = value; }
        get { return this.runFile; }
    }

    public Boolean UnsavedSplit
    {
        set { this.unsavedSplits = value; }
        get { return this.unsavedSplits; }
    }

    public void Add(Segment s)
    {
        this.segments.Add(s);
        if ((s.BestTime != 0.0) && ((s.BestTime < s.OldTime) || (s.OldTime == 0.0)))
        {
            this.oldFaster = false;
        }
        else
        {
            this.oldFaster = true;
        }
    }

    public void Clear()
    {
        this.segments.Clear();
        this.Reset();
    }

    public double CompTime()
    {
        return this.CompTime(Math.Min(this.liveIndex, this.LastIndex));
    }

    // Now has the ability to return the sum of bests as the comparison time
    public double CompTime(int index)
    {
        if ((index < 0) || (index > this.LastIndex))
        {
            return 0.0;
        }
        if (this.CompType != CompareType.Best)
        {
            if (this.CompType == CompareType.Old)
            {
                return this.segments[index].OldTime;
            }
            if (this.CompType == CompareType.SumOfBests)
            {
                return this.SumOfBests(index);
            }
            if (this.oldFaster)
            {
                return this.segments[index].OldTime;
            }
        }
        return this.segments[index].BestTime;
    }

    public double SumOfBests(int index)
    {
        double sum = 0.0;
        for (int i = 0; i <= index; ++i)
        {
            if (this.segments[i].BestSegment == 0.0)
                return 0.0;
            sum += this.segments[i].BestSegment;
        }
        return sum;
    }

    public void DoSplit(double time)
    {
        if (this.LiveRun && !this.Done)
        {
            this.segments[this.liveIndex].LiveTime = time;
            if ((this.liveIndex == this.LastIndex) && ((this.segments[this.LastIndex].LiveTime < this.segments[this.LastIndex].BestTime) || (this.segments[this.LastIndex].BestTime == 0.0)))
            {
                this.NewBestTime = true;
            }
            this.liveIndex++;
        }
    }

    public double LastDelta(int index)
    {
        if ((index > 0) && (index <= this.LastIndex))
        {
            for (int i = index - 1; i >= 0; i--)
            {
                if ((this.segments[i].LiveTime != 0.0) && (this.CompTime(i) != 0.0))
                {
                    return (this.segments[i].LiveTime - this.CompTime(i));
                }
            }
        }
        return 0.0;
    }

    public double LiveSegment(int index)
    {
        if ((index >= 0) && (index <= this.LastIndex))
        {
            double liveTime = this.segments[index].LiveTime;
            if ((index <= 0) || (liveTime <= 0.0))
            {
                return liveTime;
            }
            if (this.segments[index - 1].LiveTime > 0.0)
            {
                return (liveTime - this.segments[index - 1].LiveTime);
            }
        }
        return 0.0;
    }

    public void LiveToOld()
    {
        foreach (Segment segment in this.segments)
        {
            segment.OldTime = segment.LiveTime;
        }
    }

    public void Next()
    {
        if (this.liveIndex < this.LastIndex)
        {
            this.DoSplit(0.0);
        }
    }

    public void Previous()
    {
        this.liveIndex--;
        this.CurrentSegment.LiveTime = 0.0;
        this.NewBestTime = false;
    }

    public void Reset()
    {
        foreach (Segment segment in this.segments)
        {
            segment.BackupBest = segment.BestTime;
            segment.BackupBestSegment = segment.BestSegment;
            segment.LiveTime = 0.0;
        }
        this.liveIndex = 0;
        this.NewBestTime = false;
    }

    public void RestoreBest()
    {
        foreach (Segment segment in this.segments)
        {
            segment.BestTime = segment.BackupBest;
            segment.BestSegment = segment.BackupBestSegment;
        }
        if (this.LastIndex >= 0)
        {
            if ((this.segments[this.LastIndex].BestTime != 0.0) && ((this.segments[this.LastIndex].BestTime < this.segments[this.LastIndex].OldTime) || (this.segments[this.LastIndex].OldTime == 0.0)))
            {
                this.oldFaster = false;
            }
            else
            {
                this.oldFaster = true;
            }
        }
    }

    public double RunDelta(double time, int index)
    {
        if ((this.CompTime(index) > 0.0) && (time > 0.0))
        {
            return (time - this.CompTime(index));
        }
        return 0.0;
    }

    public double RunDeltaAt(int index)
    {
        if ((index >= 0) && (index <= this.LastIndex))
        {
            return this.RunDelta(this.segments[index].LiveTime, index);
        }
        return 0.0;
    }

    public double SegDelta(double time, int index)
    {
        if (this.CompTime(index) > 0.0)
        {
            return (this.RunDelta(time, index) - this.LastDelta(index));
        }
        return 0.0;
    }

    public bool NeedUpdate(bool bestOverall)
    {
        // This will never return true if no run is loaded.
        if (NewBestTime)
            return true;
        for (int i = 0; i <= this.LastIndex; ++i)
        {
            if (((!bestOverall && this.segments[i].LiveTime != 0.0) && (this.segments[i].LiveTime < this.segments[i].BestTime || this.segments[i].BestTime == 0.0)) // If !bestoverall and the split is faster than before
                || (this.segments[i].LiveTime != 0.0 && (i == 0 || this.segments[i - 1].LiveTime != 0.0) && (this.segments[i].BestSegment == 0.0 || this.LiveSegment(i) < this.segments[i].BestSegment)))   // Or if it's a new Best Segment
                return true;
        }
        return false;
    }

    public void UpdateBest(bool bestOverall)
    {
        // Updates each segment if needed
        foreach (Segment segment in this.segments)
        {
            if ((bestOverall && this.NewBestTime) || ((!bestOverall && (segment.LiveTime != 0.0)) && ((segment.LiveTime < segment.BestTime) || (segment.BestTime == 0.0))))
            {
                segment.BestTime = segment.LiveTime;
            }
        }

        // Gets rid of incoherences
        double bestTime = 0.0;
        for (int i = this.LastIndex; i >= 0; i--)
        {
            if (this.segments[i].BestTime != 0.0)
            {
                if (bestTime == 0.0)
                {
                    bestTime = this.segments[this.LastIndex].BestTime;
                }
                if (this.segments[i].BestTime > bestTime)
                {
                    this.segments[i].BestTime = 0.0;
                }
                else
                {
                    bestTime = this.segments[i].BestTime;
                }
            }
        }

        // Updates bests if needed
        for (int j = 0; j <= this.LastIndex; j++)
        {
            if (((this.segments[j].LiveTime != 0.0) && ((j == 0) || (this.segments[j - 1].LiveTime != 0.0))) && ((this.LiveSegment(j) < this.segments[j].BestSegment) || (this.segments[j].BestSegment == 0.0)))
            {
                this.segments[j].BestSegment = this.LiveSegment(j);
            }
        }
        if (this.LastIndex >= 0)
        {
            if ((this.segments[this.LastIndex].BestTime != 0.0) && ((this.segments[this.LastIndex].BestTime < this.segments[this.LastIndex].OldTime) || (this.segments[this.LastIndex].OldTime == 0.0)))
            {
                this.oldFaster = false;
            }
            else
            {
                this.oldFaster = true;
            }
        }

        NewBestTime = false;
    }

    public CompareType ComparingType
    {
        get
        {
            if (this.CompType != CompareType.Fastest)
            {
                return this.CompType;
            }
            if (this.oldFaster)
            {
                return CompareType.Old;
            }
            return CompareType.Best;
        }
    }

    public int Count
    {
        get
        {
            return this.segments.Count;
        }
    }

    public Segment CurrentSegment
    {
        get
        {
            if (this.LiveRun && !this.Done)
            {
                return this.segments[this.liveIndex];
            }
            return new Segment(null);
        }
    }

    public bool Done
    {
        get
        {
            return (this.LiveRun && (this.LiveIndex > this.LastIndex));
        }
    }

    public int LastIndex
    {
        get
        {
            return (this.segments.Count - 1);
        }
    }

    public Segment LastSegment
    {
        get
        {
            if (this.LiveRun)
            {
                return this.segments[this.LastIndex];
            }
            return new Segment(null);
        }
    }

    public int LiveIndex
    {
        get
        {
            return this.liveIndex;
        }
    }

    public bool LiveRun
    {
        get
        {
            return (this.segments.Count > 0);
        }
    }

    public enum CompareType
    {
        Fastest,
        Old,
        Best,
        SumOfBests
    }
}

