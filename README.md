# Challenge

The purpose of this test is to understand the capabilities both on backend server
programming using:

* C#
* ASP.NET 
* Web API 
* SQL server, 

Frontend, with client side technologies: 

* VUE.JS
* typescript
* Tailwindcss 

For this test, you’ll be required to develop:

* simple photo upload application for a tailored t-shirt application. 
In this example, we will consider that a t-shirt can be customized on 2 variations:

* Color - the color of the t-shirt.

* Fabric - the type of cloth used.
A variable number of Images can be uploaded for each combination of Color+Fabric.


## The application will contain

* A page where you will list the existing items (the application doesn’t need to create
new items, just use the ones on the database), how many colors and fabrics are
associated with that item, and how many images exist in total. There should be an
edit photo button that will open the upload page.

* The upload page - where you can upload the images for a specific item.
Your page will list the item id and name, and a grid that will be filled with the
configured database product configuration of colors and fabric.
For each combination, you should be able to add any number of images, and should
display an “add” button that will open a popup to add the new image.

* Any images that have been uploaded, will have an overlay button that will remove the
image.

* Your solution should take into consideration best practices, using Entity Framework to
manipulate database data, and also look into the best performant solution.


## Delivery

You should send us a git repo to clone containing a working .NET solution, with all the
assets necessary to run the project.
Please provide the database creation scripts, alongside with any initialization data
necessary. You can use a code-first or database-first approach.

You will be evaluated in:


### Criteria Weight

* Design patterns 8

* Correct usage of the entity framework, following best practices 8

* Understanding of .NET libraries 8

* Understanding of ASP.NET Web API 8

* Usage of javascript/typescript to retrieve data and populate the front-end 7

* Vue.JS understanding / DOM manipulation 7

* .Net Project Structure 6

* Git knowledge 7


### Get started

## Backend
```bash
cd backend

# Start database
# Open Docker Desktop and after execute the command below
docker-compose up --build 

# Create database
dotnet ef database update

# Insert SQL register in database
INSERT INTO "Products" ("Name", "Price", "Settings") VALUES
('Gorillaz t-shirt', 10.99, '{"fabrics":[{"name":"silk","colors":[{"name":"white","images":[]},{"name":"yellow","images":[]},{"name":"green","images":[]}]}, {"name":"linen","colors":[{"name":"white","images":[]},{"name":"yellow","images":[]},{"name":"green","images":[]}]}]}'),
('Abba t-shirt', 25.50, '{"fabrics":[{"name":"silk","colors":[{"name":"white","images":[]},{"name":"yellow","images":[]},{"name":"green","images":[]}]}, {"name":"linen","colors":[{"name":"white","images":[]},{"name":"yellow","images":[]},{"name":"green","images":[]}]}]}'),
('DaftPunk t-shirt', 5.75, '{"fabrics":[{"name":"silk","colors":[{"name":"white","images":[]},{"name":"yellow","images":[]},{"name":"green","images":[]}]}, {"name":"linen","colors":[{"name":"white","images":[]},{"name":"yellow","images":[]},{"name":"green","images":[]}]}]}');

# Start server
dotnet clean
dotnet build
dotnet run
```

## Frontend
```bash
cd frontend

# install dependencies
npm i

# run application in dev mode
npm run dev
```

## Migration

If create more models execute `dotnet ef migrations add InitialCreate` to update the database models.