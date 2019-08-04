namespace Officium.Commands
{
    public interface IAuthResult
    {
        void AddAllowedClaim(string name);
        void AddDeniedClaim(string name);
        string BearerId { get; set; }
        bool HasAllowedClaim(string name);
    }
}
