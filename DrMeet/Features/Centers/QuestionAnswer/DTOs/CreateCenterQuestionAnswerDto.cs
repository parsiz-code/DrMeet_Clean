namespace DrMeet.Api.Features.Centers.QuestionAnswer.DTOs
{
    public class CreateCenterQuestionAnswerDto
    {
        public required int CenterId { get; set; }
        public List<CenterQuestionAnswerDto> QuestionAnswer { get; set; } = [];
    }

    public class CenterQuestionAnswerDto
    {
        //public int Id { get; set; }
        public required int CenterId { get; set; }
        public int? ParentId { get; set; }
        public required string Text { get; set; }
        public required string Type { get; set; }
        public List<CommentPointsDto> PositivePoints { get; set; } = [];
        public List<CommentPointsDto> NegativePoints { get; set; } = [];


        public DateTime CreateDate { get; set; }
    }

    public class CommentPointsDto
    {
        //public int Id { get; set; }
        public required string Message { get; set; }
    }
}
