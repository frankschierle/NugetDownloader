# NugetDownloader
A simple tool to download a NuGet package including all of its dependencies.

## Usage of NugetDownloader command line tool
```
Usage: NugetDownloader.exe DownloadTargetDirectory NuGetPackageID [NuGetPackageVersion]
```

### Examples

Download the latest version of NuGet package Microsoft.CodeAnalysis to directory C:\Downloads:

```
NugetDownloader.exe "C:\Downloads" Microsoft.CodeAnalysis
```

Download a specific version of NuGet package Microsoft.CodeAnalysis to directory C:\Downloads:

```
NugetDownloader.exe "C:\Downloads" Microsoft.CodeAnalysis 2.1.0
```

## Use NugetDownloader in your own software
The core functionality of NugetDownloader is available using the ``NugetDownloader.Core`` assembly. Add the appropriate reference to your project and you can use the library as follows:

```C#
// The url factory is required to get the download url of a NuGet package.
// Implement your own realization of IUrlFactory to download the packages from
// somewhere else than nuget.org.
var urlFactory = new NugetOrgUrlFactory();

// The PackageDownloader is used to download a single NuGet package.
var packageDownloader = new PackageDownloader(urlFactory);

// The PackageDependencyResolver is used to get all dependencies of a NuGet package.
var dependencyResolver = new PackageDependencyResolver();

// The RecursivePackageDownloader uses all the objects instantiated above
// to download a NuGet package and all of its dependencies.
var recursivePackageDownloader = new RecursivePackageDownloader(packageDownloader, dependencyResolver);

// Use the RecursivePackageDownloader to download the latest version
// of the NuGet package Microsoft.CodeAnalysis.
var packageToDownload = new PackageId("Microsoft.CodeAnalysis", null);
recursivePackageDownloader.DownloadPackageAndAllDependencies(packageToDownload, @"C:\MyDownloadDirectory");

// To download a specific version of the NuGet package, just pass it as
// second parameter to the PackageId constructor.
packageToDownload = new PackageId("Microsoft.CodeAnalysis", "2.1.0");
recursivePackageDownloader.DownloadPackageAndAllDependencies(packageToDownload, @"C:\MyDownloadDirectory");
```

## Requirements

* .NET Framework 4.6 or higher.
  * It shouldn't be a problem to recompile the source code with an older version of .NET.

## Known issues

* [Version ranges](https://docs.microsoft.com/en-us/nuget/create-packages/dependency-versions#version-ranges) are currently not supported. A version range specified in a NUSPEC file causes an exception.
* Target frameworks are currently not supported. The tool is downloading all dependencies for all target frameworks specified in a NUSPEC file.
* Unit tests are missing.
