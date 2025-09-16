// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Attributes.BranchTreeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL.Attributes;

[PXDBInt]
[PXDefault]
[PXUIField(DisplayName = "Branch", FieldClass = "COMPANYBRANCH", Required = false)]
public class BranchTreeAttribute : BaseOrganizationTreeAttribute
{
  public BranchTreeAttribute(Type treeDataMember = null, bool onlyActive = true)
    : base(treeDataMember, onlyActive, branchMode: true)
  {
    this.SelectionMode = BaseOrganizationTreeAttribute.SelectionModes.Branches;
  }
}
