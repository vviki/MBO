### General:

* Project is written in C# language, .net core version 2.1 is used.
* Project can be build by typing "dotnet publish -o output_directory" (output_directory can be any not yet existing directory name) inside main solution directory (the one containing MarvelBO.sln file) on both Windows and Linux systems with .net core framework installed ([Linux](https://docs.microsoft.com/en-us/dotnet/core/linux-prerequisites?tabs=netcore2x), [Windows](https://docs.microsoft.com/en-us/dotnet/core/windows-prerequisites?tabs=netcore2x)).



### Api application: 

* To run rest api application local standard installation of Redis is required ([Linux](https://redis.io/topics/quickstart), [Windows](https://github.com/ServiceStack/redis-windows)). 
* To run rest api application on Linux also Apache server needs to be installed and .net core framework needs to be configured to work with it ([howto](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-apache?view=aspnetcore-2.1)).
* Application can be started by typing "dotnet MarvelBO.Api.dll" inside main_solution_directory/MarvelBO.Api/output_directory/ (output_directory is the one created by "publish" method). This is how it is currently deployed and running on AWS ([Creators](http://ec2-52-15-204-100.us-east-2.compute.amazonaws.com/api/creators) or [Notes](http://ec2-52-15-204-100.us-east-2.compute.amazonaws.com/api/notes) can be used for verification). 
* To run rest api application on Windows, IIS Express shipped with Visual Studio can be used (by pressing F5 when solution is loaded) or it can be deployed to regular [ISS server](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-2.1). 
* MarvelApi public key, private key and url need to be placed in MarvelClientSettings section of MarvelBO.Api/output_directory/appsettings.json file. There is also ImportLimit parameter used for limiting data import from  MarvelApi.
* All requests and responses are in Json (two additional parameterless Get methods exist: /api/creators and /api/notes that do not have any request types). 
* Listing creators is provided on: /api/creators
* Comparing creators is provided on: /api/creatorsComparison
* Sorting both ascending and descending can be applied on all attributes on both Creators and Notes lists. 
* Filtering by text attributes (creator's name and note) is done by checking if text provided as a filter can be found inside their content, i.e. if it is their substring. 
* Filtering by number and date attributes has three options: instances with attributes that are either equal to, bigger than or smaller than provided value can be selected. 
* Although MarvelApi provides some sorting and filtering functionality, filtering and sorting by number of comics and number of series is not provided and has to be implemented by the tool itself. In order to do that entire list of creators need to be imported from MarvelApi (although it can be limited by ImportLimit configuration parameter, currently instance deployed on AWS imports only 500).
* To cache imported list of creators Redis store is used. Creators cache has 24 hours expiration period, which is compliant with MarvelApi team's use guidelines. 
* Readers-writers problem for accessing the creators cache and notes store is solved using ReaderWriterLockSlim class available in System.Threading standard .net library.
* Listing notes is provided on: /api/notes
* Manipulating notes is provided on: /api/noteOperations
* For notes manipulation functionality: adding, updating and removing http methods: post, put and delete are used respectively.
* Notes are stored inside Redis without any expiration period.
* For serialization and deserialization of notes and creators for Redis store [NewtonSoft.Json](https://www.newtonsoft.com/json) library is used.
* For accessing Redis store [StackExchange.Redis](https://stackexchange.github.io/StackExchange.Redis/) library is used.
* For consuming MarvelApi [RestSharp](http://restsharp.org/) library is used. 
* For simple unit tests of rest api [xUnit](https://xunit.github.io/) framework is used with [Moq](https://github.com/moq/moq) library. 
* Unit tests can be performed by typing "dotnet test" inside main solution directory.



### Client console application:

* Client console application can be run on both Linux and Windows by typing "dotnet MarvelBO.ApiClientConsole/output_directory/MarvelBO.ApiClientConsole.dll" (output_directory is the one created by "publish" method) inside main solution directory. 
* For managing commands [Mono.Options.Core](https://github.com/mParticle/Mono.Options.Core) library is used.
* For consuming rest api [RestSharp](http://restsharp.org/) library is used.
* For serializing api requests [NewtonSoft.Json](https://www.newtonsoft.com/json) library is used.
* List of commands and parameters can be seen by using -h or --Help option, that shows:

```
Usage:
Example:  -L -c=5 -i=">4000" -o=-note,comics,name,-date,-id -N -C -a=8764 -b=
8571 -A -d=11122 -x="New Note!!!" -U -D
Options:
  -h, --Help                 Show this message and exit.
  -H, --Host=VALUE           Set remote host address. Example:
                                -H="http://ec2-52-15-204-100.us-east-2.compute.
                               amazonaws.com"
  -L, --List                 List Creators.
  -N, --Notes                List Notes.
  -o, --order=VALUE          Order by string, possible any coma separated
                               combination of values and '-' for descending.
                               Values for Creators: id, name, date, comics,
                               series, note.
                               Values for Notes: id, name, note.
                               E.g.: -o=-note,comics,name,-date,-id
  -i, --id=VALUE             Filter by id.
  -n, --name=VALUE           Filter by name.
  -t, --note=VALUE           Filter by note.
  -s, --series=VALUE         Filter by series.
  -c, --comics=VALUE         Filter by comics.
  -m, --modified=VALUE       Filter by modified date, e.g.: -m=">2007-01-02"
  -C, --Compare              Compare Creators.
  -a, --first=VALUE          First Creator Id.
  -b, --second=VALUE         Second Creator Id.
  -A, --Add                  Add Note.
  -U, --Update               Update Note.
  -D, --Delete               Delete Note.
  -d, --iD=VALUE             Id for notes operations.
  -x, --text=VALUE           Text for notes operations.



```

