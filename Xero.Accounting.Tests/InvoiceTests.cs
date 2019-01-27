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
    }
}