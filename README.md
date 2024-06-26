# MimeTypesMapper

A simple library for MIME type mapping based on file extensions.

## Table of Contents

- [Introduction](#introduction)
- [Installation](#installation)
- [Usage](#usage)

## Introduction

MimeTypesMapper is a .NET library that provides mapping file extensions to MIME types without overhead.
<br />It includes a small set of predefined MIME types.

## Installation

To install MimeTypesMapper, you can use the NuGet package manager.
<br />Run the following command in the Package Manager Console:

```shell
dotnet add package MimeTypesMapper
```

## Usage

Retrieve MIME types for various file extensions:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MimeTypesMapper.Infrastructure;

class Program
{
    static void Main()
    {
        // Set up dependency injection
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Configure the logger
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        if (loggerFactory != null) MimeTypesLogger.ConfigureLogger(loggerFactory);

        // Example usage
        var mimeTypeHandler = MimeTypesMapper.MimeTypesHandler.Instance;
        mimeTypeHandler.AddOrUpdateMimeType(".example", "application/example");
        Console.WriteLine(mimeTypeHandler.GetMimeType("file.example"));
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Configure logging
        services.AddLogging(config =>
        {
            config.AddConsole();
            config.AddDebug();
        });
    }
}
```

