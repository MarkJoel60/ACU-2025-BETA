// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPayment_CurrencyInfo_Currency_Vendor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public class APPayment_CurrencyInfo_Currency_Vendor(PXGraph graph) : 
  PXSelectJoin<APPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APPayment.curyInfoID>>, InnerJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<PX.Objects.CM.Extensions.CurrencyInfo.curyID>>, LeftJoin<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>>, LeftJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<APPayment.cashAccountID>>>>>>, Where<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>(graph)
{
}
