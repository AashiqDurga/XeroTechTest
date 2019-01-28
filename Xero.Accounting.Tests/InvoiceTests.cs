using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Xero.Accounting.Tests
{
    public class InvoiceTests
    {
        private readonly Invoice _invoice;
        private readonly InvoiceLine _appleLineItem;
        private readonly InvoiceLine _bananaLineItem;
        private readonly InvoiceLine _orangeLineItem;

        public InvoiceTests()
        {
            _invoice = new Invoice();
            _appleLineItem = new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 6.99m,
                Quantity = 1,
                Description = "Apple"
            };
            _bananaLineItem = new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 10.99m,
                Quantity = 4,
                Description = "Banana"
            };
            _orangeLineItem = new InvoiceLine
            {
                InvoiceLineId = 2,
                Cost = 5.21m,
                Quantity = 1,
                Description = "Orange"
            };
        }

        [Fact]
        public void GivenAnInvoiceWhenGeneratingThenAddAnInvoiceLine()
        {
            _invoice.AddLineItem(_appleLineItem);

            var expectedLineItems = new List<InvoiceLine> {_appleLineItem};
            _invoice.LineItems.Should().BeEquivalentTo(expectedLineItems);
        }

        [Fact]
        public void GivenAnInvoiceWithItemsWhenAnItemIsDeletedThenRemoveItFromTheInvoice()
        {
            _invoice.AddLineItem(_bananaLineItem);
            _invoice.AddLineItem(_orangeLineItem);

            _invoice.RemoveLineItemBy(1);

            var expectedLineItems = new List<InvoiceLine> {_orangeLineItem};
            _invoice.LineItems.Should().BeEquivalentTo(expectedLineItems);
        }

        [Fact]
        public void GivenAnInvoiceWithItemsWhenGeneratingTheInvoiceThenCalculateTheTotal()
        {
            _invoice.AddLineItem(_bananaLineItem);
            _invoice.AddLineItem(_orangeLineItem);

            var total = _invoice.Total();

            var expectedTotal = 49.17m;
            Assert.Equal(expectedTotal, total);
        }

        [Fact]
        public void GivenTwoInvoicesWhenGeneratingThenMergeTheInvoices()
        {
            _invoice.AddLineItem(_bananaLineItem);

            var invoiceToMerge = CreateInvoiceToMerge();

            _invoice.MergeInvoicesFrom(invoiceToMerge);

            Assert.Collection(_invoice.LineItems,
                lineItemOne =>
                {
                    Assert.Equal(1, lineItemOne.InvoiceLineId);
                    Assert.Equal(10.99m, lineItemOne.Cost);
                    Assert.Equal(4, lineItemOne.Quantity);
                    Assert.Equal("Banana", lineItemOne.Description);
                },
                lineItemTwo =>
                {
                    Assert.Equal(2, lineItemTwo.InvoiceLineId);
                    Assert.Equal(5.21m, lineItemTwo.Cost);
                    Assert.Equal(1, lineItemTwo.Quantity);
                    Assert.Equal("Orange", lineItemTwo.Description);
                },
                lineItemThree =>
                {
                    Assert.Equal(3, lineItemThree.InvoiceLineId);
                    Assert.Equal(6.27m, lineItemThree.Cost);
                    Assert.Equal(3, lineItemThree.Quantity);
                    Assert.Equal("Blueberries", lineItemThree.Description);
                });
        }

        [Fact]
        public void GivenAnInvoiceWhenRequiredToMakeACopyThenCloneTheInvoice()
        {
            _invoice.AddLineItem(_appleLineItem);

            _invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 2,
                Cost = 6.27m,
                Quantity = 3,
                Description = "Blueberries"
            });

            var clonedInvoice = (Invoice) _invoice.Clone();

            clonedInvoice.Should().BeEquivalentTo(_invoice);
            Assert.Equal(_invoice.LineItems.Count, clonedInvoice.LineItems.Count);
        }

        [Fact]
        public void GivenAnInvoiceWhenDisplayingInConsoleThenConvertToSting()
        {
            var invoice = new Invoice
            {
                Date = new DateTime(2019, 01, 28),
                Number = 1000,
                LineItems = new List<InvoiceLine>
                {
                    _appleLineItem
                }
            };

            const string expectedInvoiceString = "InvoiceNumber: 1000, InvoiceDate: 01/28/2019, LineItemCount: 1";

            Assert.Equal(expectedInvoiceString, invoice.ToString());
        }

        private Invoice CreateInvoiceToMerge()
        {
            var invoiceToMerge = new Invoice();
            invoiceToMerge.AddLineItem(_orangeLineItem);
            invoiceToMerge.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 3,
                Cost = 6.27m,
                Quantity = 3,
                Description = "Blueberries"
            });
            return invoiceToMerge;
        }
    }
}