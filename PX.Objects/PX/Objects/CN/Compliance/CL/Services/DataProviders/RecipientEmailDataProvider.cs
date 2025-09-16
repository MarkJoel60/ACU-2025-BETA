// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Services.DataProviders.RecipientEmailDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Services.DataProviders;

public class RecipientEmailDataProvider : IRecipientEmailDataProvider
{
  private readonly PXGraph graph;

  public RecipientEmailDataProvider(PXGraph graph) => this.graph = graph;

  public string GetRecipientEmail(NotificationRecipient notificationRecipient, int? vendorId)
  {
    switch (notificationRecipient.ContactType)
    {
      case "P":
        return this.GetEmailForPrimaryVendor(vendorId);
      case "E":
        return this.GetContactEmail(notificationRecipient.ContactID);
      case "R":
        return this.GetEmailForRemittanceContact(vendorId);
      case "S":
        return this.GetEmailForShippingContact(vendorId);
      default:
        return this.GetContactEmail(notificationRecipient.ContactID);
    }
  }

  private string GetEmailForRemittanceContact(int? vendorId)
  {
    return this.GetContactEmail(this.GetLocationExtensionAddress(vendorId).VRemitContactID);
  }

  private string GetEmailForShippingContact(int? vendorId)
  {
    return this.GetContactEmail(this.GetLocationExtensionAddress(vendorId).DefContactID);
  }

  private string GetContactEmail(int? contactId)
  {
    return this.graph.Select<Contact>().SingleOrDefault<Contact>((Expression<Func<Contact, bool>>) (c => c.ContactID == contactId))?.EMail;
  }

  private string GetEmailForPrimaryVendor(int? vendorId)
  {
    return this.GetContactEmail(this.GetLocationExtensionAddress(vendorId).VDefContactID);
  }

  private Location GetLocationExtensionAddress(int? vendorId)
  {
    return PXResultset<Location>.op_Implicit(PXSelectBase<Location, PXViewOf<Location>.BasedOn<SelectFromBase<Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Location.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(this.graph, new object[1]
    {
      (object) vendorId
    }));
  }
}
