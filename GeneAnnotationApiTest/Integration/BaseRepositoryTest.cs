using System;
using System.ComponentModel;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.Repositories;
using GeneAnnotationApi.Repositories.EntityFramework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GeneAnnotationApiTest.Integration
{
    public class BaseRepositoryTest<T, U>: BaseDbTest where T : BaseEfRepository<U> where U : class
    {
        protected readonly T Repository;

        public BaseRepositoryTest(bool initDb): base(initDb)
        {
            Repository = (T)Activator.CreateInstance(typeof(T), Context);
            
        }
    }
}