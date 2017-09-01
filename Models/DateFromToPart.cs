using Orchard.ContentManagement;
using System;

namespace Moov2.Orchard.DateFromTo.Models
{
    public class DateFromToPart : ContentPart<DateFromToPartRecord>
    {
        public DateTime? FromDateTimeUtc
        {
            get { return Retrieve(x => x.FromDateTimeUtc); }
            set { Store(x => x.FromDateTimeUtc, value); }
        }

        public DateTime? ToDateTimeUtc
        {
            get { return Retrieve(x => x.ToDateTimeUtc); }
            set { Store(x => x.ToDateTimeUtc, value); }
        }
    }
}