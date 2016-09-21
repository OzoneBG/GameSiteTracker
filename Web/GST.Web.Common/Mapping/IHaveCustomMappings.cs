namespace GST.Web.Common.Mapping
{
    using AutoMapper;

    public interface IMapTo
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
