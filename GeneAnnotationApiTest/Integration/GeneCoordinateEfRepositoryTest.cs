using GeneAnnotationApi.Entities;
using GeneAnnotationApi.Repositories.EntityFramework;
using GeneAnnotationApiTest.TestData;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class GeneCoordinateEfRepositoryTest: BaseRepositoryTest<GeneCoordinateEfRepository, GeneCoordinate>
    {
        
        [Fact]
        public void TestMin()
        {
            _context.Gene.Add(GeneTestData.Genes[0]);
            _context.GeneLocation.Add(GeneLocationTestData.GeneLocations[0]);
            _context.GeneCoordinate.Add(GeneCoordinateTestData.GeneCoordinates[0]);
            _context.SaveChanges();
            var gene = _context.Gene.Find(1);
            var min = _repository.FindMinByGene(gene);
            
            Assert.True(min == GeneCoordinateTestData.GeneCoordinates[0].Start);
        }
    }
}