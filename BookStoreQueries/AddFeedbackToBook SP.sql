Create Procedure SP_AddFeedback(
    @BookId bigint,
	@FeedBack varchar(Max),
    @Ratings bigint,
    @UserId bigint
)
As 
Begin
   Insert into FeedBackTable(BookId,FeedBack,Ratings,UserId)
   Values (@BookId,@FeedBack,@Ratings,@UserId)
End