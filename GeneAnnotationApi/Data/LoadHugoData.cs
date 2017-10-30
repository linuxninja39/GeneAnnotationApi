using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Data
{
    public static class LoadHugoData
    {
        public static void LoadData(GeneAnnotationDBContext context, string fileName)
        {
            var path = Directory.GetCurrentDirectory();
            if (fileName == null)
            {
                fileName = @"hugo.txt";
            }

            fileName = path + Path.DirectorySeparatorChar + fileName;

            if (!File.Exists(fileName))
            {
                return;
            }

            var fileStream = new FileStream(fileName, FileMode.Open);

            doImport(context, fileStream);
        }

        private static void doImport(GeneAnnotationDBContext context, Stream fileStream)
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
                    populateGene(gene, cells);
                    context.Gene.Add(gene);
                    context.SaveChanges();
                    SaveSymbols(context, gene, cells);
                    SaveNames(context, gene, cells);
                }
            }
        }

        private static void populateGene(Gene gene, IReadOnlyList<string> cells)
        {
        }

        private static void SaveSymbols(GeneAnnotationDBContext context, Gene gene, string[] cells)
        {
            var now = DateTime.Now;
            if (context.Symbol.Count(symbol => symbol.Name.Equals(cells[1])) < 0) return;
            context.Add(new Symbol {Name = cells[1], ActiveDate = now, Gene = gene});
            context.SaveChanges();
            var date = now.AddMinutes(1);
            foreach (var previousSymbol in cells[6].Split(','))
            {
                if (previousSymbol.Length == 0) continue;
                if (context.Symbol.Count(symbol => symbol.Name.Equals(previousSymbol)) < 0) continue;
                
                context.Add(new Symbol {Name = previousSymbol, ActiveDate = date, Gene = gene});
                context.SaveChanges();
                date = date.AddMinutes(1);
            }
        }

        private static void SaveNames(GeneAnnotationDBContext context, Gene gene, IReadOnlyList<string> cells)
        {
            var now = DateTime.Now;
            if (context.GeneName.Count(geneName => geneName.Name.Equals(cells[2])) < 0) return;

            context.Add(new GeneName {Name = cells[2], ActiveDate = now, Gene = gene});
            context.SaveChanges();
            var date = now.AddMinutes(1);
           
            const string pattern = "\"[^\"]*\"|\\w[^\",]*";
            var matches = Regex.Matches(cells[7], pattern);
            foreach (Match match in matches)
            {
                var previousName = match.Value.Replace("\"", string.Empty);
                if (previousName.Length == 0) continue;
                if (context.GeneName.Count(geneName => geneName.Name.Equals(previousName)) < 0) continue;
                
                context.Add(new GeneName {Name = previousName, ActiveDate = date, Gene = gene});
                context.SaveChanges();
                date = date.AddMinutes(1);
            }
        }
    }
}