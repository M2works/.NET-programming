
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/02/2017 03:00:34
-- Generated from EDMX file: C:\Users\mcmomot96\Desktop\Doktor_i_Pacjent_2017_XD_Pro\App\SurgeryModel.edmx
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
IF OBJECT_ID(N'[dbo].[FK_DoctorPatient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Patients] DROP CONSTRAINT [FK_DoctorPatient];
GO
IF OBJECT_ID(N'[dbo].[FK_DoctorVisitTime]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timetables] DROP CONSTRAINT [FK_DoctorVisitTime];
GO
IF OBJECT_ID(N'[dbo].[FK_FamilyDoctor_inherits_Doctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Doctors_FamilyDoctor] DROP CONSTRAINT [FK_FamilyDoctor_inherits_Doctor];
GO
IF OBJECT_ID(N'[dbo].[FK_Specialist_inherits_Doctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Doctors_Specialist] DROP CONSTRAINT [FK_Specialist_inherits_Doctor];
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
IF OBJECT_ID(N'[dbo].[Doctors_FamilyDoctor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctors_FamilyDoctor];
GO
IF OBJECT_ID(N'[dbo].[Doctors_Specialist]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctors_Specialist];
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
    [Address_HomeNumber] int  NOT NULL,
    [Address_PostalCode] int  NOT NULL,
    [FamilyDoctorId] int  NOT NULL,
    [DoctorId] int  NOT NULL
);
GO

-- Creating table 'Doctors'
CREATE TABLE [dbo].[Doctors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [ClinicId] int  NOT NULL
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
    [Address_HomeNumber] int  NOT NULL,
    [Address_PostalCode] int  NOT NULL
);
GO

-- Creating table 'Timetables'
CREATE TABLE [dbo].[Timetables] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Day] int  NOT NULL,
    [TimeOfTheVisit_Country] nvarchar(max)  NOT NULL,
    [TimeOfTheVisit_City] nvarchar(max)  NOT NULL,
    [TimeOfTheVisit_Street] nvarchar(max)  NOT NULL,
    [TimeOfTheVisit_StreetNumber] int  NOT NULL,
    [TimeOfTheVisit_HomeNumber] int  NOT NULL,
    [TimeOfTheVisit_PostalCode] int  NOT NULL,
    [DoctorId] int  NOT NULL
);
GO

-- Creating table 'Doctors_FamilyDoctor'
CREATE TABLE [dbo].[Doctors_FamilyDoctor] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'Doctors_Specialist'
CREATE TABLE [dbo].[Doctors_Specialist] (
    [Specialization] int  NOT NULL,
    [VisitPrice] int  NOT NULL,
    [Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'Doctors_FamilyDoctor'
ALTER TABLE [dbo].[Doctors_FamilyDoctor]
ADD CONSTRAINT [PK_Doctors_FamilyDoctor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Doctors_Specialist'
ALTER TABLE [dbo].[Doctors_Specialist]
ADD CONSTRAINT [PK_Doctors_Specialist]
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

-- Creating foreign key on [DoctorId] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [FK_DoctorPatient]
    FOREIGN KEY ([DoctorId])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorPatient'
CREATE INDEX [IX_FK_DoctorPatient]
ON [dbo].[Patients]
    ([DoctorId]);
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

-- Creating foreign key on [Id] in table 'Doctors_FamilyDoctor'
ALTER TABLE [dbo].[Doctors_FamilyDoctor]
ADD CONSTRAINT [FK_FamilyDoctor_inherits_Doctor]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Doctors_Specialist'
ALTER TABLE [dbo].[Doctors_Specialist]
ADD CONSTRAINT [FK_Specialist_inherits_Doctor]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------