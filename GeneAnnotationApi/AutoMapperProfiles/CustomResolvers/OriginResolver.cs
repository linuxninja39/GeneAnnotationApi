using System.Collections.Generic;
using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles.CustomResolvers
{
    public class OriginResolver: IValueResolver<Gene, GeneJsonModel, OriginJsonType[]>
    {
        public OriginJsonType[] Resolve(
            Gene source,
            GeneJsonModel destination,
            OriginJsonType[] destMember,
            ResolutionContext context
            )
        {
            var originTypes = new List<OriginJsonType>();
            foreach (var geneorigintype in source.GeneOriginType)
            {
                originTypes.Add(new OriginJsonType{Name = geneorigintype.OriginType.Name});
            }
            return originTypes.ToArray();
        }
    }
}