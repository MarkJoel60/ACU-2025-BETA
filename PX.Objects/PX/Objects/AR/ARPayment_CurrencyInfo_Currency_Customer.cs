// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPayment_CurrencyInfo_Currency_Customer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AR;

public class ARPayment_CurrencyInfo_Currency_Customer(PXGraph graph) : 
  PXSelectJoin<ARPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARPayment.curyInfoID>>, InnerJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<PX.Objects.CM.Extensions.CurrencyInfo.curyID>>, LeftJoin<Customer, On<Customer.bAccountID, Equal<ARPayment.customerID>>, LeftJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<ARPayment.cashAccountID>>>>>>, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>(graph)
{
}
