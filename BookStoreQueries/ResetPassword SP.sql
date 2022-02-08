CREATE PROCEDURE SP_ResetPassword
(
	@EmailId varchar(30),
	@NewPassword varchar(30)
)
AS
BEGIN
	 UPDATE UserTable set Password=@NewPassword where EmailId=@EmailId
END