# time-logs-task

Time Logs Task:


1.	There is a database with 3 tables: User, Project, and TimeLog. The User table should store the following information: first name, last name, and email. The Project table should store an ID and the name of the project. The TimeLog table should store user information, project details, date, and hours worked (as float).

2.	Create a method/service/stored procedure/something else to initialize the database with the following steps:
a.	Clear the contents of the User, Project, and TimeLog tables each time it is executed.
b.	Generate 100 records in the Users table with random names and email addresses based on the following criteria:
c.	First names: John, Gringo, Mark, Lisa, Maria, Sonya, Philip, Jose, Lorenzo, George, Justin.
d.	Last names: Johnson, Lamas, Jackson, Brown, Mason, Rodriguez, Roberts, Thomas, Rose, McDonalds.
e.	Domains: hotmail.com, gmail.com, live.com. Each generated record should have a random first name, last name, and the email address should be in the format name.lastname@randomdomain.
f.	Generate 3 projects with the names "My own," "Free Time," and "Work.
g.	For each record in the User table, generate a random number of entries (1-20) in the TimeLog table with random projects and hours worked (0.25-8.00) for each user. The entries should not exceed 8 working hours per day per user
3.	Create a user interface consisting of a single page divided into two columns, each taking up 50% of the page:
h.	The left column should contain a grid displaying users' time logs. The grid's columns must be user's name, user's email, the project, date of the log, time worked. The grid must use server-side pagination with 10 rows per page.
i.	Add sorting functionality to the table and a date filter from...to... to refine the displayed data.
j.	The right column should display a bar chart (using Google Charts) representing the TOP 10 Users with the highest number of hours worked during the selected period. The size of each bar in the chart should represent the total hours per user or project. A radio button should allow the selection of either users or projects for comparison.
k.	Add a "Compare" button for each user in the Users table. When clicked, this button should asynchronously fetch the hour data for the selected user and display it in the chart as a red line, allowing for comparison with the TOP 10 users. The data for the selected user should not be loaded with the table but fetched only when the "Compare" button is pressed.
l.	Add a button that triggers the database initialization procedure and reloads the page.


