:w

i
# RadAPI

## Run Tests
In source directory, you can run tests executing:

dotnet test .\tests\UnitTests\
 
## Run API using Docker Compose

In source directory, you can run the application using docker compose:

cd src

docker compose up

The swagger page should be available (a) https://localhost:443/swagger/index.html (b) http://localhost:80/swagger/index.html

## Example Case

1. We must first create a user in order to create accounts for them.

Use POST api/User/ in order to generate a user id.

2. Now that we have the user id, we can create accounts!

Use POST api/Account to create an account. If you call POST api/Account multiple times, multiple accounts will be generated.
The default balance for the account is 100.

3. Now the we have an account, we can deposit or withdraw. 

Use PUT api/Account in order to deposit or withdraw. 
AccountAction = 0 will carry out a deposit.
AccountAction = 1 will carry out a withdrawal.

4. If we wish to close an account for a user:

Use DELETE api/Account to delete a specific account for a user.
