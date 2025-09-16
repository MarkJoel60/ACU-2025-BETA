// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailSyncPolicy
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (EMailSyncPolicyMaint))]
[Serializable]
public class EMailSyncPolicy : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [PXDBString(16 /*0x10*/, IsKey = true, IsFixed = false)]
  [PXUIField(DisplayName = "Policy Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (EMailSyncPolicy.policyName), new System.Type[] {typeof (EMailSyncPolicy.policyName), typeof (EMailSyncPolicy.description), typeof (EMailSyncPolicy.contactsSync), typeof (EMailSyncPolicy.emailsSync), typeof (EMailSyncPolicy.tasksSync), typeof (EMailSyncPolicy.eventsSync)})]
  public virtual 
  #nullable disable
  string PolicyName { get; set; }

  [PXDBString(64 /*0x40*/, InputMask = "")]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description { get; set; }

  [PXDefault]
  [ToReinitializeAccount]
  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Category Name")]
  public virtual string Category { get; set; }

  [PXDBString(255 /*0xFF*/, InputMask = "")]
  [PXUIField(DisplayName = "External Link Template")]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string LinkTemplate { get; set; }

  [PXDBString(1, InputMask = "")]
  [PXUIField(DisplayName = "Conflict Resolution Priority")]
  [PXSyncPriority.List]
  [PXDefault("A")]
  public virtual string Priority { get; set; }

  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [ToReinitializeAccount]
  [PXUIField(DisplayName = "Category Color")]
  [PXDefault("None")]
  [ExchangeColorList]
  public virtual string Color { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Continue on Error", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(false)]
  public virtual bool? SkipError { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Synchronize Contacts", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(true)]
  public virtual bool? ContactsSync { get; set; }

  [PXDBString(1, InputMask = "")]
  [PXUIField(DisplayName = "Direction")]
  [PXEmailSyncDirection.List]
  [PXDefault("F")]
  public virtual string ContactsDirection { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Folder Name")]
  public virtual string ContactsFolder { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Use Separate Folder for Contacts")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? ContactsSeparated { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Merge contacts by email")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? ContactsMerge { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Filter")]
  [PXEmailSyncContactsFilter.List]
  [PXDefault("Workgroup", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string ContactsFilter { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Contact Class")]
  public virtual string ContactsClass { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Synchronize New Items without Category")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? ContactsSkipCategory { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Use Hyperlink to System Contact as Web Page Address")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? ContactsGenerateLink { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Synchronize Emails", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(true)]
  public virtual bool? EmailsSync { get; set; }

  [PXDBString(1, InputMask = "")]
  [PXUIField(DisplayName = "Direction")]
  [PXEmailSyncDirection.List]
  [PXDefault("F")]
  public virtual string EmailsDirection { get; set; }

  [PXDefault]
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Folder Name")]
  public virtual string EmailsFolder { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Synchronize Attachments")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? EmailsAttachments { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Create Contact if Not Found", Visible = false)]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? EmailsValidateContact { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Synchronize Tasks", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(true)]
  public virtual bool? TasksSync { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Folder Name")]
  public virtual string TasksFolder { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Use Separate Folder for Tasks")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? TasksSeparated { get; set; }

  [PXDBString(1, InputMask = "")]
  [PXUIField(DisplayName = "Direction")]
  [PXEmailSyncDirection.List]
  [PXDefault("F")]
  public virtual string TasksDirection { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Synchronize New Items without Category")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? TasksSkipCategory { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Synchronize Events", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(true)]
  public virtual bool? EventsSync { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Folder Name")]
  public virtual string EventsFolder { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Use Separate Folder for Events")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? EventsSeparated { get; set; }

  [PXDBString(1, InputMask = "")]
  [PXUIField(DisplayName = "Direction")]
  [PXEmailSyncDirection.List]
  [PXDefault("F")]
  public virtual string EventsDirection { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Synchronize New Items without Category")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? EventsSkipCategory { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  public abstract class policyName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncPolicy.policyName>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncPolicy.description>
  {
  }

  public abstract class category : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncPolicy.category>
  {
  }

  public abstract class linkTemplate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncPolicy.linkTemplate>
  {
  }

  public abstract class priority : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncPolicy.priority>
  {
  }

  public abstract class color : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncPolicy.color>
  {
  }

  public abstract class skipError : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncPolicy.skipError>
  {
  }

  public abstract class contactsSync : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncPolicy.contactsSync>
  {
  }

  public abstract class contactsDirection : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncPolicy.contactsDirection>
  {
  }

  public abstract class contactsFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncPolicy.contactsFolder>
  {
  }

  public abstract class contactsSeparated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailSyncPolicy.contactsSeparated>
  {
  }

  public abstract class contactsMerge : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncPolicy.contactsMerge>
  {
  }

  public abstract class contactsFilter : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncPolicy.contactsFilter>
  {
  }

  public abstract class contactsClass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncPolicy.contactsClass>
  {
  }

  public abstract class contactsSkipCategory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailSyncPolicy.contactsSkipCategory>
  {
  }

  public abstract class contactsGenerateLink : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailSyncPolicy.contactsGenerateLink>
  {
  }

  public abstract class emailsSync : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncPolicy.emailsSync>
  {
  }

  public abstract class emailsDirection : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncPolicy.emailsDirection>
  {
  }

  public abstract class emailsFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncPolicy.emailsFolder>
  {
  }

  public abstract class emailsAttachments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailSyncPolicy.emailsAttachments>
  {
  }

  public abstract class emailsValidateContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailSyncPolicy.emailsValidateContact>
  {
  }

  public abstract class tasksSync : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncPolicy.tasksSync>
  {
  }

  public abstract class tasksFolder : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncPolicy.tasksFolder>
  {
  }

  public abstract class tasksSeparated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailSyncPolicy.tasksSeparated>
  {
  }

  public abstract class tasksDirection : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncPolicy.tasksDirection>
  {
  }

  public abstract class tasksSkipCategory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailSyncPolicy.tasksSkipCategory>
  {
  }

  public abstract class eventsSync : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncPolicy.eventsSync>
  {
  }

  public abstract class eventsFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncPolicy.eventsFolder>
  {
  }

  public abstract class eventsSeparated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailSyncPolicy.eventsSeparated>
  {
  }

  public abstract class eventsDirection : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncPolicy.eventsDirection>
  {
  }

  public abstract class eventsSkipCategory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailSyncPolicy.eventsSkipCategory>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EMailSyncPolicy.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncPolicy.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailSyncPolicy.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EMailSyncPolicy.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncPolicy.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailSyncPolicy.lastModifiedDateTime>
  {
  }
}
