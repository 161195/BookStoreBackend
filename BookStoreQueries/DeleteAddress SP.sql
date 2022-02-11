Create Procedure SP_DeleteAddressWithAddressId(  
    @AddressId bigint,
	@UserId bigint  
)
As 
Begin
   Delete from AddressTable where AddressId=@AddressId  and UserId=@UserId
End