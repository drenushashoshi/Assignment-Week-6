Based on Clean Architecture principles and structure, create web api for managing employee days off.
You are provided with 4, entities, feel free to use them or to create totally different ones, or add new ones
Show creativity.
However, the following features should be implemented and working properly:
1. Authentication and Authorization
2. Unit of work with repositories
3. Clean Architecture
4. Role based endpoints
5. Tables Relationships

Concrete features:
6. There should be 4 leave types created once application starts (seed):
   Annual, Sick, Replacement, Unpaid
Sick leave is set to default 20 days, Replacement (is per user, so no default value), and Unpaid is set to 10 
These types of leave can be used only within one calendar year (example, only within 2024) and once the new year comes these are to be reseted.

7. Annual leave starts with 0, once user starts the work, and to all users for each month that passes, example on 1st of each month, 2 days are to be added to the existing value of annual leave accumulated days (this should be done using a backgroun job processing)

8. Users should be able to use the accumulated annual days upon 1st of July of the next year, and once 1st of july hits, then all the days from previous year are to be reseted and only the days accumulated in the current year remain (Also by background job processing).

9. When user/employee wants to take days off, he/she has to make a leave request, when creating leave request an email should be sent to his/her lead (to whom the employee reports)

10. The lead can approve or cancel the request

11. The request can not be done if user has not enough days off to use, if not return a message to the user (note that when creating request user chooses dates)

12. If user has enough days and the lead approved the request, once approved the days should be substracted from the accumulated ones

13. User chooses the type of leave and the same flow is for any type.

14. Only the lead can see the time off requests, and only for the employess that report to him/her, while employees can see only their requests (And Do the Cruds ofc).