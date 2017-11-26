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
        private const int COL_NAME = 2;
        private const int ColPrevName = 7;
        private const string ComaQuoteSplitPattern = "\"[^\"]*\"|\\w[^\",]*";

        private readonly GeneAnnotationDBContext Context;
        private string _fileName = "hugo.txt";

        public LoadHugoData(GeneAnnotationDBContext context, string fileName)
        {
            Context = context;
            _fileName = fileName;
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
                    Context.Gene.Add(gene);
                    Context.SaveChanges();
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
                Name = cells[COL_NAME]
            };
            Context.GeneName.Add(geneName);
            Context.SaveChanges();
            var matches = Regex.Matches(cells[ColPrevName], ComaQuoteSplitPattern);
            foreach (Match match in matches)
            {
                dateTime = dateTime.AddMinutes(1);
                if (match.Length == 0) continue;
                var previousName = match.Value.Replace("\"", string.Empty);
                if (Context.GeneName.Count(geneNameEntity => geneName.Name.Equals(previousName)) < 0) continue;

                Context.Add(new GeneName {Name = previousName, ActiveDate = dateTime, Gene = gene});
                Context.SaveChanges();
            }
        }

        private void SaveSymbols(Gene gene, string[] cells)
        {
            var now = DateTime.Now;
            if (Context.Symbol.Count(symbol => symbol.Name.Equals(cells[1])) < 0) return;
            Context.Add(new Symbol {Name = cells[1], ActiveDate = now, Gene = gene});
            Context.SaveChanges();
            var date = now.AddMinutes(1);
            foreach (var previousSymbol in cells[6].Split(','))
            {
                if (previousSymbol.Length == 0) continue;
                if (Context.Symbol.Count(symbol => symbol.Name.Equals(previousSymbol)) < 0) continue;

                Context.Add(new Symbol {Name = previousSymbol, ActiveDate = date, Gene = gene});
                Context.SaveChanges();
                date = date.AddMinutes(1);
            }
        }

        private void SaveNames(Gene gene, IReadOnlyList<string> cells)
        {
            var now = DateTime.Now;
            if (Context.GeneName.Count(geneName => geneName.Name.Equals(cells[2])) < 0) return;

            Context.Add(new GeneName {Name = cells[2], ActiveDate = now, Gene = gene});
            Context.SaveChanges();
            var date = now.AddMinutes(1);

            var matches = Regex.Matches(cells[7], ComaQuoteSplitPattern);
            foreach (Match match in matches)
            {
                var previousName = match.Value.Replace("\"", string.Empty);
                if (previousName.Length == 0) continue;
                if (Context.GeneName.Count(geneName => geneName.Name.Equals(previousName)) < 0) continue;

                Context.Add(new GeneName {Name = previousName, ActiveDate = date, Gene = gene});
                Context.SaveChanges();
                date = date.AddMinutes(1);
            }
        }
    }
}