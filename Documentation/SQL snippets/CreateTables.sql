
create table Item(
serialnumber int primary key,
devicename varchar(50),
devicetype varchar(50),
accessories varchar(50),
);

create table [Case]
(
caseID int primary key,
serialnumber int not null,
department varchar(25),
description varchar(1000),
dateStart date,
dateEnd date,
status varchar(16)
foreign key (serialnumber) references Item(serialnumber)
);



create table Cases
(
casesID int primary key,
caseID int,
foreign key (caseID) references [Case](caseID)
);