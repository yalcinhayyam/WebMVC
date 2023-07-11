
namespace WebMVC.Utilities;

[Serializable]
public class BaseException : Exception
{
    public BaseException(string? message) : base(message)
    {
    }
}