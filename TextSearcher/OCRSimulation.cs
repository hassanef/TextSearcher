using ConsoleApp1.BruteForce;

namespace ConsoleApp1
{
    public class OCRSimulation
    {
        private static readonly Random Random = new Random();
        private static readonly string[] SampleTexts = { "Dear", "lunch", "Sir", "afternOOn", "Madam", "invite" };

        public List<List<TextSegmentStruct>> GenerateDocumentUsingStructRecord(int pages, int linesPerPage)
        {
            var document = new List<List<TextSegmentStruct>>();
            for (int i = 0; i < pages; i++)
            {
                var page = new List<TextSegmentStruct>();
                for (int j = 0; j < linesPerPage; j++)
                {
                    var segment = new TextSegmentStruct(SampleTexts[Random.Next(SampleTexts.Length)], Random.Next(0, 100),
                        Random.Next(100, 200),
                        j * 20,
                        j * 20 + 10);
                    page.Add(segment);
                }
                document.Add(page);
            }
            return document;
        }
        public List<List<TextSegment>> GenerateDocumentUsingClass(int pages, int linesPerPage)
        {
            var document = new List<List<TextSegment>>();
            for (int i = 0; i < pages; i++)
            {
                var page = new List<TextSegment>();
                for (int j = 0; j < linesPerPage; j++)
                {
                    var segment = new TextSegment(SampleTexts[Random.Next(SampleTexts.Length)], Random.Next(0, 100),
                        Random.Next(100, 200),
                        j * 20,
                        j * 20 + 10);
                    page.Add(segment);
                }
                document.Add(page);
            }
            return document;
        }
    }
}
