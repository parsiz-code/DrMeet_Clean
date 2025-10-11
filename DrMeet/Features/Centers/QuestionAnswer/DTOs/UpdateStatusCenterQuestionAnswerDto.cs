namespace DrMeet.Api.Features.Centers.QuestionAnswer.DTOs;

public class UpdateStatusCenterQuestionAnswerDto
{
    public int CenterId { get; set; }
    public int QuestionAnswerId { get; set; }
    public bool Status { get; set; }


}
