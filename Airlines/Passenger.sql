create table [dbo].[Passenger]
(
	[ID_psg] int NOT NULL primary key, 
    [name] char(20) NOT NULL,
	foreign key (ID_psg) references Pass_in_trip(ID_psg) on update cascade on delete cascade
)
