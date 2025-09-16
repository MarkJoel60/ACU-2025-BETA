// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZSubTask
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.WZ;

[Serializable]
public class WZSubTask : WZTask
{
  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  public override 
  #nullable disable
  string Name { get; set; }

  [PXDBGuid(false)]
  [PXUIField]
  public override Guid? ParentTaskID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public override bool? IsOptional { get; set; }

  [Owner(DisplayName = "Assigned To", Enabled = false)]
  public override int? AssignedTo { get; set; }

  [PXDBString(2, IsFixed = true)]
  [WizardTaskStatuses]
  [PXDefault("PN")]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public override string Status { get; set; }

  public new abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZSubTask.name>
  {
  }

  public new abstract class parentTaskID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZSubTask.parentTaskID>
  {
  }

  public new abstract class isOptional : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WZSubTask.isOptional>
  {
  }

  public new abstract class assignedTo : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZSubTask.assignedTo>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZSubTask.status>
  {
  }
}
