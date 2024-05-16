# 🛠️ Microservices: Content Management System 🛠️  

This is an optimistic attempt to implement a **Content Management System** in a truly **Microservices** Architecture inside a **Kubernetes** cluster . 

Each service is planned to be implemented using a different technology.  

 - So this is the basic plan: 
![First Attempt](./readme/Simple-Arch.png)

- Let's dive a bit deeper
![Second Attempt](./readme/Slightly-Detailed-Arch.png)

- A bit simpler: 
![Third Attempt](./readme/Confusing-Arch.png)


More explanation on the structure: 

## Front-End 

Front-End is divided into 2 parts so far
- **General Pages:** where users/guests browse the posts, this will be a **React** Application
- **Admin Pages:** where users/admin, this will be an **Angular** Application 
- A React Native application will be added eventually. 

**Front-End** will communicate with the **Back-End** through an **API Gateway**

## API Gateway
This in an **NGINX Ingress Controller** for **Kubernetes**.

## Back-End
This is where the **Microservices** Shine. 
Details about each Service will be inside their respective folder.  
- **Posts Service**: A .NET Web API application, this is the core component. 
	- Responsible for managing the Posts, Caching to **Redis** and publishing events to **RabbitMQ** for other services on updates on posts: 
		- Post Added Event
		- Post Deleted Event
		- Post Cached Event
		- Post Requested Event
	- Managing a **Scheduled Background Service** responsible for Caching highly rated and most viewed posts. 
- **Comments Service:** A Spring Web API application. 
	- Responsible for managing comments, It will also subscribe to some of the posts events. 
- **Ratings Service:** A NodeJS/Express Web API application. 
	- Responsible for managing votes and ratings of posts and comments, It will also subscribe to some of the posts events. 
- **Views Service:** A NodeJS/Express Web API application. 
	- Responsible for managing posts' views, It will also subscribe to some of the posts events. 
- **Authentication Service**: A .NET Authentication Server responsible for handling **Users**, **Authentication** and **Authorization**.