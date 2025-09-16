// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoice_CurrencyInfo_Terms_Vendor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public class APInvoice_CurrencyInfo_Terms_Vendor(PXGraph graph) : 
  PXSelectJoin<APInvoice, LeftJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, LeftJoin<PX.Objects.CS.Terms, On<PX.Objects.CS.Terms.termsID, Equal<APInvoice.termsID>>, LeftJoin<Vendor, On<Vendor.bAccountID, Equal<APInvoice.vendorID>>>>>, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>(graph)
{
}
