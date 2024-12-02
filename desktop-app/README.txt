Desktop Application for Professor
gxk220025
Gwangmo Kim

---
Importnat:
Before execution, modify MySQL access information after build sql using scripts:
MY_DB.cs
#24: static string connect_info = "server=localhost;uid=root;pwd=kotori1430;database=project";
into your access information.
---
Purpose:
This desktop application is designed for professors to manage and edit students' information.
Professors can use the application through access to appropriate authority. This is done through login.
---
instructions:
1. Login screen
The professor can access the management screen through the appropriate id and password.

2. Main Screen
The main screen consists of login information, class information, save, import, add, enroll, change, delete, logout, and data display.

3. Your Class
User can select a class from combo box under "Your Class".
The All students option displays all students, regardless of whether or not they are enrolled in a class.

4. Save
The Save function saves the list of students currently displayed as a csv file.

5. Import
The import function reads the csv file that contains the list of students and stores it on the Sql server.
The Student csv file must be in the format "Last Nam, First Name, Username, Student ID".
An example file is provided for testing. (student_to_test.csv)

6. Add
New student's information can be registered. Students' duplicate status is made through their id number.

7. Enroll
You can enroll the selected students in a specific class.

8. Change
You can modify the information of the selected student.

9. Delete
Delete all information for the selected student. This is irreversible. All information related to the student will be deleted. This should be done carefully.

10. Work Hours
You can double-click on the student's information to access the Work Hour session. You can double-click the Work Hours information to modify it.

11. Log out
Unleash the information you are connecting to and return to the login screen.
---
File Components:
Desktop_App_For_Professor.exe(To execution program)
Sql Script_create table.txt(To build sql db table)
Sql Script_insert data.txt(To insert sql db contents)
student_to_test.csv(To test csv file functions)
README.txt(description document)
Desktop_App_For_Professor(Source code folder)
---
Source Components:
Desktop_App_For_Professor.sln(solution for project)
Program.cs(initializing program)
MY_DB.cs(To connect MySQL server)
ProfessorSession.cs(for professor information)
STUDENT.cs(for accessed student information)
Form_login.cs(login screen)
Form_main.cs(main screen)
Form_spsh_add.cs(for add function)
Form_spsh_enroll.cs(for enroll function)
Form_std_work.cs(work hours session)
Form_std_work_edit.cs(for work hour content modify)

Form_spsh.cs(not use)
Form_std_add.cs(not use)
Form_std_edit.cs(not use)
---
