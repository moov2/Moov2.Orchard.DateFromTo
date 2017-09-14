using Moov2.Orchard.DateFromTo.Models;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using System;

namespace Moov2.Orchard.DateFromTo.Projections.Filters
{
    [OrchardFeature(Features.DateFromToProjections)]
    public class FromInFutureFilter : IFilterProvider
    {
        public FromInFutureFilter()
        {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(dynamic describe)
        {
            describe.For("DateFromTo", T("Date From To"), T("Date From To"))
                .Element("FromInFuture", T("From In Future"), T("Content items which have 'From' date in the future"),
                    (Action<dynamic>)ApplyFilter,
                    (Func<dynamic, LocalizedString>)DisplayFilter,
                    null
                );
        }

        public void ApplyFilter(dynamic context)
        {
            var query = (IHqlQuery)context.Query;
            context.Query = query.Where(x => x.ContentPartRecord<DateFromToPartRecord>(), x => x.Gt("FromDateTimeUtc", DateTime.UtcNow));
        }

        public LocalizedString DisplayFilter(dynamic context)
        {
            return T("From In Future");
        }
    }
}