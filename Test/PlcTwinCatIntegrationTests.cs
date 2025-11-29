using BeckhoffMiddleware.Controllers;
using BeckhoffMiddleware.Models;
using BeckhoffMiddleware.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeckhoffMiddleware.Tests;
/* 
[TestClass]
public class PlcApiIntegrationTests
{
    private PlcController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        var plcClient = new FakePlcClient();

        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Debug);
        });

        var logger = loggerFactory.CreateLogger<PlcController>();
        _controller = new PlcController(plcClient, logger);
    }

    [TestMethod]
    public async Task ReadVariable_ReturnsValue_FromFakePlc()
    {
        var result = await _controller.ReadVariable("MAIN.myRealVar");

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var body = okResult.Value as PlcReadResponse;
        Assert.IsNotNull(body);

        Assert.AreEqual("MAIN.myRealVar", body!.Symbol);
        Assert.IsNotNull(body.Value);
    }

    [TestMethod]
    public async Task WriteVariable_ThenReadBack_ReturnsUpdatedValue()
    {
        var writeRequest = new PlcWriteRequest { Value = 42.5 };

        var writeResult = await _controller.WriteVariable("MAIN.myRealVar", writeRequest);
        Assert.IsInstanceOfType(writeResult, typeof(NoContentResult));

        var readResult = await _controller.ReadVariable("MAIN.myRealVar");
        var okResult = readResult.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var body = okResult.Value as PlcReadResponse;
        Assert.IsNotNull(body);

        Assert.AreEqual(42.5, Convert.ToDouble(body!.Value));
    }

    [TestMethod]
    public async Task Snapshot_ReturnsRequestedSymbols()
    {
        var request = new PlcSnapshotRequest
        {
            Symbols = new List<string> { "MAIN.myRealVar", "MAIN.myIntVar" }
        };

        var result = await _controller.ReadSnapshot(request);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var body = okResult.Value as PlcSnapshotResponse;
        Assert.IsNotNull(body);

        Assert.IsTrue(body!.Values.ContainsKey("MAIN.myRealVar"));
        Assert.IsTrue(body.Values.ContainsKey("MAIN.myIntVar"));
    }
}
 */