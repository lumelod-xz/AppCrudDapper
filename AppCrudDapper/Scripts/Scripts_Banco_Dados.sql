Create table Usuarios(
	Id int IDENTITY(1,1),
	NomeCompleto varchar(100),
	Email varchar(100),
	Cargo varchar(100),
	CPF varchar(11),
	Salario decimal,
	Situacao bit,
	Senha varchar(100)
)

