# CarProject
Test implementation of c# web application via MVC technology. The project is focused on the use and manipulation of databases, access via roles, logging, unit testing, data presentation and manipulation.

## Technologies used
- C#
- ASP.NET Framework 4.8
- MS-SQL
- Javascript
- Entity Framework
- HTML

## Project description
This section is dedicated to describing CarProject in detail.
### Databases
There are four databases in the project. The first database is the default. Therefore, it is formed with the project creation. The database is used to store users and roles. The second and third databases are created by Entity Framework - code first technique. The second database is dedicated to storing and managing information about factories and employees who work there. Moreover, there is relation one-to-many between factories table and employees table. The third database focuses on storing and managing logging information. 

The fourth database is created Entity Framework - database first technique. There are stored car-related data (customers, sales, etc.). Moreover, this database is used for data presentation.

### Functionality


## License
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
