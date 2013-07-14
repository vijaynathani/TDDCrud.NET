using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest.CustomerTableCheck
{
    [TestClass]
    public class NotNullTest : CustomerBase
    {
        [TestMethod]
        [ExpectedException(typeof (SqlException))]
        public void EnsureNumberNotNull()
        {
            DbCtx.Database.ExecuteSqlCommand(
                "update Customer set number=null where id = {0}", GetLastCustomerId());
        }

        [TestMethod]
        [ExpectedException(typeof (SqlException))]
        public void EnsureNameNotNull()
        {
            DbCtx.Database.ExecuteSqlCommand(
                "update Customer set number=null where id = {0}", GetLastCustomerId());
        }

        [TestMethod]
        [ExpectedException(typeof (SqlException))]
        public void EnsureAddressNotNull()
        {
            DbCtx.Database.ExecuteSqlCommand(
                "update Customer set address=null where id = {0}", GetLastCustomerId());
        }

        [TestMethod]
        [ExpectedException(typeof (SqlException))]
        public void EnsureMobileNotNull()
        {
            DbCtx.Database.ExecuteSqlCommand(
                "update Customer set mobile=null where id = {0}", GetLastCustomerId());
        }
    }
}