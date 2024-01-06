using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace p20161_ergasia_MVC.Models;

[Table("SCREENINGS")]
public partial class Screening
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("MOVIES_NAME")]
    [StringLength(45)]
    [Unicode(false)]
    public string? MoviesName { get; set; }

    [Column("CINEMAS_NAME")]
    [StringLength(45)]
    [Unicode(false)]
    public string? CinemasName { get; set; }

    [Column("TIME", TypeName = "datetime")]
    public DateTime? Time { get; set; }

    [Column("CONTENT_ADMINS_USERNAME")]
    [StringLength(32)]
    [Unicode(false)]
    public string? ContentAdminsUsername { get; set; }

    [ForeignKey("CinemasName")]
    [InverseProperty("Screenings")]
    public virtual Cinema? CinemasNameNavigation { get; set; }

    [ForeignKey("ContentAdminsUsername")]
    [InverseProperty("Screenings")]
    public virtual ContentAdmin? ContentAdminsUsernameNavigation { get; set; }

    [ForeignKey("MoviesName")]
    [InverseProperty("Screenings")]
    public virtual Movie? MoviesNameNavigation { get; set; }
}
