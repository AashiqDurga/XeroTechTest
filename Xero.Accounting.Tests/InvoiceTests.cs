using XeroTechnicalTest;
using Xunit;

namespace Xero.Accounting.Tests
{
    public class InvoiceTests
    {
        [Fact]
        public void GivenAnInvoiceWhenGeneratingThenAddAnInvoiceLine()
        {
            var invoice = new Invoice();

            var invoiceLine = new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 6.99,
                Quantity = 1,
                Description = "Apple"
            };
            invoice.AddInvoiceLine(invoiceLine);

            Assert.Single(invoice.LineItems);
        }

        [Fact]
        public void GivenAnInvoiceWithItemsWhenAnItemIsDeletedThenRemoveItFromTheInvoice()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 5.21,
                Quantity = 1,
                Description = "Orange"
            });

            invoice.AddInvoiceLine(new InvoiceLine
            {
                InvoiceLineId = 2,
                Cost = 10.99,
                Quantity = 4,
                Description = "Banana"
            });

            invoice.RemoveInvoiceLine(1);
            Assert.Single(invoice.LineItems);
        }

        [Fact]
        public void GivenAnInvoiceWithItemsWhenGeneratingTheInvoiceThenCalculateTheTotal()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 5.21,
                Quantity = 1,
                Description = "Orange"
            });

            invoice.AddInvoiceLine(new InvoiceLine
            {
                InvoiceLineId = 2,
                Cost = 10.99,
                Quantity = 4,
                Description = "Banana"
            });


            var total = invoice.GetTotal();
            Assert.Equal(49.17m, total);
        }

        [Fact]
        public void GivenTwoInvoicesWhenGenenratingThenMergeTheInvoices()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = 10.33,
                Quantity = 4,
                Description = "Banana"
            });

            var invoice2 = new Invoice();

            invoice2.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = 5.22,
                Quantity = 1,
                Description = "Orange"
            });

            invoice2.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 3,
                Cost = 6.27,
                Quantity = 3,
                Description = "Blueberries"
            });

            invoice.MergeInvoices(invoice2);

            Assert.Collection(invoice.LineItems,
                elem1 =>
                {
                    Assert.Equal(1, elem1.InvoiceLineId);
                    Assert.Equal(10.33, elem1.Cost);
                    Assert.Equal(4, elem1.Quantity);
                    Assert.Equal("Banana", elem1.Description);
                },
                elem2 =>
                {
                    Assert.Equal(2, elem2.InvoiceLineId);
                    Assert.Equal(5.22, elem2.Cost);
                    Assert.Equal(1, elem2.Quantity);
                    Assert.Equal("Orange", elem2.Description);
                },
                elem3 =>
                {
                    Assert.Equal(3, elem3.InvoiceLineId);
                    Assert.Equal(6.27, elem3.Cost);
                    Assert.Equal(3, elem3.Quantity);
                    Assert.Equal("Blueberries", elem3.Description);
                });
        }
    }
}