CREATE PROCEDURE SP_Login
(
	@EmailId varchar(30),
	@Password varchar(30)
)
AS
BEGIN
	SELECT UserId,EmailId,Password FROM UserTable WHERE EmailId=@EmailId AND Password=@Password
END;