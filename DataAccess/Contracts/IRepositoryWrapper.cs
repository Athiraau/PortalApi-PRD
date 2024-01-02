namespace DataAccess.Contracts
{
    public interface IRepositoryWrapper 
    { 
        IPortalRepository Portal { get; } 
        ILoginRepository Login { get; }
        void Save(); 
    }
}
