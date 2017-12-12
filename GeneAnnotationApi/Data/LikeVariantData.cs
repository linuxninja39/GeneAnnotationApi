using System;
using System.Collections.Generic;
using System.Linq;
using GeneAnnotationApi.Data.Constants;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Data
{
    public class LikeVariantData
    {
        private readonly GeneAnnotationDBContext _context;
        private readonly IReadOnlyList<string> _currentRow;
        public readonly GeneVariant CurrentVariant;
        private IDictionary<string, VariantType> _variantTypeMap;

        public LikeVariantData(GeneAnnotationDBContext context, IReadOnlyList<string> currentRow)
        {
            _context = context;
            _currentRow = currentRow;
            CurrentVariant = new GeneVariant();

            SetupTypeMap();
        }

        public void DoImport()
        {
            if (!ShouldImport()) return;
            AddVariantType();
        }

        public void AddVariantType()
        {
            if (string.IsNullOrEmpty(_currentRow[LoadLikeData.ColVariantType]))
                throw new InvalidOperationException("Variant type required in col " + LoadLikeData.ColVariantType);

            CurrentVariant.VariantType = _variantTypeMap[_currentRow[LoadLikeData.ColVariantType]];
            _context.SaveChanges();
        }

        private bool ShouldImport()
        {
            if (string.IsNullOrEmpty(_currentRow[LoadLikeData.ColAnnotator])) return false;
            if (string.IsNullOrEmpty(_currentRow[LoadLikeData.ColVariantType])) return false;
            if (string.IsNullOrEmpty(_currentRow[LoadLikeData.ColZygosity])) return false;
            if (string.IsNullOrEmpty(_currentRow[LoadLikeData.ColCall])) return false;
            return true;
        }

        private void SetupTypeMap()
        {
            var variantTypeDbSet = _context.VariantType;
            var wholeDeletion = (
                from childVt in variantTypeDbSet
                join parentVt in variantTypeDbSet on childVt.ParentId equals parentVt.Id
                where childVt.Name == VariantTypeConstants.VariantTypes[1].Children[0].Name
                      && parentVt.Name == VariantTypeConstants.VariantTypes[1].Name
                select childVt
            ).Single();

            var wholeDuplicate = (
                from childVt in variantTypeDbSet
                join parentVt in variantTypeDbSet on childVt.ParentId equals parentVt.Id
                where childVt.Name == VariantTypeConstants.VariantTypes[2].Children[0].Name
                      && parentVt.Name == VariantTypeConstants.VariantTypes[2].Name
                select childVt
            ).Single();

            var svn = (
                from childVt in variantTypeDbSet
                join parentVt in variantTypeDbSet on childVt.ParentId equals parentVt.Id
                where childVt.Name == VariantTypeConstants.VariantTypes[0].Children[0].Name
                      && parentVt.Name == VariantTypeConstants.VariantTypes[0].Name
                select childVt
            ).Single();


            _variantTypeMap = new Dictionary<string, VariantType>
            {
                {
                    "1",
                    wholeDeletion
                },
                {
                    "3",
                    wholeDuplicate
                },
                {
                    "5",
                    svn
                },
            };
        }
    }
}