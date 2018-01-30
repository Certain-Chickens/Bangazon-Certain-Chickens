# Building the Bangazon Platform API

Welcome, new Bangazonians!

Your job is to build out a .NET Web API that makes each resource in the Bangazon ERD available to application developers throughout the entire company.

1. Products
1. Product types
1. Customers
1. Orders
1. Payment types
1. Employees
1. Computers
1. Training programs
1. Departments

> **Pro tip:** You do not need to make a Controller for the join tables, because those aren't resources.

Your product owner will provide you with a prioritized backlog of features for you to work on over the development sprint. The first version of the API will be completely open since we have not determined which authentication method we want to use yet.

The only restriction on the API is that only requests from the `www.bangazon.com` domain should be allowed. Requests from that domain should be able to access every resource, and perform any operation a resource.

## What You Should Do

### Plan

First, you need to plan. Your team needs to come to a consensus about the Bangazon ERD design. Once you feel you have consensus, you must get it approved by your manager before you begin writing code for the API.

### Modeling

Next, you need to author the Models needed for your API. Make sure that each model has the approprate foreign key relationship defined on it, either with a custom type or an `ICollection<T>` to store many related things. The boilerplate code shows you one example - the relationship between `Order` and `OrderProduct`, which is 1 -> &#8734;. For every _OrderId_, it can be stored in the `OrderProduct` table many times.

Once you've added a Model, remember to add a new `DBSet<T>` to the `BangazonContext` class for that type.

### Migrate

Once you have all of your Models defined, attempt your first migration.

```
dotnet ef migrations add InitialDBCreation
```

This will read your Models and build the instructions necessary to create corresponding tables in the database based off of your annotations and foreign key relationships. To be responsible, you should read the migration file before committing the changes to your database.

### Update Database

If the migration was successful. Go ahead and execute the instructions and create the tables.

```
dotnet ef database update
```

Once that is complete, go ahead and open up the database in the DB Browser for SQLite tool and see what it created for you.

### Controllers

Now it's time to build the controllers that handle GET, POST, PUT, and DELETE operations on each resource.

# About ASP.NET Core

We've made some big updates in this release, so it’s **important** that you spend a few minutes to learn what’s new.

You've created a new ASP.NET Core project. [Learn what's new](https://go.microsoft.com/fwlink/?LinkId=518016)

## This application consists of:

*   Sample pages using ASP.NET Core MVC
*   [Bower](https://go.microsoft.com/fwlink/?LinkId=518004) for managing client-side libraries
*   Theming using [Bootstrap](https://go.microsoft.com/fwlink/?LinkID=398939)

## How to

*   [Add a Controller and View](https://go.microsoft.com/fwlink/?LinkID=398600)
*   [Add an appsetting in config and access it in app.](https://go.microsoft.com/fwlink/?LinkID=699562)
*   [Manage User Secrets using Secret Manager.](https://go.microsoft.com/fwlink/?LinkId=699315)
*   [Use logging to log a message.](https://go.microsoft.com/fwlink/?LinkId=699316)
*   [Add packages using NuGet.](https://go.microsoft.com/fwlink/?LinkId=699317)
*   [Add client packages using Bower.](https://go.microsoft.com/fwlink/?LinkId=699318)
*   [Target development, staging or production environment.](https://go.microsoft.com/fwlink/?LinkId=699319)

## Overview

*   [Conceptual overview of what is ASP.NET Core](https://go.microsoft.com/fwlink/?LinkId=518008)
*   [Fundamentals of ASP.NET Core such as Startup and middleware.](https://go.microsoft.com/fwlink/?LinkId=699320)
*   [Working with Data](https://go.microsoft.com/fwlink/?LinkId=398602)
*   [Security](https://go.microsoft.com/fwlink/?LinkId=398603)
*   [Client side development](https://go.microsoft.com/fwlink/?LinkID=699321)
*   [Develop on different platforms](https://go.microsoft.com/fwlink/?LinkID=699322)
*   [Read more on the documentation site](https://go.microsoft.com/fwlink/?LinkID=699323)

## Run & Deploy

*   [Run your app](https://go.microsoft.com/fwlink/?LinkID=517851)
*   [Run tools such as EF migrations and more](https://go.microsoft.com/fwlink/?LinkID=517853)
*   [Publish to Microsoft Azure Web Apps](https://go.microsoft.com/fwlink/?LinkID=398609)

We would love to hear your [feedback](https://go.microsoft.com/fwlink/?LinkId=518015)
