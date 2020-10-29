
insert into [Case](serialnumber,department,dateStart,dateEnd,status,deviceName,deviceType,accessories) 
values('0003zz','it','2020/10/29','2020/10/29','ok','lpv42','printer','usb cable');

insert into Log(caseID,employeeName,description)
values((select SCOPE_IDENTITY() from [Case]),'kent','printer stopped working');

alter table [case] 
alter column serialNumber varchar(50);


select * from Log;
select * from [Case];


--truncate table log;
--truncate table [Case];
delete from Log;
delete from [Case];
dbcc checkident([Case],reseed,0);
dbcc checkident(Log,reseed,0);
