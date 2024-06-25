# MimeTypesMapper

A comprehensive library for MIME type mapping based on file extensions.

## Table of Contents

- [Introduction](#introduction)
- [Installation](#installation)
- [Usage](#usage)
  - [Basic Usage](#basic-usage)
  - [Adding and Updating MIME Types](#adding-and-updating-mime-types)
  - [Loading MIME Types from a Configuration File](#loading-mime-types-from-a-configuration-file)
  - [Saving MIME Types to a Configuration File](#saving-mime-types-to-a-configuration-file)
- [Contributing](#contributing)
- [License](#license)

## Introduction

MimeTypesMapper is a .NET library that provides a comprehensive way to map file extensions to MIME types. It includes a large set of predefined MIME types and allows for easy addition and updating of custom MIME types. The library is designed to be thread-safe and supports loading and saving MIME types from/to configuration files.

## Installation

To install MimeTypesMapper, you can use the NuGet package manager. Run the following command in the Package Manager Console:

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
        var manager = MimeTypeManager.Instance;

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
        var manager = MimeTypeManager.Instance;

        // Add a new MIME type
        manager.AddOrUpdateMimeType(".custom", "application/custom");
        Console.WriteLine("MIME type for .custom: " + manager.GetMimeType("file.custom"));

        // Update an existing MIME type
        manager.AddOrUpdateMimeType(".pdf", "application/x-pdf");
        Console.WriteLine("Updated MIME type for .pdf: " + manager.GetMimeType("document.pdf"));
    }
}
```

### Loading MIME Types from a Configuration File

Load MIME types from a configuration file (mime_types.csv):

```csharp
using MimeTypesMapper;
using System;

class Program
{
    static void Main()
    {
        var manager = MimeTypeManager.Instance;

        // Load MIME types from a file
        manager.LoadMimeTypesFromFile("mime_types.csv");

        // Check loaded MIME types
        Console.WriteLine("MIME type for .custom: " + manager.GetMimeType("file.custom"));
        Console.WriteLine("MIME type for .txt: " + manager.GetMimeType("document.txt"));
    }
}
```

### Saving MIME Types to a Configuration File

Save the current MIME types to a configuration file (saved_mime_types.csv):

```csharp
using MimeTypesMapper;
using System;

class Program
{
    static void Main()
    {
        var manager = MimeTypeManager.Instance;

        // Save current MIME types to a file
        manager.SaveMimeTypesToFile("saved_mime_types.csv");
        Console.WriteLine("MIME types have been saved to 'saved_mime_types.csv'");
    }
}
```

# Contributing

We welcome contributions to enhance MimeTypesMapper! If you have any ideas, suggestions, or bug reports, please create an issue or submit a pull request. When contributing, please follow these guidelines:

- Fork the repository.
- Create a new branch for your feature or bug fix.
- Write tests for your changes.
- Ensure all tests pass.
- Submit a pull request with a detailed description of your changes.
