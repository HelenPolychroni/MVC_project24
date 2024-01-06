using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace p20161_ergasia_MVC.Models;

[Table("ADMINS")]
public partial class Admin
{
    [Column("fullname")]
    [StringLength(45)]
    [Unicode(false)]
    public string Fullname { get; set; } = null!;

    [Key]
    [Column("username")]
    [StringLength(32)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("email")]
    [StringLength(45)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [StringLength(45)]
    [Unicode(false)]
    public string Password { get; set; } = null!;
}
