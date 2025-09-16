// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethodDetailHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class PaymentMethodDetailHelper
{
  public static void PaymentMethodDetailValueFieldSelecting(
    PXGraph graph,
    PX.Data.Events.FieldSelecting<PaymentMethodDetail, PaymentMethodDetail.defaultValue> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PaymentMethodDetail, PaymentMethodDetail.defaultValue>>) e).ReturnState = PaymentMethodDetailHelper.ApplyControlType(((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PaymentMethodDetail, PaymentMethodDetail.defaultValue>>) e).ReturnState, (int?) e.Row?.ControlType, "DefaultValue");
  }

  public static void CashAccountDetailValueFieldSelecting(
    PXGraph graph,
    PX.Data.Events.FieldSelecting<CashAccountPaymentMethodDetail, CashAccountPaymentMethodDetail.detailValue> e)
  {
    if (e.Row == null)
      return;
    PaymentMethodDetail paymentMethodDetail = PXResultset<PaymentMethodDetail>.op_Implicit(PXSelectBase<PaymentMethodDetail, PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<CashAccountPaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.detailID, Equal<Required<CashAccountPaymentMethodDetail.detailID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) e.Row.PaymentMethodID,
      (object) e.Row.DetailID
    }));
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<CashAccountPaymentMethodDetail, CashAccountPaymentMethodDetail.detailValue>>) e).ReturnState = PaymentMethodDetailHelper.ApplyControlType(((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<CashAccountPaymentMethodDetail, CashAccountPaymentMethodDetail.detailValue>>) e).ReturnState, (int?) paymentMethodDetail?.ControlType);
  }

  public static void VendorDetailValueFieldSelecting(
    PXGraph graph,
    PX.Data.Events.FieldSelecting<VendorPaymentMethodDetail, VendorPaymentMethodDetail.detailValue> e)
  {
    if (e.Row == null)
      return;
    PaymentMethodDetail paymentMethodDetail = PXResultset<PaymentMethodDetail>.op_Implicit(PXSelectBase<PaymentMethodDetail, PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<VendorPaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.detailID, Equal<Required<VendorPaymentMethodDetail.detailID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) e.Row.PaymentMethodID,
      (object) e.Row.DetailID
    }));
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<VendorPaymentMethodDetail, VendorPaymentMethodDetail.detailValue>>) e).ReturnState = PaymentMethodDetailHelper.ApplyControlType(((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<VendorPaymentMethodDetail, VendorPaymentMethodDetail.detailValue>>) e).ReturnState, (int?) paymentMethodDetail?.ControlType);
  }

  private static object ApplyControlType(
    object origReturnState,
    int? controlType,
    string fieldName = "DetailValue")
  {
    if (!controlType.HasValue)
      return origReturnState;
    int? nullable1 = controlType;
    PaymentMethodDetailType? nullable2 = nullable1.HasValue ? new PaymentMethodDetailType?((PaymentMethodDetailType) nullable1.GetValueOrDefault()) : new PaymentMethodDetailType?();
    if (nullable2.HasValue)
    {
      switch (nullable2.GetValueOrDefault())
      {
        case PaymentMethodDetailType.Text:
          return origReturnState;
        case PaymentMethodDetailType.AccountType:
          List<string> source1 = new List<string>();
          List<string> stringList1 = new List<string>();
          source1.Add("22");
          stringList1.Add("Checking Account");
          source1.Add("32");
          stringList1.Add("Savings Account");
          return (object) PXStringState.CreateInstance(origReturnState, new int?(10), new bool?(true), fieldName, new bool?(false), new int?(-1), (string) null, source1.ToArray(), stringList1.ToArray(), new bool?(true), source1.First<string>(), (string[]) null);
        case PaymentMethodDetailType.TransactionCode:
          List<string> source2 = new List<string>();
          List<string> stringList2 = new List<string>();
          source2.Add("22");
          stringList2.Add("22 Demand credit");
          source2.Add("32");
          stringList2.Add("32 Savings credit");
          return (object) PXStringState.CreateInstance(origReturnState, new int?(10), new bool?(true), fieldName, new bool?(false), new int?(-1), (string) null, source2.ToArray(), stringList2.ToArray(), new bool?(true), source2.First<string>(), (string[]) null);
        case PaymentMethodDetailType.CountryCode:
          List<string> source3 = new List<string>();
          List<string> stringList3 = new List<string>();
          source3.Add("US");
          stringList3.Add("US");
          source3.Add("CA");
          stringList3.Add("CA");
          return (object) PXStringState.CreateInstance(origReturnState, new int?(10), new bool?(true), fieldName, new bool?(false), new int?(-1), (string) null, source3.ToArray(), stringList3.ToArray(), new bool?(true), source3.First<string>(), (string[]) null);
        case PaymentMethodDetailType.CurrencyCode:
          List<string> source4 = new List<string>();
          List<string> stringList4 = new List<string>();
          source4.Add("USD");
          stringList4.Add("USD");
          source4.Add("CAD");
          stringList4.Add("CAD");
          return (object) PXStringState.CreateInstance(origReturnState, new int?(10), new bool?(true), fieldName, new bool?(false), new int?(-1), (string) null, source4.ToArray(), stringList4.ToArray(), new bool?(true), source4.First<string>(), (string[]) null);
        case PaymentMethodDetailType.TransactionTypeCode:
          List<string> source5 = new List<string>();
          List<string> stringList5 = new List<string>();
          source5.Add("BUS");
          stringList5.Add("BUS (Business/Commercial)");
          source5.Add("DEP");
          stringList5.Add("DEP (Deposit)");
          source5.Add("MIS");
          stringList5.Add("MIS (Miscallaneous)");
          return (object) PXStringState.CreateInstance(origReturnState, new int?(10), new bool?(true), fieldName, new bool?(false), new int?(-1), (string) null, source5.ToArray(), stringList5.ToArray(), new bool?(true), source5.First<string>(), (string[]) null);
      }
    }
    return origReturnState;
  }
}
