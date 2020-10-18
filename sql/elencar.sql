CREATE TABLE [Actor] (
  [id] [int] IDENTITY (1,1) NOT NULL,
  [userId] int NOT NULL,
  [bio] varchar (255) NOT NULL,
  [fee] decimal (10,2) NOT NULL,
  [status] bit DEFAULT 1,
  [createdAt] [datetime] DEFAULT getdate(),
  [updatedAt] [datetime] DEFAULT getdate(),
  CONSTRAINT PK_Actor_id PRIMARY KEY CLUSTERED (id),
  CONSTRAINT FK_Actor_User FOREIGN KEY (userId) REFERENCES [dbo].[User] (id),
);

CREATE TABLE [User] (
  [id] [int] IDENTITY (1,1) NOT NULL,
  [name] varchar (255) NOT NULL,
  [email] varchar (255) NOT NULL,
  [password] varchar (255) NOT NULL,
  [status] bit DEFAULT 1,
  [roleId] int NOT NULL,
  [createdAt] [datetime] DEFAULT getdate(),
  [updatedAt] [datetime] DEFAULT getdate(),
  CONSTRAINT PK_User_id PRIMARY KEY CLUSTERED (id),
  CONSTRAINT FK_User_Role FOREIGN KEY (roleId) REFERENCES [dbo].[Role] (id)
);

CREATE TABLE [ActorGenre] (
  [genreId] int NOT NULL,
  [actorId] int NOT NULL,
  CONSTRAINT FK_ActorGenre_Genre FOREIGN KEY (genreId) REFERENCES [dbo].[Genre] (id),
  CONSTRAINT FK_ActorGenre_Actor FOREIGN KEY (actorId) REFERENCES [dbo].[Acto] (id)
);

CREATE TABLE [Genre] (
  [id] [int] IDENTITY (1,1) NOT NULL,
  [name] varchar (255) NOT NULL,
  [description] varchar (255) NOT NULL,
  CONSTRAINT PK_Genre_id PRIMARY KEY CLUSTERED (id),
);

CREATE TABLE [Role] (
  [id] [int] IDENTITY (1,1) NOT NULL,
  [name] varchar (255) NOT NULL,
  [createdAt] [datetime] DEFAULT getdate(),
  CONSTRAINT PK_Role_id PRIMARY KEY CLUSTERED (id),
);

CREATE TABLE [Reservation] (
  [id] [int] IDENTITY (1,1) NOT NULL,
  [startAt] datetime,
  [endAt] datetime,
  [name] varchar (255) NOT NULL,
  [genreId] int NOT NULL,
  [createdAt] datetime,
  [updatedAt] datetime,
  CONSTRAINT PK_Reservation_id PRIMARY KEY CLUSTERED (id),
  CONSTRAINT FK_Reservation_Genre FOREIGN KEY (genreId) REFERENCES [dbo].[Genre] (id)
);

CREATE TABLE [UserReservation] (
  [reservationId] int NOT NULL,
  [producerId] int NOT NULL,
  [actorId] int NOT NULL,
  CONSTRAINT FK_UserReservation_Reservation FOREIGN KEY (reservationId) REFERENCES [dbo].[Reservation] (id),
  CONSTRAINT FK_UserReservation_User FOREIGN KEY (producerId) REFERENCES [dbo].[User] (id),
  CONSTRAINT FK_UserReservation_Actor FOREIGN KEY (actorId) REFERENCES [dbo].[Acto] (id)
);

