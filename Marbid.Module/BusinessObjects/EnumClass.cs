using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marbid.Module.BusinessObjects
{
   public enum Gender
   {
      Undefined,
      Male,
      Female
   }

   public enum Priority
   {
      [ImageName("State_Priority_Low")]
      Low = 0,
      [ImageName("State_Priority_Normal")]
      Normal = 1,
      [ImageName("State_Priority_High")]
      High = 2,
      [ImageName("State_Validation_Warning")]
      Critical = 3
   }
   public enum Approval
   {
      NotYetApproved = 0,
      Approved = 1,
      NotApproved = 2
   }
   public enum TaskStatus
   {
      [ImageName("State_Task_NotStarted")]
      NotStarted = 0,
      [ImageName("State_Task_InProgress")]
      InProgress = 1,
      [ImageName("State_Task_WaitingForSomeoneElse")]
      Waiting = 2,
      [ImageName("State_Task_Deferred")]
      Deferred = 3,
      [ImageName("State_Task_Completed")]
      Completed = 4
   }

   public enum CompanyDocumentType
   {
      Other = 0,
      AnnualReport = 1,
      FinancialReport = 1,
      CompanyProfile = 1
   }

   public enum Religion
   {
      Undefined = 0,
      Islam = 1,
      Protestant = 2,
      Catholic = 3,
      Hindu = 4,
      Buddha = 5,
      KongHuChu = 6
   }

   public enum MaritalStatus
   {
      Undefined = 0,
      Single = 1,
      Married = 2,
      Widowed = 3,
      Divorced = 4
   }

   public enum CorporationType
   {
      Undifined = 0,
      Private = 1,
      Public = 2,
      BUMN = 3,
      BUMD = 4
   }

   public enum BusinessRole
   {
      Undefined = 0,
      Insurance = 1,
      Broker = 2,
      Reinsurance = 3
   }

   public enum PeriodType
   {
      Daily,
      Weekly,
      BiWeekly,
      Monthly,
      BiMonthly,
      Quarterly,
      Semester,
      Yearly
   }

   public enum AcademicDegree
   {
      NotAssigned = 0,
      SD = 1,
      SMP = 2,
      SMA = 3,
      D3 = 4,
      S1 = 5,
      S2 = 6,
      S3 = 7,
      Professional = 8
   }

   public enum EducationInstitutionType
   {
      Undefined = 0,
      StateOwnedInstitution = 1,
      PrivateInstitution = 2
   }

   public enum PromotionType
   {
      Undefined = 0,
      Promotion = 1,
      Mutation = 2
   }
   public enum ExamStatus
   {
      Undefined = 0,
      DidNotPassed = 1,
      Passed = 2
   }

   public enum MonthName
   {
      January = 1,
      February = 2,
      March = 3,
      April = 4,
      Mei = 5,
      June = 6,
      July = 7,
      August = 8,
      September = 9,
      October = 10,
      November = 11,
      December = 12
   }
   public enum AssessmentScore
   {
      NotScored = 0,
      VeryBad = 1,
      Bad = 2,
      Avarage = 3,
      Good = 4,
      VeryGood = 5,
      Excellent = 6
   }

   public enum RequisitonStatus
   {
      Pending = 0,
      RequestApproval = 1,
      NotApproved = 2,
      Approved = 3,
      ReceivedByLogistic = 4,
      OnProcess = 5,
      PartialyFullfilled = 6,
      Fullfiled = 7,
      Completed = 8,
      Canceled = 9
   }

   public enum DocumentStatus
   {
      Registered = 0,
      Scanned = 1,
      Verified = 2,
      Archived = 3
   }

   public enum FolderStatus
   {
      Open = 0,
      Closed = 1,
      Archived = 2
   }

   public enum CheckingStatus
   {
      Available = 0,
      Requested = 1,
      Delivering = 2,
      Received = 3,
      CheckingIn = 4
   }

   public enum DocumentSource
   {
      Mail = 0,
      Email = 1,
      Facimile = 2,
      Website = 3,
      Other = 4
   }

   public enum FitchRatings
   {
      NotRated = 0
   }

   public enum StandardPoor
   {
      NotRated = 0
   }

   public enum Pefindo
   {
      NotRated = 0
   }

   public enum Moody
   {
      NotRated = 0
   }

   public enum AMBest
   {
      NotRated= 0
   }

    public enum ParameterPropertyType
    {
        String = 0,
        Integer = 1,
        Numeric = 2,
        DateTime = 3,
        DataSource = 4
    }
}
