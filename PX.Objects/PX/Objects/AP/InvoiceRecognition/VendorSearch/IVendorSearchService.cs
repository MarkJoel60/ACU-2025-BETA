// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.VendorSearch.IVendorSearchService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CloudServices.DocumentRecognition;
using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.VendorSearch;

internal interface IVendorSearchService
{
  VendorSearchResult FindVendor(
    PXGraph graph,
    string vendorName,
    IList<FullTextTerm> fullTextTerms,
    string email);
}
