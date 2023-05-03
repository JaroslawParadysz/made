using FluentAssertions;
using Made.Domain;
using Xunit;

namespace Made.Tests;


public class BatchTest
{
    (Batch batch, OrderLine orderLine) Create(string sku, Guid batchReference, Guid orderId, uint batchQuantity, uint orderLineQuantity)
    {
        return (new Batch(batchReference, sku, batchQuantity),
            new OrderLine(sku, orderLineQuantity, orderId));
    }

    [Fact]
    public void ShouldAllocateOrderLine()
    {
        (Batch batch, OrderLine orderLine) = Create("Test-SKU", Guid.NewGuid(), Guid.NewGuid(), 10, 10);

        var canAllocate = batch.CanAllocate(orderLine);
        canAllocate.Should().BeTrue();
    }

    [Fact]
    public void ShouldNotAllocateOrderLine()
    {
        (Batch batch, OrderLine orderLine) = Create("Test-SKU", Guid.NewGuid(), Guid.NewGuid(), 10, 11);
        
        var canAllocate = batch.CanAllocate(orderLine);
        canAllocate.Should().BeFalse();
    }
    
    [Fact]
    public void ShouldDeallocateOrderLine()
    {
        (Batch batch, OrderLine orderLine) = Create("Test-SKU", Guid.NewGuid(), Guid.NewGuid(), 10, 5);
        
        var canAllocate = batch.CanAllocate(orderLine);
        batch.TryAllocate(orderLine);
        batch.Deallocate(orderLine);
        
        canAllocate.Should().BeTrue();
        batch.AvailableQuantity.Should().Be(10);
    }

    [Fact]
    public void AllocationIsIdempotent()
    {
        (Batch batch, OrderLine orderLine) = Create("Test-SKU", Guid.NewGuid(), Guid.NewGuid(), 10, 5);
        batch.TryAllocate(orderLine);
        batch.TryAllocate(orderLine);
        
        batch.AvailableQuantity.Should().Be(5);
    }
}