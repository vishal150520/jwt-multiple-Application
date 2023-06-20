using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Web_Token.Database;

public partial class TbEmployee
{
    [Key]
    public int Eid { get; set; }
    [Required]
    public string Ename { get; set; } = null!;
    [Required]
    public int Age { get; set; }
    [Required]

    public string EmailId { get; set; } = null!;
    [Required]

    public string Passwords { get; set; } = null!;
}
