namespace Library.Models
{
    public class Payment
    {
        public int PaymentId { get; set; } 
       
        public int PaymentMethodId { get; set; } 
        public decimal PaymentAmount { get; set; } 
        public DateTime PaymentDate { get; set; } 
       
        public virtual PaymentMethod PaymentMethod { get; set; } 
    }
}
