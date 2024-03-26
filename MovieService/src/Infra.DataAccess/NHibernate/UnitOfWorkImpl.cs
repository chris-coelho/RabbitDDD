using Domain.Repositories;
using NHibernate;

namespace Infra.DataAccess.NHibernate;

public class UnitOfWorkImpl : UnitOfWork, IUnitOfWorkDomain
{
    public UnitOfWorkImpl(ISession session) : base(session) {}
}