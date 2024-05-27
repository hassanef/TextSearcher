using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ConsoleApp1;
using ConsoleApp1.BruteForce;
using System.Text;


[MemoryDiagnoser]
public class SearchBenchmark
{
    private readonly List<List<List<TextSegment>>> document;
    private readonly List<List<List<TextSegment2>>> document2;
    private readonly TextSearcherBruteForceWithRecord textSearcherBruteForceWithRecord = new();
    private readonly TextSearcherBruteForceWithClass textSearcherBruteForceWithClass = new();

    public SearchBenchmark()
    {
        var ocrSimulation = new OCRSimulation();
        var rawDocument = ocrSimulation.GenerateDocument(50, 50);
        var rawDocument2 = ocrSimulation.GenerateDocument2(50, 50);
        var lineBuilder = new LineBuilder();
        document = rawDocument.Select(page => lineBuilder.BuildLines(page)).ToList();
        document2 = rawDocument2.Select(page => lineBuilder.BuildLines2(page)).ToList();
    }

   [Benchmark]
    public void BruteForceSearchWithStructRecord()
    {
        textSearcherBruteForceWithRecord.Search(document, "dear");
    }
    [Benchmark]
    public void BruteForceSearchWithClass()
    {
        textSearcherBruteForceWithClass.Search(document2, "dear");
    }
}

class Program
{
    static void Main(string[] args)
    {
           BenchmarkRunner.Run<SearchBenchmark>();
    }
}










