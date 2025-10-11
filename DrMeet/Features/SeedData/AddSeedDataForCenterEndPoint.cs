//using DNTCommon.Web.Core;
//using DrMeet.Api.Features.SeedData.DTOs;
//using DrMeet.Api.Shared;
//using DrMeet.Api.Shared.Contracts;
//using DrMeet.Api.Shared.Domian;

//using DrMeet.Api.Shared.Domian.Doctors;
//using DrMeet.Api.Shared.Persistence.UnitOfWork;
//using DrMeet.Api.Shared.Services.Centers;
//using DrMeet.Api.Shared.Services.Doctors;
//using DrMeet.Api.Shared.Services.UserService;
//using ExtentionLibrary.Strings;
//using MongoDB.Driver;
//using MongoDB.Driver.Linq;
//using static iTextSharp.text.pdf.AcroFields;
//using IranProvince = DrMeet.Api.Shared.Domian.IranProvince;
//namespace DrMeet.Api.Features.SeedData.EndPoints;
//public static class AddSeedDataForCenterEndPoint
//{
//    public class EndPoint : BaseEndpoint, IEndpoint
//    {
//        public void MapEndpoint(IEndpointRouteBuilder app)
//        {
//            app.MapGet($"{ApiInfo.Prefix}/Center", async (
//                ICenterService centerService, IUnitOfWork unitOfWork, IUserService userService

//                ) =>
//            {
//                var client = new MongoClient("mongodb://localhost:27017");
//                var database = client.GetDatabase("test");
//                var collection = database.GetCollection<CenterServiceSeeDataModel>("Center");

//                var data = await collection.Find(_ => true).ToListAsync();
//                var distinctResult = (await collection.Find(_ => true).ToListAsync())
//                     .GroupBy(x => new { x.ClinicId, x.OfficeId })
//                     .Select(g => g.First())
//                     .ToList();
//                var count = data.DistinctBy(_ => _.ClinicId).DistinctBy(_ => _.OfficeId).ToList().Count;
//                string typeId = await unitOfWork.Centers.AsQueryable().Select(_ => _.Id).FirstOrDefaultAsync();

//                foreach (var item in data)
//                {
//                    string? centerName = item.ClinicName.IsNullOrEmpty() ? item.OfficeName : item.ClinicName;
//                    int? centerId = item.ClinicId != null ? item.ClinicId : item.OfficeId;
//                    if (!await unitOfWork.Centers.AsQueryable().AnyAsync(_ => (_.ClinicId == item.ClinicId && _.OfficeId == item.OfficeId)))
//                    {
//                        string provinceId = await unitOfWork.IranProvinces.AsQueryable().Where(_ => _.Name == item.StateName).Select(_ => _.Id).FirstOrDefaultAsync();

//                        if (provinceId == null)
//                        {
//                            var province = new IranProvince
//                            {
//                                Name = item.StateName,
//                            };
//                            await unitOfWork.IranProvinces.AddAsync(province);
//                            provinceId = province.Id;
//                        }
//                        string cityId = await unitOfWork.IranCities.AsQueryable().Where(_ => _.Name == item.CityName).Select(_ => _.Id).FirstOrDefaultAsync();
//                        if (cityId == null)
//                        {
//                            var city = new IranCity
//                            {
//                                Name = item.StateName,
//                                ProvinceId = provinceId
//                            };
//                            await unitOfWork.IranCities.AddAsync(city);
//                            cityId = city.Id;
//                        }

//                        var username = "admin" + new Random().Next(10000, 9999999);
//                        var userId = await userService.CreateUser(
//                                new Features.Account.DTOs.CreateUserRequestDto()
//                                {
//                                    Password = "111",
//                                    UserName = username,
//                                    UserType = Features.Account.DTOs.UserType.Center,
//                                });
//                        await unitOfWork.Centers.AddAsync(new Center
//                        {
//                            DateOfEstablishment = DateTime.Now,
//                            CenterTypeId = typeId,
//                            CenterRemoteId = centerId.HasValue ? centerId.Value : 0,
//                            Name = centerName ?? string.Empty,
//                            ProvinceId = provinceId,
//                            ClinicId = item.ClinicId,
//                            OfficeId = item.OfficeId,
//                            CityId = cityId,
//                            Region = "Iran",
//                            User = new User
//                            {
//                                //Password = dto.Password,
//                                UserName = username,
//                                UserType = Features.Account.DTOs.UserType.Center,
//                            },
//                            UserId = userId

//                        });
//                    }

//                    var center = await unitOfWork.Centers.AsQueryable().FirstOrDefaultAsync(_ => (_.ClinicId == item.ClinicId && _.OfficeId == item.OfficeId));
//                    if (center == null)
//                        continue;

//                    string servicesAvailableId = await unitOfWork.ServicesAvailable.AsQueryable().Where(_ => _.Name == item.ServiceName).Select(_ => _.Id).FirstOrDefaultAsync();
//                    if (servicesAvailableId == null)
//                    {
//                        if(!item.ServiceName.IsNullOrEmpty())
//                        {
//                            var service = new ServicesAvailable
//                            {
//                                Name = item.ServiceName,
//                                Status = true,
//                                Order = 1
//                            };
//                            await unitOfWork.ServicesAvailable.AddAsync(service);
//                            center.ServicesAvailableId?.Add(new Shared.Domian.Centers.CenterService { Id = service.Id, Name = service.Name, Status = true });
//                            servicesAvailableId = service.Id;
//                        }
                       
//                    }
                

//                    if (center.CenterDepartment == null)
//                        center.CenterDepartment = new List<CenterDepartment>();
//                    if (!center.CenterDepartment.Any(_ => _.Id == item.DepartmentId))
//                        center.CenterDepartment?.Add(new CenterDepartment
//                        {
//                            Id = item.DepartmentId,
//                            Name = item.DepartmentName
//                        });


//                    await unitOfWork.Centers.UpdateAsync(center);

//                }
//                return Ok("update");
//            }).WithTags(ApiInfo.Tag);
//        }
//    }


//}

//public static class AddSeedDataForDoctorEndPoint
//{
//    public class EndPoint : BaseEndpoint, IEndpoint
//    {
//        public void MapEndpoint(IEndpointRouteBuilder app)
//        {
//            app.MapGet($"{ApiInfo.Prefix}/Doctor", async (
//                IDoctorService DoctorService, IUnitOfWork unitOfWork, IUserService userService

//                ) =>
//            {
//                var client = new MongoClient("mongodb://localhost:27017");
//                var database = client.GetDatabase("test");
//                var collection = database.GetCollection<DoctorSeeDataModel>("Doctor");

//                var data = await collection.Find(_ => true).ToListAsync();
//                var data1 = await collection.Find(_ => _.DoctorId == 1).ToListAsync();

//                var distinctResult = (await collection.Find(_ => true).ToListAsync())
//                    .GroupBy(x => new { x.DoctorId })
//                    .Select(g => g.First())
//                    .ToList();


//                var data3 = await collection.Find(_ => _.DoctorId == 1).ToListAsync();
//                data3 = data3.DistinctBy(_ => _.ClinicId).ToList();
//                string typeId = await unitOfWork.Doctors.AsQueryable().Select(_ => _.Id).FirstOrDefaultAsync();


//                foreach (var item in data)
//                {
//                    int? _centerId = item.ClinicId != null ? item.ClinicId : item.OfficeId;
//                    string? centerName = item.ClinicName.IsNullOrEmpty() ? item.OfficeName : item.ClinicName;
//                    if (!await unitOfWork.Doctors.AsQueryable().AnyAsync(_ => _.RemoteDoctorId == item.DoctorId))
//                    {

//                        string centerId = await unitOfWork.Centers.AsQueryable().Where(_ => _.Name == centerName).Select(_ => _.Id).FirstOrDefaultAsync();

//                        if (centerId == null)
//                        {
//                            var usernameCenter = "admin" + new Random().Next(10000, 9999999);
//                            var userIdCenter = await userService.CreateUser(
//                                    new Features.Account.DTOs.CreateUserRequestDto()
//                                    {
//                                        Password = "111",
//                                        UserName = usernameCenter,
//                                        UserType = Features.Account.DTOs.UserType.Center,
//                                    });
//                            var _center = new Center
//                            {
//                                Name = centerName ?? string.Empty,
//                                ClinicId = item.ClinicId,
//                                OfficeId = item.OfficeId
//                            };
//                            await unitOfWork.Centers.AddAsync(_center);

//                        }
//                        string provinceId = await unitOfWork.IranProvinces.AsQueryable().Where(_ => _.Name == item.state).Select(_ => _.Id).FirstOrDefaultAsync();

//                        if (provinceId == null)
//                        {
//                            var province = new IranProvince
//                            {
//                                Name = item.state ?? string.Empty,
//                            };
//                            await unitOfWork.IranProvinces.AddAsync(province);
//                            provinceId = province.Id;
//                        }
//                        string cityId = await unitOfWork.IranCities.AsQueryable().Where(_ => _.Name == item.city).Select(_ => _.Id).FirstOrDefaultAsync();
//                        if (cityId == null)
//                        {
//                            var city = new IranCity
//                            {
//                                Name = item.city ?? string.Empty,
//                                ProvinceId = provinceId
//                            };
//                            await unitOfWork.IranCities.AddAsync(city);
//                            cityId = city.Id;
//                        }

//                        string expertiseId = await unitOfWork.Expertises.AsQueryable().Where(_ => _.Name == item.ExpertiseName).Select(_ => _.Id).FirstOrDefaultAsync();
//                        if (expertiseId == null)
//                        {
//                            var expertise = new Shared.Domian.Expertise
//                            {
//                                Name = item.ExpertiseName ?? string.Empty,
//                            };
//                            await unitOfWork.Expertises.AddAsync(expertise);
//                            expertiseId = expertise.Id;
//                        }
//                        var username = "doctor" + new Random().Next(10000, 9999999);
//                        var userId = await userService.CreateUser(
//                                new Features.Account.DTOs.CreateUserRequestDto()
//                                {
//                                    Password = "111",
//                                    UserName = username,
//                                    UserType = Features.Account.DTOs.UserType.Doctor,
//                                });

//                        var doctor = new Doctor()
//                        {
//                            UserId = userId,
//                            Picture = DefaultValues.DoctorManPicture,
//                            PhoneNumber = item.Mobile,
//                            NationalCode = item.NationalCode,
//                            RemoteDoctorId = item.DoctorId,
//                            FirstName = item.FirstName,
//                            Region = "ایران",

//                            ExpertiseName = item.ExpertiseName ?? string.Empty,
//                            ExpertiseId = expertiseId,
//                            CityId = cityId,
//                            BirthDate = item.BirthDate.HasValue ? item.BirthDate.Value.ToShortDateString() : DateTime.Now.ToShortDateString(),
//                            ProvinceId = provinceId,
//                            LastName = item.LastName,
//                            DayVisitTime = item.DayVisitTime,
//                            NumberMedicalSystem = item.NumberMedicalSystem,
//                            Gender = item.Gender == 2 ? GenderModel.Female : GenderModel.Male,
//                            Mobile = item.Mobile,
//                            user = new User
//                            {
//                                UserName = username,
//                                UserType = Features.Account.DTOs.UserType.Doctor,
//                            }
//                        };

//                        await unitOfWork.Doctors.AddAsync(doctor);
//                    }

//                    var Doctor = await unitOfWork.Doctors.AsQueryable().FirstOrDefaultAsync(_ => _.RemoteDoctorId == item.DoctorId);

//                    var center = await unitOfWork.Centers
//                    .AsQueryable()
//                    .Where(_ => (_.ClinicId == item.ClinicId && _.OfficeId == item.OfficeId))
//                    .FirstOrDefaultAsync();
//                    if (center != null)
//                    {
//                        if (!Doctor.Centers.Any(_ => _.CenterId == center.Id))
//                            Doctor.Centers.Add(new DoctorCenter { CenterId = center.Id, Name = center.Name });

//                        var departmant = Doctor.Centers
//                        .Where(_ => _.CenterId == center.Id)
//                        .SelectMany(_ => _.Departments)
//                        .Where(_ => _.Id == (item.departmentId))
//                        .FirstOrDefault();

//                        var curentCenter = Doctor.Centers.Where(_ => _.CenterId == center.Id).FirstOrDefault();
//                        if (curentCenter is not null)
//                        {
//                            var curentDepartman = center.CenterDepartment?
//                              .Where(_ => _.Id == (item.departmentId))
//                              .FirstOrDefault();
//                            if (curentDepartman is not null)
//                                curentCenter.Departments.Add(new DoctorCenterDepartment { Name = curentDepartman.Name, Id = curentDepartman.Id });

//                        }



//                    }



//                    await unitOfWork.Doctors.UpdateAsync(Doctor);
//                    var center1 = await unitOfWork.Centers
//             .AsQueryable()
//             .Where(_ => (_.OfficeId == 13312)).FirstOrDefaultAsync();
//                }
//                return Ok("update");
//            }).WithTags(ApiInfo.Tag);
//        }
//    }


//}

//public static class AddSeedDataForPatientEndPoint
//{
//    public class EndPoint : BaseEndpoint, IEndpoint
//    {
//        public void MapEndpoint(IEndpointRouteBuilder app)
//        {
//            app.MapGet($"{ApiInfo.Prefix}/Patient", async (
//                IDoctorService DoctorService, IUnitOfWork unitOfWork, IUserService userService

//                ) =>
//            {
//                var client = new MongoClient("mongodb://localhost:27017");
//                var database = client.GetDatabase("test");
//                var collection = database.GetCollection<PatientSeeDataModel>("Patient");

//                var data = await collection.Find(_ => true).ToListAsync();


//                //var distinctResult = (await collection.Find(_ => true).ToListAsync())
//                //    .GroupBy(x => new { x.Id })
//                //    .Select(g => g.First())
//                //    .ToList();



//                foreach (var item in data)
//                {
//                    int? _centerId = item.ClinicId != null ? item.ClinicId : item.OfficeId;
//                    string? centerName = item.ClinicName.IsNullOrEmpty() ? item.OfficeName : item.ClinicName;
//                    if (!await unitOfWork.Patients.AsQueryable().AnyAsync(_ => _.PatientRemoteId == item.PatientId))
//                    {

//                        string centerId = await unitOfWork.Centers.AsQueryable().Where(_ => _.Name == centerName).Select(_ => _.Id).FirstOrDefaultAsync();

//                        if (centerId == null)
//                        {
//                            var _center = new Center
//                            {
//                                Name = centerName ?? string.Empty,
//                                ClinicId = item.ClinicId,
//                                OfficeId = item.OfficeId
//                            };
//                            await unitOfWork.Centers.AddAsync(_center);

//                        }

//                        string InsuranceId = await unitOfWork.Insurances.AsQueryable().Where(_ => _.Name == item.Insurance && _.IsBaseInsurance == true).Select(_ => _.Id).FirstOrDefaultAsync();

//                        if (InsuranceId == null)
//                        {
//                            var insurance = new Insurance
//                            {
//                                Name = item.Insurance ?? string.Empty,
//                                IsBaseInsurance = true,
//                                Status = true
//                            };
//                            await unitOfWork.Insurances.AddAsync(insurance);
//                            InsuranceId = insurance.Id;
//                        }
//                        string SupplementInuranceId = await unitOfWork.Insurances.AsQueryable().Where(_ => _.Name == item.Insurance && _.IsBaseInsurance == true).Select(_ => _.Id).FirstOrDefaultAsync();

//                        if (SupplementInuranceId == null)
//                        {
//                            var SupplementInurance = new Insurance
//                            {
//                                Name = item.SupplementInurance ?? string.Empty,
//                                IsBaseInsurance = true,
//                                Status = true
//                            };
//                            await unitOfWork.Insurances.AddAsync(SupplementInurance);
//                            SupplementInuranceId = SupplementInurance.Id;
//                        }



//                        var username = item.UserName;
//                        var userId = await userService.CreateUser(
//                                new Features.Account.DTOs.CreateUserRequestDto()
//                                {
//                                    Password = "patient",
//                                    UserName = username,
//                                    UserType = Features.Account.DTOs.UserType.Patient,
//                                });

//                        var patient = new Patient()
//                        {
//                            UserId = userId,
//                            Picture = DefaultValues.DoctorManPicture,
//                            Mobile = item.Mobile,
//                            NationalCode = item.NationalCode,
//                            PatientRemoteId = item.PatientId,
//                            FatherName = item.FatherName,

//                            Email = item.Email,

//                            BirthDate = item.BirthDate.HasValue ? item.BirthDate.Value.ToShortDateString() : DateTime.Now.ToShortDateString(),
//                            FirstName = item.FirstName,
//                            LastName = item.LastName,
//                            CenterId = centerId,
//                            InsuranceId=InsuranceId,
//                            SupplementInsuranceId=SupplementInuranceId,
//                            Gender = item.Gender == 2 ? GenderModel.Female : GenderModel.Male,
                          
//                            User = new User
//                            {
//                                UserName = username ?? string.Empty,
//                                UserType = Features.Account.DTOs.UserType.Doctor,
//                            }
//                        };

//                        await unitOfWork.Patients.AddAsync(patient);
//                    }

           


//                }
//                return Ok("update");
//            }).WithTags(ApiInfo.Tag);
//        }
//    }


//}


