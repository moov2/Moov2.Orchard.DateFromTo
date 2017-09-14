using Moov2.Orchard.DateFromTo.Models;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using System;

namespace Moov2.Orchard.DateFromTo.Projections.Filters
{
    [OrchardFeature(Features.DateFromToProjections)]
    public class ToEmptyFilter : IFilterProvider
    {
        public ToEmptyFilter()
        {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(dynamic describe)
        {
            describe.For("DateFromTo", T("Date From To"), T("Date From To"))
                .Element("ToEmpty", T("To Is Empty"), T("Content items which have no 'To' date"),
                    (Action<dynamic>)ApplyFilter,
                    (Func<dynamic, LocalizedString>)DisplayFilter,
                    null
                );
        }

        public void ApplyFilter(dynamic context)
        {
            var query = (IHqlQuery)context.Query;
            context.Query = query.Where(x => x.ContentPartRecord<DateFromToPartRecord>(), x => x.IsNull("ToDateTimeUtc"));
        }

        public LocalizedString DisplayFilter(dynamic context)
        {
            return T("To Is Empty");
        }
    }
}