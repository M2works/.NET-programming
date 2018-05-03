
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/13/2017 18:33:21
-- Generated from EDMX file: C:\Users\mcmomot96\Desktop\Doktor_i_Pacjent_2017_XD_Pro\Doktor_i_Pacjent_2017_XD_Pro\DBConnectionLogic\SurgeryModel\SurgeryModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ClinicDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClinicDoctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Doctors] DROP CONSTRAINT [FK_ClinicDoctor];
GO
IF OBJECT_ID(N'[dbo].[FK_DoctorVisitTime]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timetables] DROP CONSTRAINT [FK_DoctorVisitTime];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientVisitTime]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timetables] DROP CONSTRAINT [FK_PatientVisitTime];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Patients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patients];
GO
IF OBJECT_ID(N'[dbo].[Doctors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctors];
GO
IF OBJECT_ID(N'[dbo].[Clinics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clinics];
GO
IF OBJECT_ID(N'[dbo].[Timetables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Timetables];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [PESELNumber] nvarchar(max)  NOT NULL,
    [Address_Country] nvarchar(max)  NOT NULL,
    [Address_City] nvarchar(max)  NOT NULL,
    [Address_Street] nvarchar(max)  NOT NULL,
    [Address_StreetNumber] int  NOT NULL,
    [Address_HomeNumber] int  NULL,
    [Address_PostalCode] int  NOT NULL
);
GO

-- Creating table 'Doctors'
CREATE TABLE [dbo].[Doctors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [ClinicId] int  NOT NULL,
    [Specialization] int  NOT NULL,
    [VisitPrice] int  NULL,
    [LicenceNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Clinics'
CREATE TABLE [dbo].[Clinics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address_Country] nvarchar(max)  NOT NULL,
    [Address_City] nvarchar(max)  NOT NULL,
    [Address_Street] nvarchar(max)  NOT NULL,
    [Address_StreetNumber] int  NOT NULL,
    [Address_HomeNumber] int  NULL,
    [Address_PostalCode] int  NOT NULL
);
GO

-- Creating table 'Timetables'
CREATE TABLE [dbo].[Timetables] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [DoctorId] int  NOT NULL,
    [PatientId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Doctors'
ALTER TABLE [dbo].[Doctors]
ADD CONSTRAINT [PK_Doctors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Clinics'
ALTER TABLE [dbo].[Clinics]
ADD CONSTRAINT [PK_Clinics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Timetables'
ALTER TABLE [dbo].[Timetables]
ADD CONSTRAINT [PK_Timetables]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClinicId] in table 'Doctors'
ALTER TABLE [dbo].[Doctors]
ADD CONSTRAINT [FK_ClinicDoctor]
    FOREIGN KEY ([ClinicId])
    REFERENCES [dbo].[Clinics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClinicDoctor'
CREATE INDEX [IX_FK_ClinicDoctor]
ON [dbo].[Doctors]
    ([ClinicId]);
GO

-- Creating foreign key on [DoctorId] in table 'Timetables'
ALTER TABLE [dbo].[Timetables]
ADD CONSTRAINT [FK_DoctorVisitTime]
    FOREIGN KEY ([DoctorId])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorVisitTime'
CREATE INDEX [IX_FK_DoctorVisitTime]
ON [dbo].[Timetables]
    ([DoctorId]);
GO

-- Creating foreign key on [PatientId] in table 'Timetables'
ALTER TABLE [dbo].[Timetables]
ADD CONSTRAINT [FK_PatientVisitTime]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientVisitTime'
CREATE INDEX [IX_FK_PatientVisitTime]
ON [dbo].[Timetables]
    ([PatientId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------