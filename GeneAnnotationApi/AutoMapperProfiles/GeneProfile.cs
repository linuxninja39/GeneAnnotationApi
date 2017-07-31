using System.Linq;
using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneProfile: Profile
    {
        public GeneProfile()
        {
            CreateMap<Gene, GeneJsonModel>()
                .ForMember(
                    destinationMember => destinationMember.Origin,
                    opt => opt.MapFrom(
                        gene => gene.GeneOriginType.Select(geneOriginType => geneOriginType.OriginType).ToArray()
                        )
                )
                .ForMember(
                    destinationMember => destinationMember.Annotation,
                    opt => opt.MapFrom(
                        gene => gene.AnnotationGene.Select(annotationGene => annotationGene.Annotation).ToArray()
                    )
                )
                ;
        }
    
    }
    
}