/*
    Welcome to the Xero technical excercise!
    ---------------------------------------------------------------------------------
    The test consists of a small invoice application that has a number of issues.

    Your job is to fix them and make sure you can perform the functions in each method below.

    Note your first job is to get the solution compiling! 
	
    Rules
    ---------------------------------------------------------------------------------
    * The entire solution must be written in C# (any version)
    * You can modify any of the code in this solution, split out classes, add projects etc
    * You can modify Invoice and InvoiceLine, rename and add methods, change property types (hint) 
    * Feel free to use any libraries or frameworks you like as long as they are .net based
    * Feel free to write tests (hint) 
    * Show off your skills! 

    Good luck :) 

    When you have finished the solution please zip it up and email it back to the recruiter or developer who sent it to you
*/

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