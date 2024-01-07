using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace p20161_ergasia_MVC.Models;

[Table("RESERVATIONS")]
public partial class Reservation
{
    [Column("SCREENINGS_MOVIES_NAME")]
    [StringLength(45)]
    [Unicode(false)]
    public string? ScreeningsMoviesName { get; set; }

    [Column("SCREENINGS_CINEMAS_NAME")]
    [StringLength(45)]
    [Unicode(false)]
    public string? ScreeningsCinemasName { get; set; }

    [Column("CUSTOMERS_USERNAME")]
    [StringLength(32)]
    [Unicode(false)]
    public string? CustomersUsername { get; set; }

    [Column("NUMBER_OF_SEATS")]
    public int? NumberOfSeats { get; set; }

    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [ForeignKey("CustomersUsername")]
    [InverseProperty("Reservations")]
    public virtual Customer? CustomersUsernameNavigation { get; set; }

    [ForeignKey("ScreeningsCinemasName")]
    [InverseProperty("Reservations")]
    public virtual Cinema? ScreeningsCinemasNameNavigation { get; set; }

    [ForeignKey("ScreeningsMoviesName")]
    [InverseProperty("Reservations")]
    public virtual Movie? ScreeningsMoviesNameNavigation { get; set; }
}
