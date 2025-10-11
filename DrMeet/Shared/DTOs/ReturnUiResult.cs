namespace DrMeet.Api.Shared.DTOs;

public class ReturnUiResult
{
    public ReturnUiResult()
    {
        ReturnResult = ReturnResult.Error;
        LstMessage = new List<string>();

    }
    public ReturnResult ReturnResult { get; set; }
    public List<string> LstMessage { get; set; }
    public object Value { get; set; }

}
public enum ReturnResult
{
    Success,
    Error,
    Null
}
