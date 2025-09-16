// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.VendorSearch.IVendorRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.VendorSearch;

internal interface IVendorRepository
{
  Vendor GetActiveVendorByNoteId(PXGraph graph, Guid noteId);

  bool IsExcludedDomain(string domain);

  IEnumerable<Vendor> GetVendorsByEmail(PXGraph graph, string email);

  (string DomainQuery, IEnumerable<Vendor> Results) GetVendorsByDomain(PXGraph graph, string domain);

  int? GetActiveVendorIdByVendorName(PXGraph graph, string vendorName);
}
