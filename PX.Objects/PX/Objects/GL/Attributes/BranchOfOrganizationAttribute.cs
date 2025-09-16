// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Attributes.BranchOfOrganizationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.GL.Attributes;

public class BranchOfOrganizationAttribute : BranchBaseAttribute
{
  public readonly Type OrganizationFieldType;
  public readonly Type FeatureFieldType;

  public BranchOfOrganizationAttribute(
    Type organizationFieldType,
    bool onlyActive = true,
    Type sourceType = null,
    Type featureFieldType = null)
    : this(organizationFieldType, onlyActive, true, sourceType, featureFieldType)
  {
  }

  public BranchOfOrganizationAttribute(
    Type organizationFieldType,
    bool onlyActive,
    bool addDefaultAttribute,
    Type sourceType = null,
    Type featureFieldType = null)
  {
    Type sourceType1 = sourceType;
    if ((object) sourceType1 == null)
      sourceType1 = typeof (AccessInfo.branchID);
    // ISSUE: explicit constructor call
    base.\u002Ector(sourceType1, addDefaultAttribute: addDefaultAttribute);
    this.OrganizationFieldType = organizationFieldType;
    this.FeatureFieldType = featureFieldType;
    this.InitializeAttributeRestrictions(onlyActive, this.OrganizationFieldType, this.FeatureFieldType);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this.OrganizationFieldType != (Type) null))
      return;
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this.OrganizationFieldType), this.OrganizationFieldType.Name, new PXFieldUpdated((object) this, __methodptr(OrganizationFieldUpdated)));
    // ISSUE: method pointer
    sender.Graph.RowSelected.AddHandler(BqlCommand.GetItemType(this.OrganizationFieldType), new PXRowSelected((object) this, __methodptr(OrganizationRowSelected)));
  }

  private void OrganizationFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    cache.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
  }

  private void OrganizationRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    bool flag = true;
    int? organizationID = (int?) sender.GetValue(e.Row, this.OrganizationFieldType.Name);
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID(sender.Graph, organizationID);
    if (organizationById != null)
      flag = organizationById.OrganizationType != "WithoutBranches";
    PXUIFieldAttribute.SetEnabled(sender, ((PXEventSubscriberAttribute) this)._FieldName, flag);
  }

  private void InitializeAttributeRestrictions(
    bool onlyActive,
    Type organizationFieldType,
    Type featureFieldType)
  {
    if (onlyActive)
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<PX.Objects.GL.Branch.active, Equal<True>>), "Branch is inactive.", Array.Empty<Type>()));
    PXRestrictorAttribute restrictorAttribute;
    if (featureFieldType == (Type) null)
      restrictorAttribute = new PXRestrictorAttribute(BqlCommand.Compose(new Type[9]
      {
        typeof (Where<,,>),
        typeof (PX.Objects.GL.Branch.organizationID),
        typeof (Equal<>),
        typeof (Optional2<>),
        organizationFieldType,
        typeof (Or<,>),
        typeof (Optional2<>),
        organizationFieldType,
        typeof (IsNull)
      }), "The specified branch does not belong to the selected company. Specify the branch that is associated with the company on the Branches (CS102000) form.", Array.Empty<Type>());
    else
      restrictorAttribute = new PXRestrictorAttribute(BqlCommand.Compose(new Type[18]
      {
        typeof (Where<,,>),
        typeof (PX.Objects.GL.Branch.organizationID),
        typeof (Equal<>),
        typeof (Optional2<>),
        organizationFieldType,
        typeof (And<,,>),
        typeof (Optional2<>),
        organizationFieldType,
        typeof (IsNotNull),
        typeof (Or<>),
        typeof (Where<,,>),
        typeof (Optional2<>),
        organizationFieldType,
        typeof (IsNull),
        typeof (And<>),
        typeof (Not<>),
        typeof (FeatureInstalled<>),
        featureFieldType
      }), "The specified branch does not belong to the selected company. Specify the branch that is associated with the company on the Branches (CS102000) form.", Array.Empty<Type>());
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) restrictorAttribute);
  }
}
