using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace p20161_ergasia_MVC.Models;

[Keyless]
[Table("RESERVATIONS")]
public partial class Reservation
{
    [Column("SCREENINGS_MOVIES_NAME")]
    [StringLength(45)]
    [Unicode(false)]
    public string ScreeningsMoviesName { get; set; } = null!;

    [Column("SCREENINGS_CINEMAS_ID")]
    public int ScreeningsCinemasId { get; set; }

    [Column("NUMBER_OF_SEATS")]
    public int NumberOfSeats { get; set; }

    [Column("CUSTOMER_FULLNAME")]
    [StringLength(45)]
    [Unicode(false)]
    public string? CustomerFullname { get; set; }
}
