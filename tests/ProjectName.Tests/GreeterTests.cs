namespace ProjectName.Tests;

public class GreeterTests
{
    [Fact]
    public void Hello_ReturnsGreeting()
    {
        var msg = Greeter.Hello("Fad3");
        Assert.Equal("Hello, Fad3!", msg);
    }
}