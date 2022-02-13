Create Procedure SP_GetOrderDetailsWithBookId(  
    @BookId bigint,
	@UserId bigint  
)
As 
Begin
   Select * from OrderTable where BookId=@BookId and UserId=@UserId
End