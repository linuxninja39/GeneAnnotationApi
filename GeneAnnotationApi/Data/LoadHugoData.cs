using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Data
{
    public class LoadHugoData
    {
        private const int ColName = 2;
        private const int ColPrevName = 7;
        public const int ColChromosome = 10;
        private const string ComaQuoteSplitPattern = "\"[^\"]*\"|\\w[^\",]*";

        public static readonly Regex LocusRegex = new Regex("^([0-9xy]{1,2})[pq]{0,1}.*", RegexOptions.IgnoreCase);

        private readonly GeneAnnotationDBContext _context;
        private string _fileName;

        public LoadHugoData(GeneAnnotationDBContext context, string fileName)
        {
            _context = context;
            _fileName = fileName ?? "hugo.txt";
        }

        public void LoadData()
        {
            var path = Directory.GetCurrentDirectory();
            if (_fileName == null)
            {
                _fileName = @"hugo.txt";
            }

            var fullPath = path + Path.DirectorySeparatorChar + _fileName;

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("File not found: " + fullPath);
            }

            var fileStream = new FileStream(fullPath, FileMode.Open);

            doImport(fileStream);
        }

        private void doImport(Stream fileStream)
        {
            using (var reader = new StreamReader(fileStream))
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

                    var cells = line.Split("\t".ToCharArray());
                    var gene = new Gene();
                    PopulateGene(gene, cells);
                    _context.Gene.Add(gene);
                    _context.SaveChanges();
                    SaveSymbols(gene, cells);
                    SaveNames(gene, cells);
                }
            }
        }

        private void PopulateGene(Gene gene, IReadOnlyList<string> cells)
        {
        }

        public void addToGeneNames(Gene gene, IReadOnlyList<string> cells)
        {
            var dateTime = DateTime.Now;
            var geneName = new GeneName
            {
                Gene = gene,
                ActiveDate = dateTime,
                Name = cells[ColName]
            };
            _context.GeneName.Add(geneName);
            _context.SaveChanges();
            var matches = Regex.Matches(cells[ColPrevName], ComaQuoteSplitPattern);
            foreach (Match match in matches)
            {
                dateTime = dateTime.AddMinutes(1);
                if (match.Length == 0) continue;
                var previousName = match.Value.Replace("\"", string.Empty);
                if (_context.GeneName.Count(geneNameEntity => geneName.Name.Equals(previousName)) < 0) continue;

                _context.Add(new GeneName {Name = previousName, ActiveDate = dateTime, Gene = gene});
                _context.SaveChanges();
            }
        }

        private void SaveSymbols(Gene gene, string[] cells)
        {
            var now = DateTime.Now;
            if (_context.Symbol.Count(symbol => symbol.Name.Equals(cells[1])) < 0) return;
            _context.Add(new Symbol {Name = cells[1], ActiveDate = now, Gene = gene});
            _context.SaveChanges();
            var date = now.AddMinutes(1);
            foreach (var previousSymbol in cells[6].Split(','))
            {
                if (previousSymbol.Length == 0) continue;
                if (_context.Symbol.Count(symbol => symbol.Name.Equals(previousSymbol)) < 0) continue;

                _context.Add(new Symbol {Name = previousSymbol, ActiveDate = date, Gene = gene});
                _context.SaveChanges();
                date = date.AddMinutes(1);
            }
        }

        private void SaveNames(Gene gene, IReadOnlyList<string> cells)
        {
            var now = DateTime.Now;
            if (_context.GeneName.Count(geneName => geneName.Name.Equals(cells[2])) < 0) return;

            _context.Add(new GeneName {Name = cells[2], ActiveDate = now, Gene = gene});
            _context.SaveChanges();
            var date = now.AddMinutes(1);

            var matches = Regex.Matches(cells[7], ComaQuoteSplitPattern);
            foreach (Match match in matches)
            {
                var previousName = match.Value.Replace("\"", string.Empty);
                if (previousName.Length == 0) continue;
                if (_context.GeneName.Count(geneName => geneName.Name.Equals(previousName)) < 0) continue;

                _context.Add(new GeneName {Name = previousName, ActiveDate = date, Gene = gene});
                _context.SaveChanges();
                date = date.AddMinutes(1);
            }
        }

        public void SaveLocation(Gene gene, IReadOnlyList<string> cells)
        {
            var locus = cells[ColChromosome];
            var match = LocusRegex.Match(locus);
            var chromosomeName = match.Groups[1].Value;
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

            var location = new GeneLocation
                {
                    Gene = gene,
                    Locus = locus,
                    Chromosome = chromosome,
                    HgVersion = 19
                }
                ;
            _context.GeneLocation.Add(location);
            _context.SaveChanges();
        }
    }
}