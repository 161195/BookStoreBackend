Create Procedure SP_AddressAdd(
    @TypeId bigint,
	@FullName varchar(150),
    @FullAddress varchar(max),
    @City varchar(255),
    @State varchar(255),
    @UserId bigint
)
As 
Begin
   Insert into AddressTable (TypeId,FullName,FullAddress,City,State,UserId)
   Values (@TypeId,@FullName,@FullAddress,@City,@State,@UserId)
End