CREATE PROCEDURE SP_DeleteBookWithBookId
(
	@BookId bigint,
	@UserId bigint
)
AS
BEGIN
	Delete from Books WHERE BookId=@BookId and UserId=@UserId 
END