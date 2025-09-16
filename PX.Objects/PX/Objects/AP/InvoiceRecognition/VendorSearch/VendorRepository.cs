// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.VendorSearch.VendorRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.VendorSearch;

internal class VendorRepository : IVendorRepository
{
  private const string _domainAtPrefix = "@";

  public Vendor GetActiveVendorByNoteId(PXGraph graph, Guid noteId)
  {
    PXCache cach = graph.Caches[typeof (VendorR)];
    if (cach.LocateByNoteID(noteId) != 1)
      return (Vendor) null;
    return !(cach.Current is Vendor current) || current.VStatus == "H" || current.VStatus == "I" ? (Vendor) null : current;
  }

  public bool IsExcludedDomain(string domain)
  {
    ExcludedVendorDomainDefinition slot = ExcludedVendorDomainDefinition.GetSlot();
    return slot != null && slot.Contains(domain);
  }

  public IEnumerable<Vendor> GetVendorsByEmail(PXGraph graph, string email)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(email, email, (string) null);
    return PXSelectBase<Vendor, PXViewOf<Vendor>.BasedOn<SelectFromBase<Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CR.Contact>.On<BqlOperand<Vendor.bAccountID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.bAccountID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.CR.Contact.eMail, Equal<P.AsString>>>>, PX.Data.And<BqlOperand<Vendor.vStatus, IBqlString>.IsNotEqual<VendorStatus.hold>>>>.And<BqlOperand<Vendor.vStatus, IBqlString>.IsNotEqual<VendorStatus.inactive>>>.Aggregate<To<GroupBy<Vendor.bAccountID>>>>.ReadOnly.Config>.Select(graph, (object) email).FirstTableItems;
  }

  public (string DomainQuery, IEnumerable<Vendor> Results) GetVendorsByDomain(
    PXGraph graph,
    string domain)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(domain, nameof (domain), (string) null);
    string str = "@" + domain;
    List<Vendor> list = PXSelectBase<Vendor, PXViewOf<Vendor>.BasedOn<SelectFromBase<Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CR.Contact>.On<BqlOperand<Vendor.bAccountID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>>, PX.Data.And<BqlOperand<PX.Objects.CR.Contact.eMail, IBqlString>.EndsWith<P.AsString>>>, PX.Data.And<BqlOperand<Vendor.vStatus, IBqlString>.IsNotEqual<VendorStatus.hold>>>>.And<BqlOperand<Vendor.vStatus, IBqlString>.IsNotEqual<VendorStatus.inactive>>>.Aggregate<To<GroupBy<Vendor.bAccountID>>>>.ReadOnly.Config>.Select(graph, (object) str).FirstTableItems.ToList<Vendor>();
    if (list.Count > 0)
      return (str, (IEnumerable<Vendor>) list);
    IEnumerable<Vendor> firstTableItems = PXSelectBase<Vendor, PXViewOf<Vendor>.BasedOn<SelectFromBase<Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CR.Contact>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Vendor.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>>>>>.And<BqlOperand<PX.Objects.CR.BAccount.primaryContactID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.contactID>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>>>>, PX.Data.And<BqlOperand<PX.Objects.CR.Contact.eMail, IBqlString>.EndsWith<P.AsString>>>, PX.Data.And<BqlOperand<Vendor.vStatus, IBqlString>.IsNotEqual<VendorStatus.hold>>>>.And<BqlOperand<Vendor.vStatus, IBqlString>.IsNotEqual<VendorStatus.inactive>>>>.ReadOnly.Config>.Select(graph, (object) str).FirstTableItems;
    return (str, firstTableItems);
  }

  public int? GetActiveVendorIdByVendorName(PXGraph graph, string vendorName)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(vendorName, vendorName, (string) null);
    string vendorPrefixFromName = RecognizedVendorMapping.GetVendorPrefixFromName(vendorName);
    RecognizedVendorMapping topFirst = PXSelectBase<RecognizedVendorMapping, PXViewOf<RecognizedVendorMapping>.BasedOn<SelectFromBase<RecognizedVendorMapping, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<RecognizedVendorMapping.vendorNamePrefix, Equal<P.AsString>>>>>.And<BqlOperand<RecognizedVendorMapping.vendorName, IBqlString>.IsEqual<P.AsString>>>>.ReadOnly.Config>.Select(graph, (object) vendorPrefixFromName, (object) vendorName)?.TopFirst;
    if (topFirst == null)
      return new int?();
    Vendor vendor = Vendor.PK.Find(graph, topFirst.VendorID);
    return vendor == null || vendor.VStatus == "H" || vendor.VStatus == "I" ? new int?() : topFirst.VendorID;
  }
}
