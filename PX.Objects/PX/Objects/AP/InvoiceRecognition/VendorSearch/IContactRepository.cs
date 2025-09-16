// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.VendorSearch.IContactRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.VendorSearch;

internal interface IContactRepository
{
  PX.Objects.CR.Contact GetAccountContact(PXGraph graph, int baccountId, int defContactId);

  PX.Objects.CR.Contact GetPrimaryContact(PXGraph graph, int baccountId, int primaryContactID);

  List<string> GetOtherContactEmails(
    PXGraph graph,
    int baccountId,
    PX.Objects.CR.Contact accountContact,
    PX.Objects.CR.Contact primaryContact);
}
