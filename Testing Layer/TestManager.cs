using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer;
using Data_Layer;
using Microsoft.EntityFrameworkCore;

namespace Testing_Layer
{
    [TestFixture]
    public class TestManager
    {

        internal static AppDbContext dbContext;

        static TestManager()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("DbForTest");
            dbContext = new AppDbContext(builder.Options);
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
