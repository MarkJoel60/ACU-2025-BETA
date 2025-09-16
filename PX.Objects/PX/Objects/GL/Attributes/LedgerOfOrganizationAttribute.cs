// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Attributes.LedgerOfOrganizationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL.Attributes;

[PXDBInt]
[PXUIField(DisplayName = "Ledger")]
public class LedgerOfOrganizationAttribute : PXAggregateAttribute
{
  public readonly Type OrganizationFieldType;
  public readonly Type BranchFieldType;
  protected static readonly Type _defaultingSelect = typeof (Search5<Ledger.ledgerID, LeftJoin<Branch, On<Branch.ledgerID, Equal<Ledger.ledgerID>>>, Aggregate<GroupBy<Ledger.ledgerID>>>);
  protected static readonly Type _defaultingSelectDefault = typeof (Search2<Ledger.ledgerID, InnerJoin<Branch, On<Branch.ledgerID, Equal<Ledger.ledgerID>>>, Where<Branch.branchID, Equal<Optional2<AccessInfo.branchID>>>>);

  public LedgerOfOrganizationAttribute(
    Type organizationFieldType,
    Type branchFieldType,
    Type restrict = null)
    : this(organizationFieldType, branchFieldType, LedgerOfOrganizationAttribute._defaultingSelect, LedgerOfOrganizationAttribute._defaultingSelectDefault, restrict)
  {
  }

  public LedgerOfOrganizationAttribute(
    Type organizationFieldType,
    Type branchFieldType,
    Type select,
    Type selectDefault,
    Type restrictSelect)
  {
    this.OrganizationFieldType = organizationFieldType;
    this.BranchFieldType = branchFieldType;
    if (select == (Type) null)
      select = LedgerOfOrganizationAttribute._defaultingSelect;
    this._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(select)
    {
      SubstituteKey = typeof (Ledger.ledgerCD),
      DescriptionField = typeof (Ledger.descr)
    });
    Type type = BqlCommand.Compose(new Type[23]
    {
      typeof (Where2<,>),
      typeof (Where2<,>),
      typeof (Where<,,>),
      typeof (Branch.organizationID),
      typeof (Equal<>),
      typeof (Optional2<>),
      organizationFieldType,
      typeof (Or<,>),
      typeof (Optional2<>),
      organizationFieldType,
      typeof (IsNull),
      typeof (And<>),
      typeof (Where<,,>),
      typeof (Branch.branchID),
      typeof (Equal<>),
      typeof (Optional2<>),
      branchFieldType,
      typeof (Or<,>),
      typeof (Optional2<>),
      branchFieldType,
      typeof (IsNull),
      typeof (Or<>),
      typeof (Where<Ledger.balanceType, NotEqual<LedgerBalanceType.actual>>)
    });
    if (restrictSelect != (Type) null)
    {
      type = BqlCommand.Compose(new Type[4]
      {
        typeof (Where2<,>),
        type,
        typeof (And<>),
        restrictSelect
      });
      if (selectDefault != (Type) null)
        selectDefault = BqlCommand.CreateInstance(new Type[1]
        {
          selectDefault
        }).WhereAnd(restrictSelect).GetType();
    }
    this._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(type, "The selected ledger does not belong to the selected company or branch.", Array.Empty<Type>()));
    this._Attributes.Add(selectDefault != (Type) null ? (PXEventSubscriberAttribute) new PXDefaultAttribute(selectDefault) : (PXEventSubscriberAttribute) new PXDefaultAttribute());
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (this.OrganizationFieldType != (Type) null)
    {
      // ISSUE: method pointer
      sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this.OrganizationFieldType), this.OrganizationFieldType.Name, new PXFieldUpdated((object) this, __methodptr(\u003CCacheAttached\u003Eb__7_0)));
    }
    if (!(this.BranchFieldType != (Type) null))
      return;
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this.BranchFieldType), this.BranchFieldType.Name, new PXFieldUpdated((object) this, __methodptr(\u003CCacheAttached\u003Eb__7_1)));
  }
}
