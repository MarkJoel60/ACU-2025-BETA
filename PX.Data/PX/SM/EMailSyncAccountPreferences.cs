// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailSyncAccountPreferences
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
public class EMailSyncAccountPreferences : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(16 /*0x10*/, IsKey = true, IsFixed = false)]
  [PXUIField(DisplayName = "Policy Name")]
  [PXSelector(typeof (EMailSyncPolicy.policyName), new System.Type[] {typeof (EMailSyncPolicy.policyName), typeof (EMailSyncPolicy.description), typeof (EMailSyncPolicy.contactsSync), typeof (EMailSyncPolicy.emailsSync), typeof (EMailSyncPolicy.tasksSync), typeof (EMailSyncPolicy.eventsSync)})]
  [PXParent(typeof (Select<EMailSyncPolicy, Where<EMailSyncPolicy.policyName, Equal<Current<EMailSyncAccountPreferences.policyName>>>>))]
  public virtual 
  #nullable disable
  string PolicyName { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Employee ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual int? EmployeeID { get; set; }

  [PXDBString(100, InputMask = "")]
  [PXUIField(DisplayName = "Email Address", Visibility = PXUIVisibility.SelectorVisible, IsReadOnly = true)]
  public virtual string Address { get; set; }

  [PXString(InputMask = "")]
  [PXUIField(DisplayName = "Employee Name", Visibility = PXUIVisibility.SelectorVisible, IsReadOnly = true)]
  public virtual string EmployeeCD { get; set; }

  public abstract class policyName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncAccountPreferences.policyName>
  {
  }

  public abstract class employeeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailSyncAccountPreferences.employeeID>
  {
  }

  public abstract class address : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncAccountPreferences.address>
  {
  }

  public abstract class employeeCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailSyncAccountPreferences.employeeCD>
  {
  }
}
