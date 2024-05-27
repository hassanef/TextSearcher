using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.BruteForce
{
    public class TextSearcherBruteForceWithClass
    {
        public List<(int PageIndex, int LineIndex)> Search(
            List<List<List<TextSegment2>>> document,
            string searchText,
            bool caseSensitive = false)
        {
            var results = new List<(int PageIndex, int LineIndex)>();
            var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            for (int pageIndex = 0; pageIndex < document.Count; pageIndex++)
            {
                var page = document[pageIndex];
                for (int lineIndex = 0; lineIndex < page.Count; lineIndex++)
                {
                    var lineSegments = page[lineIndex];
                    var mergedLine = MergeSegments(lineSegments, caseSensitive);

                    if (mergedLine.Contains(searchText, comparison))
                    {
                        results.Add((pageIndex, lineIndex));
                    }
                }
            }

            return results;
        }
        private ReadOnlySpan<char> MergeSegments(List<TextSegment2> segments, bool caseSensitive)
        {
            if (segments.Count == 0)
            {
                return ReadOnlySpan<char>.Empty;
            }

            segments.Sort((a, b) => a.Left.CompareTo(b.Left));

            var mergedText = new StringBuilder();
            var currentSegment = segments[0];
            mergedText.Append(caseSensitive ? currentSegment.Text : currentSegment.Text.ToLowerInvariant());

            for (int i = 1; i < segments.Count; i++)
            {
                var nextSegment = segments[i];
                if (nextSegment.Left <= currentSegment.Right)
                {
                    var overlapLength = currentSegment.Right - nextSegment.Left + 1;
                    var nonOverlapText = nextSegment.Text.AsSpan(Math.Max(0, overlapLength));
                    mergedText.Append(caseSensitive ? nonOverlapText : nonOverlapText.ToString().ToLowerInvariant());
                    currentSegment = new TextSegment2(
                        currentSegment.Text + nextSegment.Text,
                        currentSegment.Left,
                        Math.Max(currentSegment.Right, nextSegment.Right),
                        currentSegment.Top,
                        currentSegment.Bottom);
                }
                else
                {
                    mergedText.Append(' ');
                    mergedText.Append(caseSensitive ? nextSegment.Text : nextSegment.Text.ToLowerInvariant());
                    currentSegment = nextSegment;
                }
            }

            return mergedText.ToString().AsSpan();
        }
    }
}
