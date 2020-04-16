create database BancoUConteudo

go

use BancoUConteudo

go

create table Usuario
(
IdUsuario int primary key identity,
Nome varchar(50) not null,
Email varchar(50) not null unique,
Senha varchar(50) not null,
StatusU bit default 1 not null,
)

go

create table Conteudo
(
IdConteudo int primary key identity,
ConteudoTexto text not null
)

go

create table Curte
(
IdCurtir int primary key identity,
IdUsuario int foreign key references Usuario(IdUsuario),
IdConteudo int foreign key references Conteudo(IdConteudo)
)