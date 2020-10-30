
select * from Log
join [Case] on Log.caseID = [Case].caseID
where [Case].serialnumber = '0003zz';

exec GetData @serialNumber ='0003zz'