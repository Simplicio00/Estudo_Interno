use BancoUConteudo

--executa devagar, query por query

select Usuario.Nome, Conteudo.ConteudoTexto from Curte
inner join Usuario on Usuario.IdUsuario = Curte.IdUsuario
inner join Conteudo on Conteudo.IdConteudo = Curte.IdConteudo

go

--criação de um procedimento que busca todas as curtias pelo nome do usuario

create procedure PorNome
@PNome varchar(50) as
select Usuario.Nome, Conteudo.ConteudoTexto from Curte
inner join Usuario on Usuario.IdUsuario = Curte.IdUsuario
inner join Conteudo on Conteudo.IdConteudo = Curte.IdConteudo
where Nome like @PNome

-- executando procedimento

exec PorNome '%Jos%'