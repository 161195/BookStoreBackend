CREATE PROCEDURE SP_ForgetPassword
(
	@EmailId varchar(30)
)
AS
BEGIN
	SELECT UserId,EmailId FROM UserTable WHERE EmailId=@EmailId 
END;