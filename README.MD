Running the Backend API with Docker Compose (Visual Studio)
Follow these steps to build and run the backend API using Docker Compose:

1. Open Command Prompt
cd <path-to-solution-folder>
Replace <path-to-solution-folder> with the actual folder containing your docker-compose.yml file.

2. Build the Docker containers
docker-compose build
This builds all defined services (e.g., the .NET API) into Docker images.

3. Run the containers
docker-compose up
This starts the backend API (and any other services defined) using Docker containers.

 The backend will be accessible at:

http://localhost:5001 

4. Stop the containers
docker-compose down
This will stop and remove the running containers.

Optional: Test with Swagger
You can access Swagger UI to explore the API once it’s running:

# run at  http://localhost:5001/swagger/index.html
