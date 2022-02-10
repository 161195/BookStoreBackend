CREATE PROCEDURE SP_UpdateCart
	@CartId bigint,
	@UserId bigint,
	@Quantity bigint
AS 
BEGIN	
	UPDATE CartTable SET Quantity=@Quantity WHERE CartId=@CartId and UserId=@UserId 
END