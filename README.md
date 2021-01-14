

# TODO in .NET Core 5.0

![Picture of app](https://i.imgur.com/uNioowG.jpg)

## About

This is a simple-to-use todo app that lets users create and collaborate on todo lists. 

### Tech:

It uses a number of open-source projects to work properly:

- ASP.NET Core 5.0
- Entity Framework Core
- SQL Server Express LocalDB

UI libraries and controls:

- [TailwindCSS](https://tailwindcss.com/)

- [flatpickr](https://github.com/flatpickr/flatpickr) 

- [jquery](http://jquery.com)  

  

## Getting started

### Prerequisites

- [.NET 5.0 SDK or higher](https://dotnet.microsoft.com/download/dotnet/5.0)

- [SQL Server Express LocalDB](http://www.hanselman.com/blog/download-sql-server-express)

- An IDE or Editor of your choice

  

```sh
> git clone https://github.com/oskvr/TodoApp.git TodoApp
> cd todo.core
> cd dotnet ef database update
> dotnet run
```

Open a browser and navigate to https://localhost:5000 to see the application running.

For people running Visual Studio 2019 most of the above steps will be handled by the IDE. Just get the source code, open the `.sln` file, run `Update-Database` in Package Manager Console and you should be ready to go. 

### Additional information

Modifications to the Tailwind [configuration](https://tailwindcss.com/docs/configuration) will require [Node.js](https://nodejs.org/) for rebuilding the Tailwind CSS file:

```sh
> npm run build
```

Also consider checking out [optimizing Tailwind for production](https://tailwindcss.com/docs/optimizing-for-production) to remove any unused CSS classes. 

## Roadmap

- [ ] Add AJAX calls to task actions to minimize full page refreshes
- [ ] Make UI mobile responsive
- [ ] Allow users to edit lists and tasks
- [ ] Add notifications on list invites

## Architecture

![7ukkadi.jpg (1302×485) (imgur.com)](https://i.imgur.com/7ukkadi.jpg)

## License

MIT
