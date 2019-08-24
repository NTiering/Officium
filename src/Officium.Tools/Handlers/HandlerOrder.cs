namespace Officium.Tools.Handlers
{
    public enum HandlerOrder
    {
        OnNotHandled = -100,
        OnError = -200,
        Authorise = 100,
        BeforeEveryRequest = 200,
        ValidateRequest = 300,
        OnRequest = 400,
        AfterEveryRequest = 500
    }
}
