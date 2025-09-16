// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZTaskAssign
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.WZ;

[PXHidden]
[Serializable]
public class WZTaskAssign : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssignedTo;

  [PXGuid(IsKey = true)]
  [PXDefault(typeof (WZScenario.scenarioID))]
  public virtual Guid? ScenarioID { get; set; }

  [PXGuid(IsKey = true)]
  [PXDefault(typeof (WZTask.taskID))]
  public virtual Guid? TaskID { get; set; }

  [PXBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? OverrideAssignee { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Assigned To")]
  [PXSelector(typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<EPEmployee, On<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>, LeftJoin<Users, On<Users.pKID, Equal<EPEmployee.userID>>>>, Where<Users.isHidden, Equal<False>, And<Users.isApproved, Equal<True>, And<Users.guest, NotEqual<True>>>>>), new System.Type[] {typeof (Users.username), typeof (PX.Objects.CR.Contact.displayName), typeof (Users.fullName), typeof (Users.state), typeof (EPEmployee.acctCD), typeof (EPEmployee.acctName)}, DescriptionField = typeof (PX.Objects.CR.Contact.fullName), DirtyRead = true)]
  public virtual int? AssignedTo { get; set; }

  public abstract class scenarioID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  WZTaskAssign.scenarioID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTaskAssign.taskID>
  {
  }

  public abstract class overrideAssignee : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    WZTaskAssign.overrideAssignee>
  {
  }

  public abstract class assignedTo : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZTaskAssign.assignedTo>
  {
  }
}
