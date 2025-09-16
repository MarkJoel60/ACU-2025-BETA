// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRBillingAddressAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;

#nullable disable
namespace PX.Objects.CR;

public class CRBillingAddressAttribute(System.Type SelectType) : CRAddressAttribute(typeof (CRBillingAddress.addressID), typeof (CRBillingAddress.isDefaultAddress), SelectType)
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    CRBillingAddressAttribute addressAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) addressAttribute1, __vmethodptr(addressAttribute1, Record_Override_FieldVerifying));
    fieldVerifying.AddHandler<CRBillingAddress.overrideAddress>(pxFieldVerifying);
    PXGraph.RowInsertedEvents rowInserted = sender.Graph.RowInserted;
    CRBillingAddressAttribute addressAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) addressAttribute2, __vmethodptr(addressAttribute2, CRBillingAddress_RowInserted));
    rowInserted.AddHandler<CRBillingAddress>(pxRowInserted);
  }

  protected virtual void CRBillingAddress_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is CRBillingAddress row) || !row.OverrideAddress.GetValueOrDefault())
      return;
    row.IsValidated = new bool?(false);
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultAddress<CRBillingAddress, CRBillingAddress.addressID>(sender, DocumentRow, Row);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyAddress<CRBillingAddress, CRBillingAddress.addressID>(sender, DocumentRow, SourceRow, clone);
  }

  public override void Record_IsDefault_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  public virtual void Record_Override_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    object obj1;
    if (e.NewValue != null)
    {
      bool? newValue = (bool?) e.NewValue;
      bool flag = false;
      obj1 = (object) (newValue.GetValueOrDefault() == flag & newValue.HasValue);
    }
    else
      obj1 = e.NewValue;
    object obj2 = obj1;
    this.Address_IsDefaultAddress_FieldVerifying<CRBillingAddress>(sender, new PXFieldVerifyingEventArgs(e.Row, obj2, e.ExternalCall));
  }

  protected override (PXView, object[]) GetViewWithParameters(
    PXCache sender,
    object DocumentRow,
    object ContactRow)
  {
    PXView pxView = (PXView) null;
    object[] objArray = (object[]) null;
    if (sender.GetValue<CROpportunity.bAccountID>(DocumentRow) != null)
    {
      object obj = sender.GetValue<CROpportunity.bAccountID>(DocumentRow);
      objArray = new object[2]{ obj, obj };
      BqlCommand bqlCommand = (BqlCommand) new SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<BAccount>.On<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<Address.bAccountID>>>, FbqlJoins.Left<Customer>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.bAccountID, Equal<Address.bAccountID>>>>>.And<BqlOperand<Customer.defBillAddressID, IBqlInt>.IsEqual<Address.addressID>>>>, FbqlJoins.Left<CRBillingAddress>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRBillingAddress.bAccountID, Equal<Address.bAccountID>>>>, And<BqlOperand<CRBillingAddress.bAccountAddressID, IBqlInt>.IsEqual<Address.addressID>>>, And<BqlOperand<CRBillingAddress.revisionID, IBqlInt>.IsEqual<Address.revisionID>>>>.And<BqlOperand<CRBillingAddress.isDefaultAddress, IBqlBool>.IsEqual<True>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Address.bAccountID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.bAccountID, IsNotNull>>>>.Or<BqlOperand<BAccount.defAddressID, IBqlInt>.IsEqual<Address.addressID>>>>();
      pxView = sender.Graph.TypedViews.GetView(bqlCommand, false);
    }
    return (pxView, objArray);
  }
}
