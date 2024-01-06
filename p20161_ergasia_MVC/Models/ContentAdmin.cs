using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace p20161_ergasia_MVC.Models;

[Table("CONTENT_ADMINS")]
public partial class ContentAdmin
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
    [StringLength(32)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("password")]
    [StringLength(45)]
    [Unicode(false)]
    public string? Password { get; set; }

    [InverseProperty("ContentAdminsUsernameNavigation")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    [InverseProperty("ContentAdminsUsernameNavigation")]
    public virtual ICollection<Screening> Screenings { get; set; } = new List<Screening>();
}
