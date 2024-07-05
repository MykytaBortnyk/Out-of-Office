# Out of Office
A simple app to manage leave request.
Made on ASP.NET Core Blazor Server, with Database-first principle.
<a href="http://github.com/microsoft/fluentui-blazor">Made on Fluent UI components.</a>
PostgreSQL and EntityFramework Core for storing data.

<h4>How to launch</h4>
Requirements: .NET 8, PostgreSQL 16
<ul>
  <li>Clone project</li>
  <li>Configure connection string in appsettings.json</li>
  <li>Create database, types, tabels</li>
  <li>dontet run</li>
</ul>

<details>
  <summary>
    DB commands
  </summary>
  
  CREATE DATABASE "CRM"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

CREATE TYPE "Names" AS ENUM ('Vasyl', 'Petro', 'Volodymyr', 'Igor');
CREATE TYPE "Subd" AS ENUM ('smm', 'it', 'Support', 'Sales', 'Human Resources');
CREATE TYPE "Positions" AS ENUM ('Employee', 'HR Manager', 'Project Manager', 'Administrator');
CREATE TYPE "Status" AS ENUM ('Active', 'Inactive');
CREATE TYPE "AbsenceReason" AS ENUM ('Vacation', 'Health Issue', 'Family Emergancy');
CREATE TYPE "RequestStatus" AS ENUM ('New', 'Approve', 'Reject');
CREATE TYPE "ProjectType" AS ENUM ('SaaS', 'Fintech', 'Education', 'Gambling', 'Telecom');

CREATE TABLE "Employees" (	
	"Id" BIGSERIAL primary key,
	"FullName" "Names" NOT NULL,
	"Subdivision" "Subd" NOT NULL,
	"Position" "Positions" NOT NULL DEFAULT 'Employee' 
	"Status" "Status" NOT NULL,
	"PartnerId" int references "Employees"("Id"),
	"Balance" smallint NOT NULL DEFAULT 28
);
-- it's not possible to create first entry with NOT NULL, 
-- so we have to change the constraint after first entry has been added
ALTER TABLE "Employees" ALTER COLUMN "PartnerId" SET NOT NULL

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

</details>

<h4>Features</h4>
<ul>
  <li>List of Employees</li>
  <li>List of Projects</li>
  <li>List of Leave Requests</li>
  <li>List of Approval Requests</li>
</ul>

<h4>Planned Features</h4>
<details>
  <ul>
    <li>Filters for DataGrid</li>
    <li>Authentification and Authirization</li>
    <li>Appearence Settings</li>
    <li>Localization and Globalization</li>
  </ul>
</details>
<h4>Screenshots:</h4>
<img src="/Images/home-page.jpeg"/>
<img src="/Images/employees.jpeg"/>
<img src="/Images/projects.jpeg"/>
<img src="/Images/leave-request.jpeg"/>
<img src="/Images/arrpove.jpeg"/>
<img src="/Images/DataBase.png"/>
