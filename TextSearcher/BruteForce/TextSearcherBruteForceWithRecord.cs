using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.BruteForce
{
    public class TextSearcherBruteForceWithRecord
    {
        public List<(int PageIndex, int LineIndex)> Search(
            List<List<List<TextSegment>>> document,
            ReadOnlySpan<char> searchText,
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
        private ReadOnlySpan<char> MergeSegments(List<TextSegment> segments, bool caseSensitive)
        {
            if (segments.Count == 0)
            {
                return ReadOnlySpan<char>.Empty;
            }

            // Sort segments by their Left position
            segments.Sort((a, b) => a.Left.CompareTo(b.Left));

            string mergedText = caseSensitive ? segments[0].Text : segments[0].Text.ToLowerInvariant();
            var currentSegment = segments[0];

            for (int i = 1; i < segments.Count; i++)
            {
                var nextSegment = segments[i];

                // Check if the next segment overlaps horizontally with the current segment
                if (nextSegment.Left <= currentSegment.Right)
                {
                    // If they overlap, concatenate their texts
                    // Ensure we only add the overlapping part once
                    var overlapLength = currentSegment.Right - nextSegment.Left + 1;
                    var nonOverlapText = nextSegment.Text.AsSpan(Math.Max(0, overlapLength));
                    mergedText += caseSensitive ? nonOverlapText.ToString() : nonOverlapText.ToString().ToLowerInvariant();
                    currentSegment = new TextSegment(
                        currentSegment.Text + nextSegment.Text, // Merged text
                        currentSegment.Left,
                        Math.Max(currentSegment.Right, nextSegment.Right), // Expanded right boundary
                        currentSegment.Top,
                        currentSegment.Bottom);
                }
                else
                {
                    // If they don't overlap, add a space and start a new segment
                    mergedText += ' ' + (caseSensitive ? nextSegment.Text : nextSegment.Text.ToLowerInvariant());
                    currentSegment = nextSegment;
                }
            }

            return mergedText.AsSpan();
        }

    }
}
