using Moov2.Orchard.DateFromTo.Models;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using System;

namespace Moov2.Orchard.DateFromTo.Projections.Filters
{
    [OrchardFeature(Features.DateFromToProjections)]
    public class FromEmptyFilter : IFilterProvider
    {
        public FromEmptyFilter()
        {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Describe(dynamic describe)
        {
            describe.For("DateFromTo", T("Date From To"), T("Date From To"))
                .Element("FromEmpty", T("From Is Empty"), T("Content items which have no 'From' date"),
                    (Action<dynamic>)ApplyFilter,
                    (Func<dynamic, LocalizedString>)DisplayFilter,
                    null
                );
        }

        public void ApplyFilter(dynamic context)
        {
            var query = (IHqlQuery)context.Query;
            context.Query = query.Where(x => x.ContentPartRecord<DateFromToPartRecord>(), x => x.IsNull("FromDateTimeUtc"));
        }

        public LocalizedString DisplayFilter(dynamic context)
        {
            return T("From Is Empty");
        }
    }
}