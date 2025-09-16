// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CROpportunityAddressAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CR;

public class CROpportunityAddressAttribute(System.Type SelectType) : 
  CRAddressAttribute(typeof (CRAddress.addressID), typeof (CRAddress.isDefaultAddress), SelectType),
  IPXRowUpdatedSubscriber
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    CROpportunityAddressAttribute addressAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) addressAttribute, __vmethodptr(addressAttribute, Record_Override_FieldVerifying));
    fieldVerifying.AddHandler<CRAddress.overrideAddress>(pxFieldVerifying);
  }

  protected override (PXView, object[]) GetViewWithParameters(
    PXCache sender,
    object DocumentRow,
    object ContactRow)
  {
    PXView pxView = (PXView) null;
    object obj = (object) null;
    if (sender.GetValue<CROpportunity.contactID>(DocumentRow) != null)
    {
      obj = sender.GetValue<CROpportunity.contactID>(DocumentRow);
      BqlCommand bqlCommand = (BqlCommand) new SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Contact>.On<BqlOperand<Contact.defAddressID, IBqlInt>.IsEqual<Address.addressID>>>, FbqlJoins.Left<CRAddress>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRAddress.bAccountID, Equal<Address.bAccountID>>>>, And<BqlOperand<CRAddress.bAccountAddressID, IBqlInt>.IsEqual<Address.addressID>>>, And<BqlOperand<CRAddress.revisionID, IBqlInt>.IsEqual<Address.revisionID>>>>.And<BqlOperand<CRAddress.isDefaultAddress, IBqlBool>.IsEqual<boolTrue>>>>>.Where<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<P.AsInt>>();
      pxView = sender.Graph.TypedViews.GetView(bqlCommand, false);
    }
    else if (sender.GetValue<CROpportunity.bAccountID>(DocumentRow) != null)
    {
      obj = sender.GetValue<CROpportunity.bAccountID>(DocumentRow);
      BqlCommand bqlCommand = (BqlCommand) new SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.defAddressID, IBqlInt>.IsEqual<Address.addressID>>>, FbqlJoins.Left<CRAddress>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRAddress.bAccountID, Equal<Address.bAccountID>>>>, And<BqlOperand<CRAddress.bAccountAddressID, IBqlInt>.IsEqual<Address.addressID>>>, And<BqlOperand<CRAddress.revisionID, IBqlInt>.IsEqual<Address.revisionID>>>>.And<BqlOperand<CRAddress.isDefaultAddress, IBqlBool>.IsEqual<boolTrue>>>>>.Where<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<P.AsInt>>();
      pxView = sender.Graph.TypedViews.GetView(bqlCommand, false);
    }
    else if (sender.GetValue<CROpportunity.locationID>(DocumentRow) != null)
    {
      obj = sender.GetValue<CROpportunity.locationID>(DocumentRow);
      BqlCommand bqlCommand = (BqlCommand) new SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Location>.On<BqlOperand<Location.defAddressID, IBqlInt>.IsEqual<Address.addressID>>>, FbqlJoins.Left<CRAddress>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRAddress.bAccountID, Equal<Address.bAccountID>>>>, And<BqlOperand<CRAddress.bAccountAddressID, IBqlInt>.IsEqual<Address.addressID>>>, And<BqlOperand<CRAddress.revisionID, IBqlInt>.IsEqual<Address.revisionID>>>>.And<BqlOperand<CRAddress.isDefaultAddress, IBqlBool>.IsEqual<boolTrue>>>>>.Where<BqlOperand<Location.locationID, IBqlInt>.IsEqual<P.AsInt>>();
      pxView = sender.Graph.TypedViews.GetView(bqlCommand, false);
    }
    return (pxView, new object[1]{ obj });
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultAddress<CRAddress, CRAddress.addressID>(sender, DocumentRow, Row);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyAddress<CRAddress, CRAddress.addressID>(sender, DocumentRow, SourceRow, clone);
  }

  public override void Record_IsDefault_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  public virtual void Record_Override_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    PXFieldVerifyingEventArgs verifyingEventArgs1 = e;
    object obj1;
    if (e.NewValue != null)
    {
      bool? newValue = (bool?) e.NewValue;
      bool flag = false;
      obj1 = (object) (newValue.GetValueOrDefault() == flag & newValue.HasValue);
    }
    else
      obj1 = e.NewValue;
    verifyingEventArgs1.NewValue = obj1;
    try
    {
      this.Address_IsDefaultAddress_FieldVerifying<CRAddress>(sender, e);
    }
    finally
    {
      PXFieldVerifyingEventArgs verifyingEventArgs2 = e;
      object obj2;
      if (e.NewValue != null)
      {
        bool? newValue = (bool?) e.NewValue;
        bool flag = false;
        obj2 = (object) (newValue.GetValueOrDefault() == flag & newValue.HasValue);
      }
      else
        obj2 = e.NewValue;
      verifyingEventArgs2.NewValue = obj2;
    }
  }

  protected override void Record_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.Record_RowSelected(sender, e);
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<CRAddress.overrideAddress>(sender, e.Row, true);
    PXUIFieldAttribute.SetEnabled<CRAddress.isValidated>(sender, e.Row, false);
  }
}
