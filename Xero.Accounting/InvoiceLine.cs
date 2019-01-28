namespace Xero.Accounting
{
    public class InvoiceLine
    {
        public int InvoiceLineId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }

        public decimal Total()
        {
            return Cost * Quantity;
        }
    }
}