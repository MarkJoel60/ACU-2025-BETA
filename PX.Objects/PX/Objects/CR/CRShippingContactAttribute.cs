// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRShippingContactAttribute
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

public class CRShippingContactAttribute(System.Type SelectType) : CRContactAttribute(typeof (CRShippingContact.contactID), typeof (CRShippingContact.isDefaultContact), SelectType)
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    CRShippingContactAttribute contactAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) contactAttribute, __vmethodptr(contactAttribute, Record_Override_FieldVerifying));
    fieldVerifying.AddHandler<CRShippingContact.overrideContact>(pxFieldVerifying);
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultContact<CRShippingContact, CRShippingContact.contactID>(sender, DocumentRow, Row);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyContact<CRShippingContact, CRShippingContact.contactID>(sender, DocumentRow, SourceRow, clone);
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
    this.Contact_IsDefaultContact_FieldVerifying<CRShippingContact>(sender, new PXFieldVerifyingEventArgs(e.Row, obj2, e.ExternalCall));
  }

  protected override (PXView, object[]) GetViewWithParameters(
    PXCache sender,
    object DocumentRow,
    object ContactRow)
  {
    PXView pxView = (PXView) null;
    object obj = (object) null;
    if (sender.GetValue<CROpportunity.locationID>(DocumentRow) != null)
    {
      obj = sender.GetValue<CROpportunity.locationID>(DocumentRow);
      BqlCommand bqlCommand = (BqlCommand) new SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Location>.On<BqlOperand<Location.defContactID, IBqlInt>.IsEqual<Contact.contactID>>>, FbqlJoins.Left<CRShippingContact>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRShippingContact.bAccountID, Equal<Contact.bAccountID>>>>, And<BqlOperand<CRShippingContact.bAccountContactID, IBqlInt>.IsEqual<Contact.contactID>>>, And<BqlOperand<CRShippingContact.revisionID, IBqlInt>.IsEqual<Contact.revisionID>>>>.And<BqlOperand<CRShippingContact.isDefaultContact, IBqlBool>.IsEqual<boolTrue>>>>>.Where<BqlOperand<Location.locationID, IBqlInt>.IsEqual<P.AsInt>>();
      pxView = sender.Graph.TypedViews.GetView(bqlCommand, false);
    }
    else if (sender.GetValue<CROpportunity.bAccountID>(DocumentRow) != null)
    {
      obj = sender.GetValue<CROpportunity.bAccountID>(DocumentRow);
      BqlCommand bqlCommand = (BqlCommand) new SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.defContactID, IBqlInt>.IsEqual<Contact.contactID>>>, FbqlJoins.Left<CRShippingContact>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRShippingContact.bAccountID, Equal<Contact.bAccountID>>>>, And<BqlOperand<CRShippingContact.bAccountContactID, IBqlInt>.IsEqual<Contact.contactID>>>, And<BqlOperand<CRShippingContact.revisionID, IBqlInt>.IsEqual<Contact.revisionID>>>>.And<BqlOperand<CRShippingContact.isDefaultContact, IBqlBool>.IsEqual<boolTrue>>>>>.Where<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<P.AsInt>>();
      pxView = sender.Graph.TypedViews.GetView(bqlCommand, false);
    }
    return (pxView, new object[1]{ obj });
  }
}
