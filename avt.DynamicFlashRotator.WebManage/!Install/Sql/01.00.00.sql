﻿

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------------------------------------------
------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'{databaseOwner}[{objectQualifier}avtRotator_Settings]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
    CREATE TABLE {databaseOwner}[{objectQualifier}avtRotator_Settings](
        [ControlId] [nvarchar](100) NOT NULL,
        [SettingName] [nvarchar](50) NOT NULL,
        [SettingValue] [ntext] NULL,
    CONSTRAINT [PK_{objectQualifier}avtRotator_Settings] PRIMARY KEY CLUSTERED 
    (
        [ControlId] ASC,
        [SettingName] ASC
    )
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

    GO

------------------------------------------------------------------------------------
------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'{databaseOwner}[{objectQualifier}avtRotator_Slides]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
    CREATE TABLE {databaseOwner}[{objectQualifier}avtRotator_Slides](
        [SlideId] [int] IDENTITY(1,1) NOT NULL,
        [ControlId] [nvarchar](100) NOT NULL,
        [Title] [nvarchar](100) NULL,
        [DurationSeconds] [int] NULL,
        [BackgroundGradientFrom] [nvarchar](30) NULL,
        [BackgroundGradientTo] [nvarchar](30) NULL,
        [Link_Url] [nvarchar](1024) NULL,
        [Link_Caption] [nvarchar](255) NULL,
        [Link_Target] [nvarchar](30) NULL,
        [Link_ClickAnywhere] [int] NULL,
        [Link_UseTextsBackground] [int] NULL,
        [Mp3_Url] [nvarchar](1024) NULL,
        [Mp3_ShowPlayer] [int] NULL,
        [Mp3_IconColor] [nvarchar](30) NULL,
        [ViewOrder] [int] NULL,
     CONSTRAINT [PK_{objectQualifier}avtRotator_Slides] PRIMARY KEY CLUSTERED 
    (
        [SlideId] ASC
    )
    ) ON [PRIMARY]

    GO


------------------------------------------------------------------------------------
------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'{databaseOwner}[{objectQualifier}avtRotator_SlideObjects]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
    CREATE TABLE {databaseOwner}[{objectQualifier}avtRotator_SlideObjects](
        [ObjectId] [int] IDENTITY(1,1) NOT NULL,
        [SlideId] [int] NOT NULL,
        [ObjectType] [nvarchar](20) NOT NULL,
        [Name] [nvarchar](50) NULL,
        [LinkUrl] [nvarchar](1024) NULL,
        [Text] [ntext] NULL,
        [ResourceUrl] [nvarchar](1024) NULL,
        [DelaySeconds] [int] NULL,
        [DurationSeconds] [int] NULL,
        [Opacity] [int] NULL,
        [PositionX] [int] NULL,
        [PositionY] [int] NULL,
        [VerticalAlign] [nvarchar](50) NULL,
        [GlowSize] [int] NULL,
        [GlowStrength] [int] NULL,
        [GlowColor] [nvarchar](50) NULL,
        [AppearMode] [nvarchar](50) NULL,
        [SlideFrom] [nvarchar](50) NULL,
        [SlideMoveType] [nvarchar](50) NULL,
        [SlideEasingType] [nvarchar](50) NULL,
        [EffectAfterSlide] [nvarchar](50) NULL,
        [TextColor] [nvarchar](50) NULL,
        [TextBackgroundColor] [nvarchar](50) NULL,
        [TextBackgroundOpacity] [int] NULL,
        [TextBackgroundPadding] [int] NULL,
        [ViewOrder] [int] NULL,
     CONSTRAINT [PK_{objectQualifier}avtRotator_SlideObjects] PRIMARY KEY CLUSTERED 
    (
        [ObjectId] ASC
    )
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


ALTER TABLE {databaseOwner}[{objectQualifier}avtRotator_SlideObjects]  WITH CHECK ADD  CONSTRAINT [FK_{objectQualifier}avtRotator_SlideObjects_{objectQualifier}avtRotator_Slides] FOREIGN KEY([SlideId])
REFERENCES {databaseOwner}[{objectQualifier}avtRotator_Slides] ([SlideId])
ON DELETE CASCADE
GO

ALTER TABLE {databaseOwner}[{objectQualifier}avtRotator_SlideObjects] CHECK CONSTRAINT [FK_{objectQualifier}avtRotator_SlideObjects_{objectQualifier}avtRotator_Slides]
GO


------------------------------------------------------------------------------------
------------------------------------------------------------------------------------


------------------------------------------------------------------------------------
------------------------------------------------------------------------------------


------------------------------------------------------------------------------------
------------------------------------------------------------------------------------
