# Model(Domain)-driven development sample with .Net Core and MongoDB #

### Summary ###
This sample shows base/reference structure and development techniques for implementing model-oriented(DDD) solution with .NET Core.  

Concepts like Entity & Value Object are introduced in a sense of properly modeling your business/domain interfaces & logic by applying Rich Domain Model.

MongoDB is used as persistance store with implementation of core interfaces based on CQS(Command/Query Separation) principle.

 Application layer is also designed around that to have clearance on interfaces, that changes the target domain/model or just serving data results.

### Prerequisites ###
 - In order to run this sample should install MongoDB instance and apply necessary changes in appsettings.json

### Solution ###
Solution | Author(s)
---------|----------
DDD.Sample | Ilker Karimanov

### Version history ###
Version  | Date | Comments
---------| -----| --------
1.0  | August 2016 | Initial release

### Future improvements

- Go from static factory method to abstract factory.
- Fully utilize railway-oriented programming technique.

*PS: Any suggestions will be greatly appreciated*

### Disclaimer ###
**THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.**


----------

# Q/A #
**Q**: What is all about structuring solution in Core/Infrastructure/App/Web folders? 

**A**: Sample is designed to incorporate Onion Architecture concept.

*The fundamental rule is that all code can depend on layers more central, but code cannot depend on layers further out from the core.  In other words, all coupling is toward the center. 
This architecture is unashamedly biased toward object-oriented programming, and it puts objects before all others.*

**Q**: What is the purpose of DDD.Common.CQS?

**A**: That is a core interface with which you can clearly define and organize code around **Command and Query Separation** principle. This way application is designed by implementing separate Query and Command objects to retrieve and modify data, respectively.

**Q**: What is the idea behind Result/FailureResult?

**A**: Both classes implement core interface IResult/IFailure as the main concern is to simplify our return/error handling interface by doing some railway-oriented programming. This way we can define contract that our code will accept one input and output either success/failure result state.

*Examples:*

From     | To
---------|----------
<void, Command> | <Result, Command>
IQuery\<T> | IQuery<Result\<T>>
void | Result


**Q**: What is the idea behind IFinder/IRepository?

**A**: Data-access layer which is designed to clearly separate read & write interface specialization.

**Q**: What is Maybe?

**A**: Maybe is an approach to deal with 'no value' value which is alternative to the concept **null**.

*Examples:*

From     | To
---------|----------
T | Maybe\<T>
IEnumerable\<T> | Maybe\<IEnumerable\<T>>

**Q**: What is DDD.Logging.Mongo?

**A**: Global exception handling of unhandled errors - [repo](https://github.com/ilkerkarimanov/GEH.Sample). This way we can differentiate between code-defined exception like FailureResult and unhandled exception. It is your choice whether you want to implement both cases globally or this way.

# Code Samples #

## Enums vs Value Objects ##
Description:
Sample for handling enum as a value object - *no more mapping to/from something*:

Code snippet:
```C#
public static readonly TodoState Pending = new TodoState("Pending", false);
public static readonly TodoState InProgress = new TodoState("In Progress", false);
public static readonly TodoState Completed = new TodoState("Completed", false);
        
```

## Entity with versioning ##
Description:
Sample for handling created and modified details:

Code snippet:
```C#
    public interface IHasHistory
    {
        DateTime? Created { get; }
        DateTime? Modified { get; }
        void SetAsCreated();
        void SetAsModified();
    }
    ...
        public abstract class HistoricEntity : Entity, IHasHistory
    {
    ...
    }        
```






