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

<<<<<<< HEAD


<p>![Capture](https://user-images.githubusercontent.com/28908397/57332932-6cd26580-7124-11e9-88ca-5af885c4d7f9.JPG)</p>
<p>![Capture2](https://user-images.githubusercontent.com/28908397/57332938-6e039280-7124-11e9-91f1-c19211d5abe8.JPG)</p>
<p>![Capture3](https://user-images.githubusercontent.com/28908397/57332945-6fcd5600-7124-11e9-9454-1b409f18245d.JPG)</p>
<p>![Capture4](https://user-images.githubusercontent.com/28908397/57332949-70fe8300-7124-11e9-9436-985175796dff.JPG)</p>
<p>![Capture5](https://user-images.githubusercontent.com/28908397/57332952-722fb000-7124-11e9-94bd-ea6a179f5cb1.JPG)</p>
<p>![Capture6](https://user-images.githubusercontent.com/28908397/57332955-73f97380-7124-11e9-8b7b-c24255a8598f.JPG)</p>

=======
![Capture](https://user-images.githubusercontent.com/28908397/57332932-6cd26580-7124-11e9-88ca-5af885c4d7f9.JPG)
![Capture2](https://user-images.githubusercontent.com/28908397/57332938-6e039280-7124-11e9-91f1-c19211d5abe8.JPG)
![Capture3](https://user-images.githubusercontent.com/28908397/57332945-6fcd5600-7124-11e9-9454-1b409f18245d.JPG)
![Capture4](https://user-images.githubusercontent.com/28908397/57332949-70fe8300-7124-11e9-9436-985175796dff.JPG)
![Capture5](https://user-images.githubusercontent.com/28908397/57332952-722fb000-7124-11e9-94bd-ea6a179f5cb1.JPG)
![Capture6](https://user-images.githubusercontent.com/28908397/57332955-73f97380-7124-11e9-8b7b-c24255a8598f.JPG)


>>>>>>> 382b7b6b26e06f2603456cd4caaa7924d1d37ea3
