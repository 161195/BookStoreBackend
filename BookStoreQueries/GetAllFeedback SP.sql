 /****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [UserId]
      ,[FullName]
      ,[EmailId]
      ,[Password]
      ,[MobileNumber]
  FROM [BookStoreDB].[dbo].[UserTable]