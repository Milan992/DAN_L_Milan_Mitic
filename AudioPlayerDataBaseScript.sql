IF DB_ID('AudioPlayer') IS NULL
CREATE DATABASE AudioPlayer
GO
USE AudioPlayer;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblSong')
drop table tblSong;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblUser')
drop table tblUser;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblPlayedSong')
drop table tblPlayedSong;


create table tblUser(
UserID int identity(1,1) primary key,
UserName varchar(30) check(len(UserName) > 6) not null unique,
Pass varchar(30) check(len(Pass) > 6) not null
)

create table tblSong(
SongID int identity(1,1) primary key,
UserID int foreign key (UserID) references tblUser(UserID),
SongName varchar(30) not null,
Author varchar(30),
DurationInSeconds int not null 
)

create table tblPlayedSong(
SongID int identity(1,1) primary key,
SongName varchar(30) not null,
Author varchar(30),
DurationInSeconds int not null 
)
