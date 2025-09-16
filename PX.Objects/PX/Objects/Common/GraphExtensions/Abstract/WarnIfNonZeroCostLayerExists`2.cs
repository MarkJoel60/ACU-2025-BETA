// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.WarnIfNonZeroCostLayerExists`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract;

public abstract class WarnIfNonZeroCostLayerExists<TField, TGraph> : 
  EditPreventor<TypeArrayOf<IBqlField>.FilledWith<TField>>.On<TGraph>
  where TField : class, IBqlField
  where TGraph : PXGraph
{
  protected virtual bool FilterBySiteID => false;

  protected virtual void OnPreventEdit(GetEditPreventingReasonArgs e)
  {
    if (e.Row != null)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual string GetEditPreventingReasonImpl(GetEditPreventingReasonArgs e)
  {
    if (((CancelEventArgs) e).Cancel)
      return (string) null;
    int? originalAccountID;
    int? newAccountID;
    if (!this.AccountIDChanged(e, out originalAccountID, out newAccountID))
      return (string) null;
    int? inventoryID = (int?) e.Cache.GetValue(e.Row, "inventoryID");
    List<object> objectList = new List<object>()
    {
      (object) inventoryID,
      (object) originalAccountID
    };
    BqlCommand command = (BqlCommand) new SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.inventoryID, Equal<BqlField<INCostStatus.inventoryID, IBqlInt>.AsOptional>>>>, And<BqlOperand<INCostStatus.qtyOnHand, IBqlDecimal>.IsNotEqual<decimal0>>>>.And<BqlOperand<INCostStatus.accountID, IBqlInt>.IsEqual<BqlField<INCostStatus.accountID, IBqlInt>.AsOptional>>>();
    if (this.FilterBySiteID)
    {
      int? nullable = (int?) e.Cache.GetValue(e.Row, "siteID");
      command = command.WhereAnd<Where<BqlOperand<INCostStatus.siteID, IBqlInt>.IsEqual<BqlField<INCostStatus.siteID, IBqlInt>.AsOptional>>>();
      objectList.Add((object) nullable);
    }
    if (command.SelectSingleReadonly<INCostStatus>(e.Graph, objectList.ToArray()) == null)
      return (string) null;
    this.ShowError(e, originalAccountID, newAccountID, inventoryID);
    return (string) null;
  }

  protected virtual bool AccountIDChanged(
    GetEditPreventingReasonArgs e,
    out int? originalAccountID,
    out int? newAccountID)
  {
    originalAccountID = (int?) e.Cache.GetValueOriginal<TField>(e.Row);
    newAccountID = (int?) e.NewValue;
    int? nullable1 = originalAccountID;
    int? nullable2 = newAccountID;
    return !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue);
  }

  protected void ShowError(
    GetEditPreventingReasonArgs e,
    int? originalAccountID,
    int? newAccountID,
    int? inventoryID)
  {
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find(e.Graph, originalAccountID);
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(e.Graph, inventoryID);
    e.Cache.RaiseExceptionHandling<TField>(e.Row, (object) newAccountID, (Exception) new PXSetPropertyException((IBqlTable) e.Row, "There is a non-zero balance on the {0} inventory account for the {1} stock item. A change in the inventory account can cause the issue of multiple inventory accounts being used in inventory transactions simultaneously. To change the inventory account, clear the account's balance for the {1} item first.", (PXErrorLevel) 2, new object[2]
    {
      (object) account.AccountCD,
      (object) inventoryItem.InventoryCD
    }));
  }
}
