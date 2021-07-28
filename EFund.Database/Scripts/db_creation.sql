CREATE DATABASE IF NOT EXISTS [efund];
USE [efund];


CREATE TABLE [network] ( 
  [NetworkId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [PlatformContractAddress] BINARY(20) NOT NULL 
)

CREATE TABLE [hedgefund_infos] (
  [ContractId] BINARY(20) NOT NULL PRIMARY KEY,
  [Name] VARCHAR(255) NOT NULL,
  [Description] VARCHAR(255) NOT NULL, 
  [ImgUrl] VARCHAR(100) NOT NULL, 
);

create Table [Users] (
	[UserAddress] BINARY(20) NOT NULL PRIMARY KEY, 
	[Nonce] VARCHAR(40) NOT NULL,
	[Name] VARCHAR(255) NULL,
	[ImgUrl] VARCHAR(100) NULL, 
)