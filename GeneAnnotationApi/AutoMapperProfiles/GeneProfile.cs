using System.Linq;
using AutoMapper;
using GeneAnnotationApi.AutoMapperProfiles.CustomResolvers;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneProfile: Profile
    {
        public GeneProfile()
        {
            CreateMap<Gene, GeneDto>()
                .ForMember(
                    destinationMember => destinationMember.Origin,
                    opt => opt.MapFrom(
                        gene => gene.GeneOriginType.Select(geneOriginType => geneOriginType.OriginType)
                        )
                )
                .ForMember(
                    destinationMember => destinationMember.Annotation,
                    opt => opt.MapFrom(
                        gene => gene.AnnotationGene.Select(annotationGene => annotationGene.Annotation)
                    )
                )
                ;
            
           CreateMap<GeneDto, Gene>()
                .ForMember(
                    gene => gene.GeneOriginType,
                    opt => opt
                        .ResolveUsing<OriginDtoToGeneOriginTypeResolver>()
                )
               .ForMember(
                   geneEntity => geneEntity.AnnotationGene,
                   opt => opt.Ignore()
                   )
               .ForMember(
                   geneEntity => geneEntity.NameSynonym,
                   opt => opt.Ignore()
                   )
               .ForMember(
                   geneEntity => geneEntity.EnsembleId,
                   opt => opt.Ignore()
                   )
               .ForMember(
                   geneEntity => geneEntity.Ucsc,
                   opt => opt.Ignore()
                   )
               .ForMember(
                   geneEntity => geneEntity.Accession,
                   opt => opt.Ignore()
                   )
               .ForMember(
                   geneEntity => geneEntity.Refseq,
                   opt => opt.Ignore()
                   )
               .ForMember(
                   geneEntity => geneEntity.OmimId,
                   opt => opt.Ignore()
                   )
                ;
        }
    
    }
    
}