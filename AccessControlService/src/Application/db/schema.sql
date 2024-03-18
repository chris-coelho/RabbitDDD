
    drop table if exists accounts cascade

    create table accounts (
        Id uuid not null,
       modified_at timestamp not null,
       name varchar(255) not null,
       email varchar(255) not null,
       created_on timestamp not null,
       active boolean not null,
       primary key (Id),
      unique (email)
    )
