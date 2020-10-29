


create table [Case]
(
	caseID int not null IDENTITY(1,1) PRIMARY KEY,
	serialnumber varchar(50) not null,
	department varchar(25),
	dateStart date,
	dateEnd date,
	status varchar(16),
	deviceName varchar(50),
	deviceType varchar(50),
	accessories varchar(50),
	
);

create table Log
(
	logID int not null IDENTITY(1,1) PRIMARY KEY,
	caseID int,
	employeeName varchar(50),
	description varchar(1000),
	foreign key (caseID) references [Case](caseID)
);