namespace Officium.Tools.Handlers
{
    public enum HandlerOrder
    {
        OnNotHandled = -100,
        OnError = -200,
        BeforeEveryRequest = 100,
        ValidateRequest = 200,
        OnRequest = 300,
        AfterEveryRequest = 400
    }
}
