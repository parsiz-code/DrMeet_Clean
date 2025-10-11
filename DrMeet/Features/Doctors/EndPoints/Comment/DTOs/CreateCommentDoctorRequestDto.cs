namespace DrMeet.Api.Features.Doctors.Comment.DTOs;

public class CreateCommentDoctorRequestDto
{
    public int DoctorId { get; set; }
    //public int CommentId { get; set; }
    public string Subject { get; set; }

    public string Text { get; set; }
    public int Score { get; set; }

    public int BehaviorScore { get; set; }         // برخورد مناسب
    public int TreatmentQualityScore { get; set; } // کیفیت درمان
    public int EconomicEfficiencyScore { get; set; } // صرفه اقتصادی
    public int RecoveryScore { get; set; }         // بهبودی بیمار

}

