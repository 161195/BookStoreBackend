CREATE PROCEDURE SP_DeleteBookFromWishListwithId
(
	@WishListId bigint,
	@UserId bigint
)
AS
BEGIN
	Delete from WishListTable WHERE WishListId=@WishListId and UserId=@UserId 
END