// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Attributes.BranchCDOfOrganizationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL.Attributes;

[PXDBString(255 /*0xFF*/, IsUnicode = true)]
[PXString(255 /*0xFF*/, IsUnicode = true)]
[PXUIField(DisplayName = "Branch", FieldClass = "BRANCH")]
public class BranchCDOfOrganizationAttribute : PXEntityAttribute
{
  public const string _FieldClass = "BRANCH";
  public const string _DimensionName = "BRANCH";
  public readonly Type OrganizationFieldType;

  public virtual PXSelectorMode SelectorMode { get; set; } = (PXSelectorMode) 8;

  public BranchCDOfOrganizationAttribute(
    Type organizationFieldType,
    bool onlyActive = true,
    Type searchType = null)
  {
    this.Initialize();
    this.OrganizationFieldType = organizationFieldType;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(typeof (Search<Branch.branchCD, Where<MatchWithBranch<Branch.branchID>>>))
    {
      DescriptionField = typeof (Branch.acctName),
      SelectorMode = this.SelectorMode
    });
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(BqlCommand.Compose(new Type[9]
    {
      typeof (Where<,,>),
      typeof (Branch.organizationID),
      typeof (Equal<>),
      typeof (Optional2<>),
      this.OrganizationFieldType,
      typeof (Or<,>),
      typeof (Optional2<>),
      this.OrganizationFieldType,
      typeof (IsNull)
    }), "The specified branch does not belong to the selected company. Specify the branch that is associated with the company on the Branches (CS102000) form.", Array.Empty<Type>()));
    if (!onlyActive)
      return;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<Branch.active, Equal<True>>), "Branch is inactive.", Array.Empty<Type>()));
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (!(this.OrganizationFieldType != (Type) null))
      return;
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this.OrganizationFieldType), this.OrganizationFieldType.Name, new PXFieldUpdated((object) this, __methodptr(OrganizationFieldUpdated)));
  }

  private void OrganizationFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    cache.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
  }
}
