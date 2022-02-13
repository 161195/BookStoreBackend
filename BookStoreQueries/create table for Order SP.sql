use BookStoreDB;
create table OrderTable
(
	 OrderId int not null identity (1,1) primary key,
     BookId bigint,
	 UserId bigint,
     AddressId bigint,
	 Price bigint,
	 Quantity bigint
     FOREIGN KEY (BookId) REFERENCES Books(BookId) ON DELETE No Action,
	 FOREIGN KEY (UserId) REFERENCES UserTable (UserId) ON DELETE No Action,
	 FOREIGN KEY (AddressId) REFERENCES AddressTable (AddressId) ON DELETE No Action
       				
);