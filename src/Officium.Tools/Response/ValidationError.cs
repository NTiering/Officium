namespace Officium.Tools.Response
{
    public class ValidationError : IValidationError
    {
        public string PropertyName { get; }
        public string ErrorMessage { get; }
        public ValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }
}