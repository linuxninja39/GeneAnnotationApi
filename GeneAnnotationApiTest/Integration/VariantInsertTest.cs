using System.Collections.Generic;
using System.Linq;
using GeneAnnotationApi.Data.Constants;
using GeneAnnotationApi.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class VariantInsertTest
    {
        private readonly GeneAnnotationDBContext _context;
        private IDictionary<string, VariantType> _variantTypeMap;

        public VariantInsertTest()
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            var contextOptionsBuilder = new DbContextOptionsBuilder<GeneAnnotationDBContext>();
            contextOptionsBuilder.UseLoggerFactory(loggerFactory);

            var connectionString =
                @"Server=localhost;Database=GeneAnnotationDB;User=sa;Password=C0rv3tt3!";
            contextOptionsBuilder.UseSqlServer(connectionString);

            _context = new GeneAnnotationDBContext(contextOptionsBuilder.Options);

            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            
            SetupTypeMap();
        }

        [Fact]
        public void NewVariantTest()
        {
            var variant = new GeneVariant
            {
                Start = 1,
                End = 30,
                VariantTypeId = _variantTypeMap["1"].Id,
                ZygosityTypeId = 1
            };

            var ex = Record.Exception(
                () =>
                {
                    _context.GeneVariant.Add(variant);
                    _context.SaveChanges();
                }
            );
            Assert.Null(ex);
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