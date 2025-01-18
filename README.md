# YtBookStore

Now upgraded to .net 9.0

## How to run the project

1. clone the project
   git clone https://github.com/rd003/YtBookStore.git
2. open `appsettings.json` file and update connection string's `data source=your server name`
   
   ```json
    "ConnectionStrings": { "conn": "data source=RAVINDRA\\MSSQLSERVER01;initial catalog=YtBookStore;integrated security=true;encrypt=false"
    }
   ```
   
4. Open Tools > Package Manager > Package manager console
5. Run thia command `update-database`
6. Now you can run this project
   
