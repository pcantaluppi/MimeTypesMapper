# MimeTypesMapper

A comprehensive library for MIME type mapping based on file extensions.

## Table of Contents

- [Introduction](#introduction)
- [Installation](#installation)
- [Usage](#usage)
  - [Basic Usage](#basic-usage)
  - [Adding and Updating MIME Types](#adding-and-updating-mime-types)
- [Contributing](#contributing)
- [License](#license)

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

### Basic Usage

Retrieve MIME types for various file extensions:

```csharp
using MimeTypesMapper;
using System;

class Program
{
    static void Main()
    {
        var manager = MimeTypesHandler.Instance;

        // Retrieve MIME type from filename
        Console.WriteLine("MIME type for .pdf: " + manager.GetMimeType("document.pdf"));
    }
}
```

### Adding and Updating MIME Types

Add a new MIME type or update an existing one:

```csharp
using MimeTypesMapper;
using System;

class Program
{
    static void Main()
    {
        var manager = MimeTypesHandler.Instance;

        // Add a new MIME type
        manager.AddOrUpdateMimeType(".custom", "application/custom");
        Console.WriteLine("MIME type for .custom: " + manager.GetMimeType("file.custom"));

        // Update an existing MIME type
        manager.AddOrUpdateMimeType(".pdf", "application/x-pdf");
        Console.WriteLine("Updated MIME type for .pdf: " + manager.GetMimeType("document.pdf"));
    }
}
```

