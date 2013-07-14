using System;
using System.Linq;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest.Common
{
    public class DbBaseTest
    {
        protected Entities2 DbCtx = new Entities2();

        [TestCleanup]
        public void BaseTearDown()
        {
            DbCtx.Dispose();
        }

        protected int GetMaxLengthOfNumericColumnFromDatabase(string columnName, string tableName)
        {
            var sql = String.Format("SELECT NUMERIC_PRECISION FROM INFORMATION_SCHEMA.COLUMNS " +
                                       "WHERE (TABLE_SCHEMA = 'dbo') AND (TABLE_NAME = '{0}') AND (COLUMN_NAME = '{1}')",
                                       tableName, columnName);
            return DbCtx.Database.SqlQuery<Byte>(sql).First();
        }

        protected int GetMaxLengthOfAlphanumericColumnFromDatabase(string columnName, string tableName)
        {
            var sql = String.Format("SELECT CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS " +
                                       "WHERE (TABLE_SCHEMA = 'dbo') AND (TABLE_NAME = '{0}') AND (COLUMN_NAME = '{1}')",
                                       tableName, columnName);
            return DbCtx.Database.SqlQuery<int>(sql).First();
        }
    }
}