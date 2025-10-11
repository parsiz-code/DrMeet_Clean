namespace DrMeet.Api.Features.ServicesAvailables.DTOs;

public class GetServicesAvailableListResponseDto
{

    public int Id { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }


}

public class GetCenterServicesAvailableListRequestDto
{

    public int Id { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }


}


public class GetServicesAvailableSelectedListResponseDto
{

    public int Id { get; set; }
    public string Name { get; set; }


}

