# What Was I Thinking?

## Comments

You will not find many comments in my code. Comments are a bad habit. What can a comment tell me, that is not obvious by reading the code? If a comment is required to understand a block of code, this should be considered a code smell and refactored into smaller components that are easier to understand. Comments cannot be refactored, they are invisible to the compiler. Comments are magic strings and you can never trust them. I only do inline documentation to describe interfaces.

However you will find a couple of comments. While there are only very few of them, these are only for the purpose to tell you what I was thinking in regards to the test. They wouldn't be in there if this was going on a productive system.

## My Solution

### Architecture

When looking at performance and concurrency, I would consider using CQRS for the architecture. However since I don't have any practical experience with CQRS it would be futile to try such an approach within the given time frame, so I went for the classic layered solution with all its downsides (heavy use of mapping, impact on performance).

### Unit Testing

Due to the time constraints in this little test I passed on doing unit tests at all.

### My Little Helpers

In order to prevent null at all cost, I used the Maybe monad. For a wide availability throughout the solution, I moved it into its own project avoiding unnecessary dependencies between the layers. My Maybe implementation is lifted from Mark Seemann's excellent [blog](http://blog.ploeh.dk/2011/02/04/TheBCLalreadyhasaMaybemonad/), where he realized that .NET already sort of provides this feature with LINQ. The implementation uses LINQ's existing methods (Any and First) to check if a Maybe is empty and to access its value respectively.

### Inversion Of Control

I always use dependency injection to wire up my applications. This provides full flexibility for using different implementations (e.g. for the data access). It allows me to chose my dependencies either on build time (configuration as code) or runtime (configuration in app.config/web.config). In the past I used Spring.NET, but recently tried nInject. Unfortunately this was completely unsufficient, so I switched to Microsoft's Unity. The next IOC container I'd like to try is Castle Windsor which is expected to provide the largest set of features available in DI (using Unity I already reached certain limits). However in this little test I went for "Poor Man's DI" to keep it simple. It's merely an implementation detail and using any other IOC container is as simple as providing a different composition root implementation without changing anything else.

### How I Put This Together

First I created a domain project, defining my model and my business logic in the form of services. For simplicity I implemented the core logic (finding the first highest bid) as an extension method. This is basically cheating since one should not consider using static classes for this purpose. The reason is obvious: it makes unit-testing hard. I also kept the logic very simple, most likely not very well-performing by just sorting my IEnumerable of Bids by amount and timestamp. Performance optimization is just an implementation detail.

- I chose strings for IDs to not restrict the underlying persistent storage. Depending on the expected number of items, users, and bids to deal with, the ID might differ in size; depending on the way to store the data persistently the ID might differ in type.

- In order to keep it simple I decided to skip any currency handling when using amounts of money. Of course one should consider to make the currency part of the model, provide translation and configuration.

For the persistence layer I went for a prototype implementation only using the memory to store our data. Of course, once you close the application, all data will be gone. This is sufficient for the demonstrational purpose of this test and allows me to avoid worrying about database technologies, their setup and delivery to you. The architecture allows you to attach any persistent data store you like, given you implement a corresponding data access layer and replace my in-memory implementation.

The application layer provides either a REST API allowing access by any frontend you desire to attach, or a Web UI to see the system in action. 

The REST API has the following interface: 

    GET api/items                         // returns all items
    GET api/items/{string:id}             // returns a specific item
    GET api/items/{string:id}/highestBid  // returns the highest bid for an item
    PUT api/bids/{json:bid}               // adds a bid for an item
    
The API is dead-simple and can surely be optimized. I made it with the intent to create a decoupled frontend project, which can connect to it. Due to time constraints I skipped this part which leaves the REST API unused (and partly untested!). Instead I quickly made a MVC UI which directly connects to the domain layer. This is not meant to be a productive solution, but some kind of integration test to see if everything is working. Any productive UI should connect either through the REST API (most flexibility) or an optional WCF API (limiting the technology to .NET).

For the demonstrational UI I used the Semantic UI css framework to not to be bothered with too much css/html fiddling. All the JavaScript/JQuery I used is quick&dirty. Although I'm quite familiar with these technologies I haven't got much practical experience and am therefore missing a lot of knowledge about patterns and practices in the JavaScript world.

- Due to time constraints, I left out the "real-time" refresh for the highest bid. For the demonstration UI it will suffice to simply refresh the page in order to update the highest bid. For a frontend implementation you could go with classic polling (and risk to flood your web server with lots of requests). Or you can setup web sockets and receive push notifications from the server. I looked into that matter, but had to abandon an implementation because there was no time left. 

- ASP.NET SignalR could do the job in .NET with the ability to fall back to polling, if the client doesn't support web-sockets. However .NET always comes with tons of auto-generated code, dependencies, and therefore intransparencies, hiding how it works under the hood and making it hard to understand the technology. Alone with my simple MVC and WebApi projects I had to spend a significant amount of time to remove all the auto-generated components, features, and dependencies which clutter the project, although I'm not even using them. A far more interesting way would be to use node.js to implement web sockets. One can either create the whole frontend UI in node.js or just use it as a push server, while the main UI is in .NET or whatever technology seems appropriate.

## TL;DR

I probably spent around 10 hours on my solution including web-sockets investigation, cleaning up the .nuget mess, creating a git repository, and writing this pamphlet.

What do you think?

*Jan Kockrow*
