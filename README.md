# SeniorProject

Note: You might need Microsoft Visual Studio (VS) to run the program. 
A community version of visual studio is available for free.  



1.1 Progress
The Project is an audit scheduling project which consist of assigning audits to auditors at a given date along with satisfying constraints. 


The tables that are going to store information about the auditors and audits have been created locally on my computer. SQL Server is been for design and creation of these tables. 


A User Interface (UI) comprising of CRUD operation to these tables is been developed. Operations such as create a new record, update a record and delete a record have been built so far. 
The algorithm design and development for scheduling is been done separately using dummy variables and printing out results in the console. I tried using randomized approach to schedule audits. The approach works great on small number of auditors, audits and date range but turns out to be slow in generating output with large number of auditors, audits and date range. 

Greedy Heuristic is been used currently and is working great. It generates a year worth schedule incorporating all constraints within 36 milliseconds. Which is better than randomized (15 minutes).


1.2 Problems

1. Incorporating Holidays in the schedule is a big challenge. Since holidays change every year.

2. User control over constraints - The User should be allowed to change the constraints in the long run to meet changing requirements.

3. Density per auditor every week – An optimal schedule should not assign an employee over a week several times (Even if it occurs randomly). This soft constraint needs to be incorporated without disturbing the existing constraints. 
