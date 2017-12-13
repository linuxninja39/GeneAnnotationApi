using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeneAnnotationApi.Entities;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;
using Xunit.Sdk;

namespace GeneAnnotationApi.Data
{
    public class LikeDataLoader
    {
        private readonly GeneAnnotationDBContext _context;
        private readonly string _fileName;
        private FileStream _fileStream;
        public Gene CurrentGene = null;
        public IList<string> CurrentRow;

        public static int ColSymbol = 0;
        public static int ColChromosome = 1;
        public static int ColStart = 2;
        public static int ColEnd = 3;
        public static int ColLocus = 4;
        public static int ColAnnotator = 5;
        public static int ColDateUpdated = 6;
        public static int ColOrigin = 7;
        public static int ColGeneNameExpansion = 8;
        public static int ColKnownFunction = 9;
        public static int ColVariantType = 10;
        public static int ColZygosity = 11;
        public static int ColCall = 15;
        public static int ColLit1 = 16;
        public static int ColLit2 = 17;
        public static int ColLit3 = 18;
        public static int ColLit4 = 19;
        public static int ColLit5 = 20;
        public static int ColLit6 = 21;
        public static int ColLit7 = 22;
        public static int ColAnnotation = 23;

        /*
         * this file will load data from ucsc hgTables from the following columns in this order
         * #hg19.refGene.name hg19.refGene.chrom hg19.refGene.txStart hg19.refGene.txEnd hgFixed.refLink.name
         */
        public LikeDataLoader(GeneAnnotationDBContext context, string fileName)
        {
            _context = context;
            _fileName = fileName ?? "like.csv";
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
                    CurrentRow[ColSymbol] = CurrentRow[ColSymbol].Trim();
                    if (string.Equals(CurrentRow[ColSymbol], "")) continue;
                    FindOrCreateGene();
                    if (CurrentGene == null) continue;
                    AddSymbol();
                    AddLocation();
                    AddKnownFunction();
                }
            }
        }

        public void FindOrCreateGene()
        {
            var symbolName = CurrentRow[ColSymbol];

            var geneBySymbol = FindBySymbol(symbolName);
            Gene geneByCoord = null;
            if (int.TryParse(CurrentRow[ColEnd], out var end) && int.TryParse(CurrentRow[ColStart], out var start))
            {
                geneByCoord = FindByCoord(start, end);
            }

            if (geneByCoord != null && geneBySymbol != null)
            {
                if (geneByCoord.Id != geneBySymbol.Id) return;
                CurrentGene = geneBySymbol;
                return;
            }

            if (geneByCoord == null && geneBySymbol != null)
            {
                CurrentGene = geneBySymbol;
                return;
            }

            if (geneByCoord != null && geneBySymbol == null)
            {
                CurrentGene = geneByCoord;
                return;
            }

            CurrentGene = new Gene();
            _context.Gene.Add(CurrentGene);
            _context.SaveChanges();
        }

        private Gene FindByCoord(int start, int end)
        {
            var chromosomeName = CurrentRow[ColChromosome].Substring(3);
            if (chromosomeName == null) throw new EmptyException("chromosome name required");
            var coord = _context
                .GeneCoordinate
                .Include(gc => gc.GeneLocation)
                .ThenInclude(gl => gl.Gene)
                .Include(gc => gc.GeneLocation)
                .ThenInclude(gl => gl.Chromosome)
                .SingleOrDefault(c => c.Start == start && c.End == end && c.GeneLocation.Chromosome.Name == chromosomeName);
            return coord?.GeneLocation?.Gene;
        }

        private Gene FindBySymbol(string symbolName)
        {
            var symbol = _context
                .Symbol
                .Include(s => s.Gene)
                .SingleOrDefault(s => s.Name == symbolName)
                ;

            return symbol?.Gene;
        }

        public void AddSymbol()
        {
            if (CurrentGene.Symbol.Count(s => s.Name == CurrentRow[ColSymbol]) > 0) return;
            var symbol = new Symbol { Name = CurrentRow[ColSymbol], Gene = CurrentGene, ActiveDate = DateTime.Now};
            CurrentGene.Symbol.Add(symbol);
            _context.Symbol.Add(symbol);
            _context.SaveChanges();
        }

        public void AddLocation()
        {
            if (!int.TryParse(CurrentRow[ColStart], out var start) ||
                !int.TryParse(CurrentRow[ColEnd], out var end)) return;

            var chromosomeName = CurrentRow[ColChromosome].Substring(3);
            if (chromosomeName == null) return;

            var coord = _context.GeneCoordinate.SingleOrDefault(c => c.Start == start && c.End == end);
            if (coord != null) return;

            var geneLocation = _context
                .GeneLocation
                .SingleOrDefault(gl => gl.HgVersion == 19 && gl.Gene == CurrentGene);
            if (CurrentRow[ColLocus].ToUpper() == "#N/A") CurrentRow[ColLocus] = "";
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
                    Chromosome = chromosome,
                    Locus = CurrentRow[ColLocus]
                };
                _context.GeneLocation.Add(geneLocation);
                _context.SaveChanges();
            }
            else
            {
                if (geneLocation.Locus == null)
                {
                    geneLocation.Locus = CurrentRow[ColLocus];
                    _context.SaveChanges();
                }
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

        public void AddKnownFunction()
        {
            if (!string.IsNullOrEmpty(CurrentGene.KnownFunction)) return;
            CurrentGene.KnownFunction = CurrentRow[ColKnownFunction];
            _context.SaveChanges();
        }
    }
}