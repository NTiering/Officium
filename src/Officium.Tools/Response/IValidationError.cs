namespace Officium.Tools.Response
{
    public interface IValidationError
    {
        string ErrorMessage { get; }
        string PropertyName { get; }
    }
}