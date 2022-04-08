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

# Build flow
Test-reports project is building automatically with every change via GitLab pipeline, HTML files are created from a template that can generate correct reports depending on passed data.
Final report is displayed as GitLab pages and is available [here](https://test.gitlab.io/Base/test-reports/index.html).
For more info visit notion [page](https://www.notion.so/testBase/Test-coverage-reports-9c1926903a844bb1ab48108b39e242f7)

# Tech stack
- C#
- [Chart.js](https://www.chartjs.org/)
- HTML/JavaScript
