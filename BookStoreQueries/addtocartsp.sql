Create Procedure SP_AddToCart(
    @BookId bigint,
    @Quantity bigint,
    @UserId bigint
)
As 
Begin
   Insert into CartTable(BookId,Quantity,UserId)
   Values (@BookId,@Quantity,@UserId)
End