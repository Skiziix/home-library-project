using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Library.EntityModels;

[Table("author")]
public partial class Author
{
    [Key]
    [Column("author_id")]
    public int AuthorId { get; set; }

    [Column("first_name", TypeName = "varchar(50)")]
    public string? FirstName { get; set; }

    [Column("last_name", TypeName = "varchar(50)")]
    public string? LastName { get; set; }

    [Column("pseudonym", TypeName = "varchar(50)")]
    public string? Pseudonym { get; set; }

    [Column("date_of_birth", TypeName = "year")]
    public int? DateOfBirth { get; set; }

    [InverseProperty("Author")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
