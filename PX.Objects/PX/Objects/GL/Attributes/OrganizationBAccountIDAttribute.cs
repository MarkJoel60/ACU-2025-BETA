// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Attributes.OrganizationBAccountIDAttribute
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
public class OrganizationBAccountIDAttribute : PXAggregateAttribute, IPXRowSelectedSubscriber
{
  protected Type SourceOrganizationID;
  protected Type SourceBranchID;

  public OrganizationBAccountIDAttribute(Type sourceOrganizationID, Type sourceBranchID)
  {
    this.SourceOrganizationID = sourceOrganizationID;
    this.SourceBranchID = sourceBranchID;
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || this.SourceOrganizationID == (Type) null && this.SourceBranchID == (Type) null)
      return;
    int? nullable = new int?();
    if (this.SourceOrganizationID != (Type) null)
      nullable = (int?) ((PXAccess.Organization) PXAccess.GetOrganizationByID((int?) sender.GetValue(e.Row, this.SourceOrganizationID.Name)))?.BAccountID;
    if (this.SourceBranchID != (Type) null && !nullable.HasValue)
      nullable = PXAccess.GetBranch((int?) sender.GetValue(e.Row, this.SourceBranchID.Name))?.BAccountID;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) nullable);
  }
}
