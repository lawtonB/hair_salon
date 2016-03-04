USE [hair_salon_test]
GO
/****** Object:  Table [dbo].[clients]    Script Date: 3/3/2016 4:12:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[stylist_id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stylists]    Script Date: 3/3/2016 4:12:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stylists](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[clients] ON 

INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (1, N'david', NULL)
INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (2, N'patrick', NULL)
INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (3, N'patrick', NULL)
INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (4, N'patrick', NULL)
INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (5, N'', NULL)
INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (6, N'barbara', NULL)
INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (7, N'barbara', NULL)
INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (8, N'', NULL)
INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (9, N'', NULL)
INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (10, N'', NULL)
INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (11, N'larry', NULL)
INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (12, N'werqewr', NULL)
SET IDENTITY_INSERT [dbo].[clients] OFF
SET IDENTITY_INSERT [dbo].[stylists] ON 

INSERT [dbo].[stylists] ([id], [name]) VALUES (10, N'jerry')
INSERT [dbo].[stylists] ([id], [name]) VALUES (11, N'lawton')
SET IDENTITY_INSERT [dbo].[stylists] OFF
