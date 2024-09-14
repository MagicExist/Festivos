
# Holidays

This project, built in a .NET environment using Onion Architecture and T-SQL, provides a set of stored procedures, methods, and endpoints that allow the user to determine if a specific date is a holiday or not.

## DataBase SetUp
- Go to the Database folder outside the solution project and execute the DDL and DML files.

- Change the default connection string from the appsettings.json file


## API Reference

#### Determine if a date is holiday or not

```http
  GET /api/Festivos
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `date` | `string` | **Required**. Your custom date |

### **HolidayStoredProceduresHandler**
**Description**: This is a static class that has the stored procedures management responsibility
### Methods
####  public static NextMondayDto? **NextMondayProcedure**(DataBaseContext,DateTime CustomDate,Festivo)
Executes a stored procedure that takes a specific date and returns a new date moved to the next Monday.
####  public static NextMondayDto? **NextMondayProcedure**(DataBaseContext,DateTime CustomDate,DateTime PascuaSunday)
This is an overloaded method that takes a DateTime as a third parameter instead of a Holiday.
####  public static DateTime **GetDateByPascuaSunday**(DataBaseContext,DateTime CustomDate,Festivo)
Executes a stored procedure that takes a specific date and returns a new date based on Easter Sunday.

## Authors

- [@Johhan Parra](https://github.com/MagicExist)
- [@Alejandro Berrio](https://github.com/target-id)
## Usage/Examples

#### How to check if a custom date is a holiday
```c#
public async Task<bool> IsHoliday(DateTime date)
{
    NextMondayDto? dateResult;
    _holidayList = dbContext.Festivos.ToArrayAsync(); //Gets the holiday table from the data base
    foreach(var holiday in _holidayList){
        switch(holiday){
            case 1: break;
            case 2:  dateResult = HolidayStoredProceduresHandler.NextMondayProcedure(_ctx,date,holiDay)
            break;
            //Here you can add more cases if you have more holiday types to handle
        }
        //Here you can verify if the dateResult is equals to the custom date, return true and break the loop
    }
    //Here you can return false cause the custome date is not a holiday 
        
}
```

