namespace Services.SystemSecurity
{
    public interface IParameterGroupRepository : IStandardMakerCheckerService<SysParameter, SysParameterDto, string, StandardStatusFilter>
    {
        List<GenericData<ReportParameterDataType>> GetDataTypes();
    }
    
}