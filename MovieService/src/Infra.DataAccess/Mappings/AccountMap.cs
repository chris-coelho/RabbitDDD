using Domain.Models;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace Infra.DataAccess.Mappings;

public class AccountMap : ClassMap<Account>
{
    public AccountMap()
    {
        Table("accounts");

        Id(x => x.Id)
            .GeneratedBy.Assigned();

        Version(x => x.ModifiedAt)
            .Column("modified_at")
            .CustomType<UtcDateTimeType>();

        Map(x => x.Username)
            .Column("name")
            .Not.Nullable();
    }
}