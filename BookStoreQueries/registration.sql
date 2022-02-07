use  BookStoreDB;

create procedure SP_AddNewUser
(
@FullName varchar(30),
@EmailId varchar(40),
@Password varchar(15),
@MobileNumber bigint
)
as 
begin 
	insert into UserTable values(@FullName,@EmailId,@Password,@MobileNumber)
End;

select * from UserTable;