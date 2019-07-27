namespace Officium.CommandValidators
{
    public interface IValidationResult
    {
        string PropertyName { get; }
        string PropertyValue { get; }
    }
}
