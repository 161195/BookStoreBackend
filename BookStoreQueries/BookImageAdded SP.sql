Create Procedure spBookImageUpdate(
	@BookId bigint,
	@BookImage varchar(max),
	@UserId bigint
)
As
Begin
	UPDATE Books set BookImage=@BookImage where BookId=@BookId and UserId=@UserId
End