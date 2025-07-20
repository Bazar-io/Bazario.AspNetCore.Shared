namespace Bazario.AspNetCore.Shared.Abstractions.Mappers
{
    /// <summary>
    /// Abstract class representing mapper that maps objects of type <typeparamref name="TSource"/> to <typeparamref name="TDestination"/>.
    /// </summary>
    /// <typeparam name="TSource">The type of the source object that will be mapped to the destination object.</typeparam>
    /// <typeparam name="TDestination">The type of the destination object that the source object will be mapped to.</typeparam>
    public abstract class Mapper<TSource, TDestination>
    {
        /// <summary>
        /// Maps an object of type <typeparamref name="TSource"/> to an object of type <typeparamref name="TDestination"/>.
        /// </summary>
        /// <param name="source">The source object that will be mapped to the destination object.</param>
        /// <returns>
        /// An object of type <typeparamref name="TDestination"/> that represents the mapped version of the source object.
        /// </returns>
        public abstract TDestination Map(TSource source);

        /// <summary>
        /// Maps a collection of objects of type <typeparamref name="TSource"/> to a collection of objects of type <typeparamref name="TDestination"/>.
        /// </summary>
        /// <param name="source">The collection of source objects that will be mapped to the destination objects.</param>
        /// <returns>
        /// An <see cref="IEnumerable{TDestination}"/> that contains the mapped objects of type <typeparamref name="TDestination"/>.
        /// </returns>
        public virtual IEnumerable<TDestination> Map(IEnumerable<TSource> source)
            => source.Select(Map);
    }
}
