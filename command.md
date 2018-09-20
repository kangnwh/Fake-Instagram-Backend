# Command for this project


### DB related
1. scaffold entity classes from database
```shell
dotnet ef dbcontext scaffold -o Model "server=123.56.22.40;port=3306;database=sns;uid=sns;password=pw4mobile" "Pomelo.EntityFrameworkCore.MySql"  --prefix-output
```