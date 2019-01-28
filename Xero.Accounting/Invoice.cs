using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xero.Accounting
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

        public void MergeInvoices(Invoice sourceInvoice)
        {
            LineItems.AddRange(sourceInvoice.LineItems);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            var invoiceSting = new StringBuilder();

            invoiceSting.Append($"InvoiceNumber: {InvoiceNumber}, ");
            invoiceSting.Append($"InvoiceDate: {InvoiceDate:d}, ");
            invoiceSting.Append($"LineItemCount: {LineItems.Count}");

            return invoiceSting.ToString();
        }
    }
}