using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ConsoleApp1;
using ConsoleApp1.BruteForce;
using TextSearcher.BruteForce;


[MemoryDiagnoser]
public class SearchBenchmark
{
    private readonly List<List<List<TextSegmentStruct>>> documentUsingStructRecord;
    private readonly List<List<List<TextSegment>>> documentUsingClass;
    private readonly TextSearcherBruteForceUsingStructRecord textSearcherBruteForceUsingStructRecord = new();
    private readonly TextSearcherBruteForceUsingClass textSearcherBruteForceUsingClass = new();

    public SearchBenchmark()
    {
        var ocrSimulation = new OCRSimulation();
        var rawDocumentUsingStructRecord = ocrSimulation.GenerateDocumentUsingStructRecord(50, 50);
        var rawDocumentUsingClass = ocrSimulation.GenerateDocumentUsingClass(50, 50);
        var lineBuilder = new LineBuilder();
        documentUsingStructRecord = rawDocumentUsingStructRecord.Select(page => lineBuilder.BuildLines(page)).ToList();
        documentUsingClass = rawDocumentUsingClass.Select(page => lineBuilder.BuildLines(page)).ToList();
    }

   [Benchmark]
    public void BruteForceSearchWithStructRecord()
    {
        textSearcherBruteForceUsingStructRecord.Search(documentUsingStructRecord, "dear");
    }
    [Benchmark]
    public void BruteForceSearchWithClass()
    {
        textSearcherBruteForceUsingClass.Search(documentUsingClass, "dear");
    }
}

class Program
{
    static void Main(string[] args)
    {
           BenchmarkRunner.Run<SearchBenchmark>();
    }
}










