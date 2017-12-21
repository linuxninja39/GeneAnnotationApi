using System;
using System.Collections.Generic;
using System.Linq;
using GeneAnnotationApi.Data.Constants;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Data
{
    public class LikeVariantLoader
    {
        private readonly GeneAnnotationDBContext _context;
        private readonly IList<string> _currentRow;
        public GeneVariant CurrentVariant;
        private IDictionary<string, VariantType> _variantTypeMap;

        public LikeVariantLoader(GeneAnnotationDBContext context, IList<string> currentRow)
        {
            _context = context;
            _currentRow = currentRow;

            SetupTypeMap();
        }

        private void FindOrCreateVariant()
        {
            if (
                !int.TryParse(_currentRow[LikeDataLoader.ColStart], out var start)
                || !int.TryParse(_currentRow[LikeDataLoader.ColEnd], out var end)
            ) throw new InvalidOperationException("start and end required");
            if (_currentRow[LikeDataLoader.ColVariantType] == null)
                throw new InvalidOperationException("start and end required");

            CurrentVariant = _context.GeneVariant
                .SingleOrDefault(
                    gv => gv.Start == start
                          && gv.End == end
                          && gv.VariantTypeId == _variantTypeMap[_currentRow[LikeDataLoader.ColVariantType]].Id
                );

            if (CurrentVariant != null) return;

            CurrentVariant = new GeneVariant
            {
                Start = start,
                End = end,
                VariantTypeId = _variantTypeMap[_currentRow[LikeDataLoader.ColVariantType]].Id
            };
            _context.GeneVariant.Add(CurrentVariant);
        }

        public void DoImport()
        {
            if (!ShouldImport()) return;
            FindOrCreateVariant();
            AddZygosity();
            _context.SaveChanges();
            AddCall();
            _context.SaveChanges();
        }

        public void AddZygosity()
        {
            ValidateData(LikeDataLoader.ColZygosity, "Zygosity");

            var zygosityName = (_currentRow[LikeDataLoader.ColZygosity].ToUpper() == "HET")
                ? "Heterozygous"
                : "Homozygous";
            var zyType = _context.ZygosityType
                .Single(z => z.Name == zygosityName);

            CurrentVariant.ZygosityTypeId = zyType.Id;
        }

        public void AddCall()
        {
            ValidateData(LikeDataLoader.ColCall, "Call");

            var callType = _context.CallType.SingleOrDefault(ct => ct.Name == _currentRow[LikeDataLoader.ColCall]);
            if (callType == null)
            {
                callType = new CallType
                {
                    Name = _currentRow[LikeDataLoader.ColCall]
                };
                _context.CallType.Add(callType);
            }

            var date = DateTime.Now;
            if (!string.IsNullOrEmpty(_currentRow[LikeDataLoader.ColDateUpdated]))
            {
                date = DateTime.Parse(_currentRow[LikeDataLoader.ColDateUpdated]);
            }

            if (
                _context.CallTypeGeneVariant.Any(
                    ctgv => ctgv.GeneVariantId == CurrentVariant.Id && ctgv.CallTypeId == callType.Id
                )
            ) return;

            var callTypeGeneVariant = new CallTypeGeneVariant
            {
                CallType = callType,
                GeneVariant = CurrentVariant,
                ActiveDate = date
            };

            _context.CallTypeGeneVariant.Add(callTypeGeneVariant);
        }

        private void ValidateData(int col, string name)
        {
            if (string.IsNullOrEmpty(_currentRow[col]))
                throw new InvalidOperationException(name + " type required in col " + col);
        }

        private bool ShouldImport()
        {
            if (string.IsNullOrEmpty(_currentRow[LikeDataLoader.ColStart])) return false;
            if (string.IsNullOrEmpty(_currentRow[LikeDataLoader.ColEnd])) return false;
            if (string.IsNullOrEmpty(_currentRow[LikeDataLoader.ColVariantType])) return false;
            if (string.IsNullOrEmpty(_currentRow[LikeDataLoader.ColZygosity])) return false;
            if (string.IsNullOrEmpty(_currentRow[LikeDataLoader.ColCall])) return false;
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