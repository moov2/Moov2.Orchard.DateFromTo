using Moov2.Orchard.DateFromTo.Models;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using System;

namespace Moov2.Orchard.DateFromTo.Projections.Filters
{
    [OrchardFeature(Features.DateFromToProjections)]
    public class ToInFutureFilter : IFilterProvider
    {
        public ToInFutureFilter()
        {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(dynamic describe)
        {
            describe.For("DateFromTo", T("Date From To"), T("Date From To"))
                .Element("ToInFuture", T("To In Future"), T("Content items which have 'To' date in the future"),
                    (Action<dynamic>)ApplyFilter,
                    (Func<dynamic, LocalizedString>)DisplayFilter,
                    null
                );
        }

        public void ApplyFilter(dynamic context)
        {
            var query = (IHqlQuery)context.Query;
            context.Query = query.Where(x => x.ContentPartRecord<DateFromToPartRecord>(), x => x.Gt("ToDateTimeUtc", DateTime.UtcNow));
        }

        public LocalizedString DisplayFilter(dynamic context)
        {
            return T("To In Future");
        }
    }
}