using System;
using System.ComponentModel.DataAnnotations;

public class UserSessions {
    [Key]
    public string RefreshToken { get; set; }

    public DateTime Expire { get; set; }

    public int UserId { get; set; }
}