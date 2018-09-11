# Backend of Mobile Assignment 2

- Using Java Spring Boot
- Restful API


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
5. *unmark src folder as source and mark folder java (src/main/java) as source* 

#### workplace 
Open [this](https://kangnwh.visualstudio.com/MobileBackend/MobileBackend%20Team/_dashboards/MobileBackend%20Team/8b20c756-4048-4b0d-ab5c-07cc380afb3e) for our project dashboard


#### how to run (internal)
Just run `/src/main/java/com/unimelb/mobile/eric/SnsbackendApplication.java`

#### Server Info
db: 123.56.22.40:3306/sns/pw4mobile

#### Pending Tasks
1. Requirement Analysis
2. Database modelling
3. Api Design
4. Security Control
5. Request Validation
