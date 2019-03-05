# WebUni
<p>Small web app platform for online education - ASP.NET Core/ Sql<p>

## Registered users accounts for testing
| Username        	| Password 	| Role          	|
|-----------------	|----------	|---------------	|
| admin 			| 123456 	| Administrator 	|
| user  			| 123456  	| User          	| 

<p>WebUni is small platform for online courses.</p> 
<p>Depending on its role every user can acsess different sections of the application.</p>

<p>If the user is with role - 'user'. He can see his profile, watch video, make articles, add comments for articles.
Мay follow a course or event.</p>

<p>If the user is with role - 'admin'. He can to make new categories,courses,articles,events and he can to edit/delete them.
Also he have permission to make another user with role - 'admin'.</p>

<p>Used technologies</p>
<ul>
  <li>Asp.Net Core 2.1</li>
  <li>Entity Framework Core</li>
</ul>
	
	
<h3>Getting started:</h3>
<p>In order to run the project you just need to replace the connection string which is located in appsettings.json and run the project. <p> 

## Set up Facebook login
<p>If you want to have facecbook login functionality is need to create your own user secrets App Secrets/App Id. You can see more details here:</p>
<ul>
  <li><a href="https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/facebook-logins?view=aspnetcore-2.1&tabs=aspnetcore2x" target="_blank">Facebook</a></li>
</ul>


## Set up Cloudinary (required)
<p>1. Register a [Cloudinary](https://cloudinary.com/) account.</p>
<p>2. [Create a Cloud, API key and API secret](https://cloudinary.com/documentation/solution_overview#account_and_api_setup).</p>

Example:
```
"Cloudinary": {
  "CloudName": "AuctionSystemCloud",
  "ApiKey": "488*********516",
  "ApiSecret": "3m7******************KdS"
}
```

