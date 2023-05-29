# Sigma-MVC-EFCore-Course-Marketplace
This is an ongoing project as of June 2023.
This is a more involved MVC EF Core application built from scratch.

The list of components to be developed, roughly in order are as follows:

VIEW SNIPPETS
View Snippet – Home
	Index (foreach with list of products)
	Details - View Snippet - Details
	Privacy
View Snippet – Cart
	Index
	(Do not implement Order Confirmation, Payment Confirmation and Summary – these are Stripe Integration Related)
View Snippet – Manage Courses
	Index
	Add
	Edit View Snippet – Edit Courses
	Edit View Snippet – Edit Courses
	Delete

PACKAGES
Packages for the Data Access Project
Packages for the Models Project
Packages for the Utilities Project
Packages fro the SigmaWeb Project

PARTIALS
See if any Partial Views Are Needed
ViewImports, See the rest

ESSENTIAL
Setup Program.cs
Setup Appsettings.json
Setup Layout

DATA
Setup ApplicationDbContext.cs

Setup Repository and Unit of Work
	Setup Interfaces
		IRepository, IProductRepository, IShoppingCartRepository, IUnitOfWork, IOrderHeaderRepository, IOrderDetailRepository, IApplicationUserRepository

	Setup Repositories
		Repository, ProductRepository, ShoppingCartRepository, UnitOfWork, OrderHeaderRepository, OrderDetailRepository, ApplicationUserRepository

MODELS
Setup Models – Product (Course)
Setup Models – ShoppingCart
	many to many with Product
	one to one with ApplicationUser
Setup Models - ApplicationUser
Setup Models – (maybe) CoverType
Setup Models – (maybe) OrderDetail
Setup Models – (maybe) OrderHeader

VIEW MODELS
Setup ViewModels - ShoppingCartVM
Setup ViewModels – (maybe) OrderVM
Setup ViewModels – (maybe) ProductVM
Setup Static Details (Utilities)

CONTROLLERS
Controllers and Views – Admin – PRODUCT
	TBD Index, Edit, Remove
Controllers and Views – Customer – HOME
	Index, Details, Details (POST), Privacy
Controllers and Views – Customer – CART
	Index, Plus, Minus, Remove
Controllers and Views - Identity

VIEW COMPONENTS
ViewComponent – ShoppingCartViewComponent
Views/Shared/Components/ShoppingCart/Default

IDENTITY
Scaffold Identity

View Snippet - Login
View Snippet - Register

UNIT TESTING
Unit Test Packages
Unit Tests -  See Bongo Data Access.Tests
Unit Tests -  See Bongo Models.Tests
Unit Tests -  See Bongo Web.Tests
Unit Tests -  See and Review other Core.Test

EF CORE ENHANCEMENTS
EF Core Modifications based on Project
Review EFCore Project

