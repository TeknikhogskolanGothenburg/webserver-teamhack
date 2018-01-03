# .NET Webserver assignment
The assigment should be solved in teams of 3-4 students.

The assignment consist of two parts, a programming part and a theory/docmentation part. Please see the prerequsists section (in the [README.md](README.md)).

The work should be done in the teams github repo which should be public. Whit a [commit early and often](https://blog.codinghorror.com/check-in-early-check-in-often/) ([1](https://sethrobertson.github.io/GitBestPractices/)) strategy, the recomendation is of course only to commit code and documentation that builds.

## Programming part

Implement a webserver using the .NET framework which can read the files from the *content* folder and make it accessible on a local webserver running on port 8080.

When making a request to a resouse at the webserver (eg. a file in the content folder) should the following HTTP header be set to the correct value:

* ContentType, the correct content type of the file (eg. text/html)
* Etag, should be implemented as a MD5 hash of the file content
* Expires, should be set to one year from "now"
* StatusCodes, a usable status code should be returned (eg. 200 OK)
* Cookie, the server should return a cookie (see cookie subsection)

The webserver should also expose two dynamic pages.

### Cookies

The webserver should hold a cookie called *counter*. The cookie should be a session-cookie and for each session count the number of requests to the server.

By requesting [/counter](http://localhost:8080/counter) should the number of requests be shown (and just the number, no formating). 

In the inital request of the session is value of the cookie ofcourse unknown, but it should be returned as part of the response, and this returend cookie value should be used as cookie value for all following requests.

The value of the counter-cookie should be an incremental value starting at "1". So the cookie created for the first session after the server is started has the value "1" and the second new session should have the value "2" etc. 

### Dynamic

A dynamic page should be implemented at [/dynamic](http://localhost:8080/dynamic) it should take two get parameters called *input1* and *input2*. And the output should be the sum of the two inputs.

The page should the request header *Accept* into consideration. As default should the output be formated as HTML and if the Accept-header is set to *application/xml* should the output be formated as XML. 

HTML exampel: ```<html><body>3</body></html>```

XML exampel: ```<result><value>5</value></result>```

## Teory / Documentation part

In the folder DocsSrc is an more or less empty DocFx project. DocFx is compiled documentation written in [markdown](https://guides.github.com/features/mastering-markdown/).

Your assigment is to fill the files:

* articles\HttpVersion2.md
* articles\WebserverArchitecture.md
* articles\SessionHijacking.md

And to build the documentation for the source code to the webserver.

The run the documentation you can see the content of this by opening commandline and navigate to the DocsSrc folder, and write ```docfx .\docfx.json --serve --port 8081```, then open your browser and navigate to [localhost:8081](http://localhost:8081).

Remeber to include references to all resources you use.
