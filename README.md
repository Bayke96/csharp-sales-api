# csharp-sales-api

RESTful Sales / Inventory CRUD API created using C# and Entity Framework 6.0

Version 1.0, it contains the following operations:

-   Create
-   Read
-   Update 
-   Delete

For categories, products in the inventory and sales done of such items.

It includes daily and monthly statistics of the sales done.

ROUTES:

-   Categories

   GET:	
	
	/inventory/categories : Lists all of the existing categories,
	
	/inventory/categories/id : Returns a category by searching for its id,
	
	/inventory/categories/name/{name} : Returns a category by searching for its name,
	
	/inventory/categories/{order}/{orderby} : Lists all of the existing categories by Ascending or Descending order and by id, name or price.
	
 Available  Params :
	
	Order: "asc", "desc".
	
	OrderBy: "id", "name", "ammount".
	
   POST:	
	
	/inventory/categories : Creates a new category.
	
   PUT:
	
	/inventory/categories/{id} : Updates an existing category based on its ID.
	
   DELETE:
	
	/inventory/categories/{id} : Deletes an existing category based on its ID.

-   Products

   GET:	
	
	/inventory/products : Lists all of the existing products,
	
	/inventory/products/id : Returns a product by searching for its id,
	
	/inventory/products/name/{name} : Returns a product by searching for its name,
	
	/inventory/products/{order}/{orderby} : Lists all of the existing products by Ascending or Descending order and by id, name, description, price 
	or ammount existing in the inventory.
	
	 Available Params:
	
	Order: "asc", "desc".
	
	OrderBy: "id", "name", "description", "price", "ammount".
	
   POST:	
	
	/inventory/products : Creates a new product.
	
   PUT:
	
	/inventory/products/{id} : Updates an existing product based on its ID.
	
   DELETE:
	
	/inventory/products/{id} : Deletes an existing product based on its ID.

-   Sales

      GET:
	
	/sales : Lists all of the existing sales,
	
	/sales/id : Returns a sale by searching for its id,
	
	/sales/{order}/{orderby} : Lists all of the existing products by Ascending or Descending order and by id, sales date, sales description or sales total price.
	or ammount existing in the inventory.
	
	-   Available Params:
	
	Order: "asc", "desc".
	
	OrderBy: "id", "date", "description", "total".
	
   POST:	
	
	/sales : Creates a new sale.
	
   PUT:
	
	/sales/{id} : Updates an existing sale based on its ID.
	
   DELETE:
	
	/sales/{id} : Deletes an existing sale based on its ID.

-   Daily Sale Stats

      GET:	
	
	/salestats/dailystats : Lists all of the existing daily sales stats,
	
	/salestats/dailystats/id : Returns a single daily sale stats by searching for its id.
	
      POST:	
	
	/salestats/dailystats : Creates a new daily sales stats.
	
      PUT:
	
	/salestats/dailystats/{id} : Updates an existing daily sales stats based on its ID.
	
      DELETE:
	
	/salestats/dailystats/{id} : Deletes an existing daily sales stats based on its ID.

-   Monthly Sale Stats

      GET:	
	
	   /salestats/monthlystats : Lists all of the existing monthly sales stats,
	
	   /salestats/monthlystats/id : Returns a single monthly sale stats by searching for its id.
	
      POST:	
	
	   /salestats/monthlystats : Creates a new monthly sales stats.
	
      PUT:
	
	   /salestats/monthlystats/{id} : Updates an existing monthly sales stats based on its ID.
	
      DELETE:
	
	   /salestats/monthlystats/{id} : Deletes an existing monthly sales stats based on its ID.
