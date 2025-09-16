// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BaseCurrencyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using System;

#nullable disable
namespace PX.Objects.GL;

[PXDBString(5, IsUnicode = true)]
[PXSelector(typeof (Search<CurrencyList.curyID>))]
[PXUIField(DisplayName = "Base Currency ID")]
public class BaseCurrencyAttribute : PXAggregateAttribute
{
  private Type _branchID;

  public BaseCurrencyAttribute(Type branchID) => this._branchID = branchID;

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldDefaultingEvents fieldDefaulting = sender.Graph.FieldDefaulting;
    Type itemType1 = sender.GetItemType();
    string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
    BaseCurrencyAttribute currencyAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) currencyAttribute1, __vmethodptr(currencyAttribute1, FieldDefaulting));
    fieldDefaulting.AddHandler(itemType1, fieldName, pxFieldDefaulting);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType2 = sender.GetItemType();
    string name = this._branchID.Name;
    BaseCurrencyAttribute currencyAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) currencyAttribute2, __vmethodptr(currencyAttribute2, BranchID_FieldUpdated));
    fieldUpdated.AddHandler(itemType2, name, pxFieldUpdated);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PXFieldState valueExt = (PXFieldState) sender.GetValueExt(e.Row, this._branchID.Name);
    if (valueExt != null && valueExt.Value != null)
    {
      PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(valueExt.Value is int? ? (int?) valueExt.Value : PXAccess.GetBranchID(((string) valueExt.Value).Trim()));
      e.NewValue = (object) branch?.BaseCuryID;
    }
    else
      e.NewValue = (object) null;
  }

  public virtual void BranchID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PXFieldState valueExt = (PXFieldState) sender.GetValueExt(e.Row, this._branchID.Name);
    if (valueExt != null && valueExt.Value != null)
    {
      PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(valueExt.Value is int? ? (int?) valueExt.Value : PXAccess.GetBranchID(((string) valueExt.Value).Trim()));
      sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) branch?.BaseCuryID);
    }
    else
      sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
  }
}
