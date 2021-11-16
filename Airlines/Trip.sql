create table [dbo].[Trip]
(
	[trip_no] int NOT NULL, 
    [ID_comp] int NOT NULL, 
    [plane] char(10) NOT NULL,
    [town_from] char(25) NOT NULL, 
    [town_to] char(25) NOT NULL,
    [time_out] datetime NOT NULL,
    [time_in] datetime NOT NULL,
    foreign key (trip_no) references Pass_in_trip(trip_no) on update cascade on delete cascade
)
