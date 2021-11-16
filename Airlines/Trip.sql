create table [dbo].[Trip]
(
	[trip_no] int NOT NULL, 
    [ID_comp] int NOT NULL, 
    [plane] char(10) NOT NULL,
    [town_from] char(25) NOT NULL, 
    [town_to] char(25) NOT NULL,
    [time_out] DATETIME2 NOT NULL,
    [time_in] DATETIME2 NOT NULL,
    primary key (trip_no),
    foreign key (ID_comp) references Company on update cascade on delete cascade
)
