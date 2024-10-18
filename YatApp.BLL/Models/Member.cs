using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models;

public class Member
{
    public int MemberId { get; set; }
    
    //Name
    [Required(ErrorMessage = "آame is required")]
    public string MemberName { get; set; } = null!;

    //Email
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }
    public DateTime MembershipDate { get; set; }
    
    //Password
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = null!;
    public int RoleId { get; set; }

    // Relations
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }
  
}