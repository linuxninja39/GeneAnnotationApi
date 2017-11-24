using GeneAnnotationApi.AutoMapperProfiles.CustomResolvers;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApiTest.TestData
{
    public class GeneCoordinateTestData
    {
        public static readonly GeneCoordinate[] GeneCoordinates =
        {
            new GeneCoordinate
            {
                GeneLocation = GeneLocationTestData.GeneLocations[0],
                Start = 10,
                End = 20
            }
        };
    }
}