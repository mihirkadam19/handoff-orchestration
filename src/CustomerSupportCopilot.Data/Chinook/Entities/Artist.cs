using System;
using System.Collections.Generic;

namespace CustomerSupportCopilot.Data.Chinook.Entities;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
