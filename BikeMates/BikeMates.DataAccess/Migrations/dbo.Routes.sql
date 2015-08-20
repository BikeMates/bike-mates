CREATE TABLE [dbo].[Routes] (
    [Id]      INT            IDENTITY (1, 1) NULL,
    [User_Id] NVARCHAR (128) NULL,
    [Name] CHAR(20) NULL, 
    [Meeting_Place] CHAR(40) NULL, 
    [Distance] INT NULL, 
    [Description] NCHAR(128) NULL, 
    [Start] DATE NULL, 
    [ParticipantsCount] INT NULL, 
    CONSTRAINT [PK_dbo.Routes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Routes_dbo.AspNetUsers_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_User_Id]
    ON [dbo].[Routes]([User_Id] ASC);

