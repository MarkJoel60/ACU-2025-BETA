// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.Descriptor.TaxPeriodFilterBranchAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.TX.Descriptor;

public class TaxPeriodFilterBranchAttribute : 
  BranchBaseAttribute,
  IPXRowSelectedSubscriber,
  IPXFieldDefaultingSubscriber,
  IPXFieldUpdatingSubscriber
{
  public bool HideBranchField { get; set; }

  public Type OrganizationFieldType { get; set; }

  public TaxPeriodFilterBranchAttribute(Type organizationFieldType, bool hideBranchField = true)
    : base(organizationFieldType, addDefaultAttribute: false)
  {
    this.HideBranchField = hideBranchField;
    this.OrganizationFieldType = organizationFieldType;
    (((PXAggregateAttribute) this)._Attributes[this._UIAttrIndex] as PXUIFieldAttribute).Required = true;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(BqlCommand.Compose(new Type[5]
    {
      typeof (Where<,>),
      typeof (PX.Objects.GL.Branch.organizationID),
      typeof (Equal<>),
      typeof (Optional2<>),
      organizationFieldType
    }), "The specified branch does not belong to the selected company. Specify the branch that is associated with the company on the Branches (CS102000) form.", Array.Empty<Type>()));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<PX.Objects.GL.DAC.Organization.fileTaxesByBranches, Equal<True>>), "A branch can be specified only for companies for which the File Taxes by Branches check box is selected on the Companies (CS101500) form.", Array.Empty<Type>()));
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || !this.HideBranchField)
      return;
    int? organizationID = (int?) sender.GetValue(e.Row, this.OrganizationFieldType.Name);
    PXUIFieldAttribute.SetVisible(sender, ((PXEventSubscriberAttribute) this)._FieldName, organizationID.HasValue && OrganizationMaint.FindOrganizationByID(sender.Graph, organizationID).FileTaxesByBranches.GetValueOrDefault());
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this.OrganizationFieldType != (Type) null))
      return;
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this.OrganizationFieldType), this.OrganizationFieldType.Name, new PXFieldUpdated((object) this, __methodptr(OrganizationFieldUpdated)));
  }

  private void OrganizationFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    cache.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
  }

  public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null)
      return;
    int? organizationID = (int?) sender.GetValue(e.Row, this.OrganizationFieldType.Name);
    if (!organizationID.HasValue || OrganizationMaint.FindOrganizationByID(sender.Graph, organizationID).FileTaxesByBranches.GetValueOrDefault() || e.NewValue == null)
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    int? organizationID = (int?) sender.GetValue(e.Row, this.OrganizationFieldType.Name);
    if (!organizationID.HasValue || !OrganizationMaint.FindOrganizationByID(sender.Graph, organizationID).FileTaxesByBranches.GetValueOrDefault())
      return;
    e.NewValue = (object) sender.Graph.Accessinfo.BranchID;
  }
}
