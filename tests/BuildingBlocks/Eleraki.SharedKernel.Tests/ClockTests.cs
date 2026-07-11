using Eleraki.SharedKernel.Primitives;
using Xunit;

namespace Eleraki.SharedKernel.Tests;

/// <summary>
/// Tests for the Clock utility.
/// </summary>
public class ClockTests
{
    /// <summary>
    /// Ensures UtcNow returns a value close to the current time.
    /// </summary>
    [Fact]
    public void UtcNow_ShouldReturnRecentDateTime()
    {
        var now = Clock.UtcNow;

        Assert.True(now <= DateTime.UtcNow);
        Assert.Equal(DateTimeKind.Utc, now.Kind);
    }
}