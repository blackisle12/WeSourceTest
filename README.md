# WeSourceTest

Hey guys, before running the application make sure you have the following:

1. Visual Studio 2019 
2. SQL Server 2016 or higher
3. .Net Core 3.0 runtime

Instructions:

1. Open the solution in Visual Studio (Run as Administrator is recommended)
2. Open 'Package Manager Console'
3. Run 'update-database' on the terminal, make sure the Default Project is set to 'WeSourceTest'
4. Run the application (F5, Play button or right-click on web project then select 'View in Browser'

Now that we have launched the application, please follow these steps to generate all the necessary data:

1. Please change url address to {base}/api/fixer/2019-10-01/2019-10-31
  - these endpoint will pull the data from 'Fixer' api and save it to db
  - CURRENTLY THIS ENDPOINT IS NOT WORKING BECAUSE '/timeseries' IS NOT AVAILABLE FOR FREE LICENSE

2. Please change url address to {base}/api/csv/parsedata
  - these endpoint will parse the 3 csv files and save it to db
  
3. Please change url address to {base}/api/data/mergedatawithrates
  - these endpoint will merge the data from step '1' and '2' and save it to db
  
-- to be continue (pending until issue on 'fixer/api/timeseries' is resolved)
