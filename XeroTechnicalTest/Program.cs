using System;
using System.Collections.Generic;
using Xero.Accounting;

namespace XeroTechnicalTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Xero Tech Test!");

            CreateInvoiceWithOneItem();
            CreateInvoiceWithMultipleItemsAndQuantities();
            RemoveItem();
            MergeInvoices();
            CloneInvoice();
            InvoiceToString();
        }

        private static void CreateInvoiceWithOneItem()
        {
            var invoice = new Invoice();

            invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 6.99m,
                Quantity = 1,
                Description = "Apple"
            });

            Console.WriteLine(invoice.Total());
        }

        private static void CreateInvoiceWithMultipleItemsAndQuantities()
        {
            var invoice = new Invoice();

            invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 10.21m,
                Quantity = 4,
                Description = "Banana"
            });

            invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 2,
                Cost = 5.21m,
                Quantity = 1,
                Description = "Orange"
            });

            invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 3,
                Cost = 5.21m,
                Quantity = 5,
                Description = "Pineapple"
            });

            Console.WriteLine(invoice.Total());
        }

        private static void RemoveItem()
        {
            var invoice = new Invoice();

            invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 5.21m,
                Quantity = 1,
                Description = "Orange"
            });

            invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 2,
                Cost = 10.99m,
                Quantity = 4,
                Description = "Banana"
            });

            invoice.RemoveLineItemBy(1);
            Console.WriteLine(invoice.Total());
        }

        private static void MergeInvoices()
        {
            var invoice1 = new Invoice();

            invoice1.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 10.33m,
                Quantity = 4,
                Description = "Banana"
            });

            var invoice2 = new Invoice();

            invoice2.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 2,
                Cost = 5.22m,
                Quantity = 1,
                Description = "Orange"
            });

            invoice2.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 3,
                Cost = 6.27m,
                Quantity = 3,
                Description = "Blueberries"
            });

            invoice1.MergeInvoicesFrom(invoice2);
            Console.WriteLine(invoice1.Total());
        }

        private static void CloneInvoice()
        {
            var invoice = new Invoice();

            invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 1,
                Cost = 6.99m,
                Quantity = 1,
                Description = "Apple"
            });

            invoice.AddLineItem(new InvoiceLine
            {
                InvoiceLineId = 2,
                Cost = 6.27m,
                Quantity = 3,
                Description = "Blueberries"
            });

            var clonedInvoice = (Invoice)invoice.Clone();
            Console.WriteLine(clonedInvoice.Total());
        }

        private static void InvoiceToString()
        {
            var invoice = new Invoice
            {
                Date = DateTime.Now,
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

            Console.WriteLine(invoice.ToString());
        }
    }
}