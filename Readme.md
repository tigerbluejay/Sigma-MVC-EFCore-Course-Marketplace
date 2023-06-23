# Sigma-MVC-EFCore-Course-Marketplace
This is an ongoing project as of June 2023.
This is a more involved MVC EF Core application built from scratch.

## DESCRIPTION 

This course marketplace consists of a listing of courses with their respective prices (home controller - index view), the posibility of viewing details about the course (home controller - details view) . It implements .NET Scaffolded Identity ( identity area - pages) which allows users to register with their personal information ( identity area - pages - account - register view) as well as log in once they are registered ( identity area - pages - account - login).

Customers can then view a shopping cart count on the main screen (views - shared - layout && views - shared - components - shopping cart - default view) and may add courses they want to purchase (from home controller - details view) and view them in their shopping cart ( cart controller - index view). They can make adjustments such as adding or removing courses and proceed to a order summary page (cart controller - summary view).

## DEVELOPMENT

The development of this application proceeded in four stages

### STAGE ONE - BASIC SETUP 

At this stage I developed view snippets inlcuding the general layout home and cart controller snippets for the views. 
Additionally I set up the initial folder structure for the solution and made it a multiple project solution (N-Tier).
I installed all the necessary packages for the Data Access, Models, Utilities and Main (SigmaWeb) projects.
I set up any partial views that I could anticipate and I did basic setup and commented advanced setup based on a previous project of the:
	- Program.cs
	- Appsettings.json
	- Layout
I set up data access files such as ApplicationDbContext.cs and the Repository and unit of work files (such as):
	- IRepository and Repository
	- IProductRepository and ProductRepository
	- IShoppingCartRepository and ShoppingCartRepository
	- IUnitOfWork and UnitOfWork
	- IOrderHeaderRepository and OrderHeaderRepository
	- IOrderDetailRepository and OrderDetailRepository
	- IApplicationUserRepository and ApplicationUserRepository
I set up some models including Product, ShoppingCart, ApplicationUser, and OrderHeader as well as the ShoppingCartViewModel (ShoppingCartVM)

### STAGE TWO - INITIAL DEVELOPMENT PHASE 

During the initial phase I developed the Home Controller (Index, Details, Privacy) and its views.
I proceeded early on scaffolding Identity and setting up the Register and Login models and views.
I implemented EmailSender to send notifications upon login and later on, but quickly disabled it to
simplify development. The Utility for Sending Email as well as relevant code snippets exist in the code but are commented.
I tested and Enabled Register and Login functionality.
After that, I proceeded to add relevant code to the Home Controller methods and implemented sessions.
Specifically I created a ShoppingCart View Component and all related files and code.
Finally I worked on the Cart Controller and its methods as well as the Index and Summary views for adding and managing products before getting a summary of your order.

### STAGE THREE - SECONDARY DEVELOPMENT PHASE

This stage focuses on the product area of the Admin Controller.
First I added a dropdown during registration so users chould choose whether they would be regular users or admins.
This naturally is not so in a real world application, but to display the full features of the app and access all areas users should be at time customers, at time admins.
The Admin Area has a dropdown where they can register new users and where they can add, edit and remove courses (products) available to customers.
The Product Contoller (courses) handles product CRUD operations and display has been implemented with the use of DataTables and json managed through js file products (under wwwroot)111111
Create New Course (product) and Edit Existing Course are handled by the Upsert (GET and POST) methods/actions. The delete method deletes the courses that are offered. GetAll() is a method that uses API Calls to Datatables to populate the courses.
In addition, there's an Order Controller that has an index view associated with it. Just the view is visible there is no implementation because Cart Controller in the Customer Area doesn't process (save to db or register with Stripe or other third party service) the order, so there's nothing to work with here. 


### STAGE FOUR - WRAP UP - UNIT TESTING AND EF CORE FEATURES 

The final stage explores Unit Testing (adding Unit Testing Projects to the Data, Models, and Web Projects). Several tests were added using NUnit and Moq. Tests are not exhaustive. They evaluate a subset of the available methods, particullary in the Home, Cart and Product Controllers but use Mocking extensively since Controllers methods although lightweight, reference many dependencies. Finally a new EF Core feature makes its way into the application:
Specifically, the addition of fluent api calls in the OnModelCreating method in the ApplicationDbContext. The fluent api statements are commented, since
the application already uses data annotations in the Models to achieve its goals, but it is there nontheless to show the alternative implementation.

## LIMITATIONS

For the sake of simplicity the project does not implement payments integration with stripe or other portals, neither does it implement third party logins. It it an advanced or at least intermediate project demonstrating many of the features of .NET EF, Unit Testing, and EF Core.

The project was based of three distinct projects from Dot Net Mastery that set the foundations for much of what I know about .NET Core MVC.

Enjoy!







