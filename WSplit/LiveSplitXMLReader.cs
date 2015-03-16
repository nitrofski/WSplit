using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Collections;

public class LiveSplitXMLReader
{
    private const String GAME_NAME = "GameName";
    private const String CATEGORY_NAME = "CategoryName";
    private const String OFFSET = "Offset";
    private const String ATTEMPTS_COUNT = "AttemptCount";
    private const String SEGMENTS = "Segments";
    private const String SEGMENT_NAME = "Name";
    private const String SPLIT_TIMES = "SplitTimes";
    private const String BEST_SEGMENT = "BestSegmentTime";

    private Split split;
    private XmlDocument xmlDocument;

    public LiveSplitXMLReader()
    {
        this.split = new Split();
        xmlDocument = new XmlDocument();
    }

    /// <summary>
    /// Read split information from the file and returns it.
    /// </summary>
    /// <param name="file">The filename of the LiveSplit XML file.</param>
    /// <returns>The split with all its information.</returns>
    public Split ReadSplit(String file)
    {
        xmlDocument.Load(file);
        StringBuilder stringBuilder = new StringBuilder();
        String runTitle = "";
        String attemptsCountString = "";
        String startDelay = "";
        int attemptsCount = 0;
        XmlNodeList rootNode;
        List<Segment> segments = new List<Segment>();

        rootNode = xmlDocument.DocumentElement.SelectNodes("/Run");
        foreach(XmlNode runNode in rootNode)
        {
            foreach (XmlNode infoNode in runNode.ChildNodes)
            {
                if (infoNode.Name == GAME_NAME)
                {
                    stringBuilder.Append(infoNode.InnerText + " ");
                }
                else if (infoNode.Name == CATEGORY_NAME)
                {
                    stringBuilder.Append(infoNode.InnerText);
                }
                else if (infoNode.Name == OFFSET)
                {
                    startDelay = infoNode.InnerText;
                }
                else if (infoNode.Name == ATTEMPTS_COUNT)
                {
                    attemptsCountString = infoNode.InnerText;
                }
                else if (infoNode.Name == SEGMENTS)
                {
                    PopulateSegments(segments, infoNode);
                }
            }  
        }

        runTitle = stringBuilder.ToString();
        this.split.RunTitle = runTitle;
        this.split.StartDelay = SetRunDelay(startDelay);
        Int32.TryParse(attemptsCountString, out attemptsCount);
        this.split.AttemptsCount = attemptsCount;
        this.split.segments = segments;
        return this.split;
    }

    /// <summary>
    /// In livesplit you can have a delay or start the run later. In wsplit you can only delay the start of a run.
    /// So in livesplit if there is a delay, there will be a "-" in front of the time. If we see this "-" we read after the time after it
    /// to set the time delay in the Split. If there is not "-" it means that the run start later and we do nothing with it.
    /// </summary>
    /// <param name="delayString">The initial delay string from the file.</param>
    /// <returns>The delay into the form of a int.</returns>
    private int SetRunDelay(String delayString)
    {
        String delayStringModified = "";
        int delay = 0;

        if (delayString.IndexOf('-') != -1)
        {
            delayStringModified = delayString.Remove(0, 1);
            delay = ParseDelayString(delayStringModified);
        }

        return delay;
    }

    /// <summary>
    /// Take the delay string from the livesplit file and convert it into int.
    /// </summary>
    /// <param name="delayString">The delay into the form of a String.</param>
    /// <returns>The delay into the form of a int.</returns>
    private int ParseDelayString(String delayString)
    {
        int delay = 0;
        int delayTimeSection = 0;
        String[] timeSection = delayString.Split(':');
        String[] secondsAndMilliseconds = timeSection[timeSection.Length - 1].Split('.');
        //Millseconds
        if (secondsAndMilliseconds.Length == 2)
        {
            if (Int32.TryParse(secondsAndMilliseconds[1].Substring(0, 2), out delayTimeSection))
            {
                delay += (delayTimeSection * 10);
            }
        }
        //Seconds
        if (Int32.TryParse(secondsAndMilliseconds[0], out delayTimeSection))
        {
            delay += (delayTimeSection * 1000);
        }
        //Minutes
        if (Int32.TryParse(timeSection[1], out delayTimeSection))
        {
            delay += (delayTimeSection * 60 * 1000);
        }
        //Hours
        if (Int32.TryParse(timeSection[0], out delayTimeSection))
        {
            delay += (delayTimeSection * 3600 * 1000);
        }
        return delay;
    }

    /// <summary>
    /// Read the segments from the file and populate the segment list.
    /// </summary>
    /// <param name="segments">The array list of segments.</param>
    /// <param name="segmentsNode">The node containing the segments in the xml file.</param>
    private void PopulateSegments(List<Segment> segments, XmlNode segmentsNode)
    {
        Segment newSegment;
        String segmentName = "";
        double segmentBestTime = 0.0;
        double segmentBestSegment = 0.0;
        XmlNode nodeSegmentTime;
        foreach(XmlNode segmentNode in segmentsNode.ChildNodes)
        {
            foreach (XmlNode segmentInfoNode in segmentNode.ChildNodes)
            {
                if (segmentInfoNode.Name == SEGMENT_NAME)
                {
                    segmentName = segmentInfoNode.InnerText;
                }
                else if (segmentInfoNode.Name == SPLIT_TIMES)
                {
                    nodeSegmentTime = segmentInfoNode.FirstChild.FirstChild;
                    segmentBestTime = WSplitUtil.timeParse(nodeSegmentTime.InnerText);
                }
                else if (segmentInfoNode.Name == BEST_SEGMENT)
                {
                    nodeSegmentTime = segmentInfoNode.FirstChild;
                    segmentBestSegment = WSplitUtil.timeParse(nodeSegmentTime.InnerText);
                }
            }
            newSegment = new Segment(segmentName, 0.0, segmentBestTime, segmentBestSegment);
            segments.Add(newSegment);
        }
    }
}

