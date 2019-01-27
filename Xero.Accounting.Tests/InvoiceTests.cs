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
    }
}