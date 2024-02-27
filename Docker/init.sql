-- init.sql

-- Insert data into ExpenseCategories table
INSERT INTO "ExpenseCategories" ("Name")
VALUES
  ('Food'),
  ('Travel'),
  ('Entertainment');

-- Insert data into Users table
INSERT INTO "Users" ("Username", "Password") VALUES 
  ('JohnDoe', 'SecurePass123'),
  ('JaneSmith', 'StrongPwd567'),
  ('MikeJohnson', 'Secret1234');

-- Insert data into Expenses table
INSERT INTO "Expenses" ("Name", "Amount", "ExpenseDate", "CategoryId")
VALUES
  ('Lunch', 20.50, '2024-01-30', 1),
  ('Train Ticket', 50.00, '2024-01-29', 2),
  ('Movie Tickets', 25.00, '2024-01-28', 3),
   ('Football Tickets', 45.00, '2024-03-28', 3);


