using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            //BonusTwo();
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var user = _context.Users;
            var numberOfUsers = user.ToList().Count();
            Console.WriteLine(numberOfUsers);

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products;


            foreach (Product product in products)
            {
                if (product.Price > 150)
                {
                    Console.WriteLine(product.Name + " " + product.Price);
                }
            }

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var products = _context.Products;
            var productsContainS = products.Where(p => p.Name.Contains("s"));
            foreach (Product product in productsContainS)
            {
                Console.WriteLine(product.Name);
            }

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.

            var users = _context.Users.Where(m => m.RegistrationDate < DateTime.Parse("01/01/2016"));

            foreach (User user in users)
            {
                Console.WriteLine(user.Email + user.RegistrationDate);
            }

            }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            // self-note: Reference for DateTime https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
            var users = _context.Users;
            DateTime date2016 = new DateTime(2016, 01, 01);
            DateTime date2018 = new DateTime(2018, 01, 01);
            var usersRegBtwn2016And2018 = users.Where(u => u.RegistrationDate > date2016 && u.RegistrationDate < date2018);
            foreach (User user in usersRegBtwn2016And2018)
            {
                Console.WriteLine("User Email: " + user.Email + " Registered: " + user.RegistrationDate);
            }

        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            
            var scProductsWithAfton = _context.ShoppingCarts.Include(ur => ur.Product).Include(ur => ur.User).Where(ur => ur.User.Email == "afton@gmail.com");
            
            foreach (ShoppingCart item in scProductsWithAfton)
            {
                Console.WriteLine($"Product: {item.Product.Name} Price: ${item.Product.Price} Quantity: {item.Quantity}"); //self-note: second dollar symbol is used to reflect $299 as price (example)
            }


        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.

            var odaShoppingCartPrice = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();
            
            Console.WriteLine($"Total Price: ${odaShoppingCartPrice}");


        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.
            var employees = _context.UserRoles.Where(ur => ur.Role.RoleName == "Employee").Select(ur => ur.User.Id);
            var scProducts = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => employees.Contains(sc.User.Id));

            foreach (ShoppingCart item in scProducts)
            {
                Console.WriteLine($"Email: {item.User.Email} Product: {item.Product.Name} Price: ${item.Product.Price} Quanity: {item.Quantity}");
            }

        }

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Elden Ring",
                Price = 70,
                Description = "RPG Video Game"
            };

            _context.Products.Add(newProduct);
            _context.SaveChanges();

        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.

            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            var productId = _context.Products.Where(p => p.Name == "Elden Ring").Select(p => p.Id).SingleOrDefault();

            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };

            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();

        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.

            var product = _context.Products.Where(p => p.Name == "Elden Ring").SingleOrDefault();
            product.Price = 60;
            _context.Products.Update(product);
            _context.SaveChanges();


        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.

            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();

        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }


        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var odaEmail = _context.Users.Where(ur => ur.Email == "oda@gmail.com");

            foreach (User user in odaEmail)
            {
                _context.Users.Remove(user);
            }
            _context.SaveChanges();


        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.WriteLine("Enter in your email:");
            string userEmail = Console.ReadLine();
            Console.WriteLine("Enter in your password:");
            string userPassword = Console.ReadLine();

            var userLogin = _context.Users.Where(ur => ur.Email == userEmail && ur.Password == userPassword).SingleOrDefault();
            
            if (userLogin != null)
            {
                Console.WriteLine("Signed In!");
            }
            else
            {
                Console.WriteLine("Invalid Email or Password");
            }
        }
        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.
            var users = _context.Users.ToList();
            int userTotal = 0;
            int sumTotal = 0;

            foreach (User user in users)
            {
                userTotal = 0;
                var userSCTotal = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.UserId == user.Id);
                foreach (ShoppingCart item in userSCTotal)
                {
                    userTotal += (int)item.Product.Price * (int)item.Quantity;
                    sumTotal += (int)item.Product.Price * (int)item.Quantity;
                }
                Console.WriteLine($"User: {user.Email} Total: ${userTotal}");
            }
            Console.WriteLine($"The Grand Total is ${sumTotal}");
        }
        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials

            bool userLoginAttempt = false;

            while (userLoginAttempt == false)
            {
                Console.WriteLine("Do you want to login? [Y/N]");
                string loginPrompt = Console.ReadLine();

                if (loginPrompt == "Y" || loginPrompt == "y" || loginPrompt == "yes")
                {
                    bool loginSuccess = false;

                    while (loginSuccess == false)
                    {
                        Console.WriteLine("Enter in your email:");
                        string userEmail = Console.ReadLine();
                        Console.WriteLine("Enter in your password:");
                        string userPassword = Console.ReadLine();

                        var userLogin = _context.Users.Where(ur => ur.Email == userEmail && ur.Password == userPassword).SingleOrDefault();

                        if (userLogin != null)
                        {
                            Console.WriteLine("Signed In!");
                            loginSuccess = true;

                            Console.WriteLine("Would you like to see available products? Or view your cart? [1] for Products, [2] for Cart");
                            string userOptions = Console.ReadLine();

                            if (userOptions == "1")
                            {
                                var products = _context.Products;
                                foreach (var product in products)
                                {
                                    Console.WriteLine(product.Id + " " + "Product: " + product.Name + " " + "Price: " + product.Price);
                                }

                                Console.WriteLine("Would you like to add anything to your cart? [1] for Yes, [2] for No");
                                string userOptions2 = Console.ReadLine();

                                if (userOptions2 == "1")
                                {
                                    bool cartSatisfaction = false;

                                    while (cartSatisfaction == false)
                                    {
                                        Console.WriteLine("Please select an item from the list of products using its item number to the left of the item");
                                        string userOptions3 = Console.ReadLine();

                                        if (userOptions3 == "1" || userOptions3 == "2" || userOptions3 == "3" || userOptions3 == "4" || userOptions3 == "5" || userOptions3 == "6" || userOptions3 == "7" || userOptions3 == "8")
                                        {
                                            var cartSelection = int.Parse(userOptions3);
                                            var userId = _context.Users.Where(u => u.Email == userEmail).Select(u => u.Id).SingleOrDefault();
                                            var productId = _context.Products.Where(p => p.Id == cartSelection).Select(p => p.Id).SingleOrDefault();

                                            try
                                            {

                                                var userShoppingCart = _context.ShoppingCarts.Where(sc => sc.UserId == userId && sc.ProductId == productId).SingleOrDefault();
                                                userShoppingCart.Quantity += 1;
                                                _context.ShoppingCarts.Update(userShoppingCart);
                                                _context.SaveChanges();

                                                
                                            }

                                            catch
                                            {
                                                ShoppingCart newShoppingCart = new ShoppingCart()
                                                {
                                                    UserId = userId,
                                                    ProductId = productId,
                                                    Quantity = 1
                                                };

                                                _context.ShoppingCarts.Add(newShoppingCart);
                                                _context.SaveChanges();
                                            }
                                            

                                        }

                                        Console.WriteLine("Would you like to add anything else to your cart? [1] for Yes, [2] for No");
                                        string userOptions4 = Console.ReadLine();

                                        if (userOptions4 == "1")
                                        {
                                            cartSatisfaction = false;
                                        }

                                        else if (userOptions4 == "2")
                                        {
                                            cartSatisfaction= true;
                                        }
                                    }

                                    Console.WriteLine("Here is everything in your cart:");

                                    var scProducts = _context.ShoppingCarts.Include(ur => ur.Product).Include(ur => ur.User).Where(ur => ur.User.Email == userEmail);
                                    int scTotal = 0;

                                    foreach (ShoppingCart item in scProducts)
                                    {
                                        scTotal += (int)(item.Quantity * item.Product.Price);
                                        Console.WriteLine($"Product: {item.Product.Id} {item.Product.Name} Quantity: {item.Quantity} Price: ${(item.Product.Price) * (item.Quantity)}"); //self-note: second dollar symbol is used to reflect $299 as price (example)
                                    }

                                    Console.WriteLine($"Total Price: ${scTotal}");

                                    bool cartSatisfaction2 = false;

                                    Console.WriteLine("Would you like to remove anything from your cart? [1] for Yes, [2] for No");
                                    string userOptions5 = Console.ReadLine();

                                    if (userOptions5 == "1")
                                    {
                                        while (cartSatisfaction2 == false)
                                        {
                                            Console.WriteLine("Please select an item from the list of products using its item number to the left of the item");
                                            string userOptions6 = Console.ReadLine();

                                            if (userOptions6 == "1" || userOptions6 == "2" || userOptions6 == "3" || userOptions6 == "4" || userOptions6 == "5" || userOptions6 == "6" || userOptions6 == "7" || userOptions6 == "8")
                                            {
                                                var cartSelection = int.Parse(userOptions6);
                                                var userId = _context.Users.Where(u => u.Email == userEmail).Select(u => u.Id).SingleOrDefault();
                                                var productId = _context.Products.Where(p => p.Id == cartSelection).Select(p => p.Id).SingleOrDefault();
                                                var selectedProduct = _context.Products.FirstOrDefault(p => p.Id == productId);
                                                var userShoppingCart = _context.ShoppingCarts.Where(sc => sc.UserId == userId && sc.ProductId == productId).SingleOrDefault();

                                                if (userShoppingCart.Quantity > 0)
                                                {
                                                    
                                                    userShoppingCart.Quantity -= 1;
                                                    _context.ShoppingCarts.Update(userShoppingCart);
                                                    _context.SaveChanges();
                                                }

                                                else
                                                {     
                                                    _context.ShoppingCarts.Remove(userShoppingCart);
                                                    _context.SaveChanges();
                                                    
                                                    
                                                }

                                                Console.WriteLine("Here is everything in your cart:");

                                                var scUpdatedProducts = _context.ShoppingCarts.Include(ur => ur.Product).Include(ur => ur.User).Where(ur => ur.User.Email == userEmail);
                                                int scUpdatedTotal = 0;

                                                foreach (ShoppingCart item in scUpdatedProducts)
                                                {
                                                    scTotal += (int)(item.Quantity * item.Product.Price);
                                                    Console.WriteLine($"Product: {item.Product.Id} {item.Product.Name} Quantity: {item.Quantity} Price: ${(item.Product.Price) * (item.Quantity)}"); //self-note: second dollar symbol is used to reflect $299 as price (example)
                                                }

                                                Console.WriteLine($"Total Price: ${scUpdatedTotal}");
                                            }
                                        }
                                    }

                                }

                            }

                            else if (userOptions == "2")
                            {

                            }

                        }
                        else
                        {
                            Console.WriteLine("Invalid Email or Password");
                        }
                    }
                    
                }

                else
                {
                    Console.WriteLine("Goodbye!");
                    userLoginAttempt = true;
                }

            }

            //afton@gmail.com
            //AftonsPass123


        }

    }
}
