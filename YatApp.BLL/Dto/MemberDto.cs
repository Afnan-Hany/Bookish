﻿namespace Dto;

public class MemberDto
{
    public string MemberName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public DateTime MembershipDate { get; set; }
    public string Password { get; set; } = null!;
    public string Username { get; set; } = null!;
    public int RoleId { get; set; }
}
