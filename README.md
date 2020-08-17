# Ecommerce App

*Authors: Na'ama Bar-ilan, Peyton Cysewski*

----

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

#### Athentication
Users are authenticated by the submission of their email and password located in a form on the Login page.

#### Authorization
- Anonymous Users: All anonymous users can view the Home, Products, About Us, Login, and Register pages.
- Signed-in Users: Once a user is registered and subsequently logs in, they receive a personalized greeting and an option to log out. Their greeting is created using claims for their first and last name upon registration.
- Admins: They have the same accessiblity as a signed-in user and they can also access and view the Dashboard page and upload or update a new image for an existing product. Their role as an admin is added to their claim upon creation/seeding of the database.

---

### Change Log
1.1: *Sprint 1* - 16 August 2020
1.0: *Initial Release, Lab 26* - 8 August 2020