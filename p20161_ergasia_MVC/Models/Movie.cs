using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace p20161_ergasia_MVC.Models;

[Table("MOVIES")]
public partial class Movie
{
    [Key]
    [Column("NAME")]
    [StringLength(45)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("LENGTH")]
    public int? Length { get; set; }

    [Column("TYPE")]
    [StringLength(45)]
    [Unicode(false)]
    public string? Type { get; set; }

    [Column("SUMMARY")]
    [StringLength(45)]
    [Unicode(false)]
    public string? Summary { get; set; }

    [Column("DIRECTOR")]
    [StringLength(45)]
    [Unicode(false)]
    public string? Director { get; set; }

    [Column("CONTENT_ADMINS_USERNAME")]
    [StringLength(32)]
    [Unicode(false)]
    public string? ContentAdminsUsername { get; set; }

    [ForeignKey("ContentAdminsUsername")]
    [InverseProperty("Movies")]
    public virtual ContentAdmin? ContentAdminsUsernameNavigation { get; set; }

    [InverseProperty("ScreeningsMoviesNameNavigation")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [InverseProperty("MoviesNameNavigation")]
    public virtual ICollection<Screening> Screenings { get; set; } = new List<Screening>();
}
