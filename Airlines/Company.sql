create table [dbo].[Company]
(
	[ID_comp] int NOT NULL primary key, 
    [name] char(10) NOT NULL,
	foreign key (ID_comp) references Trip (ID_comp) on delete cascade on update cascade 
)
