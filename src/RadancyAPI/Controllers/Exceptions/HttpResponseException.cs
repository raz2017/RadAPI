namespace RadancyAPI.Controllers.Exceptions;

public class HttpResponseException
{
    public string Message { get; set; }
    public string Source { get; set; }
    public string ExceptionType { get; set; }
}