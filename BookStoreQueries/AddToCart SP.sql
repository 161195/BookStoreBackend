use BookStoreDB;
create table FeedBackTable(
    FeedBackId bigint Identity(1,1) Not Null Primary Key,
    BookId bigint,
	FeedBack varchar(max),
	Ratings bigint,
	UserId bigint,

	FOREIGN KEY (BookId) REFERENCES Books(BookId) ON DELETE No Action,
	FOREIGN KEY (UserId) REFERENCES UserTable (UserId) ON DELETE No Action
); 