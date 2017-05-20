using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.DBContext;
using CargoMateSolution.Areas.Administration.Models.Vehicle;
using CargoMateSolution.Shared;
using Microsoft.Ajax.Utilities;

namespace CargoMateSolution.Areas.Administration.Controllers
{
    public class VehicleController : BaseController
    {
        // GET: Administration/Vehicle

        public ActionResult Index()
        {
            var vehicleModel = new VehicleViewModel
            {
                VehicleCapcitiesList = DbContext.VehicleCapacities.Include("VehicleType.LocalizedVehicleTypes,LocalizedCapacities").Select(c => new VehicleCapacityListModel
                {
                    Id = c.Id,
                    Name = c.LocalizedCapacities.FirstOrDefault(lc => lc.CultureCode == "en-US").Name,
                    Capacity = c.Capacity.Value,
                    CultureCode = c.CultureCode,
                    Length = c.Length.Value,
                    PalletNumber = c.PalletNumber.Value,
                    VehicleType = c.VehicleType.LocalizedVehicleTypes.FirstOrDefault(t => t.CultureCode == "en-US").Name
                }).ToList(),
                VehicleTypesList = DbContext.VehicleTypes.Include("LocalizedVehicleTypes").Select(t => new VehicleTypeViewModel
                {
                    Id = t.Id,
                    Name = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name,
                    Descreption = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Descreption,
                    CultureCode = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").CultureCode,
                    IsEquipment = t.IsEquipment.Value,
                    ImageUrl = t.ImageUrl
                }).ToList(),
                ConfigurationsList = DbContext.VehicleTypeConfigurations.Include("LocalizedVehicleTypesConfigurations,VehicleType.LocalizedVehicleTypes").Select(c => new VehicleTypeConfigurationListModel
                {
                    Id = c.Id,
                    Name = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lc => lc.CultureCode == "en-US").Name,
                    CultureCode = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lc => lc.CultureCode == "en-US").CultureCode,
                    Descreption = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lc => lc.CultureCode == "en-US").Descreption,
                    ImageUrl = c.ImageUrl,
                    VehicleTypeName = c.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name
                }).ToList(),
                CapicityViewModel = new VehicleCapacityViewModel
                {

                    VehicleTypesListItems = DbContext.VehicleTypes.Include("LocalizedVehicleTypes").Select(t => new SelectListItem()
                    {
                        Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name,
                        Value = t.Id.ToString()
                    }).ToList()
                },
                ConfigurationViewModel = new VehicleTypeConfigurationsViewModel
                {
                    VehicleTypesListItems = DbContext.VehicleTypes.Include("LocalizedVehicleTypes").Select(t => new SelectListItem()
                    {
                        Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name,
                        Value = t.Id.ToString()
                    }).ToList(),

                }
            };
            return View(vehicleModel);
        }


        //*********************Vehicle Types *******************************//
        // Vehicle Types
        [HttpPost]
        public JsonResult AddVehicleType(VehicleTypeViewModel typeViewModel)
        {

            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }

            if (typeViewModel.ImageUrl == null)
            {
                return Json(CargoMateMessages.ModelError);
            }
            var file = Request.Files[0];

            if (file == null || file.ContentLength == 0)
            {
                return Json(CargoMateMessages.FailureResponse);
            }

            var savedFilePath = ImageUploader.SingleFileUploader(file);

            if (string.IsNullOrEmpty(savedFilePath))
            {
                return Json(CargoMateMessages.FailureResponse);
            }

            var typeModel = new VehicleType
            {
                ImageUrl = savedFilePath + file.FileName,
                IsEquipment = typeViewModel.IsEquipment
            };
            var localizedType = new LocalizedVehicleType
            {
                Name = typeViewModel.Name,
                Descreption = typeViewModel.Descreption,
                CultureCode = typeViewModel.CultureCode
            };
            typeModel.LocalizedVehicleTypes.Add(localizedType);

            DbContext.VehicleTypes.Add(typeModel);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);
        }

        public JsonResult UpdateVehicleType(VehicleTypeViewModel typeViewModel)
        {
            var imagePath = string.Empty;
            var imageName = string.Empty;

            var savedVehicleType = DbContext.VehicleTypes.FirstOrDefault(t => t.Id == typeViewModel.Id);

            if (savedVehicleType == null)
            {
                return Json(CargoMateMessages.ModelError);
            }

            if (Request.Files.Count > 0)
            {
                var newfile = Request.Files[0];
                if (newfile != null && newfile.ContentLength > 0)
                {
                    imagePath = ImageUploader.SingleFileUploader(newfile);
                    imageName = newfile.FileName;
                    if (string.IsNullOrEmpty(imagePath))
                    {
                        return Json(CargoMateMessages.FailureResponse);
                    }
                }

            }


            var localizedSavedVehicleType = savedVehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US");
            if (localizedSavedVehicleType != null)
            {
                localizedSavedVehicleType.Name = typeViewModel.Name;
                localizedSavedVehicleType.Descreption = typeViewModel.Descreption;
            }

            savedVehicleType.IsEquipment = typeViewModel.IsEquipment;

            if (imagePath != string.Empty)
            {
                savedVehicleType.ImageUrl = imagePath + imageName;
            }

            savedVehicleType.LocalizedVehicleTypes.Add(localizedSavedVehicleType);

            DbContext.VehicleTypes.Attach(savedVehicleType);
            var type = DbContext.Entry(savedVehicleType);
            type.State = EntityState.Modified;

            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);
        }

        public JsonResult DeleteVehicleType(long typeId)
        {
            var type = DbContext.VehicleTypes.FirstOrDefault(t => t.Id == typeId);
            if (type == null)
            {
                return Json(CargoMateMessages.ModelError, JsonRequestBehavior.AllowGet);
            }
            DbContext.VehicleTypes.Remove(type);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditVehicleType(long typeId)
        {
            var vehicleType = DbContext.VehicleTypes.Where(t => t.Id == typeId).Include("LocalizedVehicleTypes").Select(t => new VehicleTypeViewModel
            {
                Id = t.Id,
                Name = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name,
                CultureCode = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").CultureCode,
                Descreption = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Descreption,
                ImageUrl = t.ImageUrl,
                IsEquipment = t.IsEquipment.Value
            }).FirstOrDefault();
            return View("Partials/_AddVehicleType", vehicleType);
        }
        public ActionResult VehicletypeList()
        {
            var vehicleTypes = DbContext.VehicleTypes.Include("LocalizedVehicleTypes").Select(t => new VehicleTypeViewModel
            {
                Id = t.Id,
                Name = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name,
                CultureCode = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").CultureCode,
                Descreption = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Descreption,
                ImageUrl = t.ImageUrl,
                IsEquipment = t.IsEquipment.Value
            }).ToList();
            return View("Partials/_VehicleTypes", vehicleTypes);
        }


        ////******************* Vehicle Capicities ******************************//

        public JsonResult AddVehicleCapacity(VehicleCapacityViewModel capacityViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }
            if (capacityViewModel.Id > 0)
            {
                return Json(UpdateVehicleCapacity(capacityViewModel));
            }
            var capacityModel = new VehicleCapacity
            {
                CultureCode = capacityViewModel.CultureCode,
                Length = capacityViewModel.Length,
                PalletNumber = capacityViewModel.PalletNumber,
                VehicleTypeId = capacityViewModel.VehicleTypeId,
                Capacity = capacityViewModel.Capacity
            };
            var localizedCapacity = new LocalizedCapacity
            {
                Name = capacityViewModel.Name,
                CultureCode = capacityModel.CultureCode
            };
            capacityModel.LocalizedCapacities.Add(localizedCapacity);

            DbContext.VehicleCapacities.Add(capacityModel);

            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);

        }

        public object UpdateVehicleCapacity(VehicleCapacityViewModel capacityViewModel)
        {
            var savedCapacity = DbContext.VehicleCapacities.FirstOrDefault(c => c.Id == capacityViewModel.Id);
            if (savedCapacity == null)
            {
                return CargoMateMessages.ModelError;
            }
            savedCapacity.Capacity = capacityViewModel.Capacity;
            savedCapacity.Length = capacityViewModel.Length;
            savedCapacity.PalletNumber = capacityViewModel.PalletNumber;
            var localizedCapacity = savedCapacity.LocalizedCapacities.FirstOrDefault(c => c.CultureCode == "en-US");
            if (localizedCapacity != null)
            {
                localizedCapacity.CultureCode = capacityViewModel.CultureCode;
                localizedCapacity.Name = capacityViewModel.Name;

                savedCapacity.LocalizedCapacities.Add(localizedCapacity);
            }
            DbContext.VehicleCapacities.Attach(savedCapacity);
            var type = DbContext.Entry(savedCapacity);
            type.State = EntityState.Modified;

            return (DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);
        }
        public ActionResult EditVehicleCapacity(long capacityId)
        {
            var capacityModel = DbContext.VehicleCapacities.Include("LocalizedCapacities").Where(c => c.Id == capacityId).Select(c => new VehicleCapacityViewModel
                {
                    Id = c.Id,
                    Capacity = c.Capacity.Value,
                    CultureCode = c.LocalizedCapacities.FirstOrDefault(lc => lc.CultureCode == "en-US").CultureCode,
                    Name = c.LocalizedCapacities.FirstOrDefault(lc => lc.CultureCode == "en-US").Name,
                    VehicleTypeId = c.VehicleTypeId.Value,
                    PalletNumber = c.PalletNumber.Value,
                    Length = c.Length.Value
                }).FirstOrDefault();
            if (capacityModel == null)
            {
                return View("Partials/_AddVehicleCapicity", new VehicleCapacityViewModel());
            }
            capacityModel.VehicleTypesListItems =
                DbContext.VehicleTypes.Include("LocalizedVehicleTypes").Select(t => new SelectListItem()
                {
                    Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name,
                    Value = t.Id.ToString()
                }).ToList();

            return View("Partials/_AddVehicleCapicity", capacityModel);
        }
        public ActionResult VehicleCapacitiesList()
        {
            var vehicleCapcitiesList =
                DbContext.VehicleCapacities.Include("VehicleType.LocalizedVehicleTypes,LocalizedCapacities").Select(c => new VehicleCapacityListModel
                {
                    Id = c.Id,
                    Name = c.LocalizedCapacities.FirstOrDefault(lc => lc.CultureCode == "en-US").Name,
                    Capacity = c.Capacity.Value,
                    CultureCode = c.CultureCode,
                    Length = c.Length.Value,
                    PalletNumber = c.PalletNumber.Value,
                    VehicleType = c.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name
                }).ToList();
            return View("Partials/_VehicleCapicities", vehicleCapcitiesList);
        }

        public JsonResult VehicleCapacityDelete(long capacityId)
        {
            var capacity = DbContext.VehicleCapacities.FirstOrDefault(c => c.Id == capacityId);
            if (capacity == null)
            {
                return Json(CargoMateMessages.ModelError, JsonRequestBehavior.AllowGet);
            }
            DbContext.VehicleCapacities.Remove(capacity);
            return
                Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }


        ////****************** Vehicle Configurations *****************************//
        public JsonResult AddVehicleConfiguration(VehicleTypeConfigurationsViewModel configurationsViewModel)
        {

            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }
            if (configurationsViewModel.ImageUrl == null)
            {
                return Json(CargoMateMessages.ModelError);
            }
            var file = Request.Files[0];

            if (file == null)
            {
                return Json(CargoMateMessages.FailureResponse);
            }

            var savedFilePath = ImageUploader.SingleFileUploader(file);

            if (string.IsNullOrEmpty(savedFilePath))
            {
                return Json(CargoMateMessages.FailureResponse);
            }

            var configurationModel = new VehicleTypeConfiguration
            {
                ImageUrl = savedFilePath + file.FileName,
                VehicleTypeId = configurationsViewModel.TypeId
            };
            var localizedConfiguration = new LocalizedVehicleTypesConfiguration
            {
                CultureCode = configurationsViewModel.CultureCode,
                Name = configurationsViewModel.Name,
                Descreption = configurationsViewModel.Descreption,
            };
            configurationModel.LocalizedVehicleTypesConfigurations.Add(localizedConfiguration);
            DbContext.VehicleTypeConfigurations.Add(configurationModel);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);
        }

        public JsonResult UpdateVehicleConfiguration(VehicleTypeConfigurationsViewModel configurationsViewModel)
        {
            var imagePath = string.Empty;
            var imageName = string.Empty;

            var savedConfiguration = DbContext.VehicleTypeConfigurations.FirstOrDefault(c => c.Id == configurationsViewModel.Id);
            if (savedConfiguration == null)
            {
                return Json(CargoMateMessages.ModelError);
            }

            if (Request.Files.Count > 0)
            {
                var newfile = Request.Files[0];
                if (newfile != null && newfile.ContentLength > 0)
                {
                    imagePath = ImageUploader.SingleFileUploader(newfile);
                    imageName = newfile.FileName;
                    if (string.IsNullOrEmpty(imagePath))
                    {
                        return Json(CargoMateMessages.FailureResponse);
                    }
                }

            }

            if (imagePath != string.Empty)
            {
                savedConfiguration.ImageUrl = imagePath + imageName;
            }
            savedConfiguration.VehicleTypeId = configurationsViewModel.TypeId;

            var localizedConfiguration = DbContext.LocalizedVehicleTypesConfigurations.FirstOrDefault(lc => lc.ConfigurationId == configurationsViewModel.Id);
            if (localizedConfiguration != null)
            {
                localizedConfiguration.Name = configurationsViewModel.Name;
                localizedConfiguration.CultureCode = configurationsViewModel.CultureCode;
                localizedConfiguration.Descreption = configurationsViewModel.Descreption;

                savedConfiguration.LocalizedVehicleTypesConfigurations.Add(localizedConfiguration);

                savedConfiguration.LocalizedVehicleTypesConfigurations.Add(localizedConfiguration);
            }

            DbContext.VehicleTypeConfigurations.Attach(savedConfiguration);
            var type = DbContext.Entry(savedConfiguration);
            type.State = EntityState.Modified;
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);

        }

        public ActionResult EditVehicleConfiguration(long configurationId)
        {
            var configurationModel =
                DbContext.VehicleTypeConfigurations.Where(c => c.Id == configurationId).Select(c => new VehicleTypeConfigurationsViewModel
                {
                    CultureCode = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lc => lc.CultureCode == "en-Us").CultureCode,
                    Descreption = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lc => lc.CultureCode == "en-Us").Descreption,
                    Name = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lc => lc.CultureCode == "en-Us").Name,
                    TypeId = c.VehicleTypeId.Value,
                    ImageUrl = c.ImageUrl,
                    Id = c.Id

                }).FirstOrDefault();
            if (configurationModel != null)
            {
                configurationModel.VehicleTypesListItems =
                    DbContext.VehicleTypes.Include("LocalizedVehicleTypes").Select(t => new SelectListItem()
                    {
                        Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name,
                        Value = t.Id.ToString()
                    }).ToList();
                return View("Partials/_AddVehicleConfiguration", configurationModel);
            }
            return View("Partials/_AddVehicleConfiguration", new VehicleTypeConfigurationsViewModel());
        }
        public ActionResult VehicleConfigurationList()
        {
            var configurationsList = DbContext.VehicleTypeConfigurations.Include("LocalizedVehicleTypesConfigurations,VehicleType.LocalizedVehicleTypes").Select(c => new VehicleTypeConfigurationListModel
                {
                    Id = c.Id,
                    Name = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lc => lc.CultureCode == "en-US").Name,
                    CultureCode = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lc => lc.CultureCode == "en-US").CultureCode,
                    Descreption = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lc => lc.CultureCode == "en-US").Descreption,
                    ImageUrl = c.ImageUrl,
                    VehicleTypeName = c.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name
                }).ToList();
            return View("Partials/_VehicleConfigurationList", configurationsList);
        }

        public JsonResult DeleteVehicleConfiguration(long configurationId)
        {
            var configuration = DbContext.VehicleTypeConfigurations.FirstOrDefault(t => t.Id == configurationId);
            if (configuration == null)
            {
                return Json(CargoMateMessages.ModelError, JsonRequestBehavior.AllowGet);
            }
            DbContext.VehicleTypeConfigurations.Remove(configuration);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VehicleMakes()
        {
            var makeViewModel = new MakeViewModel
            {
                MakeModel = new MakeModel
                {
                    Countries = DbContext.Countries.Where(c => c.IsActive == true).Select(c => new SelectListItem
                    {
                        Text = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == "en-US").Name,
                        Value = c.Id.ToString()
                    }).ToList()

                },
                MakeModelsList = DbContext.Makes.Where(m => m.IsActive == true).Select(m => new MakeModel
                {
                    Name = m.LocalizedMakes.FirstOrDefault(ml => ml.CultureCode == "en-US").Name,
                    ImageUrl = m.ImageUrl,
                    Id = m.Id,
                    CountryName = m.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == "en-US").Name

                }).ToList()

            };
            return View("VehicleMakes", makeViewModel);
        }

        public JsonResult AddVehicleMake(MakeModel makeModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }
            if (makeModel.ImageUrl == null)
            {
                return Json(CargoMateMessages.ModelError);
            }
            var file = Request.Files[0];

            if (file == null)
            {
                return Json(CargoMateMessages.FailureResponse);
            }

            var savedFilePath = ImageUploader.SingleFileUploader(file);

            if (string.IsNullOrEmpty(savedFilePath))
            {
                return Json(CargoMateMessages.FailureResponse);
            }
            var make = new Make
            {
                CountryId = makeModel.CountryId,
                ImageUrl = savedFilePath + file.FileName,
                IsActive = true
            };
            var localizedMake = new LocalizedMake
            {
                CultureCode = "en-US",
                Name = makeModel.Name
            };
            make.LocalizedMakes.Add(localizedMake);
            DbContext.Makes.Add(make);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);

        }

        public ActionResult MakeList()
        {
            var makeList = DbContext.Makes.Select(m => new MakeModel
            {
                CountryId = m.CountryId.Value,
                Name = m.LocalizedMakes.FirstOrDefault(lm => lm.CultureCode == "en-US").Name,
                CountryName = m.Country.LocalizedCountries.FirstOrDefault(lm => lm.CultureCode == "en-US").Name,
                Id = m.Id,
                ImageUrl = m.ImageUrl
            }).ToList();

            return View("Partials/VehicleMakesList", makeList);
        }

        public JsonResult DeletMake(long makeId)
        {
            var make = DbContext.Makes.FirstOrDefault(m => m.Id == makeId);
            if (make == null)
            {
                return Json(CargoMateMessages.ModelError, JsonRequestBehavior.AllowGet);
            }
            DbContext.Makes.Remove(make);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditVehicleMake(long makeId)
        {
            var vehicleMake =
                DbContext.Makes.Where(m => m.Id == makeId).Select(m => new MakeModel
                {
                    CultureCode = m.LocalizedMakes.FirstOrDefault(lc => lc.CultureCode == "en-Us").CultureCode,
                    Name = m.LocalizedMakes.FirstOrDefault(lc => lc.CultureCode == "en-Us").Name,
                    CountryId = m.CountryId.Value,
                    ImageUrl = m.ImageUrl,
                    Id = m.Id,
                    Countries = DbContext.Countries.Where(c => c.IsActive == true).Select(c => new SelectListItem
                    {
                        Text = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == "en-US").Name,
                        Value = c.Id.ToString()
                    }).ToList()
                }).FirstOrDefault();

            return View("Partials/_AddMake", vehicleMake);
        }

        public JsonResult UpdateVehicleMake(MakeModel makeModel)
        {
            var imagePath = string.Empty;
            var imageName = string.Empty;

            var savedMake = DbContext.Makes.FirstOrDefault(m => m.Id == makeModel.Id);
            if (savedMake == null)
            {
                return Json(CargoMateMessages.ModelError);
            }

            if (Request.Files.Count > 0)
            {
                var newfile = Request.Files[0];
                if (newfile != null && newfile.ContentLength > 0)
                {
                    imagePath = ImageUploader.SingleFileUploader(newfile);
                    imageName = newfile.FileName;
                    if (string.IsNullOrEmpty(imagePath))
                    {
                        return Json(CargoMateMessages.FailureResponse);
                    }
                }

            }

            if (imagePath != string.Empty)
            {
                savedMake.ImageUrl = imagePath + imageName;
            }
            savedMake.CountryId = makeModel.CountryId;
            var localizedMake = DbContext.LocalizedMakes.FirstOrDefault(lm => lm.MakeId == makeModel.Id);
            if (localizedMake != null)
            {
                localizedMake.Name = makeModel.Name;
                localizedMake.CultureCode = makeModel.CultureCode;

                savedMake.LocalizedMakes.Add(localizedMake);
            }

            DbContext.Makes.Attach(savedMake);
            var type = DbContext.Entry(savedMake);
            type.State = EntityState.Modified;
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);

        }

        public ActionResult VehicleModels()
        {
            var vehicleModel = new VehicleModelViewModel
            {
                VehicleModel = new VehicleModel
                {
                    MakeList = DbContext.Makes.Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.LocalizedMakes.FirstOrDefault(lm => lm.CultureCode == "en-US").Name
                    }).ToList(),

                },
                VehicleModelsList = DbContext.Models.Select(m => new VehicleModel
                {
                    Id = m.Id,
                    ImageUrl = m.ImageURL,
                    Make = m.Make.LocalizedMakes.FirstOrDefault(lm => lm.CultureCode == "en-US").Name,
                    Name = m.LocalizedModels.FirstOrDefault(lm => lm.CultureCode == "en-US").Name
                }).ToList()
            };
            return View(vehicleModel);
        }

        public JsonResult AddVehicleModel(VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }
            if (vehicleModel.ImageUrl == null)
            {
                return Json(CargoMateMessages.ModelError);
            }
            var file = Request.Files[0];

            if (file == null)
            {
                return Json(CargoMateMessages.FailureResponse);
            }

            var savedFilePath = ImageUploader.SingleFileUploader(file);

            if (string.IsNullOrEmpty(savedFilePath))
            {
                return Json(CargoMateMessages.FailureResponse);
            }
            var model = new Model
            {
                MakeId = vehicleModel.MakeId,
                ImageURL = savedFilePath + file.FileName
            };
            var localizedModel = new LocalizedModel
            {
                CultureCode = "en-US",
                Name = vehicleModel.Name
            };
            model.LocalizedModels.Add(localizedModel);
            DbContext.Models.Add(model);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);
        }

        public ActionResult ModelList()
        {
            var vehicleModelsList = DbContext.Models.Select(m => new VehicleModel
            {
                Id = m.Id,
                ImageUrl = m.ImageURL,
                Make = m.Make.LocalizedMakes.FirstOrDefault(lm => lm.CultureCode == "en-US").Name,
                Name = m.LocalizedModels.FirstOrDefault(lm => lm.CultureCode == "en-US").Name
            }).ToList();

            return View("Partials/_Modelslist", vehicleModelsList);

        }

        public ActionResult EditVehicleModel(long modelId)
        {
            var savedModel = DbContext.Models.Where(m => m.Id == modelId).Select(m => new VehicleModel
            {
                MakeList = DbContext.Makes.Select(ml => new SelectListItem
                {
                    Value = ml.Id.ToString(),
                    Text = ml.LocalizedMakes.FirstOrDefault(lm => lm.CultureCode == "en-US").Name
                }).ToList(),
                Id = m.Id,
                MakeId = m.MakeId,
                ImageUrl = m.ImageURL,
                Name = m.LocalizedModels.FirstOrDefault(lm => lm.CultureCode == "en-US").Name,
                CultureCode = m.LocalizedModels.FirstOrDefault(lm => lm.CultureCode == "en-US").CultureCode

            }).FirstOrDefault();

            return View("Partials/_AddModel", savedModel);
        }

        public JsonResult UpdateVehiclModel(VehicleModel vehicleModel)
        {
            var imagePath = string.Empty;
            var imageName = string.Empty;

            var savedModel = DbContext.Models.FirstOrDefault(m => m.Id == vehicleModel.Id);
            if (savedModel == null)
            {
                return Json(CargoMateMessages.ModelError);
            }

            if (Request.Files.Count > 0)
            {
                var newfile = Request.Files[0];
                if (newfile != null && newfile.ContentLength > 0)
                {
                    imagePath = ImageUploader.SingleFileUploader(newfile);
                    imageName = newfile.FileName;
                    if (string.IsNullOrEmpty(imagePath))
                    {
                        return Json(CargoMateMessages.FailureResponse);
                    }
                }

            }

            if (imagePath != string.Empty)
            {
                savedModel.ImageURL = imagePath + imageName;
            }
            savedModel.MakeId = vehicleModel.MakeId;

            var localizedModel = DbContext.LocalizedModels.FirstOrDefault(lm => lm.ModelId == vehicleModel.Id);
            if (localizedModel != null)
            {
                localizedModel.Name = vehicleModel.Name;
                localizedModel.CultureCode = vehicleModel.CultureCode;

                savedModel.LocalizedModels.Add(localizedModel);
            }

            DbContext.Models.Attach(savedModel);
            var type = DbContext.Entry(savedModel);
            type.State = EntityState.Modified;
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);

        }
        public JsonResult DeletModel(long modelId)
        {
            var model = DbContext.Models.FirstOrDefault(m => m.Id == modelId);
            if (model == null)
            {
                return Json(CargoMateMessages.ModelError, JsonRequestBehavior.AllowGet);
            }
            DbContext.Models.Remove(model);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VehicleModelYear()
        {
            var vehicleModelyears = new VehicleModelYearViewModel
            {
                VehicleModelYear = new VehicleModelYear
                {
                    ModelList = DbContext.Models.Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.LocalizedModels.FirstOrDefault(lm => lm.CultureCode == "en-US").Name
                    }).ToList(),
                    YearsList = DbContext.Years.Select(y => new SelectListItem
                    {
                        Text = y.YearName,
                        Value = y.Id.ToString()
                    }).ToList()

                },
                VehicleModelYearList = DbContext.ModelYearCombinations.Select(m => new VehicleModelYear
                {
                    Id = m.Id,
                    Model = m.Model.LocalizedModels.FirstOrDefault(lm => lm.CultureCode == "en-US").Name,
                    Year = m.Year.YearName,
                    ImageUrl = m.ImageUrl
                }).ToList()
            };
            return View("VehiclModelYear", vehicleModelyears);
        }

        public ActionResult VehicleModelYearList()
        {
            var vehicleModelYearList = DbContext.ModelYearCombinations.Select(m => new VehicleModelYear
            {
                Id = m.Id,
                Model = m.Model.LocalizedModels.FirstOrDefault(lm => lm.CultureCode == "en-US").Name,
                Year = m.Year.YearName,
                ImageUrl = m.ImageUrl
            }).ToList();

            return View("Partials/_VehicleModelYearsList", vehicleModelYearList);
        }

        public JsonResult AddVehicelModelYear(VehicleModelYear modelYear)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }

            if (modelYear.ImageUrl == null)
            {
                return Json(CargoMateMessages.ModelError);
            }
            var file = Request.Files[0];

            if (file == null)
            {
                return Json(CargoMateMessages.FailureResponse);
            }

            var savedFilePath = ImageUploader.SingleFileUploader(file);

            if (string.IsNullOrEmpty(savedFilePath))
            {
                return Json(CargoMateMessages.FailureResponse);
            }
            var modelYearCombination = new ModelYearCombination
            {
                ModelId = modelYear.ModelId,
                YearId = modelYear.YearId,
                ImageUrl = savedFilePath + file.FileName
            };

            DbContext.ModelYearCombinations.Add(modelYearCombination);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);
        }

        public JsonResult DeleteVehicelModelYear(long modelYearlId)
        {
            var model = DbContext.ModelYearCombinations.FirstOrDefault(m => m.Id == modelYearlId);
            if (model == null)
            {
                return Json(CargoMateMessages.ModelError, JsonRequestBehavior.AllowGet);
            }
            DbContext.ModelYearCombinations.Remove(model);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditVehicleModelYear(long modelYearId)
        {
            var savedModelYear = DbContext.ModelYearCombinations.Where(m => m.Id == modelYearId).Select(m => new VehicleModelYear
            {

                Id = m.Id,
                ModelId = m.ModelId,
                ImageUrl = m.ImageUrl,
                YearId = m.YearId

            }).FirstOrDefault();
            if (savedModelYear != null)
            {
                savedModelYear.ModelList = DbContext.Models.Select(ml => new SelectListItem
                {
                    Value = ml.Id.ToString(),
                    Text = ml.LocalizedModels.FirstOrDefault(lm => lm.CultureCode == "en-US").Name
                }).ToList();
                savedModelYear.YearsList = DbContext.Years.Select(y => new SelectListItem
                {
                    Text = y.YearName,
                    Value = y.Id.ToString()
                }).ToList();

            }
            return View("Partials/_AddModelYear", savedModelYear);
        }

        public JsonResult UpdateVehiclModelYear(VehicleModelYear vehicleModelyear)
        {
            var imagePath = string.Empty;
            var imageName = string.Empty;

            var savedModelYear = DbContext.ModelYearCombinations.FirstOrDefault(m => m.Id == vehicleModelyear.Id);
            if (savedModelYear == null)
            {
                return Json(CargoMateMessages.ModelError);
            }

            if (Request.Files.Count > 0)
            {
                var newfile = Request.Files[0];
                if (newfile != null && newfile.ContentLength > 0)
                {
                    imagePath = ImageUploader.SingleFileUploader(newfile);
                    imageName = newfile.FileName;
                    if (string.IsNullOrEmpty(imagePath))
                    {
                        return Json(CargoMateMessages.FailureResponse);
                    }
                }

            }

            if (imagePath != string.Empty)
            {
                savedModelYear.ImageUrl = imagePath + imageName;
            }
            savedModelYear.ModelId = vehicleModelyear.ModelId;
            savedModelYear.YearId = vehicleModelyear.YearId;

            DbContext.ModelYearCombinations.Attach(savedModelYear);
            var type = DbContext.Entry(savedModelYear);
            type.State = EntityState.Modified;
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);

        }

        public ActionResult PayLoadTypes()
        {
            var payLoadTypes = new PayLoadTypesViewModel
            {
                PayLoadModel = new PayLoadModel
                {
                    VehicleTypesList = DbContext.VehicleTypes.Include("LocalizedVehicleTypes").Select(t => new SelectListItem
                    {
                        Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name,
                        Value = t.Id.ToString()
                    }).ToList()
                },
                PayLoadModelList = DbContext.PayLoadTypes.Include("LocalizedPayLoadTypes").Select(pt => new PayLoadModel
                {
                    Id = pt.Id,
                    TypeId = pt.TypeId.Value,
                    ImageUrl = pt.ImageUrl,
                    Name = pt.LocalizedPayLoadTypes.FirstOrDefault(ptl => ptl.CultureCode == "en-US").Name,
                    CultureCode = pt.LocalizedPayLoadTypes.FirstOrDefault(ptl => ptl.CultureCode == "en-US").CultureCode,
                    TypeName = pt.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name
                }).ToList()

            };
            return View(payLoadTypes);
        }

        public JsonResult AddPayLoadtype(PayLoadModel payLoadModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }

            if (payLoadModel.ImageUrl == null)
            {
                return Json(CargoMateMessages.ModelError);
            }
            var file = Request.Files[0];

            if (file == null)
            {
                return Json(CargoMateMessages.FailureResponse);
            }

            var savedFilePath = ImageUploader.SingleFileUploader(file);

            if (string.IsNullOrEmpty(savedFilePath))
            {
                return Json(CargoMateMessages.FailureResponse);
            }
            var payLoad = new PayLoadType
            {
                IsActive = true,
                TypeId = payLoadModel.TypeId,
                ImageUrl = savedFilePath + file.FileName
            };
            var localizedPayLoad = new LocalizedPayLoadType
            {
                Name = payLoadModel.Name,
                CultureCode = payLoadModel.CultureCode
            };
            payLoad.LocalizedPayLoadTypes.Add(localizedPayLoad);
            DbContext.PayLoadTypes.Add(payLoad);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);
        }

        public ActionResult PayLoadTypeList()
        {
            var payLoadModelList = DbContext.PayLoadTypes.Include("LocalizedPayLoadTypes").Select(pt => new PayLoadModel
            {
                Id = pt.Id,
                TypeId = pt.TypeId.Value,
                ImageUrl = pt.ImageUrl,
                Name = pt.LocalizedPayLoadTypes.FirstOrDefault(ptl => ptl.CultureCode == "en-US").Name,
                CultureCode = pt.LocalizedPayLoadTypes.FirstOrDefault(ptl => ptl.CultureCode == "en-US").CultureCode,
                TypeName = pt.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name
            }).ToList();
            return View("Partials/_PayLoadtypeList", payLoadModelList);
        }

        public JsonResult DeletePayLoadTypes(long payloadId)
        {
            var model = DbContext.PayLoadTypes.FirstOrDefault(pt => pt.Id == payloadId);
            if (model == null)
            {
                return Json(CargoMateMessages.ModelError, JsonRequestBehavior.AllowGet);
            }
            DbContext.PayLoadTypes.Remove(model);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PayLoadEdit(long payloadId)
        {
            var model = DbContext.PayLoadTypes.Include("LocalizedPayLoadTypes").Where(pt => pt.Id == payloadId).Select(pt=>new PayLoadModel
            {
                Id = pt.Id,
                ImageUrl = pt.ImageUrl,
                TypeId = pt.TypeId.Value,
                Name = pt.LocalizedPayLoadTypes.FirstOrDefault(ptl=>ptl.CultureCode=="en-US").Name,
                CultureCode = pt.LocalizedPayLoadTypes.FirstOrDefault(ptl => ptl.CultureCode == "en-US").CultureCode               
                
            }).FirstOrDefault();
            if (model != null)
            {
                model.VehicleTypesList =
                    DbContext.VehicleTypes.Include("LocalizedVehicleTypes").Select(t => new SelectListItem
                    {
                        Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == "en-US").Name,
                        Value = t.Id.ToString()
                    }).ToList();               
            }
            return View("Partials/_AddPayLoadType", model);
        }

        public JsonResult UpdatePayLoadType(PayLoadModel payLoadModel)
        {
            var imagePath = string.Empty;
            var imageName = string.Empty;

            var savedModel = DbContext.PayLoadTypes.FirstOrDefault(m => m.Id == payLoadModel.Id);
            if (savedModel == null)
            {
                return Json(CargoMateMessages.ModelError);
            }

            if (Request.Files.Count > 0)
            {
                var newfile = Request.Files[0];
                if (newfile != null && newfile.ContentLength > 0)
                {
                    imagePath = ImageUploader.SingleFileUploader(newfile);
                    imageName = newfile.FileName;
                    if (string.IsNullOrEmpty(imagePath))
                    {
                        return Json(CargoMateMessages.FailureResponse);
                    }
                }

            }

            if (imagePath != string.Empty)
            {
                savedModel.ImageUrl = imagePath + imageName;
            }
            savedModel.TypeId = payLoadModel.TypeId;

            var localizedModel = DbContext.LocalizedPayLoadTypes.FirstOrDefault(lpt => lpt.PayLoadTypeId == payLoadModel.Id);
            if (localizedModel != null)
            {
                localizedModel.Name = payLoadModel.Name;
                localizedModel.CultureCode = payLoadModel.CultureCode;

                savedModel.LocalizedPayLoadTypes.Add(localizedModel);
            }

            DbContext.PayLoadTypes.Attach(savedModel);
            var type = DbContext.Entry(savedModel);
            type.State = EntityState.Modified;
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);

        }

        public ActionResult Units()
        {
            var unitViewModel = new UnitViewModel
            {
                LengthModel = new LengthModel(),
                LengthModelList = DbContext.Lengths.Select(l=>new LengthModel
                {
                    Id = l.Id,
                    IsMetric = l.IsMetric,
                    LengthMultiple = l.LengthMultiple,
                    ShortName = l.LocalizedLengths.FirstOrDefault(lw => lw.CultureCode == "en-US").ShortName,
                    FullName = l.LocalizedLengths.FirstOrDefault(lw => lw.CultureCode == "en-US").FullName,
                    CultureCode = l.LocalizedLengths.FirstOrDefault(lw => lw.CultureCode == "en-US").CultureCode,
                }).ToList(),

                WeightModel = new WeightModel(),
                WeightModelList = DbContext.Weights.Select(w=>new WeightModel
                {
                     Id = w.Id,
                     IsMetric = w.IsMetric,
                     WeightMultiple = w.WeightMultiple,
                     ShortName = w.LocalizedWeights.FirstOrDefault(lw=>lw.CultureCode=="en-US").ShortName,
                     FullName = w.LocalizedWeights.FirstOrDefault(lw => lw.CultureCode == "en-US").FullName,
                     CultureCode = w.LocalizedWeights.FirstOrDefault(lw => lw.CultureCode == "en-US").CultureCode
                     
                }).ToList()
            };
            return View(unitViewModel);
        }

        public ActionResult AddWeight(WeightModel weightModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }

           
            var weight = new Weight
            {
                IsMetric = weightModel.IsMetric,
                 WeightMultiple = weightModel.WeightMultiple
            };
            var localizedWeight = new LocalizedWeight
            {
                ShortName = weightModel.ShortName,
                CultureCode = weightModel.CultureCode,
                FullName = weightModel.FullName
            };
            weight.LocalizedWeights.Add(localizedWeight);
            DbContext.Weights.Add(weight);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);
        }

        public ActionResult WeightList()
        {
            var weightModelList = DbContext.Weights.Select(w => new WeightModel
            {
                Id = w.Id,
                IsMetric = w.IsMetric,
                WeightMultiple = w.WeightMultiple,
                ShortName = w.LocalizedWeights.FirstOrDefault(lw => lw.CultureCode == "en-US").ShortName,
                FullName = w.LocalizedWeights.FirstOrDefault(lw => lw.CultureCode == "en-US").FullName,
                CultureCode = w.LocalizedWeights.FirstOrDefault(lw => lw.CultureCode == "en-US").CultureCode

            }).ToList();

            return View("Partials/_WeightList",weightModelList);

        }

        public JsonResult DeleteWeight(long weightId)
        {
            var weight = DbContext.Weights.FirstOrDefault(w => w.Id == weightId);
            if (weight == null)
            {
                return Json(CargoMateMessages.ModelError, JsonRequestBehavior.AllowGet);
            }
            DbContext.Weights.Remove(weight);
            return Json(DbContext.SaveChanges() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse, JsonRequestBehavior.AllowGet);
            
        }

    }
}