CREATE DATABASE MyContacts;

USE MyContacts;

CREATE TABLE Contacts
(
	IdContact   INT          NOT NULL IDENTITY(1,1),
	FullName    VARCHAR(100) NOT NULL,
	Nickname    VARCHAR(100),
	PhoneNumber VARCHAR(15)  NOT NULL,
	Email       VARCHAR(100),
	PRIMARY KEY(idContact)
);