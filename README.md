"# FCCmicroservice" 

[YouTube Link](https://www.youtube.com/watch?v=CqCDOosvZIk&t=3607s)

# Tools Used
[MongoDb for VS Code](mongodb.mongodb-vscode)
[Docker Desktop](https://www.docker.com/products/docker-desktop)

# Overview
Simple WebAPI using ASP.NET 5, MongoDB and Docker to look at containerization in a Windows environment. 

docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
d - disconnected
rm - remove after exit
name - container name
p - external to internal port mapping
v - volume location for persistant data storage after container exit
mongo - image to use 