# PriceCalculation
Test project using Vs2013, C#, MVC5, IoC autofac, jQuery, Bootstrap.

Nuget packs (name - version):
Autofac - 3.5.2 
Autofac.Mvc5 - 3.3.4

- Presentation Layer (.Web proj)
It containings the views, controllers and resources as js and css.

View used for the test: Views/Basket/Index
Controller used for the test: Controllers/BasketController
Custom Javascript files added: InputButtons.js and OrderForm.js (under Scripts folder)

In the Global.asax Autofac is used to register interfaces dependencies in the MVC controller and services.

For this test DB is not used so after the services are called they return as out variable an update Basket object instead to call a Repository and store it in a session variable.

- Services (ClassLibrary)

It contains the following classes:
1)IBasketService (interface)
2)BasketService (class implements the IBasket interface)

- Entities (ClassLibrary)

It contains the models structured as base classes and children classes;
The model of the final result is BasketResult.

- Infrastructure (ClassLibrary)

It contains following files:
1)ILogger (It is used as dependency in presentation and service layer)
2)Logger implementing ILogger

- Tests (using Nunit)
It is used to tests given scenarios.
