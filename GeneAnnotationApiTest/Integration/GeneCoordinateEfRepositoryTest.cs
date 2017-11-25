using GeneAnnotationApi.Entities;
using GeneAnnotationApi.Repositories.EntityFramework;
using GeneAnnotationApiTest.TestData;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class GeneCoordinateEfRepositoryTest: BaseRepositoryTest<GeneCoordinateEfRepository, GeneCoordinate>
    {

        public GeneCoordinateEfRepositoryTest() : base(true)
        {
            Context.Gene.Add(GeneTestData.Genes[0]);
            Context.GeneLocation.Add(GeneLocationTestData.GeneLocations[0]);
            Context.GeneCoordinate.Add(GeneCoordinateTestData.GeneCoordinates[0]);
            Context.SaveChanges();
        }
        
        [Fact]
        public void TestMin()
        {
            var gene = Context.Gene.Find(1);
            var min = Repository.FindMinByGene(gene);
            
            Assert.True(min == GeneCoordinateTestData.GeneCoordinates[0].Start);
        }
        
        [Fact]
        public void TestMax()
        {
            var gene = Context.Gene.Find(1);
            var max = Repository.FindMaxByGene(gene);
            
            Assert.True(max == GeneCoordinateTestData.GeneCoordinates[0].End);
        }
    }
}