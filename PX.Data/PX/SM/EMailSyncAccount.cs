// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailSyncAccount
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class EMailSyncAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Selected", Visibility = PXUIVisibility.Service)]
  public virtual bool? Selected { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Server ID")]
  [PXSelector(typeof (Search<EMailSyncServer.accountID>), new System.Type[] {typeof (EMailSyncServer.accountCD), typeof (EMailSyncServer.address), typeof (EMailSyncServer.defaultPolicyName)}, SubstituteKey = typeof (EMailSyncServer.accountCD), DirtyRead = true)]
  [PXDBDefault(typeof (EMailSyncServer.accountID))]
  [PXParent(typeof (Select<EMailSyncServer, Where<EMailSyncServer.accountID, Equal<Current<EMailSyncAccount.serverID>>>>))]
  public virtual int? ServerID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Employee ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual int? EmployeeID { get; set; }

  [PXDBString(100, InputMask = "")]
  [PXUIField(DisplayName = "Email Address", Visibility = PXUIVisibility.SelectorVisible, IsReadOnly = true)]
  public virtual 
  #nullable disable
  string Address { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Email Account", Visibility = PXUIVisibility.SelectorVisible, IsReadOnly = true)]
  [PXSelector(typeof (EMailAccount.emailAccountID), new System.Type[] {typeof (EMailAccount.description)}, SubstituteKey = typeof (EMailAccount.description))]
  [PXParent(typeof (Select<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Current<EMailSyncAccount.emailAccountID>>>>))]
  public virtual int? EmailAccountID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Sync Account")]
  public virtual bool? SyncAccount { get; set; }

  [PXDBString(255 /*0xFF*/, InputMask = "")]
  [PXUIField(DisplayName = "Policy Name")]
  [PXSelector(typeof (EMailSyncPolicy.policyName), DescriptionField = typeof (EMailSyncPolicy.description), DirtyRead = true)]
  [PXDefault(typeof (Search<EMailSyncAccountPreferences.policyName, Where<EMailSyncAccountPreferences.employeeID, Equal<Current<EMailSyncAccount.employeeID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string PolicyName { get; set; }

  [PXString(InputMask = "")]
  [PXUIField(DisplayName = "Employee Name", Visibility = PXUIVisibility.SelectorVisible, IsReadOnly = true)]
  public virtual string EmployeeCD { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Owner ID", Visibility = PXUIVisibility.SelectorVisible, IsReadOnly = true)]
  public virtual int? OwnerID { get; set; }

  [PXString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status")]
  public virtual string EmployeeStatus { get; set; }

  [PXString(InputMask = "")]
  [PXUIField(DisplayName = "Time Zone")]
  public virtual string TimeZone { get; set; }

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsVitrual { get; set; }

  [PXDBBool]
  [PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? ToReinitialize { get; set; }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsReset { get; set; }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? HasErrors { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, DisplayMask = "g", UseSmallDateTime = false, WithoutDisplayNames = true)]
  [PXUIField(DisplayName = "Contacts Export Date")]
  public virtual System.DateTime? ContactsExportDate { get; set; }

  [PXDBString(InputMask = "")]
  public virtual string ContactsExportFolder { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, DisplayMask = "g", UseSmallDateTime = false, WithoutDisplayNames = true)]
  [PXUIField(DisplayName = "Contacts Import Date")]
  public virtual System.DateTime? ContactsImportDate { get; set; }

  [PXDBString(InputMask = "")]
  public virtual string ContactsImportFolder { get; set; }

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsContactsReset { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, DisplayMask = "g", UseSmallDateTime = false, WithoutDisplayNames = true)]
  [PXUIField(DisplayName = "Emails Export Date")]
  public virtual System.DateTime? EmailsExportDate { get; set; }

  [PXDBString(InputMask = "")]
  public virtual string EmailsExportFolder { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, DisplayMask = "g", UseSmallDateTime = false, WithoutDisplayNames = true)]
  [PXUIField(DisplayName = "Emails Import Date")]
  public virtual System.DateTime? EmailsImportDate { get; set; }

  [PXDBString(InputMask = "")]
  public virtual string EmailsImportFolder { get; set; }

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsEmailsReset { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, DisplayMask = "g", UseSmallDateTime = false, WithoutDisplayNames = true)]
  [PXUIField(DisplayName = "Tasks Export Date")]
  public virtual System.DateTime? TasksExportDate { get; set; }

  [PXDBString(InputMask = "")]
  public virtual string TasksExportFolder { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, DisplayMask = "g", UseSmallDateTime = false, WithoutDisplayNames = true)]
  [PXUIField(DisplayName = "Tasks Import Date")]
  public virtual System.DateTime? TasksImportDate { get; set; }

  [PXDBString(InputMask = "")]
  public virtual string TasksImportFolder { get; set; }

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsTasksReset { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, DisplayMask = "g", UseSmallDateTime = false, WithoutDisplayNames = true)]
  [PXUIField(DisplayName = "Events Export Date")]
  public virtual System.DateTime? EventsExportDate { get; set; }

  [PXDBString(InputMask = "")]
  public virtual string EventsExportFolder { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, DisplayMask = "g", UseSmallDateTime = false, WithoutDisplayNames = true)]
  [PXUIField(DisplayName = "Events Import Date")]
  public virtual System.DateTime? EventsImportDate { get; set; }

  [PXDBString(InputMask = "")]
  public virtual string EventsImportFolder { get; set; }

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsEventsReset { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncAccount.selected>
  {
  }

  public abstract class serverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailSyncAccount.serverID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailSyncAccount.employeeID>
  {
  }

  public abstract class address : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncAccount.address>
  {
  }

  public abstract class emailAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailSyncAccount.emailAccountID>
  {
  }

  public abstract class syncAccount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncAccount.syncAccount>
  {
  }

  public abstract class policyName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncAccount.policyName>
  {
  }

  public abstract class employeeCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncAccount.employeeCD>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailSyncAccount.ownerID>
  {
  }

  public abstract class employeeStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncAccount.employeeStatus>
  {
  }

  public abstract class timeZone : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailSyncAccount.timeZone>
  {
  }

  public abstract class isVitrual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncAccount.isVitrual>
  {
  }

  public abstract class toReinitialize : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailSyncAccount.toReinitialize>
  {
  }

  public abstract class isReset : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncAccount.isReset>
  {
  }

  public abstract class hasErrors : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncAccount.hasErrors>
  {
  }

  public abstract class contactsExportDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailSyncAccount.contactsExportDate>
  {
  }

  public abstract class contactsExportFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncAccount.contactsExportFolder>
  {
  }

  public abstract class contactsImportDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailSyncAccount.contactsImportDate>
  {
  }

  public abstract class contactsImportFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncAccount.contactsImportFolder>
  {
  }

  public abstract class isContactsReset : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EMailSyncAccount.isContactsReset>
  {
  }

  public abstract class emailsExportDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailSyncAccount.emailsExportDate>
  {
  }

  public abstract class emailsExportFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncAccount.emailsExportFolder>
  {
  }

  public abstract class emailsImportDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailSyncAccount.emailsImportDate>
  {
  }

  public abstract class emailsImportFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncAccount.emailsImportFolder>
  {
  }

  public abstract class isEmailsReset : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncAccount.isEmailsReset>
  {
  }

  public abstract class tasksExportDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailSyncAccount.tasksExportDate>
  {
  }

  public abstract class tasksExportFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncAccount.tasksExportFolder>
  {
  }

  public abstract class tasksImportDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailSyncAccount.tasksImportDate>
  {
  }

  public abstract class tasksImportFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncAccount.tasksImportFolder>
  {
  }

  public abstract class isTasksReset : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncAccount.isTasksReset>
  {
  }

  public abstract class eventsExportDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailSyncAccount.eventsExportDate>
  {
  }

  public abstract class eventsExportFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncAccount.eventsExportFolder>
  {
  }

  public abstract class eventsImportDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    EMailSyncAccount.eventsImportDate>
  {
  }

  public abstract class eventsImportFolder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncAccount.eventsImportFolder>
  {
  }

  public abstract class isEventsReset : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EMailSyncAccount.isEventsReset>
  {
  }

  public abstract class noteID : IBqlField, IBqlOperand
  {
  }
}
