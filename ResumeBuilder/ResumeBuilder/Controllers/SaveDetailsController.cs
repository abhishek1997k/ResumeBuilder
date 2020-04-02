﻿using ResumeBuilder.Helpers;
using ResumeBuilder.Models;
using ResumeBuilder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeBuilder.Controllers
{
    [AuthorizeIfSessionExists]
    public class SaveDetailsController : Controller
    {
        private ResumeBuilderDBContext db;
        
        public SaveDetailsController()
        {
            db = new ResumeBuilderDBContext();
        }

        [HttpPost]
        public ActionResult SaveBasicInformation(UserInfoVM userInfoVM)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            try
            {
                var userFromDb = db.UserDetails.FirstOrDefault(u => u.UserID == userInfoVM.UserID);

                if (userFromDb == null)
                {
                    return null;
                }
                else
                {
                    userFromDb.Name = userInfoVM.Name;
                    userFromDb.Gender = userInfoVM.Gender;
                    userFromDb.Phone = userInfoVM.PhoneNumber;
                    userFromDb.DateOfBirth = userInfoVM.DateOfBirth;
                    userFromDb.Summary = userInfoVM.Summary;

                    if (userInfoVM.LanguageIds.Any())
                    {
                        var languages = db.Languages.Where(x => userInfoVM.LanguageIds.Contains(x.LanguageID)).ToList();
                        if (languages == null)
                        {
                            return null;
                        }
                        userFromDb.Languages.AddRange(languages);
                    }

                    db.SaveChanges();

                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult SaveProjectDetails(ProjectInfoVM projectInfoVM)
        {
            var session = Session["UserID"];
            int id = (Int32)session;
            projectInfoVM.UserID = id;
            try
            {
                if (projectInfoVM.ProjectID == 0)
                {
                    db.Projects.Add(new Project
                    {
                        UserID = projectInfoVM.UserID,
                        ProjectTitle = projectInfoVM.ProjectTitle,
                        DurationInMonth = projectInfoVM.DurationInMonth,
                        ProjectRole = projectInfoVM.ProjectRole,
                        Description = projectInfoVM.Description
                    });

                    db.SaveChanges();

                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var projFromDb = db.Projects.FirstOrDefault(x => x.ProjectID == projectInfoVM.ProjectID);
                        if (projFromDb != null)
                        {
                            projFromDb.UserID = projectInfoVM.UserID;
                            projFromDb.ProjectID = projectInfoVM.ProjectID;
                            projFromDb.ProjectTitle = projectInfoVM.ProjectTitle;
                            projFromDb.ProjectRole = projectInfoVM.ProjectRole;
                            projFromDb.DurationInMonth = projectInfoVM.DurationInMonth;
                            projFromDb.Description = projectInfoVM.Description;

                            db.SaveChanges();
                        }

                        else
                        {
                            return HttpNotFound();
                        }


                    }

                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult SaveWorkExperience(WorkExperienceVM workExperienceVM)
        {
            var session = Session["UserID"];
            int id = (Int32)session;
            workExperienceVM.UserID = id;

            try
            {
                if (workExperienceVM.WorkExperienceID == 0)
                {
                    db.WorkExperiences.Add(new WorkExperience
                    {
                        UserID = workExperienceVM.UserID,
                        StartMonth = workExperienceVM.StartMonth,
                        StartYear = workExperienceVM.StartYear,
                        EndMonth = workExperienceVM.EndMonth,
                        EndYear = workExperienceVM.EndYear,
                        OrganizationName = workExperienceVM.OrganizationName,
                        Designation = workExperienceVM.Designation,
                        IsCurrentlyWorking = workExperienceVM.IsCurrentlyWorking,
                    });

                    db.SaveChanges();

                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var workExFromDb = db.WorkExperiences.FirstOrDefault(x => x.WorkExperienceID == workExperienceVM.WorkExperienceID);
                        if (workExFromDb != null)
                        {
                            workExFromDb.UserID = workExperienceVM.UserID;
                            workExFromDb.StartMonth = workExperienceVM.StartMonth;
                            workExFromDb.StartYear = workExperienceVM.StartYear;
                            workExFromDb.EndMonth = workExperienceVM.EndMonth;
                            workExFromDb.EndYear = workExperienceVM.EndYear;
                            workExFromDb.OrganizationName = workExperienceVM.OrganizationName;
                            workExFromDb.Designation = workExperienceVM.Designation;
                            workExFromDb.IsCurrentlyWorking = workExperienceVM.IsCurrentlyWorking;

                            db.SaveChanges();
                        }

                        else
                        {
                            return HttpNotFound();
                        }

                    }

                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }

            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult SaveEducationalDetails(EducationalDetailsVM educationalDetailsVM)
        {

            try
            {
                if (educationalDetailsVM.EducationalDetailsID == 0)
                {
                    db.EducationalDetails.Add(new EducationalDetails
                    {
                        UserID = educationalDetailsVM.UserID,
                        CourseID = educationalDetailsVM.CourseID,
                        BoardOrUniversity = educationalDetailsVM.BoardOrUniversity,
                        PassingYear = educationalDetailsVM.PassingYear,
                        Stream = educationalDetailsVM.Stream,
                        CGPAOrPercentage = educationalDetailsVM.CGPAOrPercentage,
                        TotalPercentageOrCGPAValue = educationalDetailsVM.TotalPercentageOrCGPAValue
                    });

                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        return HttpNotFound();
                    }

                    var educationalDetails = db.EducationalDetails.FirstOrDefault(id => id.EducationalDetailsID == educationalDetailsVM.EducationalDetailsID);

                    if (educationalDetails == null)
                    {
                        return HttpNotFound();
                    }

                    educationalDetails.UserID = educationalDetailsVM.UserID;
                    educationalDetails.CourseID = educationalDetailsVM.CourseID;
                    educationalDetails.BoardOrUniversity = educationalDetails.BoardOrUniversity;
                    educationalDetails.PassingYear = educationalDetails.PassingYear;
                    educationalDetails.Stream = educationalDetailsVM.Stream;
                    educationalDetails.CGPAOrPercentage = educationalDetailsVM.CGPAOrPercentage;
                    educationalDetails.TotalPercentageOrCGPAValue = educationalDetailsVM.TotalPercentageOrCGPAValue;
                }

                db.SaveChanges();
                return Json("SUCCESS", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult SaveUserSkills(SkillsVM skillsVM)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            try
            {
                UserDetails user = db.UserDetails.Where(x => x.UserID == skillsVM.UserID).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }

                var skillIdsList = db.Skills.Where(x => skillsVM.SkillNames.Contains(x.SkillName)).Select(m => m.SkillID).ToList();

                List<Skill> refer = new List<Skill>();

                foreach (var item in skillIdsList)
                {
                    refer.Add(db.Skills.Where(x => x.SkillID == item).FirstOrDefault());
                }

                user.Skills.AddRange(refer);
                db.SaveChanges();
                return Json("SUCCESS", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        public ActionResult SaveSettingStatus(SettingsDetailsVM settingStatus)
        {

            var session = Session["UserID"];
            int id = (Int32)session;

            var userSettings = db.Settings.SingleOrDefault(user => user.UserID == id);
            userSettings.WorkExperienceStatus = settingStatus.WorkExperienceStatus;
            userSettings.SkillsDetailsStatus = settingStatus.SkillsDetailsStatus;
            userSettings.ProjectDetailsStatus = settingStatus.ProjectDetailsStatus;
            userSettings.LanguagesStatus = settingStatus.LanguagesStatus;
            userSettings.EducationalDetailsStatus = settingStatus.EducationalDetailsStatus;
            TryUpdateModel(userSettings);
            db.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}