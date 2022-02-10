CREATE PROCEDURE SP_GetAllCartDetails(
	@UserId bigint
)
AS 
BEGIN
	SELECT 
		c.CartId,
		c.BookId,
		c.Quantity,
		c.UserId,
		b.BookName,
		b.BookAuthor,
		b.OriginalPrice,
		b.DiscountPrice,
		b.BookImage,
		b.BookDetails
	FROM [CartTable] AS c
	LEFT JOIN [Books] AS B ON c.BookId=B.BookId
	WHERE c.UserId=@UserId
END;

