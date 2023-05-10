#How to create and run ef core migration
`
dotnet ef migrations add InitialCreate --project Made.Infrastructure --startup-project Made.Api
dotnet ef database update 
`