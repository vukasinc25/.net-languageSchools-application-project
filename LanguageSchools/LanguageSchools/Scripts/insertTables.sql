
--SCHOOLS
INSERT INTO Schools (name, street, streetNumber, city, country, isActive)
	VALUES ('Great School', 'Main St', '6', 'London', 'United Kingdom', 1);
INSERT INTO Schools (name, street, streetNumber, city, country, isActive)
	VALUES ('Mega School', 'Main St', '7', 'New York', 'United States', 1);
INSERT INTO Schools (name, street, streetNumber, city, country, isActive)
	VALUES ('Best School', 'Main St', '8', 'Brighton', 'Serbia', 1);

--ADMIN
INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('admin@mail.com', 'a', 'Admin1', 'Adminkovic1', 'a', 'MALE', 'ADMIN', 'Main St', '5', 'London', 'United Kingdom', 1, null); 

--PROFESSOR
INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('professor1@mail.com', 'p', 'ProfessorName1', 'ProfessorSurname1', 'p1', 'MALE', 'PROFESSOR', 'Main St', '1', 'London', 'United Kingdom', 1, 1); 
INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('professor2@mail.com', 'p', 'ProfessorName2', 'ProfessorSurname2', 'p2', 'FEMALE', 'PROFESSOR', 'Main St', '2', 'London', 'United Kingdom', 1, 1);
INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('professor3@mail.com', 'p', 'ProfessorName3', 'ProfessorSurname3', 'p3', 'MALE', 'PROFESSOR', 'Main St', '3', 'London', 'United Kingdom', 1, 2);
INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('professor4@mail.com', 'p', 'ProfessorName4', 'ProfessorSurname4', 'p4', 'MALE', 'PROFESSOR', 'Main St', '4', 'London', 'United Kingdom', 1, 2);
INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('professor5@mail.com', 'p', 'ProfessorName5', 'ProfessorSurname5', 'p5', 'FEMALE', 'PROFESSOR', 'Main St', '5', 'London', 'United Kingdom', 1, 3);
INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('professor6@mail.com', 'p', 'ProfessorName6', 'ProfessorSurname6', 'p6', 'MALE', 'PROFESSOR', 'Main St', '6', 'London', 'United Kingdom', 1, 3);

--STUDENTS
INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('student1@mail.com', 's', 'StudentName1', 'StudentSurname1', 's1', 'MALE', 'STUDENT', 'Main St', '7', 'London', 'United Kingdom', 1, null); 
INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('student2@mail.com', 's', 'StudentName2', 'StudentSurname2', 's2', 'FEMALE', 'STUDENT', 'Main St', '8', 'London', 'United Kingdom', 1, null); 
INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('student3@mail.com', 's', 'StudentName3', 'StudentSurname3', 's3', 'MALE', 'STUDENT', 'Main St', '9', 'London', 'United Kingdom', 1, null); 
INSERT INTO Users (email, password, firstName, lastName, jmbg, gender, userType, street, streetNumber, city, country, isActive, schoolId)
	VALUES ('student4@mail.com', 's', 'StudentName4', 'StudentSurname4', 's4', 'FEMALE', 'STUDENT', 'Main St', '10', 'London', 'United Kingdom', 1, null); 

--LANGUAGES
INSERT INTO Languages (name, isActive) VALUES ('English', 1);
INSERT INTO Languages (name, isActive) VALUES ('Spanish', 1);
INSERT INTO Languages (name, isActive) VALUES ('German', 1);
INSERT INTO Languages (name, isActive) VALUES ('French', 1);

INSERT INTO SchoolClasses (date, startTime, duration, state, isActive, professorId, studentId)
	VALUES ('2023-06-02', '16:00', '01:00', 'AVAILABLE', 1, 2, 1)

INSERT INTO SchoolClasses (date, startTime, duration, state, isActive, professorId, studentId)
	VALUES ('2023-06-02', '16:00', '01:00', 'AVAILABLE', 1, 2, 1)