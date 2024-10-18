using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Penalty
{
    public int PenaltyId { get; set; }

  

    public decimal PenaltyAmount { get; set; }

    public DateTime PenaltyDate { get; set; }
    public int CheckoutId { get; set; }

  
    public virtual Checkout Checkout { get; set; } = null!;
}
