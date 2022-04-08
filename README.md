# Test coverage reports
Reports contain information about test coverage for the test and Base endpoints. Coverage is shown as graph charts and tables which maps swagger endpoints to the test references from Test Rail test cases.

# How it works
The test coverage reports are created based on TestRail data (for now only from the Backend project). 
Data flow is as follows:
- New automated test case need to be created
- Write the test you want
- Add test case which will be automated to the correct TestRail project
- Add reference to the test case (in our case this is endpoint we are creating automated test), for example "POST /v1/document/email/quote"
- Make sure that endpoints map contains your reference
- Build the test-reports project, test coverage report will be updated


# Tech stack
- C#
- [Chart.js](https://www.chartjs.org/)
- HTML/JavaScript
