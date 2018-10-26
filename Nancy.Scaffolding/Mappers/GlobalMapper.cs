using AutoMapper;

namespace Nancy.Scaffolding.Mappers
{
    /// <summary>
    /// Global mapper
    /// </summary>
    public static class GlobalMapper
    {
        public static IRuntimeMapper Mapper { get; set; }
        
        public static T Map<T>(object objToMap) where T : class
        {
            return GlobalMapper.Mapper.DefaultContext.Mapper.Map<T>(objToMap);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            
            return GlobalMapper.Mapper.DefaultContext.Mapper.Map(source, destination);
        }
    }
}
