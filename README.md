# Ecommerce App

*Authors: Na'ama Bar-Ilan, Peyton Cysewski*

----

## The Outdoor Store
Link: https://theoutdoorstore.azurewebsites.net/

## Description
This program is an ECommerce mock website built out over several weeks, adding new features each day. The process of making it will serve as a learning opportunity to understand the ins and outs of an online product retail service.

### ECommerce Products
Our fictional ECommerce store is called 'The Outdoor Store' and it sells a variety of outdoor gear. Right now we are specializing in outdoor climbing bags at a wide range of styles and prices.

---

### Getting Started
Clone this repository to your local machine.

```
$ git clone https://barilannaama@dev.azure.com/barilannaama/ECOM/_git/ECOM
```

### To run the program from Visual Studio:
Select ```File``` -> ```Open``` -> ```Project/Solution```

Next navigate to the location you cloned the Repository.

Double click on the ```Ecommerce-App``` directory.

Then select and open ```Ecommerce-App.sln```

---

### Technical Details

#### Store DB Schema

![StoreDbERD](/Assets/ERD)

1 **Carts**
	* A simple model. 
	* It has a UserId FK and an Active status that can be either true or false. 
	* The Navigation Properties are CartItems. 
	* It has a one to many relationship with the CartItems table. 

2 **CartItems**
	* A join table with payload. 
	* It has two FK that act as CK: CartId and ProductId. 
	* The Navigation Properties are Cart and Product. 
	* It has a many to one relationship with the Cart table.
	* It has a many to one relationship with the Products table. 
	* It has a many to one relationship with the Orders table. 

3 **Products**
	* A simple model. 
	* It holds all basic product information. 
	* The Navigation Properties are CartItems. 
	* It has a one to many relationship with the CartItems table. 

4 **Orders**
	* A simple model. 
	* It has two FK: UserId and CartId, but does not access them directly. 
	* It holds all Order information. 
	* The Navigation Properties are CartItems. 
	* It has a one to many relationship with the CartItems table. 

#### Athentication
Users are authenticated by the submission of their email and password located in a form on the Login page.

#### Authorization
- Anonymous Users: All anonymous users can view the Home, Products, About Us, Login, and Register pages.
- Signed-in Users: Once a user is registered and subsequently logs in, they receive a personalized greeting and an option to log out. Their greeting is created using claims for their first and last name upon registration.
- Admins: They have the same accessibility as a signed-in user and they can also access and view the Dashboard page and upload or update a new image for an existing product. Their role as an admin is added to their claim upon creation/seeding of the database.
	- Admin Access Instructions:
		- Go to the App's Login Page
		- Enter the test Admin permission: email: admin@gmail.com, PW: @Admin123!
		- The admin-only "Dashboard" tab should now be visible in the left section of the Nav bar. 



---

### Change Log
1.2: *Sprint 2* - 24 August 2020
1.1: *Sprint 1* - 16 August 2020
1.0: *Initial Release, Lab 26* - 8 August 2020