# Backend of Mobile Assignment 2

This is the backend part (C# .net core - version 2.1.402) of Fake Instagram. This should be used together with [the app part] (https://github.com/kangnwh/Fake-Instagram).




## How to setup environment
1. Ensure your computer installed git tool
> For windows, install [here](https://gitforwindows.org) if not

> For Mac/Linux, your computer may already has, just try command `git`

2. Generate your ssh key if you konw command well( Mac or Linux)
> check [here](https://confluence.atlassian.com/bitbucketserver/creating-ssh-keys-776639788.html)

3. Add your public key into our project
```shell
    cat ~/.ssh/id_rsa.pub
```

   - Open web : [https://kangnwh.visualstudio.com/_details/security/keys](https://kangnwh.visualstudio.com/_details/security/keys)
   - Add the content in your `id_rsa.pub`


4. Clone code to your own computer
```shell
    cd <your_project_folder>
    git clone kangnwh@vs-ssh.visualstudio.com:v3/kangnwh/MobileBackend/MobileBackend
```
5. Install .net core SDK [Here](https://www.microsoft.com/net/download)



## How to run 

- Our own server was shutdown so that you need to use your own server instead

- If you want to test our backend together with our IOS app:

    1. Download and install dotnet core SDK (version 2.1+) here (https://www.microsoft.com/net/download)
2. Use a terminal to run backend service:

    ```shell
    cd $backend_dir
    dotnet run
    ```

3. Please note that our database is also in that AWS server and you need to **use your local database**, you can create tables using `backend_dir/Model/dbinit.sql` AND change file `backend_dir/appsettings.json` to user your own db information.



## Contributor

1: [ningk1, ningk1@student.unimelb.edu.au] 
2: [xlyu2, xlyu2@student.unimelb.edu.au] 
3: [nianl, nianl@student.unimelb.edu.au] 
4: [xwu11, xwu11@student.unimelb.edu.au] 