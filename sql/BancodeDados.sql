-- CREATE DATABASE Elencar;

-- EXEMPLO PARA CRIAR TABELA JA COM A PK
/* 
CREATE TABLE Production.TransactionHistoryArchive1
   (
      TransactionID int IDENTITY (1,1) NOT NULL
      , CONSTRAINT PK_nometabela_nomecampo PRIMARY KEY CLUSTERED (TransactionID)
   )
;
*/

-- ADICIONAR PK EM UMA TABELA JA EXISTENTE
/* ALTER TABLE [dbo].[Actor]
ADD CONSTRAINT PK_Actor_Id PRIMARY KEY CLUSTERED (Id);
*/

-- APAGAR TABELA
-- drop  TABLE [dbo].[Actor]


/* use Elencar;

CREATE TABLE [dbo].[User](
    [Id] [int] IDENTITY (1,1) NOT NULL,
    [Name] [varchar] (255) NOT NULL,
    [Email] [varchar] (255) NOT NULL,
    [Password] [varchar] (25) NOT NULL,
    [Status] [varchar] (20) NOT NULL,
    [isProducer] [varchar] (20) NOT NULL,
    [CreatedAt] [datetime] DEFAULT getdate(),
    [UpdatedAt] [datetime],
 
)

ALTER TABLE [dbo].[User]
ADD CONSTRAINT PK_User_Id PRIMARY KEY CLUSTERED (Id);
*/

/*CREATE TABLE [dbo].[Genre](
    [Id] [int] IDENTITY (1,1) NOT NULL,
    [Description] [varchar] (255) NOT NULL,
    [CreatedAt] [datetime] DEFAULT getdate(),
    CONSTRAINT PK_Genre_Id PRIMARY KEY CLUSTERED (Id)
)
*/

/*CREATE TABLE [dbo].[Profile](
    [Id] [int] IDENTITY (1,1) NOT NULL,
    [Bio] [varchar] (255) NOT NULL,
    [Fee] [decimal] (10,2) NOT NULL,
    [Status] [varchar] (20) NOT NULL,
    [CreatedAt] [datetime] DEFAULT getdate(),
    [UpdatedAt] [datetime],
    [User_Id] [int] NOT NULL,
    [Genre_Id] [int] NOT NULL,
    CONSTRAINT PK_Profile_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Profile_User FOREIGN KEY (User_Id) REFERENCES [dbo].[User] (Id),
    CONSTRAINT FK_Profile_Genre FOREIGN KEY (Genre_Id) REFERENCES [dbo].[Genre] (Id)
)
*/

/*CREATE TABLE [dbo].[Reservation](
    [Id] [int] IDENTITY (1,1) NOT NULL,
    [StartAt] [datetime] NOT NULL,
    [EndAt] [datetime] NOT NULL,
    [ReservationName] [varchar] (255) NOT NULL,
    [Genre_Id] [int] NOT NULL,
    [CreatedAt] [datetime] DEFAULT getdate(),
    [UpdatedAt] [datetime],
    CONSTRAINT PK_Reservation_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Reservation_Genre FOREIGN KEY (Genre_Id) REFERENCES [dbo].[Genre] (Id)
)
*/

/*CREATE TABLE [dbo].[UserReservation](
    [Id] [int] IDENTITY (1,1) NOT NULL,
    [Reservation_Id] [int] NOT NULL,
    [Producer_Id] [int] NOT NULL,
    [Profile_Id] [int] NOT NULL,
    CONSTRAINT PK_UserReservation_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_UserReservation_Reservation FOREIGN KEY (Reservation_Id) REFERENCES [dbo].[Reservation] (Id),
    CONSTRAINT FK_UserReservation_User FOREIGN KEY (Producer_Id) REFERENCES [dbo].[User] (Id),
    CONSTRAINT FK_UserReservation_Profile FOREIGN KEY (Profile_Id) REFERENCES [dbo].[Profile] (Id)
)
*/