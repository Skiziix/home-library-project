using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Library.EntityModels;

[Keyless]
public partial class VRecentBook
{
    [Column("author_name")]
    public string? AuthorName { get; set; }

    [Column("book_title", TypeName = "tinytext")]
    public string? BookTitle { get; set; }

    [Column("publish_year", TypeName = "year")]
    public int? PublishYear { get; set; }
}
