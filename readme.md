# Coding Exercise

We've reviewed your CV, gotten to know a part of you during our first meeting and talked to you about Auctionata, our Technology organization and the way we think about product development.  Now we are very much interested in learning more about how you tackle and approach solving a real world problem.

Before we present the actual problem here are some notes up front:

- *Effort required*:  Please do not spend more than 8h working on this.  Rather adjust your scope to match this time frame.  We have some suggestions for cutting scope later on in the exercise.  But feel free to dice this up in any way you want.
- *Deliverables*:  We want to be able to view your working solution and review your source code.  How you allow us to do that is entirely up to you.
- *Technology*:  Please feel free to pick whatever technologies you think solve the problem best.  But please keep in mind that this story is part of our business critical application.
- *Solution and approach*:  There are no right or wrong solutions here.  Obviously different implementations will have different pros and cons.  The goal of the exercise is to get in idea of how you approach problems and on which areas you like to focus.
- *Next steps*:  If we like your solution and approach we'd love to have a brief pair programming session with you to implement a little piece of functionality together.  Please keep this in mind when deciding how you make your deliverables available to us.
- *Think aloud*:  It's really helpful for us if you could provide some context around the major decisions you made.  Where and how you do this is again up to you.  If you decide to add this information in the source code please mark it up somehow so that we can distinguish between regular comments in your code, i.e. those that you would normally add to your code, and those that you add just because of our request for you to think aloud.

# Requirements

## Narrative

As a potential buyer in an online auction
I want to be able to bid on an item
So that I can participate in the auction

## Scenario 1: displaying information about the current item

Given I am in the auction room
Then I see the current item picture, description and name
And I see the current highest bid with a button to place a new bid

## Scenario 2: single user bidding on an item

Given I am in the auction room
When I place a bid on an item
And I am the only bidder
Then I am the highest bidder

## Scenario 3: multiple users bidding on an item - you are first

Given I am in the auction room
When I place a bid on an item
And I am not the only bidder
And my bid was placed first
Then I am the highest bidder

## Scenario 4: multiple users bidding on an item - you are not first

Given I am in the auction room
When I place a bid on an item
And I am not the only bidder
And my bid was not placed first
Then I am not the highest bidder

# Scoping Options

We know that might look like too much work for an interview process.  Please remember that we are only asking you to spend 8h on this exercise.  Please consider the following options to cut scope:

## Option A - Backend Implementation

- Focus heavily on backend implementation, 
- Have a simple UI just to show what is happening behind the scenes
- Bonus points: Provide a nice API that could easily be consumed by a single page application or any other client.

## Option B - Frontend Implementation

- Make it a single page application that uses API's for most of the heavy business logic
- You can mock the API to represent the result of you working together with another team to define the interfaces
- You could make the mocks convenient for you, but not too convenient, they should be constrained by realistic assumptions.

## Option C - Your own scope

- Choose your own slice
- We want to see what you care about the most
- You could 
    - spend copious amounts of time making the CSS look perfect everywhere,
	- focus on dealing with concurrency and build a prototype which is all about speed and scalability,
	- go the classic route and do a server side MVC implementation,
	- have a super slick build pipeline before anything else.
- All that that is fine! Go for it, just let us know why that was your choice.

---

# Solution

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
