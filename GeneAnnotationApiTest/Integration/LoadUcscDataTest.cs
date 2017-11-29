using GeneAnnotationApi.Data;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class LoadUcscDataTest : BaseDbTest
    {
        [Fact]
        public void LoadFileTest()
        {
            var ucscLoader = new LoadUcscData(Context, "ucsc.txt.short");
            var ex = Record.Exception(
                () => { ucscLoader.LoadFile(); }
            );

            Assert.Null(ex);
        }
    }
}