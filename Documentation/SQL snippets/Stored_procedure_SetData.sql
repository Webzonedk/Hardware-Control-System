-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SetData 
	@caseID int,
	@serialNumber varchar(50),
	@department varchar (50),
	@dateStart date,
	@dateEnd date,
	@status varchar(50),
	@deviceName varchar(50),
	@deviceType varchar(50),
	@accessories varchar(50),
	@employeeName varchar(50),
	@description varchar (1000)

AS
BEGIN
	update [case]
	set serialnumber = @serialNumber,
	department = @department,
	dateStart = @dateStart,
	dateEnd = @dateEnd,
	status = @status,
	deviceName =@deviceName,
	deviceType =@deviceType,
	accessories =@accessories
	where caseID = @caseID;


 
	update log
	set employeeName = @employeeName,
	description = @description
	from log 
	join [Case] ON Log.caseID = [case].caseID
	where Log.caseID = [Case].caseID;




END
GO
