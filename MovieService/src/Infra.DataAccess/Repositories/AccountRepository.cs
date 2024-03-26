using Domain.Models;
using Domain.Repositories;

namespace Infra.DataAccess.Repositories;

public class AccountRepository : RepositoryBase<Account>, IAccountRepository
{
    public AccountRepository(IUnitOfWorkDomain unitOfWork) : base(unitOfWork) {}
}