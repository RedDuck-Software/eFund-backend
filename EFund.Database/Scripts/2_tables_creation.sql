CREATE TABLE [networks] ( 
  [ChainId] SMALLINT NOT NULL PRIMARY KEY,
  [PlatformContractAddress] BINARY(20) NOT NULL 
)

CREATE TABLE [hedgefund_infos] (
  [ContractAddress] BINARY(20) NOT NULL PRIMARY KEY,
  [ChainId] SMALLINT NOT NULL FOREIGN KEY REFERENCES networks([ChainId]),
  [Name] VARCHAR(255) NOT NULL,
  [Description] VARCHAR(255) NULL, 
  [ImageUrl] VARCHAR(100) NULL, 
);

create Table [users] (
	[Address] BINARY(20) NOT NULL PRIMARY KEY, 
	[ChainId] SMALLINT NOT NULL FOREIGN KEY REFERENCES networks([ChainId]),
	[SignNonce] VARCHAR(40) NOT NULL,
	[Username] VARCHAR(255) NULL,
	[ImageUrl] VARCHAR(100) NULL, 
	[Description] VARCHAR(100) NULL, 
)

CREATE TABLE images (
    Id VARCHAR(255) PRIMARY KEY NOT NULL,
    Image VARCHAR(MAX) NOT NULL
)