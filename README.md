# EndToEndServiceFabric
Example of how to spin up a Service Fabric application for testing end-to-end.

This links with this [blog article](http://kevsoft.net/2017/01/21/end-to-end-testing-with-service-fabric.html).

## What's in the repository?

### Projects

#### MyStatelessService

This project includes a stateless service that hosts an web api project that exposes a endpoint that can calculate the multiplication of 2 numbers:

```
/multiplication?a=10&b=10

100
```

#### EndToEndServiceFabric

This is the Service Fabric application that pulls in the `MyStatelessService` project.

#### EndToEndServiceFabric.FunctionalTests

This project includes the NUnit setup and tests around the `EndToEndServiceFabric` application. there a 2 lots of tests ones to check the status of health of the application and services, and one to check the exposed multiplication endpoint.
