# Backend of Mobile Assignment 2

This is the backend part (C# .net core - version 2.1.402) of Fake Instagram. This should be used together with [the app part] (https://github.com/kangnwh/Fake-Instagram).



## Demo

YouTube Link: https://youtu.be/xIZUexAuOMY






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

## Functionalities

- [x] Errors are treated correctly
- [x] Register and login screen
- [x] Functional Tab Bar at the bottom of screen
- [x] Scroll through photos and comments
- [x] Like a photo and display users who like a photo in the feed
- [x] Leave a comment
- [x] Sort by both date/time and location
- [x] Search for Users
- [x] Display suggested users to follow
- [x] Algorithm to suggest users
- [x] Take a photo with camera while providing flash options
- [x] Overlay a grid on top of camera view
- [x] Select photo from library instead of taking a new one
- [x] Change brightness and contrast
- [x] Crop a photo
- [x] Apply at least 3 different filters
- [x] Upload photo
- [x] Display users following that liked photos or started following user
- [x] Display activity of users that current user are following
- [x] Display stats on posts, followers and following, profile pic
- [x] Display all user photos uploaded
- [x] In Range Swiping, e.g. Swipe photos to friends nearby that they can view on feed with an "In Range" tag
- [x] Implement a server for communications or retrieve data from actual Instagram API



## Contributor

- [ningk1, ningk1@student.unimelb.edu.au]
- [xlyu2, xlyu2@student.unimelb.edu.au]
- [nianl, nianl@student.unimelb.edu.au]
- [xwu11, xwu11@student.unimelb.edu.au] 