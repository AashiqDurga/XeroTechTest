using System;
using Xunit;

namespace Xero.Accounting.Tests
{
    public class InvoiceLineTests
    {
        [Fact]
        public void GivenAnInvoiceLineWhenSummingUpThenCalculateTheTotalOfTheLine()
        {
            var invoiceLine = new InvoiceLine {Cost = 10, Quantity = 5};

            Assert.Equal(50, invoiceLine.Total());
        }
    }
}