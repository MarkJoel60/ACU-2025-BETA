// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ReassignApprovalFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <exclude />
[PXHidden]
[Serializable]
public class ReassignApprovalFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Owner(DisplayName = "New Approver", Required = true)]
  [PXDefault]
  public virtual int? NewApprover { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Ignore Approver's Delegations")]
  [PXDefault(false)]
  public virtual bool? IgnoreApproversDelegations { get; set; }

  public abstract class newApprover : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ReassignApprovalFilter.newApprover>
  {
  }

  public abstract class ignoreApproversDelegations : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ReassignApprovalFilter.ignoreApproversDelegations>
  {
  }
}
