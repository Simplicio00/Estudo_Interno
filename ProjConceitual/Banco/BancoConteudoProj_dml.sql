use BancoUConteudo

go

insert into Usuario(Nome,Email,Senha)values('José','jose@email.com','jose@123');

go

insert into Conteudo(ConteudoTexto)values('Quem não caça com cachorro, caça com gato, 
quem não caça com gato... caça com rato')

go

insert into Curte(IdUsuario,IdConteudo)values(1,1);