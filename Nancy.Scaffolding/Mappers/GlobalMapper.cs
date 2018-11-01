using AutoMapper;

namespace Nancy.Scaffolding.Mappers
{
    /// <summary>
    /// Global mapper
    /// </summary>
    public static class GlobalMapper
    {
        public static IRuntimeMapper Mapper { get; set; }
        
        public static TDestination Map<TDestination>(this object objToMap) where TDestination : class
        {
            return GlobalMapper.Mapper.DefaultContext.Mapper.Map<TDestination>(objToMap);
        }

        public static TDestination Map<TSource, TDestination>(this TDestination destination, TSource source)
        {
            return GlobalMapper.Mapper.DefaultContext.Mapper.Map(source, destination);
        }
    }
}
