//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mde.Model.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class MD_Contact
    {
        public string ID { get; set; }
        public string AssistantName { get; set; }
        public string AssistantPhone { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string Owner { get; set; }
        public string CreatedBy { get; set; }
        public string Jigsaw { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public string DoNotCall { get; set; }
        public string Email { get; set; }
        public string HasOptedOutOfEmail { get; set; }
        public string Fax { get; set; }
        public string HasOptedOutOfFax { get; set; }
        public string HomePhone { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastCURequestDate { get; set; }
        public Nullable<System.DateTime> LastCUUpdateDate { get; set; }
        public string LeadSource { get; set; }
        public string MailingAddress { get; set; }
        public string MobilePhone { get; set; }
        public string Name { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherAddress { get; set; }
        public string OtherPhone { get; set; }
        public string Phone { get; set; }
        public string ReportsTo { get; set; }
        public string Title { get; set; }
        public string AccountNumber__c { get; set; }
        public string ebMobile__IsActive__c { get; set; }
        public Nullable<System.DateTime> ebMobile__Anniversary__c { get; set; }
        public string ebMobile__CustomerOnboarding__c { get; set; }
        public string ebMobile__ExternalID__c { get; set; }
        public string ebMobile__Facebook__c { get; set; }
        public Nullable<System.Guid> ebMobile__GUID__c { get; set; }
        public string ebMobile__Hobbies__c { get; set; }
        public string ebMobile__Married__c { get; set; }
        public string ebMobile__OnboardingUser__c { get; set; }
        public string ebMobile__Primary__c { get; set; }
        public Nullable<decimal> ebMobile__RecordAction__c { get; set; }
        public string CCSM_SAP_Contact_ID__c { get; set; }
        public string ebMobile__Spouse__c { get; set; }
        public string ebMobile__Title__c { get; set; }
        public string ebMobile__Twitter__c { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
    
        public virtual MD_Account MD_Account { get; set; }
    }
}
