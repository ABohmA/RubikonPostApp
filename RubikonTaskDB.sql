--Create database RubikonTaskDB
--go
use RubikonTaskDB
go 

--Koristi se NVARCHAR posto je u sklopu zadatka pisalo da je charset=UTF-8 u 
--suprotnom bih se koristio varchar jer zauzima manje prostora na disku

Create Table BlogPost
(
Slug nvarchar(255) primary key not null,
Title nvarchar(max) not null,
PostDescription nvarchar(max) not null,
Body nvarchar (max) not null,
CreateAt datetime not null,
UpdatedAt datetime
)
GO

Create Table Tag
(
TagID int identity(1,1) primary key not null,
TName varchar(30) not null
)
GO

Create table PostTags
(
PostSlug nvarchar(255) constraint SlugPostTag foreign key (PostSlug) references BlogPost(Slug) ON delete cascade not null ,
TagID int constraint IDTagPost foreign key (TagID) references Tag(TagID) ON delete cascade not null,
Primary key(PostSlug,TagID)
)
GO
