using System;

namespace Mikkelhm_F1.Domain;

public class MetaInformation
{
    public DateTime CreatedDateUtc { get; set; }

    public MetaInformation()
    {
        CreatedDateUtc = DateTime.UtcNow;
    }
}
