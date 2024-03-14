namespace Test;

public class ErrorTest
{
    private const string ErrorCode = "ErrorCode";
    private const string ErrorDescription = "ErrorDescription";
    private static readonly Dictionary<string, object> Dictionary = new()
        {
            { "k1", "v1" },
            { "k2", 5 },
            { "k3", "v3" },
        };

    [Fact]
    public void CreateError_WhenFailureError_ShouldHaveErrorTypeFailure()
    {
        // Act
        Error error = Error.Failure(ErrorCode, ErrorDescription, Dictionary);

        // Assert
        Assertion(error, expectedErrorType: ErrorType.Failure);
    }

    [Fact]
    public void CreateError_WhenUnexpectedError_ShouldHaveErrorTypeFailure()
    {
        // Act
        Error error = Error.Unexpected(ErrorCode, ErrorDescription, Dictionary);

        // Assert
        Assertion(error, expectedErrorType: ErrorType.Unexpected);
    }

    [Fact]
    public void CreateError_WhenConflictError_ShouldHaveErrorTypeConflict()
    {
        // Act
        Error error = Error.Conflict(ErrorCode, ErrorDescription, Dictionary);

        // Assert
        Assertion(error, expectedErrorType: ErrorType.Conflict);
    }

    [Fact]
    public void CreateError_WhenNotFoundError_ShouldHaveErrorTypeNotFound()
    {
        // Act
        Error error = Error.NotFound(ErrorCode, ErrorDescription, Dictionary);

        // Assert
        Assertion(error, expectedErrorType: ErrorType.NotFound);
    }

    [Fact]
    public void CreateError_WhenNotAuthorizedError_ShouldHaveErrorTypeUnauthorized()
    {
        // Act
        Error error = Error.Unauthorized(ErrorCode, ErrorDescription, Dictionary);

        // Assert
        Assertion(error, expectedErrorType: ErrorType.Unauthorized);
    }

    [Fact]
    public void CreateError_WhenForbiddenError_ShouldHaveErrorTypeForbidden()
    {
        // Act
        Error error = Error.Forbidden(ErrorCode, ErrorDescription, Dictionary);

        // Assert
        Assertion(error, expectedErrorType: ErrorType.Forbidden);
    }

    private static void Assertion(Error error, ErrorType expectedErrorType)
    {
        error.Code.Should().Be(ErrorCode);
        error.Description.Should().Be(ErrorDescription);
        error.Type.Should().Be(expectedErrorType);
        error.Metadata.Should().BeEquivalentTo(Dictionary);
    }
}