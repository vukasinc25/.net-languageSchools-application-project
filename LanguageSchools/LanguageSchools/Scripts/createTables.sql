DROP TABLE ProfessorsLanguages, SchoolsLanguages, Languages, Schools, SchoolClasses, Users


CREATE TABLE Users
(
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	email VARCHAR(50) UNIQUE NOT NULL,
	password VARCHAR(50) NOT NULL,
	firstName VARCHAR(50) NOT NULL,
	lastName VARCHAR(50) NOT NULL,
	jmbg VARCHAR(13) UNIQUE NOT NULL,
	gender VARCHAR(10) NOT NULL,
	userType VARCHAR(10) NOT NULL,
	street VARCHAR(50) NOT NULL,
	streetNumber VARCHAR(10) NOT NULL,
	city VARCHAR(50) NOT NULL,
	country VARCHAR (50) NOT NULL, 
	isActive BIT NOT NULL,
	schoolId INT NOT NULL
)

CREATE TABLE Schools
(
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
	street VARCHAR(50) NOT NULL,
	streetNumber VARCHAR(10) NOT NULL,
	city VARCHAR(50),
	country VARCHAR (50), 
	isActive BIT NOT NULL
)

CREATE TABLE SchoolClasses
(
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	date VARCHAR(50) NOT NULL,
	startTime VARCHAR(50) NOT NULL,
	duration VARCHAR(50) NOT NULL,
	state VARCHAR(50) NOT NULL,
	isActive BIT NOT NULL,
	professorId INT NOT NULL,
	studentId INT NOT NULL,
	FOREIGN KEY (ProfessorId) REFERENCES Users (id),
	FOREIGN KEY (StudentId) REFERENCES Users (id)
)

CREATE TABLE Languages
(
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
	isActive BIT NOT NULL
)

CREATE TABLE SchoolsLanguages
(
	schoolId INT NOT NULL,
	languageId INT NOT NULL,
	PRIMARY KEY (schoolId, languageId),
	FOREIGN KEY (schoolId) REFERENCES Schools (id),
	FOREIGN KEY (languageId) REFERENCES Languages (id)
)

CREATE TABLE ProfessorsLanguages
(
	professorId INT NOT NULL,
	languageId INT NOT NULL,
	PRIMARY KEY (professorId, languageId),
	FOREIGN KEY (professorId) REFERENCES Users (id),
	FOREIGN KEY (languageId) REFERENCES Languages (id)
)