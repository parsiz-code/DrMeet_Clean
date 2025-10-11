namespace DrMeet.Api.Features.Doctors.Comment.DTOs;

public class DoctorCommentResponseDto
{
    public CommentDto? Comment { get; set; }
    public int BehaviorScore { get; set; }         // برخورد مناسب
    public int TreatmentQualityScore { get; set; } // کیفیت درمان
    public int EconomicEfficiencyScore { get; set; } // صرفه اقتصادی
    public int RecoveryScore { get; set; }         // بهبودی بیمار
}

public class CenterCommentDto
{
    public CommentDto? Comment { get; set; }
}