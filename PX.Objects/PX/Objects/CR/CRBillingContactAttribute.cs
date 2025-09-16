// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRBillingContactAttribute
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

public class CRBillingContactAttribute(System.Type SelectType) : CRContactAttribute(typeof (CRBillingContact.contactID), typeof (CRBillingContact.isDefaultContact), SelectType)
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    CRBillingContactAttribute contactAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) contactAttribute, __vmethodptr(contactAttribute, Record_Override_FieldVerifying));
    fieldVerifying.AddHandler<CRBillingContact.overrideContact>(pxFieldVerifying);
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultContact<CRBillingContact, CRBillingContact.contactID>(sender, DocumentRow, Row);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyContact<CRBillingContact, CRBillingContact.contactID>(sender, DocumentRow, SourceRow, clone);
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
    this.Contact_IsDefaultContact_FieldVerifying<CRBillingContact>(sender, new PXFieldVerifyingEventArgs(e.Row, obj2, e.ExternalCall));
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
      BqlCommand bqlCommand = (BqlCommand) new SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<BAccount>.On<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<Contact.bAccountID>>>, FbqlJoins.Left<Customer>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.bAccountID, Equal<Contact.bAccountID>>>>>.And<BqlOperand<Customer.defBillContactID, IBqlInt>.IsEqual<Contact.contactID>>>>, FbqlJoins.Left<CRBillingContact>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRBillingContact.bAccountID, Equal<Contact.bAccountID>>>>, And<BqlOperand<CRBillingContact.bAccountContactID, IBqlInt>.IsEqual<Contact.contactID>>>, And<BqlOperand<CRBillingContact.revisionID, IBqlInt>.IsEqual<Contact.revisionID>>>>.And<BqlOperand<CRBillingContact.isDefaultContact, IBqlBool>.IsEqual<True>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.bAccountID, Equal<P.AsInt>>>>, Or<BqlOperand<BAccount.defContactID, IBqlInt>.IsEqual<Contact.contactID>>>>.And<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>();
      pxView = sender.Graph.TypedViews.GetView(bqlCommand, false);
    }
    return (pxView, objArray);
  }
}
