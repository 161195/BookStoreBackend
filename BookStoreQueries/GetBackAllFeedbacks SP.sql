CREATE PROCEDURE SP_GetAllFeedback(
	@BookId bigint,
	@UserId bigint
)
AS 
BEGIN
	SELECT *From FeedBackTable WHERE UserId=@UserId and BookId=@BookId
END;