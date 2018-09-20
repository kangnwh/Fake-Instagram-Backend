# Backend of Mobile Assignment 2

- Using C# .net core webapi (version 2.1.402)




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


#### workplace 

Open [this](https://kangnwh.visualstudio.com/MobileBackend/MobileBackend%20Team/_dashboards/MobileBackend%20Team/8b20c756-4048-4b0d-ab5c-07cc380afb3e) for our project dashboard


#### how to run (internal)
```shell
cd $your_project_folder
dotnet watch run # use this in development, as app will restart when code is changed
dotnet run # just run, only static files changes can be reflect real time
```



#### Server Info
db: 123.56.22.40:3306/sns/pw4mobile



#### Pending Tasks

- [ ] Requirement Analysis
- [ ] Database modelling
- [ ] Api Design
- [x] Security Control
- [ ] Request Validation