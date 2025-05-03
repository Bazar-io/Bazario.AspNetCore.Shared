namespace Bazario.AspNetCore.Shared.Application.Mappers
{
    public abstract class Mapper<TSource, TDestination>
    {
        public abstract TDestination Map(TSource source);

        public virtual IEnumerable<TDestination> Map(IEnumerable<TSource> source)
            => source.Select(Map);
    }
}
