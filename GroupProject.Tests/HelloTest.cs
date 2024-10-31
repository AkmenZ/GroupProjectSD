namespace GroupProject.Tests;

public class HelloTest
{
    [Fact]
    public void HelloPrintsMessage()
    {
        // arrange
        var hello = new Hello();

        // act
        var result = hello.PrintHello();

        // assert
        Assert.Equal("Hello from Team 3", result);
    }
}