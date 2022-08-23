create database DBname
go

use DBname
go

create table Podatci
(
	Ime nvarchar(50),
	Prezime nvarchar(50),
	PostanskiBr nvarchar(50),
	Grad nvarchar(50),
	Telefon nvarchar(50),
	constraint PK primary key
    (
        Ime, Prezime, PostanskiBr, Grad, Telefon
    )
)
go

create procedure AddPerson
	@Ime nvarchar(50),
	@Prezime nvarchar(50),
	@PostanskiBr nvarchar(50),
	@Grad nvarchar(50),
	@Telefon nvarchar(50)
as
begin
begin try
insert into Podatci (Ime , Prezime , PostanskiBr , Grad , Telefon) values (@Ime , @Prezime , @PostanskiBr , @Grad , @Telefon)
end try
begin catch
THROW 51000, 'Could not create Person.', 1;
end catch
end
go