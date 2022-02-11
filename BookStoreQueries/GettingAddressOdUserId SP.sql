CREATE PROCEDURE SP_GetAddress
(
	@UserId bigint
)
AS
BEGIN
	select * from AddressTable WHERE UserId=@UserId 
END