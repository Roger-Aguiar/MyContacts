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

INSERT INTO Contacts(FullName, Nickname, PhoneNumber, Email)
VALUES('Roger Aguiar', 'Software Developer', '(31) 98345-3069', 'rogerdaviola@yahoo.com.br');

SELECT FullName, Nickname, PhoneNumber, Email 
FROM Contacts
ORDER BY FullName;

SELECT * FROM Contacts;

DROP TABLE Contacts;