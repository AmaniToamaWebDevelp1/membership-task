-- Create the users table
CREATE TABLE users (
  id INT AUTO_INCREMENT PRIMARY KEY,
  username VARCHAR(50) NOT NULL,
  email VARCHAR(50) UNIQUE NOT NULL,
  password VARCHAR(255) NOT NULL
);

-- Create the amount register table
CREATE TABLE amount_register (
  id INT AUTO_INCREMENT PRIMARY KEY,
  date DATE NOT NULL,
  amount INT NOT NULL,
  subject VARCHAR(50) NOT NULL,
  user_id INT NOT NULL,
  FOREIGN KEY (user_id) REFERENCES users(id)
);

-- Create the amount distribution table
CREATE TABLE amount_distribution (
  id INT AUTO_INCREMENT PRIMARY KEY,
  amount_70 INT NOT NULL,
  amount_30 INT NOT NULL,
  amount_register_id INT NOT NULL,
  FOREIGN KEY (amount_register_id) REFERENCES amount_register(id)
);
