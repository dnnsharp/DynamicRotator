-- drop foreign keys
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'{databaseOwner}[FK_{objectQualifier}avtTabsPro_Tabs_{objectQualifier}Modules]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE {databaseOwner}[{objectQualifier}avtTabsPro_Tabs] DROP CONSTRAINT [FK_{objectQualifier}avtTabsPro_Tabs_{objectQualifier}Modules]
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'{databaseOwner}[FK_{objectQualifier}avtTabsPro_TabMods_{objectQualifier}avtTabsPro_Tabs]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE {databaseOwner}[{objectQualifier}avtTabsPro_TabMods] DROP CONSTRAINT [FK_{objectQualifier}avtTabsPro_TabMods_{objectQualifier}avtTabsPro_Tabs]
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'{databaseOwner}[FK_{objectQualifier}avtTabsPro_TabLocalization_{objectQualifier}avtTabsPro_Tabs]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE {databaseOwner}[{objectQualifier}avtTabsPro_TabLocalization] DROP CONSTRAINT [FK_{objectQualifier}avtTabsPro_TabLocalization_{objectQualifier}avtTabsPro_Tabs]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'{databaseOwner}[{objectQualifier}avtTabsPro_Tabs]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
    DROP TABLE {databaseOwner}[{objectQualifier}avtTabsPro_Tabs]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'{databaseOwner}[{objectQualifier}avtTabsPro_TabMods]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
    DROP TABLE {databaseOwner}[{objectQualifier}avtTabsPro_TabMods]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'{databaseOwner}[{objectQualifier}avtTabsPro_TabLocalization]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
    DROP TABLE {databaseOwner}[{objectQualifier}avtTabsPro_TabLocalization]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'{databaseOwner}[{objectQualifier}avtTabsPro_Activations]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
    DROP TABLE {databaseOwner}[{objectQualifier}avtTabsPro_Activations]
GO

