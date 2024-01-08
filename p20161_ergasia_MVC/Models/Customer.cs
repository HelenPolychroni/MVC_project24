using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace p20161_ergasia_MVC.Models;

[Table("CUSTOMERS")]
public partial class Customer
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

    [InverseProperty("CustomersUsernameNavigation")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
