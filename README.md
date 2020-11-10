# AuthAPI
A Authentication API built with ASP.NET Core. It uses JWT as authentication method and counts with authrorization system. Also, it uses some design patterns, like dependency injection and the repository pattern.

### /auth/login:
Responsible for the login, this method expects the username and the password of the user and returns the user info(without password info) and the JWT token.

### /auth/create:
Responsible for the creation of new users, this method expects the username, the password and the role of the new user. It returns the user info without password related information.

### /users/anonymous:
The simpliest method, it just gives a message saying "Login"

### /users/welcome:
A very simple method that requires the roles User or Admin, it just shows a message of "Welcome, username"(it uses the user identity)

### /users/allusers:
Accecible only to Admins, it gives a list with the names of every single regitered user in the database.

### /users/delete:
Accecible only do Admins, it deletes all users from the database.
