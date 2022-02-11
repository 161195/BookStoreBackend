CREATE PROCEDURE SP_GetAllWishList(
	@UserId bigint
)
AS 
BEGIN
	SELECT 
		c.WishListId,
		c.BookId,
		c.UserId,
		b.BookName,
		b.BookAuthor,
		b.OriginalPrice,
		b.DiscountPrice,
		b.BookImage,
		b.BookDetails
	FROM [WishListTable] AS c
	LEFT JOIN [Books] AS B ON c.BookId=B.BookId
	WHERE c.UserId=@UserId
END;