using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Library.EntityModels;

[Table("book")]
public partial class Book
{
    [Key]
    [Column("book_id")]
    public int BookId { get; set; }

    [Column("author_id", TypeName = "INT")]
    public int? AuthorId { get; set; }

    [Column("book_title", TypeName = "tinytext")]
    public string? BookTitle { get; set; }

    [Column("publish_year", TypeName = "year")]
    public int? PublishYear { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Books")]
    [XmlIgnore]
    public virtual Author? Author { get; set; }
}
