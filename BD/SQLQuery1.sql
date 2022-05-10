create table Usuario(
	IdUsuario		integer			identity(1,1),
	Nome			varchar(100)	not null,
	DataNascimento	date			not null,
	NomeUsuario		varchar(100)	not null,
	Email			varchar(100)	not null,
	Senha			varchar(100)	not null,
	primary key(IdUsuario)
)