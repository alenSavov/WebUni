# WebUni
<p>Small web app platform for online education - ASP.NET Core/ Sql<p>

<p>Used technologies</p>
<ul>
  <li>Asp.Net Core 2.1</li>
  <li>Entity Framework Core</li>
</ul>

## Registered users accounts for testing
| Username        	| Password 	| Role          	|
|-----------------	|----------	|---------------	|
| admin 			| 123456 	| Administrator 	|
| user  			| 123456  	| User          	| 

<p>Depending on its role every user can acsess different sections of the application.</p>

<p>If the user is with role - 'user'. He can see his profile, watch video, make articles, add comments for articles.
Мay follow a course or event.</p>

<p>If the user is with role - 'admin'. He can to make new categories,courses,articles,events and he can to edit/delete them.
Also he have permission to make another user with role - 'admin'.</p>	
	
<h3>Getting started:</h3>
<p>In order to run the project you just need to replace the connection string which is located in appsettings.json and run the project. <p> 

## Set up Facebook login
<p>If you want to have facecbook login functionality is need to create your own user secrets App Secrets/App Id. You can see more details here:</p>
<ul>
  <li><a href="https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/facebook-logins?view=aspnetcore-2.1&tabs=aspnetcore2x" target="_blank">Facebook</a></li>
</ul>


## Set up Cloudinary (required)
<p>1. Register a [Cloudinary](https://cloudinary.com/) account.</p>
<p>2. [Create a Cloud, API key and API secret in Cloudinary Serivece](https://cloudinary.com/documentation/solution_overview#account_and_api_setup).</p>

Example:
```
"Cloudinary": {
  "CloudName": "cloudname",
  "ApiKey": "488*********516",
  "ApiSecret": "3m7******************KdS"
}
```

![test2](https://user-images.githubusercontent.com/28908397/60321280-00861c80-9985-11e9-8f70-0421e6bd0ef7.gif)

![test3](https://user-images.githubusercontent.com/28908397/60321378-4d69f300-9985-11e9-8c18-b0fbb280fabc.gif)

![test4](https://user-images.githubusercontent.com/28908397/60321404-607cc300-9985-11e9-91e9-c7d94f46694a.gif)

![test5](https://user-images.githubusercontent.com/28908397/60321431-712d3900-9985-11e9-8f37-9c4b6a6be270.gif)



