CREATE DATABASE IF NOT EXISTS [efund];
USE [efund];


CREATE TABLE [network] ( 
  [ChainId] SMALLINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [PlatformContractAddress] BINARY(20) NOT NULL 
)

CREATE TABLE [hedgefund_infos] (
  [ContractId] BINARY(20) NOT NULL PRIMARY KEY,
  [ChainId] SMALLINT NOT NULL FOREIGN KEY REFERENCES network([ChainId]),
  [Name] VARCHAR(255) NOT NULL,
  [Description] VARCHAR(255) NOT NULL, 
  [ImageUrl] VARCHAR(100) NOT NULL, 
);

create Table [users] (
	[Address] BINARY(20) NOT NULL PRIMARY KEY, 
	[ChainId] SMALLINT NOT NULL FOREIGN KEY REFERENCES network([ChainId]),
	[SignNonce] VARCHAR(40) NOT NULL,
	[Username] VARCHAR(255) NULL,
	[ImageUrl] VARCHAR(100) NULL, 
	[Description] VARCHAR(100) NULL, 
)