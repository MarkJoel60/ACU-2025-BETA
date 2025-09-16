// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxCategoryMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Api.Export;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

[NonOptimizable(new System.Type[] {typeof (Tax.taxType), typeof (Tax.taxApplyTermsDisc)}, IgnoreOptimizationBehavior = true)]
public class TaxCategoryMaint : PXGraph<TaxCategoryMaint, TaxCategory>
{
  public PXSelect<TaxCategory> TxCategory;
  public PXSelectJoin<TaxCategoryDet, InnerJoin<Tax, On<TaxCategoryDet.taxID, Equal<Tax.taxID>>>, Where<TaxCategoryDet.taxCategoryID, Equal<Current<TaxCategory.taxCategoryID>>>> Details;
  public PXSelect<TaxCategoryDet> TxCategoryDet;
  public PXSetup<PX.Objects.GL.Branch> Company;

  public TaxCategoryMaint()
  {
    if (!((PXSelectBase<PX.Objects.GL.Branch>) this.Company).Current.BAccountID.HasValue)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (PX.Objects.GL.Branch), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Company Branches")
      });
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<Tax.taxID, Where<Tax.isExternal, Equal<False>>>), new System.Type[] {typeof (Tax.taxID), typeof (Tax.descr), typeof (Tax.directTax)})]
  public virtual void _(PX.Data.Events.CacheAttached<TaxCategoryDet.taxID> e)
  {
  }

  protected virtual void TaxCategory_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is TaxCategory))
      return;
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (PX.Objects.AP.APTran.taxCategoryID), typeof (Search<PX.Objects.AP.APTran.taxCategoryID, Where<PX.Objects.AP.APTran.released, Equal<False>>>), (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (PX.Objects.AR.ARTran.taxCategoryID), typeof (Search<PX.Objects.AR.ARTran.taxCategoryID, Where<PX.Objects.AR.ARTran.released, Equal<False>>>), (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (ARFinCharge.taxCategoryID), (System.Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (CASplit.taxCategoryID), typeof (Search2<CASplit.taxCategoryID, InnerJoin<CAAdj, On<CASplit.adjRefNbr, Equal<CAAdj.adjRefNbr>>>, Where<CAAdj.released, Equal<False>>>), (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (EPExpenseClaimDetails.taxCategoryID), (System.Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (PX.Objects.PO.POLine.taxCategoryID), (System.Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (PX.Objects.SO.SOLine.taxCategoryID), (System.Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (PX.Objects.SO.SOOrder.freightTaxCategoryID), (System.Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (CROpportunityProducts.taxCategoryID), (System.Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (INItemClass.taxCategoryID), (System.Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (PX.Objects.IN.InventoryItem.taxCategoryID), (System.Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (PX.Objects.CS.Carrier.taxCategoryID), (System.Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (TaxZone.dfltTaxCategoryID), (System.Type) null, (string) null);
  }

  protected virtual void TaxCategoryDet_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TaxCategoryDet row))
      return;
    PXUIFieldAttribute.SetEnabled<TaxCategoryDet.taxID>(sender, (object) row, string.IsNullOrEmpty(row.TaxID));
  }

  protected virtual void TaxCategoryDet_TaxID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    TaxCategoryDet row = (TaxCategoryDet) e.Row;
    string newValue = (string) e.NewValue;
    if (!(row.TaxID != newValue))
      return;
    List<string> stringList = new List<string>()
    {
      newValue
    };
    foreach (PXResult<TaxCategoryDet> pxResult in ((PXSelectBase<TaxCategoryDet>) this.Details).Select(Array.Empty<object>()))
    {
      TaxCategoryDet taxCategoryDet = PXResult<TaxCategoryDet>.op_Implicit(pxResult);
      if (taxCategoryDet.TaxID == newValue)
      {
        ((CancelEventArgs) e).Cancel = true;
        throw new PXSetPropertyException("This tax is already included into the list.");
      }
      if (!this.IsValidTaxCombination(newValue, taxCategoryDet.TaxID, (bool?) ((PXSelectBase<TaxCategory>) this.TxCategory).Current?.TaxCatFlag))
      {
        ((CancelEventArgs) e).Cancel = true;
        throw new PXSetPropertyException("Direct-entry and non-direct-entry taxes cannot be included in the same tax category ({0}).", new object[1]
        {
          (object) ((PXSelectBase<TaxCategory>) this.TxCategory).Current?.TaxCategoryID
        });
      }
      stringList.Add(taxCategoryDet.TaxID);
    }
    bool? nullable = Tax.PK.Find((PXGraph) this, newValue).DirectTax;
    if (!nullable.GetValueOrDefault())
      return;
    string[] array = stringList.ToArray();
    TaxCategory current = ((PXSelectBase<TaxCategory>) this.TxCategory).Current;
    bool? taxCatFlag;
    if (current == null)
    {
      nullable = new bool?();
      taxCatFlag = nullable;
    }
    else
      taxCatFlag = current.TaxCatFlag;
    PXResultset<TaxZoneDet> pxResultset;
    ref PXResultset<TaxZoneDet> local = ref pxResultset;
    if (!this.TryValidateTaxZoneCombinationWithDirectTax(array, taxCatFlag, out local))
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXSetPropertyException("Multiple direct-entry taxes cannot be included in the same tax category ({0}) and tax zone ({1}).", new object[2]
      {
        (object) ((PXSelectBase<TaxCategory>) this.TxCategory).Current?.TaxCategoryID,
        (object) PXResultset<TaxZoneDet>.op_Implicit(pxResultset)?.TaxZoneID
      });
    }
  }

  protected virtual bool IsValidTaxCombination(
    string firstTaxId,
    string secondTaxId,
    bool? taxCatFlag)
  {
    if (taxCatFlag.GetValueOrDefault())
      return true;
    int num = Tax.PK.Find((PXGraph) this, firstTaxId).DirectTax.GetValueOrDefault() ? 1 : 0;
    bool? directTax = Tax.PK.Find((PXGraph) this, secondTaxId).DirectTax;
    bool flag = num != 0;
    return directTax.GetValueOrDefault() == flag & directTax.HasValue;
  }

  protected virtual bool TryValidateTaxZoneCombinationWithDirectTax(
    string[] taxIds,
    bool? taxCatFlag,
    out PXResultset<TaxZoneDet> invalidZoneCombinations)
  {
    if (taxCatFlag.GetValueOrDefault() || taxIds.Length < 1)
    {
      invalidZoneCombinations = new PXResultset<TaxZoneDet>();
      return true;
    }
    invalidZoneCombinations = PXSelectBase<TaxZoneDet, PXViewOf<TaxZoneDet>.BasedOn<SelectFromBase<TaxZoneDet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<TaxZoneDet.taxID, IBqlString>.IsIn<P.AsString>>.Aggregate<To<GroupBy<TaxZoneDet.taxZoneID>, Count<TaxZoneDet.taxID>>>.Having<BqlAggregatedOperand<Count<TaxZoneDet.taxID>, IBqlInt>.IsGreater<decimal1>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) taxIds
    });
    return invalidZoneCombinations.Count == 0;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<TaxCategory, TaxCategory.taxCatFlag> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxCategory, TaxCategory.taxCatFlag>, TaxCategory, object>) e).NewValue == null || (bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxCategory, TaxCategory.taxCatFlag>, TaxCategory, object>) e).NewValue || this.IsValidConfiguration((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxCategory, TaxCategory.taxCatFlag>, TaxCategory, object>) e).NewValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxCategory, TaxCategory.taxCatFlag>>) e).Cancel = true;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxCategory, TaxCategory.taxCatFlag>, TaxCategory, object>) e).NewValue = e.OldValue;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<TaxCategory, TaxCategory.active> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxCategory, TaxCategory.active>, TaxCategory, object>) e).NewValue == null || !(bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxCategory, TaxCategory.active>, TaxCategory, object>) e).NewValue || this.IsValidConfiguration((bool?) ((PXSelectBase<TaxCategory>) this.TxCategory).Current?.TaxCatFlag))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxCategory, TaxCategory.active>>) e).Cancel = true;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxCategory, TaxCategory.active>, TaxCategory, object>) e).NewValue = e.OldValue;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<TaxCategory> e)
  {
    if (e.Row == null)
      return;
    this.IsValidConfiguration((bool?) ((PXSelectBase<TaxCategory>) this.TxCategory).Current?.TaxCatFlag);
  }

  protected virtual bool IsValidConfiguration(bool? taxCatFlag)
  {
    List<TaxCategoryDet> list = GraphHelper.RowCast<TaxCategoryDet>((IEnumerable) ((PXSelectBase<TaxCategoryDet>) this.Details).Select(Array.Empty<object>())).ToList<TaxCategoryDet>();
    if (taxCatFlag.GetValueOrDefault() || list.Count < 1)
      return true;
    bool valueOrDefault = Tax.PK.Find((PXGraph) this, list.First<TaxCategoryDet>().TaxID).DirectTax.GetValueOrDefault();
    foreach (TaxCategoryDet taxCategoryDet in list)
    {
      bool? directTax = Tax.PK.Find((PXGraph) this, taxCategoryDet.TaxID).DirectTax;
      bool flag = valueOrDefault;
      if (!(directTax.GetValueOrDefault() == flag & directTax.HasValue))
      {
        ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<TaxCategoryDet.taxID>((object) taxCategoryDet, (object) taxCategoryDet.TaxID, (Exception) new PXSetPropertyException("Direct-entry and non-direct-entry taxes cannot be included in the same tax category ({0}).", new object[1]
        {
          (object) ((PXSelectBase<TaxCategory>) this.TxCategory).Current?.TaxCategoryID
        }));
        return false;
      }
    }
    PXResultset<TaxZoneDet> invalidZoneCombinations;
    if (!valueOrDefault || this.TryValidateTaxZoneCombinationWithDirectTax(list.Select<TaxCategoryDet, string>((Func<TaxCategoryDet, string>) (a => a.TaxID)).ToArray<string>(), taxCatFlag, out invalidZoneCombinations))
      return true;
    TaxCategoryDet taxCategoryDet1 = ((PXSelectBase) this.Details).Cache.Locate((object) new TaxCategoryDet()
    {
      TaxID = PXResultset<TaxZoneDet>.op_Implicit(invalidZoneCombinations)?.TaxID,
      TaxCategoryID = ((PXSelectBase<TaxCategory>) this.TxCategory).Current?.TaxCategoryID
    }) as TaxCategoryDet;
    ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<TaxCategoryDet.taxID>((object) taxCategoryDet1, (object) taxCategoryDet1.TaxID, (Exception) new PXSetPropertyException("Multiple direct-entry taxes cannot be included in the same tax category ({0}) and tax zone ({1}).", new object[2]
    {
      (object) taxCategoryDet1?.TaxCategoryID,
      (object) PXResultset<TaxZoneDet>.op_Implicit(invalidZoneCombinations)?.TaxZoneID
    }));
    return false;
  }
}
