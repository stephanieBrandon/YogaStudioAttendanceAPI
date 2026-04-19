- Separate ASP.NET Core Web API in the same Visual Studio solution
- Handles clock in and clock out only
- JWT Bearer authentication — validates tokens issued by main app on login
- Shares the same Oracle database — no migrations run from this project
- CORS configured to allow requests from main MVC app only
- Both projects must run simultaneously for clock in/out to work

Port configuration (based on launchSettings.json):
Main MVC app: https://localhost:7209 / http://localhost:5130
AttendanceAPI: https://localhost:7219 / http://localhost:5225
