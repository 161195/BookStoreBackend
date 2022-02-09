Create Procedure spRatingsUpdate(
	@BookId bigint,
	@TotalRating bigint,
	@NoOfPeopleRated bigint,
	@UserId bigint
)
As
Begin
	UPDATE Books set TotalRating=@TotalRating,NoOfPeopleRated=@NoOfPeopleRated where BookId=@BookId and UserId=@UserId
End