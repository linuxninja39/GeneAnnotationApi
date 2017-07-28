using System.Collections.Generic;
using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles.CustomResolvers
{
    public class GeneVariantAnnotationResolver: IValueResolver<GeneVariant, GeneVariantJsonModel, AnnotationJsonModel[]>
    {
        public AnnotationJsonModel[] Resolve(
            GeneVariant source,
            GeneVariantJsonModel destination,
            AnnotationJsonModel[] destMember,
            ResolutionContext context
            )
        {
            var originTypes = new List<AnnotationJsonModel>();
            foreach (var geneorigintype in source.AnnotationGeneVariant)
            {
                originTypes.Add(new AnnotationJsonModel());
            }
            return originTypes.ToArray();
        }
    }
}