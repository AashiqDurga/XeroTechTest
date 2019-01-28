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

        public int Number { private get; set; }
        public DateTime Date { private get; set; }

        public List<InvoiceLine> LineItems { get; set; }

        public void AddLineItem(InvoiceLine invoiceLine)
        {
            LineItems.Add(invoiceLine);
        }

        public void RemoveLineItemBy(int invoiceLineId)
        {
            var itemToRemove = LineItems.Single(lineItem => lineItem.InvoiceLineId == invoiceLineId);
            LineItems.Remove(itemToRemove);
        }

        public decimal Total()
        {
            decimal invoiceTotal = 0;
            LineItems.ForEach(lineItem => invoiceTotal += lineItem.Total());
            
            return invoiceTotal;
        }

        public void MergeInvoicesFrom(Invoice sourceInvoice)
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

            invoiceSting.Append($"InvoiceNumber: {Number}, ");
            invoiceSting.Append($"InvoiceDate: {Date:dd/MM/yyyy}, ");
            invoiceSting.Append($"LineItemCount: {LineItems.Count}");

            return invoiceSting.ToString();
        }
    }
}