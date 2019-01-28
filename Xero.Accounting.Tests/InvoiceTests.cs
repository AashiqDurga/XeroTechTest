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

        public InvoiceTests()
        {
            _invoice = new Invoice();
        }

        [Fact]
        public void GivenAnInvoiceWhenGeneratingThenAddAnInvoiceLine()
        {
            var invoiceLine = new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 6.99m,
                Quantity = 1,
                Description = "Apple"
            };
            _invoice.AddLineItem(invoiceLine);

            Assert.Single(_invoice.LineItems);
        }

        [Fact]
        public void GivenAnInvoiceWithItemsWhenAnItemIsDeletedThenRemoveItFromTheInvoice()
        {
            _invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 5.21m,
                Quantity = 1,
                Description = "Orange"
            });

            _invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 2,
                Cost = 10.99m,
                Quantity = 4,
                Description = "Banana"
            });

            _invoice.RemoveLineItemBy(1);
            Assert.Single(_invoice.LineItems);
            Assert.Equal(2, _invoice.LineItems.First().InvoiceLineId);
        }

        [Fact]
        public void GivenAnInvoiceWithItemsWhenGeneratingTheInvoiceThenCalculateTheTotal()
        {
            _invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 5.21m,
                Quantity = 1,
                Description = "Orange"
            });

            _invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 2,
                Cost = 10.99m,
                Quantity = 4,
                Description = "Banana"
            });


            var total = _invoice.Total();
            Assert.Equal(49.17m, total);
        }

        [Fact]
        public void GivenTwoInvoicesWhenGeneratingThenMergeTheInvoices()
        {
            _invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 10.33m,
                Quantity = 4,
                Description = "Banana"
            });

            var invoiceToMerge = new Invoice();

            invoiceToMerge.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 2,
                Cost = 5.22m,
                Quantity = 1,
                Description = "Orange"
            });

            invoiceToMerge.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 3,
                Cost = 6.27m,
                Quantity = 3,
                Description = "Blueberries"
            });

            _invoice.MergeInvoicesFrom(invoiceToMerge);

            Assert.Collection(_invoice.LineItems,
                lineItemOne =>
                {
                    Assert.Equal(1, lineItemOne.InvoiceLineId);
                    Assert.Equal(10.33m, lineItemOne.Cost);
                    Assert.Equal(4, lineItemOne.Quantity);
                    Assert.Equal("Banana", lineItemOne.Description);
                },
                lineItemTwo =>
                {
                    Assert.Equal(2, lineItemTwo.InvoiceLineId);
                    Assert.Equal(5.22m, lineItemTwo.Cost);
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
            _invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 6.99m,
                Quantity = 1,
                Description = "Apple"
            });

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
                    new InvoiceLine
                    {
                        InvoiceLineId = 1,
                        Cost = 6.99m,
                        Quantity = 1,
                        Description = "Apple"
                    }
                }
            };

            Assert.Equal("InvoiceNumber: 1000, InvoiceDate: 01/28/2019, LineItemCount: 1", invoice.ToString());
        }
    }
}