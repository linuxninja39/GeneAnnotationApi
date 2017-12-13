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
            _context.GeneVariant.Add(CurrentVariant);

            SetupTypeMap();
        }

        public void DoImport()
        {
            if (!ShouldImport()) return;
            AddStartStop();
            AddVariantType();
            AddZygosity();
            AddCall();
            _context.SaveChanges();
        }

        public void AddVariantType()
        {
            ValidateData(LoadLikeData.ColVariantType, "Variant Type");

            CurrentVariant.VariantType = _variantTypeMap[_currentRow[LoadLikeData.ColVariantType]];
        }

        public void AddZygosity()
        {
            ValidateData(LoadLikeData.ColZygosity, "Zygosity");

            var zygosityName = (_currentRow[LoadLikeData.ColZygosity].ToUpper() == "HET")
                ? "Heterozygous"
                : "Homozygous";
            var zyType = _context.ZygosityType
                .Single(z => z.Name == zygosityName);

            CurrentVariant.ZygosityType = zyType;
        }

        public void AddCall()
        {
            ValidateData(LoadLikeData.ColCall, "Call");

            var callType = _context.CallType.SingleOrDefault(ct => ct.Name == _currentRow[LoadLikeData.ColCall]);
            if (callType == null)
            {
                callType = new CallType
                {
                    Name = _currentRow[LoadLikeData.ColCall]
                };
                _context.CallType.Add(callType);
            }

            var date = DateTime.Now;
            if (!string.IsNullOrEmpty(_currentRow[LoadLikeData.ColDateUpdated]))
            {
                date = DateTime.Parse(_currentRow[LoadLikeData.ColDateUpdated]);
            }
            
            var callTypeGeneVariant = new CallTypeGeneVariant
            {
                CallType = callType,
                GeneVariant = CurrentVariant,
                ActiveDate = date
            };

            _context.CallTypeGeneVariant.Add(callTypeGeneVariant);
        }

        public void AddStartStop()
        {

            int start;
            int end;
            if (
                !int.TryParse(_currentRow[LoadLikeData.ColStart], out start)
                || !int.TryParse(_currentRow[LoadLikeData.ColEnd], out end)
            ) throw new InvalidOperationException("start and end required");

            CurrentVariant.Start = start;
            CurrentVariant.End = end;
        }

        private void ValidateData(int col, string name)
        {
            if (string.IsNullOrEmpty(_currentRow[col]))
                throw new InvalidOperationException(name + " type required in col " + col);
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