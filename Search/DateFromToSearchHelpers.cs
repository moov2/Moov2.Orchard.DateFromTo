using Orchard.Indexing;
using System;

namespace Moov2.Orchard.DateFromTo.Search
{
    public static class DateFromToSearchHelpers
    {
        #region ISearchBuilder Extensions
        public static ISearchBuilder SetDateFromToParameters(this ISearchBuilder builder, IDateFromToSearchParameters parameters)
        {
            if (parameters == null)
                return builder;
            return builder.SetDateFilter(parameters.DateFrom, parameters.DateTo);
        }

        public static ISearchBuilder SetDateFilter(this ISearchBuilder builder, string dateFrom, string dateTo)
        {
            return builder
                .SetDateFromFilter(dateFrom)
                .SetDateToFilter(dateTo);
        }

        public static ISearchBuilder SetDateFromFilter(this ISearchBuilder builder, string date)
        {
            var fromDate = ProcessDate(date);
            if (fromDate.HasValue)
                builder.WithinRange(Constants.FromDateTimeIndexPropertyName, fromDate, null).AsFilter().Mandatory();
            return builder;
        }

        public static ISearchBuilder SetDateToFilter(this ISearchBuilder builder, string date)
        {
            var toDate = ProcessDate(date);
            if (toDate.HasValue)
                builder.WithinRange(Constants.ToDateTimeIndexPropertyName, null, toDate).AsFilter().Mandatory();
            return builder;
        }
        #endregion

        #region Helpers
        private static DateTime? ProcessDate(string date)
        {
            DateTime parsed;
            if (!DateTime.TryParse(date, out parsed))
                return null;
            return parsed.ToUniversalTime();
        }
        #endregion
    }
}