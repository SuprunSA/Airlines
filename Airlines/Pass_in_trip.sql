create table [dbo].[Pass_in_trip]
(
	[trip_no] int NOT NULL, 
    [date] DATETIME2 NOT NULL, 
    [ID_psg] int NOT NULL, 
    [place] char(10) NOT NULL,
    primary key (trip_no, [date], ID_psg),
    foreign key (trip_no) references Trip(trip_no)
    on update cascade 
    on delete cascade,
    foreign key (ID_psg) references Passenger(ID_psg) 
    on update cascade 
    on delete cascade
)
