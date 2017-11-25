using System.Linq;
using GeneAnnotationApi.Data;
using Microsoft.Extensions.Logging;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class LoadHugoDataTest: BaseDbTest
    {
        [Fact]
        public void LoadDataTest()
        {
            LoadHugoData.LoadData(Context, "hugo.txt.short");

            var logger = LoggerFactory.CreateLogger<LoadHugoDataTest>();
            
            logger.LogDebug("bla");

            Assert.Equal(19, Context.Gene.Count());
        }
    }
}