namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

public class GetReceptionDetailResponse
{
    public VitalSignDto? VitalSign { get; set; }

    public List<TextLaboratoryDto> TextLaboratories { get; set; } = [];

    public List<PrescribeDrugDto> PrescribeDrugs { get; set; } = [];

    public List<PrescribeLabDto> PrescribeLabs { get; set; } = [];


    public class TextLaboratoryDto
    {
        public string Title { get; set; } = string.Empty;
        public Dictionary<string, string> Values { get; set; } = [];
    }


    public class PrescribeDrugDto
    {
        public string Name { get; set; }= string.Empty;
    }

    public class PrescribeLabDto
    {
        public string Name { get; set; }= string.Empty;
    }


    public class VitalSignDto
    {
        public float? Height { get; set; }

        public float? Weight { get; set; }


        public int? BloodGlucose { get; set; }

        public float? BodyTemperature { get; set; }

        public int? Pulse { get; set; }

        public int? Triglyceride { get; set; }

        public int? BreathingRate { get; set; }

        public float? AroundTheHead { get; set; }

        public int? Waist { get; set; }

        public float? BMI { get; set; }

        public float? DiasBP { get; set; }

        public float? SysBP { get; set; }

        public string Comment { get; set; }= string.Empty;
    }
}