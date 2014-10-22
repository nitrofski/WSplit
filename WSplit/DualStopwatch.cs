using System;
using System.Diagnostics;

public class DualStopwatch
{
    private bool fallbackIsRunning;
    private long pauseTicks;
    private TimeSpan pauseTime = TimeSpan.Zero;
    private long startTicks;
    private DateTime startTime;
    private Stopwatch systimer = new Stopwatch();
    public bool useFallback;

    // Used for when the timer starts at a time greater than 0 ticks
    private TimeSpan startedAt = TimeSpan.Zero;

    public DualStopwatch(bool useFallbackMethod)
    {
        this.useFallback = useFallbackMethod;
    }

    public void Reset()
    {
        this.systimer.Reset();
        this.pauseTime = TimeSpan.Zero;
        this.pauseTicks = 0L;
        this.startedAt = TimeSpan.Zero;
        this.fallbackIsRunning = false;
    }

    public void StartAt(TimeSpan offset)
    {
        // If the timer wasn't already running and it was not paused, apply the offset
        if (!this.systimer.IsRunning && !this.fallbackIsRunning && this.pauseTime.Ticks >= 0L)
            startedAt = offset;
        Start();
    }

    public void Start()
    {
        this.systimer.Start();
        if (!this.fallbackIsRunning && this.pauseTime.Ticks > 0L)
        {
            this.startTime = DateTime.UtcNow - this.pauseTime;
            this.pauseTime = TimeSpan.Zero;
            this.startTicks = Environment.TickCount - this.pauseTicks;
            this.pauseTicks = 0L;
        }
        else
        {
            this.startTime = DateTime.UtcNow;
            this.startTicks = Environment.TickCount;
        }
        this.fallbackIsRunning = true;
    }

    public void Stop()
    {
        this.systimer.Stop();
        if (this.fallbackIsRunning)
        {
            this.pauseTime = (TimeSpan) (DateTime.UtcNow - this.startTime);
            this.pauseTicks = Environment.TickCount - this.startTicks;
            this.fallbackIsRunning = false;
        }
    }

    public double driftMilliseconds
    {
        get
        {
            return (this.fallbackElapsed.TotalMilliseconds - this.systimer.Elapsed.TotalMilliseconds);
        }
    }

    public TimeSpan Elapsed
    {
        get
        {
            if (this.useFallback)
            {
                return this.fallbackElapsed + startedAt;
            }
            return this.systimer.Elapsed + startedAt;
        }
    }

    public long ElapsedMilliseconds
    {
        get
        {
            if (this.useFallback)
            {
                return (long)Math.Truncate(this.fallbackElapsed.TotalMilliseconds + startedAt.TotalMilliseconds);
                //return (long) Math.Round(this.fallbackElapsed.TotalMilliseconds + startedAt.TotalMilliseconds);
            }
            return (long)Math.Truncate(this.systimer.ElapsedMilliseconds + startedAt.TotalMilliseconds);
            //return (long) Math.Round(this.systimer.ElapsedMilliseconds + startedAt.TotalMilliseconds);
        }
    }

    public long ElapsedTicks
    {
        get
        {
            return this.Elapsed.Ticks;
        }
    }

    private TimeSpan fallbackElapsed
    {
        get
        {
            if (!this.fallbackIsRunning)
            {
                return this.pauseTime;
            }
            TimeSpan span = (TimeSpan) (DateTime.UtcNow - this.startTime);
            TimeSpan span2 = TimeSpan.FromMilliseconds(Environment.TickCount - this.startTicks);
            if (span2 < TimeSpan.Zero)
            {
                span2 += TimeSpan.FromMilliseconds(4294967295);
            }
            double num = Math.Abs((double) (span.TotalMilliseconds - span2.TotalMilliseconds));
            if ((num > 500.0) && (Math.Abs((double) (num / span.TotalMilliseconds)) > 0.00013888888888888889))
            {
                return span2;
            }
            return span;
        }
    }

    public bool IsRunning
    {
        get
        {
            if (this.useFallback)
            {
                return this.fallbackIsRunning;
            }
            return this.systimer.IsRunning;
        }
    }
}

