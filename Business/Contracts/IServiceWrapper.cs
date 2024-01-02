namespace Business.Contracts
{
    public interface IServiceWrapper
    {
        IPortalService Portal { get; }
        ILoginService Login { get; }
        IJwtUtils JwtUtil { get; }
    }
}
