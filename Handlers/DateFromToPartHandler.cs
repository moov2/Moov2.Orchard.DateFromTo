using Moov2.Orchard.DateFromTo.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using System;

namespace Moov2.Orchard.DateFromTo.Handlers
{
    public class DateFromToPartHandler : ContentHandler
    {
        public DateFromToPartHandler(IRepository<DateFromToPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));

            OnIndexing<DateFromToPart>((context, eventPart) =>
            {
                context.DocumentIndex
                    .Add(Constants.FromDateTimeIndexPropertyName, eventPart.FromDateTimeUtc ?? DateTime.MinValue).Store()
                    .Add(Constants.ToDateTimeIndexPropertyName, eventPart.ToDateTimeUtc ?? DateTime.MaxValue).Store();
            });
        }
    }
}