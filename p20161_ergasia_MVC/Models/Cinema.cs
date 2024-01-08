using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace p20161_ergasia_MVC.Models;

[Table("CINEMAS")]
public partial class Cinema
{
    [Key]
    [StringLength(45)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public int? Seats { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string? Is3D { get; set; }

    [InverseProperty("ScreeningsCinemasNameNavigation")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [InverseProperty("CinemasNameNavigation")]
    public virtual ICollection<Screening> Screenings { get; set; } = new List<Screening>();
}
