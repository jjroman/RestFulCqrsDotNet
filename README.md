# Basic RestFul API implemented with a CQRS approach.

This project is a simple implementation about CQRS in .net. 

Next steps:

1) It uses IMediatr as the library to manage commands. I will extend this functionality to connect with different buses like Azure Queue and RabbitMQ.
2) To implement a read layer using a NoSQL layer, it should be used as a cache to get information as soon as possible.
3) Incorporate more interesting functionality like custom properties and Authentication/Authorization. 
4) Create a microservice environment, to connect with other functionality.

Any comments are welcomed.
