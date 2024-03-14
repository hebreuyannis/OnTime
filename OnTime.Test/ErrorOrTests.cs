namespace Test;

public class ErrorOrTests
{
    [Fact]
    public void CreateFromFactory_WhenAccessingValue_ShouldReturnValue()
    {
        // Arrange
        IEnumerable<string> value = new[] { "value" };

        // Act
        ErrorOr<IEnumerable<string>> errorOrPerson = ErrorOrFactory.From(value);

        // Assert
        errorOrPerson.IsError.Should().BeFalse();
        errorOrPerson.Value.Should().BeSameAs(value);
    }

    [Fact]
    public void CreateError_ShouldReturnFailureError()
    {
        // Arrange
        IEnumerable<string> value = new[] { "value" };

        // Act
        ErrorOr<IEnumerable<string>> errorOrPerson = Error.Failure();

        // Assert
        errorOrPerson.IsError.Should().BeTrue();
        errorOrPerson.Errors[0].Type.Should().Be(ErrorType.Failure);
    }

    [Fact]
    public void CreateFromFactory_WhenMatchValue_ShouldValue()
    {
        // Arrange
        IEnumerable<string> value = new[] { "value" };
        ErrorOr<IEnumerable<string>> errorOrPerson = ErrorOrFactory.From(value);
        string OnValueAction(IEnumerable<string> nextValue)
        {
            errorOrPerson.Value.Should().BeEquivalentTo(nextValue);
            return "ok";
        }
        string OnErrorsAction(IReadOnlyList<Error> _) => throw new Exception("Should not be called");

        // Act

        Func<string> action = () => errorOrPerson.Match(
           OnValueAction,
           OnErrorsAction);

        // Assert
        errorOrPerson.IsError.Should().BeFalse();
        action.Should().NotThrow().Subject.Should().Be("ok"); ;
    }

    [Fact]
    public void CreateFromFactory_WhenMatchValue_ShouldValueFailureError()
    {
        // Arrange
        IEnumerable<string> value = new[] { "value" };
        ErrorOr<IEnumerable<string>> errorOrPerson = Error.Failure();
        string OnValueAction(IEnumerable<string> nextValue)
        {
            errorOrPerson.Value.Should().BeEquivalentTo(nextValue);
            return "ok";
        }
        string OnErrorsAction(IReadOnlyList<Error> _)
        {
            errorOrPerson.IsError.Should().BeTrue();
            errorOrPerson.Errors[0].Type.Should().Be(ErrorType.Failure);

            return "Error";
        }

        // Act
        Func<string> action = () => errorOrPerson.Match(
           OnValueAction,
           OnErrorsAction);

        // Assert
        action.Should().NotThrow().Subject.Should().Be("Error"); ;
    }
}