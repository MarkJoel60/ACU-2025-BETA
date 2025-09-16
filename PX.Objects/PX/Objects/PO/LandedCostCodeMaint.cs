// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCostCodeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.TX;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.PO;

public class LandedCostCodeMaint : PXGraph<LandedCostCodeMaint, PX.Objects.PO.LandedCostCode>
{
  public PXSelect<PX.Objects.PO.LandedCostCode> LandedCostCode;
  private bool doCancel;

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<NotExists<Select2<TaxCategoryDet, InnerJoin<PX.Objects.TX.Tax, On<TaxCategoryDet.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<PX.Objects.TX.TaxCategory.taxCategoryID, Equal<TaxCategoryDet.taxCategoryID>, And<PX.Objects.TX.TaxCategory.taxCatFlag, Equal<False>, And<PX.Objects.TX.Tax.directTax, Equal<True>, And<BqlField<PX.Objects.PO.LandedCostCode.allocationMethod, IBqlString>.FromCurrent, NotEqual<LandedCostAllocationMethod.none>>>>>>>>), null, new Type[] {})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.PO.LandedCostCode.taxCategoryID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.PO.LandedCostCode> e)
  {
    if (e.Row == null || e.Row.AllocationMethod == "N")
      return;
    object taxCategoryId = (object) e.Row.TaxCategoryID;
    if (!(e.Row.AllocationMethod != (string) ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.PO.LandedCostCode>>) e).Cache.GetValueOriginal<PX.Objects.PO.LandedCostCode.allocationMethod>((object) e.Row)))
      return;
    try
    {
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.PO.LandedCostCode>>) e).Cache.RaiseFieldVerifying<PX.Objects.PO.LandedCostCode.taxCategoryID>((object) e.Row, ref taxCategoryId);
    }
    catch (PXSetPropertyException ex)
    {
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.PO.LandedCostCode>>) e).Cache.RaiseExceptionHandling<PX.Objects.PO.LandedCostCode.taxCategoryID>((object) e.Row, taxCategoryId, (Exception) ex);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.LandedCostCode.taxCategoryID> e)
  {
    PX.Objects.PO.LandedCostCode row = e.Row as PX.Objects.PO.LandedCostCode;
    string newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.LandedCostCode.taxCategoryID>, object, object>) e).NewValue as string;
    if (row == null || string.IsNullOrEmpty(newValue) || row.AllocationMethod == "N")
      return;
    TaxCategoryDet taxCategoryDet = PXResultset<TaxCategoryDet>.op_Implicit(PXSelectBase<TaxCategoryDet, PXViewOf<TaxCategoryDet>.BasedOn<SelectFromBase<TaxCategoryDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.TX.TaxCategory>.On<BqlOperand<PX.Objects.TX.TaxCategory.taxCategoryID, IBqlString>.IsEqual<TaxCategoryDet.taxCategoryID>>>, FbqlJoins.Inner<PX.Objects.TX.Tax>.On<BqlOperand<PX.Objects.TX.Tax.taxID, IBqlString>.IsEqual<TaxCategoryDet.taxID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxCategoryDet.taxCategoryID, Equal<P.AsString>>>>, And<BqlOperand<PX.Objects.TX.TaxCategory.taxCatFlag, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PX.Objects.TX.Tax.directTax, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) newValue
    }));
    if (taxCategoryDet != null)
      throw new PXSetPropertyException("The {0} tax category containing the {1} direct-entry tax can be selected only for a landed cost code with the None allocation method.", new object[2]
      {
        (object) taxCategoryDet.TaxCategoryID,
        (object) taxCategoryDet.TaxID
      });
  }

  protected virtual void LandedCostCode_VendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.PO.LandedCostCode row = (PX.Objects.PO.LandedCostCode) e.Row;
    sender.SetDefaultExt<PX.Objects.PO.LandedCostCode.vendorLocationID>(e.Row);
    sender.SetDefaultExt<PX.Objects.PO.LandedCostCode.termsID>(e.Row);
    this.doCancel = true;
  }

  protected virtual void LandedCostCode_VendorLocationID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this.doCancel)
      return;
    e.NewValue = (object) ((PX.Objects.PO.LandedCostCode) e.Row).VendorLocationID;
    ((CancelEventArgs) e).Cancel = true;
    this.doCancel = false;
  }

  protected virtual void LandedCostCode_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PX.Objects.PO.LandedCostCode row = (PX.Objects.PO.LandedCostCode) e.Row;
    if (row == null)
      return;
    bool hasValue = row.VendorID.HasValue;
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.PO.LandedCostCode.vendorLocationID>(sender, e.Row, hasValue ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetRequired<PX.Objects.PO.LandedCostCode.vendorLocationID>(sender, hasValue);
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.LandedCostCode.vendorLocationID>(sender, e.Row, hasValue);
    sender.RaiseExceptionHandling<PX.Objects.PO.LandedCostCode.vendorID>((object) row, (object) row.VendorID, (Exception) null);
    if (!hasValue)
      return;
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.VendorID
    }));
    if (vendor == null)
      return;
    bool? landedCostVendor = vendor.LandedCostVendor;
    bool flag = false;
    if (!(landedCostVendor.GetValueOrDefault() == flag & landedCostVendor.HasValue))
      return;
    sender.RaiseExceptionHandling<PX.Objects.PO.LandedCostCode.vendorID>((object) row, (object) row.VendorID, (Exception) new PXSetPropertyException("This Code uses a vendor which has 'Landed Cost Vendor' set to 'off'. You should correct vendor or use another one", (PXErrorLevel) 2));
  }

  protected virtual void LandedCostCode_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
  }
}
