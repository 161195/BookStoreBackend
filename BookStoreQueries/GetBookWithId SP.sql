Create Procedure spGetBookWithBookId(
	@BookId bigint,
	@UserId bigint
)
As
Begin
	select *from Books where BookId=@BookId and UserId=@UserId
End