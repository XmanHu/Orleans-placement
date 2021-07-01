# Orleans-placement

#### Running the sample
From Visual Studio, you can start the Silo, SecondSilo and PlacementSample client projects one by one.

Alternatively, you can run from the command line:

To start the silo
```
dotnet run --project src\Silo
```

To start the second silo (you will have to use a different command window)
```
dotnet run --project src\SecondSilo
```


To start the client (you will have to use a different command window), input different integer for grain primary key.
```
dotnet run --project src\PlacementSample
```