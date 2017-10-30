using GeneAnnotationApi.Entities;
using GeneAnnotationApi.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace GeneAnnotationApiTest.Unit
{
    public class BaseEfRepositoryTest
    {
        [Fact]
        public void constructorTest()
        {
            var o = new DbContextOptions<GeneAnnotationDBContext>();
            var contextMock = new Mock<GeneAnnotationDBContext>(o);
            var baseRepo = new BaseEfRepository<Gene>(contextMock.Object);
        }
    }
}