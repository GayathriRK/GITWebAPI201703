# GITWebAPI201703
Project_Exercise1.0.0


## Installation Steps

 *	Execute the below Script in SQL Database to create a database and table 
 
	CREATE DATABASE Test_Project	
	
	Go
	
	CREATE TABLE Employee(
	EMP_ID INT NOT NULL IDENTITY(1,1), 
	FIRST_NAME VARCHAR(50),
	LAST_NAME VARCHAR(50),
	EMAIL VARCHAR(255),
	PHONE_NUMBER VARCHAR(20),
	EMP_STATUS VARCHAR(10),
	CONSTRAINT PK_EMPLOYEE PRIMARY KEY(EMP_ID)
	)
	
	Go
	
*	pull and deploy the code provided in the branch of CodeRepo

* 	Update the database connectionstring in the app.config in EmployeeDataAccess project and web.config file in the 		EmployeeService.

*	Build the Solution


## Usage:
Execute the below methods on Fiddler to test the API

Note:Update the localhost address with correct address in place of below mentioned uri "http://localhost:51988"

1)	Get:  http://localhost:51988/api/employees

2)	Get Method for a specific contact based on empid:  http://localhost:51988/api/employees/1

3)	Post:  http://localhost:51988/api/employees

Requestbody: {"FIRST_NAME":"John","LAST_NAME":"A","EMAIL":"John.A@gmail.com","PHONE_NUMBER":"571-123-0909","EMP_STATUS":"Active"}

4)	Put:  http://localhost:51988/api/employees/1

Requestbody: {"FIRST_NAME":"Mary","LAST_NAME":"S","EMAIL":"Mary.S@gmail.com","PHONE_NUMBER":"543-342-6666","EMP_STATUS":"InActive"}

5)	Delete: http://localhost:51988/api/employees/1


## Validation:
Implemented validations for the data 

* FirstName, Email, Phone Number and Status fields are mandatory
* FirstName and lastName could not be Same
* Status should be none other than "Active" or "Inactive"
* Prevented Duplicate Email entries  

## Release Notes
  1.0.0 - Feature of managing contact details(Fetch,Add,Update,Delete)
  
* This api is Used to manage the Contact details.

* Enviroment: WebAPI 2.0, Entity Framework 6.0 (Data First), MSSQL 2008, LINQ, C# and Fiddler 
