using System.Linq;
using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneVariantProfile: Profile
    {
        public GeneVariantProfile()
        {
            CreateMap<GeneVariant, GeneVariantJsonModel>()
                .ForMember(
                    destinationMember => destinationMember.Annotation,
                    opt => opt.MapFrom(
                            geneVariant =>
                                geneVariant.AnnotationGeneVariant.Select(
                                    annotationGeneVariant => annotationGeneVariant.Annotation
                                )
                                .ToArray()
                        )
                )
                .ForMember(
                    destinationMember => destinationMember.Literature,
                    opt => opt.MapFrom(
                        geneVariant => geneVariant.GeneVariantLiterature.Select(
                            literatureGeneVariant => literatureGeneVariant.Literature
                            ).ToArray()
                        )
                    )
                ;
        }
    }
}