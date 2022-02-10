Create Procedure SP_AddToCart(
@BookId bigint,
@UserId bigint,
@Quantity bigint
)
AS
BEGIN
	INSERT INTO CartTable (BookId, UserId,Quantity) VALUES (@BookId, @UserId,@Quantity)
END;