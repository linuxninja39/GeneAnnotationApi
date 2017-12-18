using System.Collections.Generic;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Data
{
    public class LikeVariantLiteratureDataLoader
    {
        private GeneAnnotationDBContext _context;
        private IReadOnlyList<string> _currentRow;
        public GeneVariant CurrentVariant { get; private set; }

        public LikeVariantLiteratureDataLoader(
            GeneAnnotationDBContext context,
            IReadOnlyList<string> currentRow,
            GeneVariant currentVariant
        )
        {
            _context = context;
            _currentRow = currentRow;
            CurrentVariant = currentVariant;
        }

        public void DoImport()
        {
            
        }
    }
}