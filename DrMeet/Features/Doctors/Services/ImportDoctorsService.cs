//using DrMeet.Api.Shared.Domian;

//using DrMeet.Api.Shared.Domian.Doctors;
//using DrMeet.Api.Shared.Persistence.UnitOfWork;
//using DrMeet.Api.Shared.Services.ParsizTeb;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using MongoDB.Driver.Linq;

//namespace DrMeet.Api.Features.Doctors.Services;

//public class ImportDoctorsService(IParsizTebApiService apiService, IUnitOfWork uow)
//{
//    public async Task ImportAsync()
//    {
//        await uow.IranProvinces.DeleteAsync();
//        await uow.Centers.DeleteAsync();
//        await uow.Doctors.DeleteAsync();
//        await uow.Expertises.DeleteAsync();

//        if (!await uow.IranProvinces.AsQueryable().AnyAsync())
//        {
//            var iranProvince = DNTPersianUtils.Core
//                .IranCities
//                .Iran
//                .Provinces
//                .Select(x => new IranProvince()
//                {
//                    Name = x.ProvinceName,
//                    Cities = x.Counties.Select(c => new IranCity()
//                    {
//                        Id = ObjectId.GenerateNewId().ToString(),
//                        Name = c.CountyName
//                    }).ToList()
//                })
//                .ToList();

//            await uow.IranProvinces.AddRangeAsync(iranProvince);
//        }

//        var doctors = await apiService.GetDoctorsAsync();

//        var cities = (await uow.IranProvinces.GetAllAsync())
//            .SelectMany(x => x.Cities.Select(c => new
//            {
//                ProvinceId = x.Id,
//                CityId = c.Id,
//                Name = c.Name,
//                ProvinceName = x.Name,
//            }))
//            .ToList();


//        List<Doctor> doctorsForAdd = new(doctors.Count);

//        foreach (var dto in doctors)
//        {
//            var expertise = await uow.Expertises.GetOrAddAsync(
//                Builders<DrMeet.Api.Shared.Domian.Expertise>.Filter.Eq("Name", dto.Expertise.Name),
//                () => new DrMeet.Api.Shared.Domian.Expertise
//                {
//                    Name = dto.Expertise.Name
//                });

//            var doctorCenters = new List<DoctorCenter>(dto.Centers.Count);

//            foreach (var centerDto in dto.Centers)
//            {
//                if (centerDto.City.Contains("برازجان"))
//                {
//                    centerDto.City = "دشتستان";
//                }

//                var city = cities.FirstOrDefault(x => x.Name.Contains(centerDto.City));

//                var center = await uow.Centers.GetOrAddAsync(
//                    Builders<Center>.Filter.Eq("CenterId", centerDto.CenterId),
//                    () => new Center
//                    {
//                        Id = centerDto.CenterId,
//                        //CenterId = centerDto.CenterId,
//                        Name = centerDto.Name,
//                        CityId = city?.CityId,
//                        ProvinceId = city?.ProvinceId,
//                        ClinicId = centerDto.ClinicId,
//                        OfficeId = centerDto.OfficeId
//                    });

//                doctorCenters.Add(new DoctorCenter()
//                {
//                    CenterId = center.Id,
//                    CityId = city?.CityId,
//                    CityName = city?.Name,
//                    ProvinceId = city?.ProvinceId,
//                    ProvinceName = city?.ProvinceName,
//                    Name = centerDto.Name,
//                    DoctorServices = centerDto.DoctorServices.Select(x => new DoctorService()
//                    {
//                        ServiceDetailId = x.ServiceDetailId,
//                        Name = x.Name,
//                        GroupName = x.GroupName
//                    }).ToList(),

//                    Departments = centerDto.Departments.Select(x => new DoctorCenterDepartment
//                    {
//                        Id = x.DepartmentId,
//                        Name = x.Name,
//                        FreeReserveTimes = x.FreeTimes.Select(ft => new DoctorFreeTimesForReserveTime
//                        {
//                            CenterDoctorShiftRangeId = ft.CenterDoctorShiftRangeId,
//                            DayOfWeek = ft.DayOfWeek,
//                            Date = ft.Date,
//                            FreeTimes = ft.FreeTimes.Select(t => new DoctorFreeTime
//                            {
//                                //CenterDoctorShiftRangeId = t.CenterDoctorShiftRangeId,
//                                ReservedBefore = t.ReservedBefore,
//                                ShiftId = t.ShiftId.ToString("D"),
//                                Time = t.Time
//                            }).ToList(),
//                        }).ToList()
//                    }).ToList()
//                });
//            }

//            var doctor = new Doctor
//            {
//                RemoteDoctorId = dto.Id,

//                user = new User
//                {
//                    UserName = dto.NationalCode,
//                    UserType = Account.DTOs.UserType.Doctor
//                },
//                FirstName = dto.FirstName,
//                LastName = dto.LastName,
//                NationalCode = dto.NationalCode,
//                Mobile = dto.Mobile,
//                BirthDate = dto.BirthDate,
//                Email = dto.Email,
//                FatherName = dto.FatherName,
//                Gender = dto.Gender,


//                Bio = dto.Bio,
//                OverFifteenYearsExperience = dto.OverFifteenYearsExperience,
//                ShowInOnlineReserveTime = dto.ShowInOnlineReserveTime,
//                ExpertiseId = expertise.Id,
//                ExpertiseName = expertise.Name,
//                PhoneNumber = dto.PhoneNumber,

//                //UserId = dto.UserName,
//                WebSite = dto.WebSite,
//                // Centers = doctorCenters
//            };
//            doctorsForAdd.Add(doctor);
//        }

//        await uow.Doctors.AddRangeAsync(doctorsForAdd);
//    }
//}