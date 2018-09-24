# API Design

[TOC]

How to use in IOS:<#T##TextRow#>

```swift
let accessToken = "veryCoolToken"
var request = URLRequest(url: myURL)
request.httpMethod = "POST" 
request.addValue("application/json", forHTTPHeaderField: "content-type")
request.addValue("application/json", forHTTPHeaderField: "Accept")
request.setValue("Basic " + accessToken, forHTTPHeaderField: "Authorization")
```





#### Post/Upload Photo

```json
Type: POST

parameter: 
1.(token in header, see `How to use in IOS`)
2. 


Response Required:
body[
	{
		"postid":postid,
		"postUsrid":usrid,
		"postUsrName": usrName,
		"comment":comment,
		"datetime":time
	}
]
```





#### Activity Feed

##### get the photo list which the current user like

```json
Type: GET

parameter: None (token in header, see `How to use in IOS`)


Response Required:
body[
	{
		"postid":postid,
		"postUsrid":usrid,
		"postUsrName": usrName,
		"comment":comment,
		"datetime":time
	}
]
```



##### get the usr who the current usr are following

```json
Type: GET
parameter: None (token in header, see `How to use in IOS`)


body[
	{
		"usrid":usrid, #the usr who current usr are following
		"usrName":usrName
	}
]
```



##### get the all activity of the usr who the current usr are following

```json
Type: GET
parameter: None (token in header, see `How to use in IOS`)


body[
	{
		"usrid": usrid,
		"usrName": usrName,
		"postid":postid,
		"postTime": time,
		"comment": comment,
		"commentTime": time
	}
]
```



#### User Feed

#### 获取刷新内容

```json
type:GET

parameter: 
1. (token in header, see `How to use in IOS`)
2. order={order}&lat={lat}&log={log} #参数TOKEN 加 排序方式（time或loc） lat 和 log 不为必填


body:#返回格式
[
    {   "img": imageUrl
        "like": num
        "likesender":["usr1","usr2"，"usr3"] #显示前3个点赞的用户
        "comments": [
            {   "usrid": usrID,
                "usrname" : username,
                "content" : content,
                "datetime"  : time
            },
            {   "usrid": usrID,
                "usrname" : username,
                "content" : content,
                "datetime"  : time
            }
        ]
    },
    {   "img": imageUrl
        "like": num
        "comments": [
            {   "usrid": usrID,
                "usrname" : username,
                "content" : content,
                "datetime"  : time
            },
            {   "usrid": usrID,
                "usrname" : username,
                "content" : content,
                "datetime"  : time
            }
        ]
    }
]

```



##### 点赞

```json
type: POST
url:defualtUrl/usr/like

parameter:
1. (token in header, see `How to use in IOS`)
2. "postid" : postid


```



##### 取消点赞

```json
type: POST
url:defualtUrl/usr/unlike

parameter:
1. (token in header, see `How to use in IOS`)
2. "postid" : postid

```



##### 返回

```json
body{
    "res":0 or 1
}
```



##### 发表评论

```json
type: POST
url:defualtUrl/usr/comment

parameter:
1. (token in header, see `How to use in IOS`)
2. "postid" : postid


# "datetime" : time 后台生成


```



#### User Profile

##### Display stats on posts, followers and following, profile pic

```json
type: POST
url:defualtUrl/usr/stats

parameter:
1. (token in header, see `How to use in IOS`)

body:#返回格式
{
	"postCount":postcount,
    "followerCount":followercount,
    "followingCount":followingcount
}
```



##### Display all user photos uploaded

```json
type: POST
url:api/usr/stats

parameter:
1. (token in header, see `How to use in IOS`)

body:#返回格式
[
    {   
        "img": imageUrl,
    }
]

```

