using Orchard.ContentManagement.Records;
using System;

namespace Moov2.Orchard.DateFromTo.Models
{
    public class DateFromToPartRecord : ContentPartVersionRecord
    {
        public virtual DateTime? FromDateTimeUtc { get; set; }
        public virtual DateTime? ToDateTimeUtc { get; set; }
    }
}