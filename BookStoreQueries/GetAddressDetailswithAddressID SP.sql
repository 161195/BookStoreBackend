CREATE PROCEDURE SP_UpdateAddress
	@AddressId bigint,
	@TypeId bigint,
	@FullName varchar(150),
    @FullAddress varchar(max),
    @City varchar(255),
    @State varchar(255),
	@UserId bigint
AS 
BEGIN	
	UPDATE AddressTable SET TypeId=@TypeId, FullName=@FullName, FullAddress=@FullAddress, City=@City, State=@State
	 WHERE AddressId=@AddressId and UserId=@UserId 
END