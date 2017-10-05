using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Data.Constants
{
    public static class OriginTypeConstants
    {
        public static readonly OriginType[] OriginTypes = new[]
        {
            new OriginType {Id = 1, Name = "Origin 1"},
            new OriginType {Id = 2, Name = "Origin 2"}
        };
    }
}