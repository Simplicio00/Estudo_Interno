use BancoUConteudo

go

insert into Usuario(Nome,Email,Senha)values('Jos�','jose@email.com','jose@123');

go

insert into Conteudo(ConteudoTexto)values('Quem n�o ca�a com cachorro, ca�a com gato, 
quem n�o ca�a com gato... ca�a com rato')

go

insert into Curte(IdUsuario,IdConteudo)values(1,1);