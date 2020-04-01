# Hospital Capacity App

This will be an application for hospitals to be able to see the various capacity of surrounding hospitals. The hope
is that this will help the to balance out capacity across all hospitals within a region.

I'm intentionally trying to keep the UX very simple to ease adoption.  I am assuming that most of the potential users
will be very busy and only wanting a easy mechanism to update and see bed capacity.

## Running the application locally

1. Create a local empty database.
2. Add the connection string to the `ConnectionStrings.Database` key in the secrets file for the `/src/Gah.HC.Spa` project.
3. Run `dotnet ef database update -s ../../Gah.HC.Spa/Gah.HC.Spa.csproj` from the `/src/app/Gah.HC.Repository.Sql` folder.

## Run from Visual Studio

1. Cone the repository
2. Run `npm install` in the `src/Gah/Hc/Spa/ClientApp` folder.
3. Open the solution in Visual Studio 2019
4. Press the play button.

## Run from command line
1. Cone the repository
2. Run `npm install` in the `src/Gah/Hc/Spa/ClientApp` folder.
3. Run `dotnet run` in the `src/Gah.HC.Spa` project folder.
