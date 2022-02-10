CREATE PROCEDURE SP_DeleteBookFromCartwithCartId
(
	@CartId bigint,
	@UserId bigint
)
AS
BEGIN
	Delete from CartTable WHERE CartId=@CartId and UserId=@UserId 
END