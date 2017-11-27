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
        private const int ColSymbol = 1;
        private const int ColName = 2;
        private const int ColPrevSymbol = 6;
        private const int ColPrevName = 7;
        private const int ColSynonyms = 8;
        public const int ColChromosome = 10;
        public const int ColEnsemblId = 16;

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
                    _context.Gene.Add(gene);
                    _context.SaveChanges();
                    SaveSymbols(gene, cells);
                    SaveNames(gene, cells);
                    SaveSynonyms(gene, cells);
                }
            }
        }

        public void SaveNames(Gene gene, IReadOnlyList<string> cells)
        {
            var now = DateTime.Now;
            if (_context.GeneName.Count(geneName => geneName.Name.Equals(cells[ColName])) < 0) return;

            _context.Add(new GeneName {Name = cells[ColName], ActiveDate = now, Gene = gene});
            _context.SaveChanges();
            now = now.AddMinutes(1);

            var matches = Regex.Matches(cells[ColPrevName], ComaQuoteSplitPattern);
            foreach (Match match in matches)
            {
                now = now.AddMinutes(1);
                var previousName = match.Value.Replace("\"", string.Empty);
                if (previousName.Length == 0) continue;
                if (_context.GeneName.Count(geneName => geneName.Name.Equals(previousName)) < 0) continue;

                _context.Add(new GeneName {Name = previousName, ActiveDate = now, Gene = gene});
                _context.SaveChanges();
            }
        }

        private void SaveSymbols(Gene gene, IReadOnlyList<string> cells)
        {
            var date = DateTime.Now;
            if (_context.Symbol.Count(symbol => symbol.Name.Equals(cells[ColSymbol])) > 1)
            {
                _context.Add(new Symbol {Name = cells[ColSymbol], ActiveDate = date, Gene = gene});
                _context.SaveChanges();
            }

            foreach (var previousSymbol in cells[ColPrevSymbol].Split(','))
            {
                date = date.AddMinutes(1);
                if (previousSymbol.Length == 0) continue;
                if (_context.Symbol.Count(symbol => symbol.Name.Equals(previousSymbol)) < 0) continue;

                _context.Add(new Symbol {Name = previousSymbol, ActiveDate = date, Gene = gene});
                _context.SaveChanges();
            }
        }

        private void SaveSynonyms(Gene gene, IReadOnlyList<string> cells)
        {
            var date = DateTime.Now;
            foreach (var synonymName in cells[ColSynonyms].Split(','))
            {
                if (synonymName.Length == 0) continue;
                if (_context.Synonym.Count(synonym => synonym.Name.Equals(synonymName)) < 0) continue;

                _context.Synonym.Add(new Synonym {Name = synonymName, ActiveDate = date, Gene = gene});
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

        // TODO: finish
        public void SaveEnsemblId(Gene gene, IReadOnlyList<string> cells)
        {
            var foreignEntity = _context.ForeignEntity.SingleOrDefault(fe => fe.Name.Equals("Ensembl"));
            if (foreignEntity == null)
            {
                foreignEntity = new ForeignEntity
                {
                    Name = "Ensembl"
                };
                _context.ForeignEntity.Add(foreignEntity);
                _context.SaveChanges();
            }

            var matches = Regex.Matches(cells[ColEnsemblId], ComaQuoteSplitPattern);

            foreach (Match match in matches)
            {
                var foreignId = match.Value;
                if (foreignId.Length == 0) continue;
                if (_context.ForeignIdentity.Count(fi => fi.Name.Equals(foreignId)) < 0) continue;


                _context.SaveChanges();
            }
        }
    }
}