using ConsoleApp1.BruteForce;

namespace ConsoleApp1
{
    public class LineBuilder
    {
        public List<List<TextSegmentStruct>> BuildLines(List<TextSegmentStruct> segments)
        {
            segments.Sort((a, b) => a.Left.CompareTo(b.Left));
            var lines = new List<List<TextSegmentStruct>>();
            var currentLine = new List<TextSegmentStruct>();
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
                    currentLine = new List<TextSegmentStruct> { segment };
                    currentTop = segment.Top;
                    currentBottom = segment.Bottom;
                }
            }
            lines.Add(currentLine);

            return lines;
        }
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
    }
}
