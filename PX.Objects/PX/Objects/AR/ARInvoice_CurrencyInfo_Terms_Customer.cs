// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoice_CurrencyInfo_Terms_Customer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AR;

public class ARInvoice_CurrencyInfo_Terms_Customer(PXGraph graph) : 
  PXSelectJoin<ARInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>, LeftJoin<PX.Objects.CS.Terms, On<PX.Objects.CS.Terms.termsID, Equal<ARInvoice.termsID>>, LeftJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, LeftJoin<PX.Objects.GL.Account, On<ARInvoice.aRAccountID, Equal<PX.Objects.GL.Account.accountID>>>>>>, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>(graph)
{
}
