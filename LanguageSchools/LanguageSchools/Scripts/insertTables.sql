INSERT INTO Schools (name, street, streetNumber, city, country, isActive)
	VALUES ('Oxford', 'Main St', '6', 'Brighton', 'United Kingdom', 1);

INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('test@mail.com', '123', 'John', 'Doe', '123456789', 'MALE', 'STUDENT', 'Main St', '5', 'London', 'United Kingdom', 1, 1); 

INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('tests@mail.com', '123', 'JohnAA', 'DoeSS', '12345678', 'MALE', 'PROFESSOR', 'Main St', '5', 'London', 'United Kingdom', 1, 1); 

INSERT INTO Languages (name, isActive) VALUES ('English', 1);

INSERT INTO SchoolClasses (date, startTime, duration, state, isActive, professorId, studentId)
	VALUES ('2023-06-02', '16:00', '01:00', 'AVAILABLE', 1, 2, 1)

INSERT INTO SchoolClasses (date, startTime, duration, state, isActive, professorId, studentId)
	VALUES ('2023-06-02', '16:00', '01:00', 'AVAILABLE', 1, 2, 1)


