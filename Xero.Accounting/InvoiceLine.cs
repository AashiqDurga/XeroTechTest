namespace Xero.Accounting
{
    public class InvoiceLine
    {
        public int InvoiceLineId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }

        public double Total()
        {
            return Cost * Quantity;
        }
    }
}