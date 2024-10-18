namespace Library.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public int ShippingId { get; set; }
        public int? PaymentId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public virtual Member Member { get; set; }
        public virtual Shipping Shipping { get; set; }
        public virtual Payment Payment { get; set; }
       
    }

}
