TwinCATBridge

TwinCATBridge is a lightweight .NET 8 middleware that provides a REST API interface for Beckhoff TwinCAT PLCs.
It abstracts PLC communication using ADS.NET, enabling seamless integration of PLC data with external systems and analytics platforms.

------------------------------------------------------------
Features
------------------------------------------------------------

- Communicates with Beckhoff TwinCAT PLCs via ADS.NET
- Provides a REST API to read and write PLC variables
- Supports:
  - AdsPlcClient – real PLC communication
  - FakePlcClient – in-memory mock for development and tests
- Includes automated integration tests using MSTest
- Configurable via appsettings.json or environment variables

------------------------------------------------------------
Running the App (Fake PLC mode)
------------------------------------------------------------

On macOS or Linux:

    dotnet restore
    dotnet run

Then open in your browser:
    https://localhost:5001/swagger

Example request:

    curl https://localhost:5001/api/plc/variables/MAIN.myRealVar -k

Example response:
    {
      "symbol": "MAIN.myRealVar",
      "value": 25.0
    }

------------------------------------------------------------
Running Tests
------------------------------------------------------------

    dotnet test Test/BeckhoffMiddleware.Tests.csproj

Expected output:
    Test summary: total: 3, failed: 0, succeeded: 3

------------------------------------------------------------
Connecting to a Real TwinCAT PLC (Windows)
------------------------------------------------------------

1. Install TwinCAT 3 XAE on Windows and set it to RUN mode.
2. Update appsettings.json with your PLC configuration:

    {
      "AdsPlc": {
        "AmsNetId": "YOUR_AMS_ID",
        "IpAddress": "192.168.1.25",
        "Port": 851
      }
    }

3. In Program.cs, switch to the real client:

    builder.Services.AddSingleton<IPlcClient, AdsPlcClient>();

4. Run the app and access PLC data through the REST API.

------------------------------------------------------------
License
------------------------------------------------------------

MIT License © 2025 Shaileshkumar Bhadani
