"# FCCmicroservice" 

[YouTube Link](https://www.youtube.com/watch?v=CqCDOosvZIk&t=3607s)

# Tools Used
[MongoDb for VS Code](mongodb.mongodb-vscode)
[Docker Desktop](https://www.docker.com/products/docker-desktop)
[Docker for VS Code](ms-azuretools.vscode-docker)

# Overview
Simple WebAPI using ASP.NET 5, MongoDB and Docker to look at containerization in a Windows environment. 

docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
d - disconnected
rm - remove after exit
name - container name
p - external to internal port mapping
v - volume location for persistant data storage after container exit
mongo - image to use 

dotnet pack -o ..\..\..\packages\
Create a nuget package

dotnet nuget add source <path> -n Name
Add a nuget location

docker-compose up -d
start docker compose in detached mode

Note:
Switching from docker to docker compose will erase the current volume, even if they are named the same. Should probably make a note of that.

Using a Message broker to communicate between Mircoservices will keep services loosely coupled between each other.
