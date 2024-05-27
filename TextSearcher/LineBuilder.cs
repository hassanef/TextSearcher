using ConsoleApp1.BruteForce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class LineBuilder
    {
        public List<List<TextSegment>> BuildLines(List<TextSegment> segments)
        {
            segments.Sort((a, b) => a.Left.CompareTo(b.Left));
            var lines = new List<List<TextSegment>>();
            var currentLine = new List<TextSegment>();
            int currentTop = segments[0].Top;
            int currentBottom = segments[0].Bottom;

            foreach (var segment in segments)
            {
                // Check for vertical alignment with some tolerance for jitter
                if (segment.Top <= currentBottom + 5 && segment.Bottom >= currentTop - 5)
                {
                    currentLine.Add(segment);
                    currentTop = Math.Min(currentTop, segment.Top);
                    currentBottom = Math.Max(currentBottom, segment.Bottom);
                }
                else
                {
                    lines.Add(currentLine);
                    currentLine = new List<TextSegment> { segment };
                    currentTop = segment.Top;
                    currentBottom = segment.Bottom;
                }
            }
            lines.Add(currentLine);

            return lines;
        }
        public List<List<TextSegment2>> BuildLines2(List<TextSegment2> segments)
        {
            segments.Sort((a, b) => a.Left.CompareTo(b.Left));
            var lines = new List<List<TextSegment2>>();
            var currentLine = new List<TextSegment2>();
            int currentTop = segments[0].Top;
            int currentBottom = segments[0].Bottom;

            foreach (var segment in segments)
            {
                // Check for vertical alignment with some tolerance for jitter
                if (segment.Top <= currentBottom + 5 && segment.Bottom >= currentTop - 5)
                {
                    currentLine.Add(segment);
                    currentTop = Math.Min(currentTop, segment.Top);
                    currentBottom = Math.Max(currentBottom, segment.Bottom);
                }
                else
                {
                    lines.Add(currentLine);
                    currentLine = new List<TextSegment2> { segment };
                    currentTop = segment.Top;
                    currentBottom = segment.Bottom;
                }
            }
            lines.Add(currentLine);

            return lines;
        }
    }
}
