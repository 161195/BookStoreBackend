Create procedure SP_OrderPlaced(
	@BookId bigint,
	@UserId bigint,
	@AddressId bigint,
	@Quantity bigint,
	@order bigint =NULL OUTPUT
)
As
Begin
		if exists(select * from[Books] where BookId = @BookId and BookQuantity < @Quantity)
		Begin
			set @order = 3;
		End
		else if exists(select * from[Books] where BookId = @BookId and BookQuantity >= @Quantity)
		Begin
			Declare @unitPrice bigint;
			Declare @available bigint;
			select @unitPrice = DiscountPrice, @available = BookQuantity from [Books] where BookId = @BookId;

			insert into [OrderTable] (UserId, AddressId, BookId, Price, Quantity)
			values(@UserId, @AddressId, @BookId, (@unitPrice*@Quantity), @Quantity);

			Update [Books]
			set
				BookQuantity = (@available - @Quantity)
			where
				BookId = @BookId;

			delete from [CartTable] where BookId = @BookId AND UserId = @UserId;
			set @order = 2;
		end
		else
			SET @order = 1;
END