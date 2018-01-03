# .NET Webserver assignment

This repository contains what is needed to solve the first assigment in the couse "Webbutveckling i ramverket för .NET" ("Webdevelopment with the .NET framework") winter 2018 at [Teknikhögskolan Gothenburg](https://github.com/TeknikhogskolanGothenburg).

The assigment should be solved in teams of 3-4 students.

The assignment consist of two parts, a programming part and a theory/docmentation part. Please see the prerequsists section.

## Code assignment

Implement a webserver using the .NET framework which can read the files from the *content* folder and make accessible on a local webserver.

ContentType
Etag md5 of content
Expires in one year
Status codes
Cookie counter

Also should the webserver make two dynamic pages avaible:
/dynamic
Accept : xml + text
get parameters
/counter

## Teory / Documentation

In the folder DocsSrc is an more or less empty DocFx project. DocFx is compiled documentation written in [markdown](https://guides.github.com/features/mastering-markdown/).

Your assigment is to fill the files:

* articles\HttpVersion2.md
* articles\WebserverArchitecture.md

And to build the documentation for the source code to the webserver.

The run the documentation you can see the content of this by opening commandline and navigate to the DocsSrc folder, and write ```docfx .\docfx.json --serve --port 8081```, then open your browser and navigate to [localhost:8081](http://localhost:8081).

## Prerequsists

You will need the following tools:

* Visual Studio 2017 (Community edition is fine).
* A git client ([Git](https://git-scm.com/), [GitHub](https://desktop.github.com/), [SourceTree](https://www.sourcetreeapp.com/)), there is also some git support in Visual Studio.
* [DocFx](https://dotnet.github.io/docfx/index.html)

## License
Unlicensed, see LICENSE file for the details.

Feel free to copy and use this material, but I would ofcourse be happy to recive an acknowlage or feedback.