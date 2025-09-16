// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.InvoiceHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class InvoiceHelper
{
  public static bool IsRunningServiceContractBilling(PXGraph graph)
  {
    return graph.Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS501300");
  }

  public static void GetChildCustomerShippingContactAndAddress(
    PXGraph graph,
    int? serviceContractID,
    out PX.Objects.CR.Contact shippingContact,
    out PX.Objects.CR.Address shippingAddress)
  {
    shippingContact = (PX.Objects.CR.Contact) null;
    shippingAddress = (PX.Objects.CR.Address) null;
    if (!serviceContractID.HasValue)
      return;
    FSServiceContract fsServiceContract = PXResultset<FSServiceContract>.op_Implicit(PXSelectBase<FSServiceContract, PXSelect<FSServiceContract, Where<FSServiceContract.serviceContractID, Equal<Required<FSServiceContract.serviceContractID>>>>.Config>.Select(graph, new object[1]
    {
      (object) serviceContractID
    }));
    if (fsServiceContract == null)
      return;
    int? customerId = fsServiceContract.CustomerID;
    int? nullable = fsServiceContract.BillCustomerID;
    if (customerId.GetValueOrDefault() == nullable.GetValueOrDefault() & customerId.HasValue == nullable.HasValue)
      return;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) fsServiceContract.CustomerID
    }));
    if (customer == null)
      return;
    nullable = customer.DefLocationID;
    if (!nullable.HasValue)
      return;
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) customer.BAccountID,
      (object) customer.DefLocationID
    }));
    if (location == null)
      return;
    PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) null;
    PX.Objects.CR.Address address = (PX.Objects.CR.Address) null;
    nullable = location.DefContactID;
    if (nullable.HasValue)
      contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select(graph, new object[1]
      {
        (object) location.DefContactID
      }));
    nullable = location.DefAddressID;
    if (nullable.HasValue)
      address = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select(graph, new object[1]
      {
        (object) location.DefAddressID
      }));
    shippingContact = contact;
    shippingAddress = address;
  }

  public static IInvoiceGraph CreateInvoiceGraph(string targetScreen)
  {
    switch (targetScreen)
    {
      case "SO":
        if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
          return (IInvoiceGraph) ((PXGraph) PXGraph.CreateInstance<SOOrderEntry>()).GetExtension<SM_SOOrderEntry>();
        throw new PXException("The Distribution module is disabled.");
      case "SI":
        if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() && PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>())
          return (IInvoiceGraph) ((PXGraph) PXGraph.CreateInstance<SOInvoiceEntry>()).GetExtension<SM_SOInvoiceEntry>();
        throw new PXException("The SO invoice cannot be generated because the Advanced SO Invoices feature is disabled on the Enable/Disable Features (CS100000) form. Please contact your system administrator.");
      case "AR":
        return (IInvoiceGraph) ((PXGraph) PXGraph.CreateInstance<ARInvoiceEntry>()).GetExtension<SM_ARInvoiceEntry>();
      case "AP":
        return (IInvoiceGraph) ((PXGraph) PXGraph.CreateInstance<APInvoiceEntry>()).GetExtension<SM_APInvoiceEntry>();
      case "PM":
        return (IInvoiceGraph) ((PXGraph) PXGraph.CreateInstance<RegisterEntry>()).GetExtension<SM_RegisterEntry>();
      case "IN":
        return (IInvoiceGraph) ((PXGraph) PXGraph.CreateInstance<INIssueEntry>()).GetExtension<SM_INIssueEntry>();
      default:
        throw new PXException("The posting module '{0}' is invalid.", new object[1]
        {
          (object) targetScreen
        });
    }
  }

  public static bool AreAppointmentsPostedInSO(PXGraph graph, int? sOID)
  {
    if (!sOID.HasValue)
      return false;
    return ((IQueryable<PXResult<FSAppointment>>) PXSelectBase<FSAppointment, PXSelectReadonly<FSAppointment, Where<FSAppointment.pendingAPARSOPost, Equal<False>, And<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>>>>.Config>.Select(graph, new object[1]
    {
      (object) sOID
    })).Count<PXResult<FSAppointment>>() > 0;
  }

  public static void CopyContact(IContact dest, IContact source)
  {
    ContactAttribute.CopyContact(dest, source);
    dest.Attention = source.Attention;
  }

  public static void CopyAddress(IAddress dest, IAddress source)
  {
    AddressAttribute.Copy(dest, source);
    dest.IsValidated = source.IsValidated;
  }

  public static void CopyAddress(IAddress dest, PX.Objects.CR.Address source)
  {
    InvoiceHelper.CopyAddress(dest, InvoiceHelper.GetIAddress(source));
  }

  public static IAddress GetIAddress(PX.Objects.CR.Address source)
  {
    if (source == null)
      return (IAddress) null;
    return (IAddress) new CRAddress()
    {
      BAccountID = source.BAccountID,
      RevisionID = source.RevisionID,
      IsDefaultAddress = new bool?(false),
      AddressLine1 = source.AddressLine1,
      AddressLine2 = source.AddressLine2,
      AddressLine3 = source.AddressLine3,
      City = source.City,
      CountryID = source.CountryID,
      State = source.State,
      PostalCode = source.PostalCode,
      IsValidated = source.IsValidated
    };
  }
}
