CREATE TABLE [dbo].[BOOK]
(
	[Название_книги] text PRIMARY KEY NOT NULL,
	[Номер] int,
	[Количество] int,
	[Автор] text NOT NULL,
	[Год_издания] int NOT NULL
)
