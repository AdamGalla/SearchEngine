using BenchmarkDotNet.Attributes;
using FeatureHubSDK;

namespace TestDataFormatter;

[MemoryDiagnoser]
public class Benchmark
{
    [Benchmark]
    public IEnumerable<IClientContext> TestFeatureHubClientBuilder()
    {
        for (int i = 0; i < 3000; i++)
        {
            var featureHubCfg = new EdgeFeatureHubConfig("http://localhost:8085", "3996fba9-d379-4fa2-bbd4-0d027cf0c694/WZJlb0y374aZelOnCLtKtw76OFEErv38GRLUAnmO");
            yield return featureHubCfg.NewContext().Build().GetAwaiter().GetResult();
        }
    }
}
