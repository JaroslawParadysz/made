using System.Net;
using System.Net.Http.Json;
using Made.Application.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Xunit;

namespace Made.Tests.Integration;

public class BatchAllocationTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public BatchAllocationTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        
        _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task HappyPath()
    {
        var batch = new BatchDto()
        {
            SKU = "ExistingSKU",
            Quantity = 120
        };
        
        var dto = new AllocateDto
        {
            SKU = batch.SKU,
            Quantity = 100
        };

        var addBatchPayload = JsonContent.Create(batch);
        await _httpClient.PostAsync("batches", addBatchPayload);
        

        var requestPayload = JsonContent.Create(dto);
        
        var response = await _httpClient.PostAsync("allocate", requestPayload);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}