// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BAccountCascadeAttribute
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

public class BAccountCascadeAttribute : PXEventSubscriberAttribute, IPXRowDeletedSubscriber
{
  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is BAccount row) || row.Type == "VC")
      return;
    bool? isBranch = row.IsBranch;
    if (isBranch.HasValue && isBranch.GetValueOrDefault() && row.Type != "CP")
      return;
    this.DetachNonAP(sender.Graph, row);
    foreach (PXResult<BAccount> rec in PXSelectBase<BAccount, PXViewOf<BAccount>.BasedOn<SelectFromBase<BAccount, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Contact>.On<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<BAccount.defContactID>>>, FbqlJoins.Left<Address>.On<BqlOperand<Address.addressID, IBqlInt>.IsEqual<BAccount.defAddressID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.bAccountID, Equal<P.AsInt>>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.contactID, IsNotNull>>>>.Or<BqlOperand<Address.addressID, IBqlInt>.IsNotNull>>>>>.ReadOnly.Config>.Select(sender.Graph, new object[1]
    {
      (object) row.BAccountID
    }))
      this.DeleteAP(sender.Graph, (PXResult) rec);
    foreach (PXResult<Customer> rec in PXSelectBase<Customer, PXViewOf<Customer>.BasedOn<SelectFromBase<Customer, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Contact>.On<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<Customer.defBillContactID>>>, FbqlJoins.Left<Address>.On<BqlOperand<Address.addressID, IBqlInt>.IsEqual<Customer.defBillAddressID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.bAccountID, Equal<P.AsInt>>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.contactID, IsNotNull>>>>.Or<BqlOperand<Address.addressID, IBqlInt>.IsNotNull>>>>>.ReadOnly.Config>.Select(sender.Graph, new object[1]
    {
      (object) row.BAccountID
    }))
      this.DeleteAP(sender.Graph, (PXResult) rec);
    foreach (PXResult<BAccount> rec in PXSelectBase<BAccount, PXViewOf<BAccount>.BasedOn<SelectFromBase<BAccount, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Standalone.Location>.On<BqlOperand<PX.Objects.CR.Standalone.Location.locationID, IBqlInt>.IsEqual<BAccount.defLocationID>>>, FbqlJoins.Left<Contact>.On<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<PX.Objects.CR.Standalone.Location.defContactID>>>, FbqlJoins.Left<Address>.On<BqlOperand<Address.addressID, IBqlInt>.IsEqual<PX.Objects.CR.Standalone.Location.defAddressID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.bAccountID, Equal<P.AsInt>>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.contactID, IsNotNull>>>>.Or<BqlOperand<Address.addressID, IBqlInt>.IsNotNull>>>>>.ReadOnly.Config>.Select(sender.Graph, new object[1]
    {
      (object) row.BAccountID
    }))
      this.DeleteAP(sender.Graph, (PXResult) rec);
    foreach (PXResult<BAccount> rec in PXSelectBase<BAccount, PXViewOf<BAccount>.BasedOn<SelectFromBase<BAccount, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Standalone.Location>.On<BqlOperand<PX.Objects.CR.Standalone.Location.locationID, IBqlInt>.IsEqual<BAccount.defLocationID>>>, FbqlJoins.Left<Contact>.On<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<PX.Objects.CR.Standalone.Location.vRemitContactID>>>, FbqlJoins.Left<Address>.On<BqlOperand<Address.addressID, IBqlInt>.IsEqual<PX.Objects.CR.Standalone.Location.vRemitAddressID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.bAccountID, Equal<P.AsInt>>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.contactID, IsNotNull>>>>.Or<BqlOperand<Address.addressID, IBqlInt>.IsNotNull>>>>>.ReadOnly.Config>.Select(sender.Graph, new object[1]
    {
      (object) row.BAccountID
    }))
      this.DeleteAP(sender.Graph, (PXResult) rec);
  }

  protected virtual void DetachNonAP(PXGraph graph, BAccount baccount)
  {
    foreach (PXResult<Contact> pxResult in PXSelectBase<Contact, PXViewOf<Contact>.BasedOn<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Address>.On<BqlOperand<Address.addressID, IBqlInt>.IsEqual<Contact.defAddressID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.bAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<Contact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.person>>>>.ReadOnly.Config>.Select(graph, new object[1]
    {
      (object) baccount.BAccountID
    }))
    {
      Contact contact = ((PXResult) pxResult).GetItem<Contact>();
      Address address = ((PXResult) pxResult).GetItem<Address>();
      contact.BAccountID = new int?();
      graph.Caches[typeof (Contact)].Update((object) contact);
      address.BAccountID = new int?();
      graph.Caches[typeof (Address)].Update((object) address);
    }
  }

  protected virtual void DeleteAP(PXGraph graph, PXResult rec)
  {
    Contact contact = rec.GetItem<Contact>();
    Address address = rec.GetItem<Address>();
    if (contact != null && contact.ContactID.HasValue)
      GraphHelper.Caches<Contact>(graph).Delete(contact);
    if (address == null || !address.AddressID.HasValue)
      return;
    GraphHelper.Caches<Address>(graph).Delete(address);
  }
}
