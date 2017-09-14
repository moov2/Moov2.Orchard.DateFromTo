using Orchard.Events;

namespace Moov2.Orchard.DateFromTo.Projections.Filters
{
    public interface IFilterProvider : IEventHandler
    {
        void Describe(dynamic describe);
    }
}
