using AutoFixture;
using AutoFixture.Kernel;
using ppedv.PizzaOrderManager.Model;
using System.Data;
using System.Reflection;

namespace ppedv.PizzaOrderManager.Data.EfCore.Tests
{
    public class EfContextTests
    {
        string conString = "Server=(localdb)\\mssqllocaldb;Database=PizzaOrderManager_test;Trusted_Connection=true;";

        [Fact]
        public void Can_create_db()
        {
            var con = new EfContext(conString);
            con.Database.EnsureDeleted();

            var res = con.Database.EnsureCreated();

            Assert.True(res);
        }

        [Fact]
        public void Can_create_Pizza()
        {
            var testPizza = new Pizza() { Name = "TestPizza" };
            var con = new EfContext(conString);
            con.Database.EnsureCreated();

            con.Add(testPizza);
            var rows = con.SaveChanges();

            Assert.Equal(1, rows);
        }

        [Fact]
        public void Can_read_Pizza()
        {
            var testPizza = new Pizza() { Name = $"TestPizza_{Guid.NewGuid()}" };

            using (var con = new EfContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(testPizza);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                //var loaded = con.Pizzas.FirstOrDefault(x => x.Id == testPizza.Id);
                var loaded = con.Pizzas.Find(testPizza.Id);

                Assert.NotNull(loaded);
                Assert.Equal(testPizza.Name, loaded.Name);
            }
        }

        [Fact]
        public void Can_update_Pizza()
        {
            var testPizza = new Pizza() { Name = $"TestPizza_{Guid.NewGuid()}" };
            var newName = $"UpdatesTestPizza_{Guid.NewGuid()}";

            using (var con = new EfContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(testPizza);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Pizzas.Find(testPizza.Id);

                Assert.NotNull(loaded);
                loaded.Name = newName;
                var rows = con.SaveChanges();
                Assert.Equal(1, rows);
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Pizzas.Find(testPizza.Id);

                Assert.NotNull(loaded);
                Assert.Equal(newName, loaded.Name);
            }
        }

        [Fact]
        public void Can_delete_Pizza()
        {
            var testPizza = new Pizza() { Name = $"TestPizza_{Guid.NewGuid()}" };
            using (var con = new EfContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(testPizza);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Pizzas.Find(testPizza.Id);
                Assert.NotNull(loaded);
                con.Remove(loaded);
                var rows = con.SaveChanges();
                Assert.Equal(1, rows);
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Pizzas.Find(testPizza.Id);
                Assert.Null(loaded);
            }
        }


        [Fact]
        public void Can_create_Pizza_with_AutoFixture()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id)));
            var testPizza = fix.Create<Pizza>();
            var con = new EfContext(conString);
            con.Database.EnsureCreated();

            con.Add(testPizza);
            var rows = con.SaveChanges();

            Assert.True(rows > 0);
        }
    }

    internal class PropertyNameOmitter : ISpecimenBuilder
    {
        private readonly IEnumerable<string> names;

        internal PropertyNameOmitter(params string[] names)
        {
            this.names = names;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;
            if (propInfo != null && names.Contains(propInfo.Name))
                return new OmitSpecimen();

            return new NoSpecimen();
        }
    }
}