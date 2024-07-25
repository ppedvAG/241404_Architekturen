using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
            using (var con = new EfContext(conString))
            {
                con.Database.EnsureCreated();

                con.Add(testPizza);
                var rows = con.SaveChanges();

                //Assert.True(rows > 0);
                rows.Should().BeGreaterThan(10);
            }

            using (var con = new EfContext(conString))
            {
                //eager Loading
                //var query = con.Pizzas.Where(x => x.Id == testPizza.Id);
                //query = query.Include(x => x.Toppings);
                //query = query.Include(x => x.Items).ThenInclude(x => x.Order).ThenInclude(x => x.BillingAddress);
                //query = query.Include(x => x.Items).ThenInclude(x => x.Order).ThenInclude(x => x.DeliveryAddress);
                //var loaded = query.FirstOrDefault();


                ////explizit loading
                //var loaded = con.Pizzas.Find(testPizza.Id);
                //con.Entry(loaded).Collection(x => x.Toppings);
                //con.Entry(loaded).Collection(x => x.Items);

                //lazy loading
                var loaded = con.Pizzas.Find(testPizza.Id);

                loaded.Should().NotBeNull();
                loaded.Should().BeEquivalentTo(testPizza, x => x.IgnoringCyclicReferences());
            }

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