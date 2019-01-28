using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XeroTechnicalTest
{
    public class Invoice : ICloneable
    {
        public Invoice()
        {
            LineItems = new List<InvoiceLine>();
        }

        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }

        public List<InvoiceLine> LineItems { get; set; }

        public void AddInvoiceLine(InvoiceLine invoiceLine)
        {
            LineItems.Add(invoiceLine);
        }

        public void RemoveInvoiceLine(int invoiceLineId)
        {
            var itemToRemove = LineItems.Single(x => x.InvoiceLineId == invoiceLineId);
            LineItems.Remove(itemToRemove);
        }

        /// <summary>
        /// GetTotal should return the sum of (Cost * Quantity) for each line item
        /// </summary>
        public decimal GetTotal()
        {
            double invoiceTotal = 0;
            foreach (var invoiceLineItem in LineItems)
            {
                var invoiceLineTotal = invoiceLineItem.Cost * invoiceLineItem.Quantity;

                invoiceTotal = invoiceTotal + invoiceLineTotal;
            }

            return (decimal) invoiceTotal;
        }

        /// <summary>
        /// MergeInvoices appends the items from the sourceInvoice to the current invoice
        /// </summary>
        /// <param name="sourceInvoice">Invoice to merge from</param>
        public void MergeInvoices(Invoice sourceInvoice)
        {
            LineItems.AddRange(sourceInvoice.LineItems);
        }

        /// <summary>
        /// Creates a deep clone of the current invoice (all fields and properties)
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Outputs string containing the following (replace [] with actual values):
        /// Invoice Number: [InvoiceNumber], InvoiceDate: [DD/MM/YYYY], LineItemCount: [Number of items in LineItems] 
        /// </summary>
        public override string ToString()
        {
            var sting = new StringBuilder();

            sting.Append($"InvoiceNumber: {InvoiceNumber}, ");
            sting.Append($"InvoiceDate: {InvoiceDate:d}, ");
            sting.Append($"LineItemCount: {LineItems.Count}");

            return sting.ToString();
        }
    }
}