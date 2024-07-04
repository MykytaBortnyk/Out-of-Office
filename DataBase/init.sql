CREATE DATABASE "CRM"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

CREATE TYPE Name AS ENUM ('Vasyl', 'Petro', 'Volodymyr', 'Igor');
CREATE TYPE Subd AS ENUM ('smm', 'it', 'Support', 'Sales', 'Human Resources');
CREATE TYPE Positions AS ENUM ('Employee', 'HR Manager', 'Project Manager', 'Administrator');
CREATE TYPE Status AS ENUM ('Active', 'Inactive');
CREATE TYPE AbsenceReason AS ENUM ('Vacation', 'Health Issue', 'Family Emergancy');
CREATE TYPE RequestStatus AS ENUM ('New', 'Approve', 'Reject');
CREATE TYPE ProjectType AS ENUM ('SaaS', 'Fintech', 'Education', 'Gambling', 'Telecom');

CREATE TABLE "Employees" (	
	"Id" BIGSERIAL primary key,
	"FullName" "Names" NOT NULL,
	"Subdivision" "Subd" NOT NULL,
	"Position" "Positions" NOT NULL,
	"Status" "Status" NOT NULL,
	"PartnerId" int references "Employees"("Id"),
	"Balance" smallint NOT NULL DEFAULT 28
);
-- it's not possible to create first entry with NOT NULL, 
-- so we have to change the constraint after first entry has been added
ALTER TABLE Employee ALTER COLUMN PoeplePartner SET NOT NULL

CREATE TABLE "LeaveRequests" (
	"Id" BIGSERIAL primary key,
	"EmployeeId" int references "Employees"("Id") NOT NULL,
	"AbsenceReason" "AbsenceReason" NOT NULL,
	"StartDate" date NOT NULL,
	"EndDate" date NOT NULL,
	"Comment" text,
	"Status" "RequestStatus" NOT NULL DEFAULT 'New'
);
CREATE TABLE "ApprovalRequests" (
	"Id" BIGSERIAL primary key,
	"ApproverId" int references "Employees"("Id") NOT NULL,
	"LeaveRequest" int references "LeaveRequests"("Id")  NOT NULL,
	"Status" "RequestStatus" NOT NULL DEFAULT 'New',
	"Comment" text	
);
CREATE TABLE "Projects" (
	"Id" BIGSERIAL primary key,
	"Type" "ProjectType" NOT NULL,
	"StartDate" date NOT NULL,
	"EndDate" date,
	"ManagerId" int references "Employees"("Id") NOT NULL,
	"Comment" text,
	"Status" "Status" NOT NULL
);