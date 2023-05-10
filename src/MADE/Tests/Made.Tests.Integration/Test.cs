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
        var orderDto = new OrderDto()
        {
            Id = Guid.NewGuid()
        };
        
        var batchDto = new BatchDto()
        {
            SKU = "ExistingSKU",
            Quantity = 120
        };
        
        var dto = new AllocateDto
        {
            SKU = batchDto.SKU,
            Quantity = 100,
            OrderId = orderDto.Id
        };

        var addOrderPayload = JsonContent.Create(orderDto);
        await _httpClient.PostAsync("orders", addOrderPayload);
        
        var addBatchPayload = JsonContent.Create(batchDto);
        await _httpClient.PostAsync("batches", addBatchPayload);
        

        var requestPayload = JsonContent.Create(dto);
        
        var response = await _httpClient.PostAsync("allocate", requestPayload);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}