CREATE PROCEDURE SP_GetAllOrderDetails
(
	@UserId bigint
)
AS
BEGIN
	select * from OrderTable WHERE UserId=@UserId 
END