﻿using System;
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
        public readonly GeneVariant CurrentVariant;
        private IDictionary<string, VariantType> _variantTypeMap;

        public LikeVariantLoader(GeneAnnotationDBContext context, IList<string> currentRow)
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
            ValidateData(LikeDataLoader.ColVariantType, "Variant Type");

            CurrentVariant.VariantType = _variantTypeMap[_currentRow[LikeDataLoader.ColVariantType]];
        }

        public void AddZygosity()
        {
            ValidateData(LikeDataLoader.ColZygosity, "Zygosity");

            var zygosityName = (_currentRow[LikeDataLoader.ColZygosity].ToUpper() == "HET")
                ? "Heterozygous"
                : "Homozygous";
            var zyType = _context.ZygosityType
                .Single(z => z.Name == zygosityName);

            CurrentVariant.ZygosityType = zyType;
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
                !int.TryParse(_currentRow[LikeDataLoader.ColStart], out start)
                || !int.TryParse(_currentRow[LikeDataLoader.ColEnd], out end)
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
            if (string.IsNullOrEmpty(_currentRow[LikeDataLoader.ColAnnotator])) return false;
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