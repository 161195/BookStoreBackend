Create Procedure SP_AddToWishList(
    @BookId bigint,
    @UserId bigint
)
As 
Begin
   Insert into WishListTable Values (@BookId,@UserId)
End