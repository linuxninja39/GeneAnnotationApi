using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeneAnnotationApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneAnnotationApi.Data
{
    public class LoadUcscData
    {
        private readonly GeneAnnotationDBContext _context;
        private readonly string _fileName;
        private FileStream _fileStream;
        public Gene CurrentGene;
        public IList<string> CurrentRow;

        public static int ColChromosome = 1;
        public static int ColStart = 2;
        public static int ColEnd = 3;
        public static int ColSymbol = 4;

        /*
         * this file will load data from ucsc hgTables from the following columns in this order
         * #hg19.refGene.name hg19.refGene.chrom hg19.refGene.txStart hg19.refGene.txEnd hgFixed.refLink.name
         */
        public LoadUcscData(GeneAnnotationDBContext context, string fileName)
        {
            _context = context;
            _fileName = fileName ?? "ucsc.txt";
        }

        public void LoadData()
        {
            LoadFile();
            ParseFile();
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

        public void ParseFile()
        {
            using (var reader = new StreamReader(_fileStream))
            {
                string line;
                var firstLineSkipped = false;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!firstLineSkipped)
                    {
                        firstLineSkipped = true;
                        continue;
                    }

                    CurrentRow = line.Split("\t".ToCharArray());
                    FindOrCreateGene();
                    AddLocation();
                }
            }
        }

        public void FindOrCreateGene()
        {
            var row = CurrentRow;
            var symbol = _context.Symbol.SingleOrDefault(s => s.Name == row[ColSymbol]);
            if (symbol == null)
            {
                if (int.TryParse(row[ColStart], out var start) && int.TryParse(row[ColEnd], out var end))
                {
                    var coord = _context
                        .GeneCoordinate
                        .Include(gc => gc.GeneLocation)
                        .ThenInclude(gl => gl.Gene)
                        .SingleOrDefault(c => c.Start == start && c.End == end);
                    CurrentGene = coord == null ? new Gene() : coord.GeneLocation.Gene;
                }
                else
                {
                    CurrentGene = new Gene();
                }
            }
            else
            {
                CurrentGene = symbol.Gene;
            }

            if (CurrentGene.Id != 0) return;
            _context.Gene.Add(CurrentGene);
            _context.SaveChanges();
        }

        public void AddLocation()
        {
            if (!int.TryParse(CurrentRow[ColStart], out var start) ||
                !int.TryParse(CurrentRow[ColEnd], out var end)) return;

            var chromosomeName = CurrentRow[ColChromosome].Substring(2);
            if (chromosomeName == null) return;

            var coord = _context.GeneCoordinate.SingleOrDefault(c => c.Start == start && c.End == end);
            if (coord != null) return;

            var geneLocation =
                _context.GeneLocation.SingleOrDefault(gl => gl.HgVersion == 19 && gl.Gene == CurrentGene);
            if (geneLocation == null)
            {
                var chromosome = _context.Chromosome.SingleOrDefault(c => c.Name == chromosomeName);
                if (chromosome == null)
                {
                    chromosome = new Chromosome
                    {
                        Name = chromosomeName
                    };
                    _context.Chromosome.Add(chromosome);
                    _context.SaveChanges();
                }

                geneLocation = new GeneLocation
                {
                    Gene = CurrentGene,
                    HgVersion = 19,
                    Chromosome = chromosome
                };
                _context.GeneLocation.Add(geneLocation);
                _context.SaveChanges();
            }

            coord = new GeneCoordinate
            {
                GeneLocation = geneLocation,
                Start = start,
                End = end
            };

            _context.GeneCoordinate.Add(coord);
            _context.SaveChanges();
        }
    }
}