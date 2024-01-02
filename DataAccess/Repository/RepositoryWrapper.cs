using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Dto;

namespace DataAccess.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper 
    { 
        private DapperContext _repoContext; 
        private IPortalRepository _portal;
        private ILoginRepository _login;
        private DtoWrapper _dto;

        public RepositoryWrapper(DapperContext repoContext, DtoWrapper dto)
        {
            _repoContext = repoContext;
            _dto = dto;
        }
        public IPortalRepository Portal 
        { 
            get 
            { 
                if (_portal == null) 
                {
                    _portal = new PortalRepository(_repoContext, _dto); 
                } 
                return _portal; 
            } 
        }

        public ILoginRepository Login
        {
            get
            {
                if (_login == null)
                {
                    _login = new LoginRepository(_repoContext, _dto);
                }
                return _login;
            }
        }

        public RepositoryWrapper(DapperContext dapperContext) 
        { 
            _repoContext = dapperContext; 
        } 
        
        public void Save() 
        {
            _repoContext.SaveChanges();
        } 
    }
}
