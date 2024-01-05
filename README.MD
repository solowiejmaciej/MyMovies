# MyMovies Application

This is a guide on how to set up and run the MyMovies application using Docker Compose.

## Prerequisites

Before you begin, ensure you have the following installed on your machine:

- Docker
- Docker Compose

## Steps to Run the Application

* **Clone the repository**

   Use the following command to clone the repository:

   ```bash
   git clone https://github.com/solowiejmaciej/MyMovies.git
   ```
* **Run the application**

   Use the following command to run the application:

   ```bash
   docker-compose up --build
   ```

### Api will be available at http://localhost:8080
### There is also a Swagger UI available at http://localhost:8080/swagger/index.html
### Client application will be available at http://localhost:8081

* **Stop the application**

    To stop the application, press `Ctrl+C` in the terminal where you started the application.

## Database

* **Database**

  The application uses a Mssql database. The database is created automatically when the application is started.

## Additional information

This was a recruitment task for the position of Junior developer

## Authors
* Maciej Sołowiej - [solowiejmaciej](mailto:solowiejmaciej@gmail.com)