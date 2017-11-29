using System.IO;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Data
{
    public class LoadUcscData
    {
        private readonly GeneAnnotationDBContext _context;
        private string _fileName;
        private FileStream _fileStream;
        private Gene _gene;

        public const int ColChromosome = 1;
        public const int ColStart = 2;
        public const int ColEnd = 3;
        public const int ColSymbol = 3;

        /*
         * this file will load data from ucsc hgTables from the following columns in this order
         * #hg19.refGene.name hg19.refGene.chrom hg19.refGene.txStart hg19.refGene.txEnd hgFixed.refLink.name
         */
        public LoadUcscData(GeneAnnotationDBContext context, string fileName)
        {
            _context = context;
            _fileName = fileName ?? "ucsc.txt";
        }

        public void LoadFile() 
        {
            var path = Directory.GetCurrentDirectory();

            var fullPath = path + Path.DirectorySeparatorChar + _fileName;

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("File not found: " + fullPath);
            }

            _fileStream = new FileStream(fullPath, FileMode.Open);
        }

        public Gene findOrCreateGene()
        {
            return null;
        }
    }
}