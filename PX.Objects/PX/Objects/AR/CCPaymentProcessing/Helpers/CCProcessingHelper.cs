// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Helpers.CCProcessingHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Wrappers;
using PX.Objects.CA;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Helpers;

public static class CCProcessingHelper
{
  public const string CustomerPrefix = "__";
  public const string MaskedCardTmpl = "****-****-****-";
  private static readonly Regex parseCardNum = new Regex("[\\d]+", RegexOptions.Compiled);

  public static IEnumerable GetPMdetails(PXGraph graph, PX.Objects.AR.CustomerPaymentMethod cpm)
  {
    if (cpm != null)
    {
      int? pmInstanceId = cpm.PMInstanceID;
      PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select(graph, new object[1]
      {
        (object) cpm.PaymentMethodID
      }));
      string paymentType = paymentMethod?.PaymentType;
      PXResultset<CustomerPaymentMethodDetail> items = PXSelectBase<CustomerPaymentMethodDetail, PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<CustomerPaymentMethodDetail.paymentMethodID>, And<PaymentMethodDetail.detailID, Equal<CustomerPaymentMethodDetail.detailID>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>, Where<CustomerPaymentMethodDetail.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select(graph, new object[1]
      {
        (object) pmInstanceId
      });
      if (pmInstanceId.HasValue)
      {
        int? nullable = pmInstanceId;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue && ((bool?) paymentMethod?.ARIsProcessingRequired).GetValueOrDefault() && (paymentType == "CCD" || paymentType == "EFT"))
        {
          PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail> savedPaymentProfile = CCProcessingHelper.GetSavedPaymentProfile(items);
          if (savedPaymentProfile != null)
          {
            yield return (object) savedPaymentProfile;
            yield break;
          }
        }
      }
      bool ccProcCenterSupportProfileManagement = false;
      if (cpm.CCProcessingCenterID != null && (paymentType == "CCD" || paymentType == "EFT") && CCProcessingHelper.IsFeatureSupported(graph, pmInstanceId, CCProcessingFeature.ProfileManagement, false))
        ccProcCenterSupportProfileManagement = true;
      foreach (PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail> pmdetail in items)
      {
        PaymentMethodDetail paymentMethodDetail = PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail>.op_Implicit(pmdetail);
        if (!ccProcCenterSupportProfileManagement || ccProcCenterSupportProfileManagement && paymentMethodDetail.IsCCProcessingID.GetValueOrDefault())
          yield return (object) pmdetail;
      }
    }
  }

  public static string GetExpirationDateFormat(PXGraph graph, string ProcessingCenterID)
  {
    PXResultset<CCProcessingCenter> pxResultset = PXSelectBase<CCProcessingCenter, PXSelectJoin<CCProcessingCenter, LeftJoin<CCProcessingCenterDetail, On<CCProcessingCenterDetail.processingCenterID, Equal<CCProcessingCenter.processingCenterID>, And<CCProcessingCenterDetail.detailID, Equal<Required<CCProcessingCenterDetail.detailID>>>>>, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Config>.Select(graph, new object[2]
    {
      (object) "DATEFRMT",
      (object) ProcessingCenterID
    });
    if (pxResultset.Count == 0)
      return (string) null;
    CCProcessingCenterDetail processingCenterDetail = ((PXResult) pxResultset[0]).GetItem<CCProcessingCenterDetail>();
    return string.IsNullOrEmpty(processingCenterDetail.DetailID) ? (string) null : processingCenterDetail.Value;
  }

  public static bool IsCCPIDFilled(PXGraph graph, int? PMInstanceID)
  {
    if (!PMInstanceID.HasValue || PMInstanceID.Value < 0)
      return false;
    PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PXResultset<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXSelect<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select(graph, new object[1]
    {
      (object) PMInstanceID
    }));
    if (customerPaymentMethod == null)
      return false;
    PXResultset<PaymentMethodDetail> pxResultset = PXSelectBase<PaymentMethodDetail, PXSelectJoin<PaymentMethodDetail, LeftJoin<CustomerPaymentMethodDetail, On<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>, And<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>, And<CustomerPaymentMethodDetail.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>>>, Where<PaymentMethodDetail.isCCProcessingID, Equal<True>, And<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) PMInstanceID,
      (object) customerPaymentMethod.PaymentMethodID
    });
    PaymentMethodDetail paymentMethodDetail1 = pxResultset.Count > 0 ? ((PXResult) pxResultset[0]).GetItem<PaymentMethodDetail>() : (PaymentMethodDetail) null;
    CustomerPaymentMethodDetail paymentMethodDetail2 = pxResultset.Count > 0 ? ((PXResult) pxResultset[0]).GetItem<CustomerPaymentMethodDetail>() : (CustomerPaymentMethodDetail) null;
    if (CCProcessingHelper.IsTokenizedPaymentMethod(graph, PMInstanceID) && paymentMethodDetail1 == null)
      throw new PXException("To create tokenized payment methods you must first configure 'Payment Profile ID' in Payment Method's 'Settings for Use in AR'");
    return paymentMethodDetail2 != null && !string.IsNullOrEmpty(paymentMethodDetail2.Value);
  }

  public static bool IsTokenizedPaymentMethod(PXGraph graph, int? PMInstanceID)
  {
    return CCProcessingHelper.IsFeatureSupported(graph, PMInstanceID, CCProcessingFeature.ProfileManagement, false);
  }

  public static bool IsTokenizedPaymentMethod(
    PXGraph graph,
    int? PMInstanceID,
    bool CheckJustDeletedPM = false)
  {
    return CCProcessingHelper.IsFeatureSupported(graph, PMInstanceID, CCProcessingFeature.ProfileManagement, CheckJustDeletedPM);
  }

  public static bool IsHFPaymentMethod(PXGraph graph, int? pmInstanceID, bool throwOnError)
  {
    CCProcessingCenter processingCenter = CCProcessingHelper.GetProcessingCenter(graph, pmInstanceID, false);
    return CCProcessingFeatureHelper.IsFeatureSupported(processingCenter, CCProcessingFeature.HostedForm, throwOnError) || CCProcessingFeatureHelper.IsFeatureSupported(processingCenter, CCProcessingFeature.ProfileForm, throwOnError);
  }

  public static bool IsFeatureSupported(
    PXGraph graph,
    int? PMInstanceID,
    CCProcessingFeature FeatureName,
    bool CheckJustDeletedPM)
  {
    return CCProcessingFeatureHelper.IsFeatureSupported(CCProcessingHelper.GetProcessingCenter(graph, PMInstanceID, CheckJustDeletedPM), FeatureName, false);
  }

  public static CCProcessingCenter GetProcessingCenter(PXGraph graph, string ProcessingCenterID)
  {
    return PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Config>.Select(graph, new object[1]
    {
      (object) ProcessingCenterID
    }));
  }

  public static CCProcessingCenter GetProcessingCenter(
    PXGraph graph,
    int? PMInstanceID,
    bool UsePMFromCacheDeleted)
  {
    if (!PMInstanceID.HasValue && !UsePMFromCacheDeleted)
      return (CCProcessingCenter) null;
    CCProcessingCenter processingCenter = (CCProcessingCenter) null;
    if (PMInstanceID.HasValue)
      processingCenter = PXResult<PX.Objects.AR.CustomerPaymentMethod, CCProcessingCenter>.op_Implicit((PXResult<PX.Objects.AR.CustomerPaymentMethod, CCProcessingCenter>) PXResultset<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXSelectJoin<PX.Objects.AR.CustomerPaymentMethod, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>>, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select(graph, new object[1]
      {
        (object) PMInstanceID
      })));
    if (processingCenter == null & UsePMFromCacheDeleted)
    {
      IEnumerator enumerator = graph.Caches[typeof (PX.Objects.AR.CustomerPaymentMethod)].Deleted.GetEnumerator();
      if (enumerator.MoveNext())
      {
        PX.Objects.AR.CustomerPaymentMethod current = (PX.Objects.AR.CustomerPaymentMethod) enumerator.Current;
        processingCenter = CCProcessingHelper.GetProcessingCenter(graph, current.CCProcessingCenterID);
      }
    }
    return processingCenter;
  }

  public static bool? CCProcessingCenterNeedsExpDateUpdate(
    PXGraph graph,
    CCProcessingCenter ProcessingCenter)
  {
    if (!CCProcessingFeatureHelper.IsFeatureSupported(ProcessingCenter, CCProcessingFeature.ProfileManagement))
      return new bool?();
    return new bool?(PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXSelect<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>, And<PX.Objects.AR.CustomerPaymentMethod.expirationDate, IsNull>>>.Config>.Select(graph, new object[1]
    {
      (object) ProcessingCenter.ProcessingCenterID
    }).Count != 0);
  }

  public static string GetTokenizedPMsString(PXGraph graph)
  {
    List<CCProcessingCenter> processingCenterList = new List<CCProcessingCenter>();
    HashSet<string> stringSet = new HashSet<string>();
    foreach (PXResult<CCProcessingCenter> pxResult in PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter, Where<CCProcessingCenter.isActive, Equal<True>>>.Config>.Select(graph, Array.Empty<object>()))
    {
      CCProcessingCenter processingCenter = PXResult<CCProcessingCenter>.op_Implicit(pxResult);
      if (CCProcessingFeatureHelper.IsFeatureSupported(processingCenter, CCProcessingFeature.ProfileManagement))
      {
        bool? nullable = CCProcessingHelper.CCProcessingCenterNeedsExpDateUpdate(graph, processingCenter);
        bool flag = false;
        if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
          processingCenterList.Add(processingCenter);
      }
    }
    foreach (CCProcessingCenter processingCenter in processingCenterList)
    {
      foreach (PXResult<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.CA.PaymentMethod> pxResult in PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXSelectJoinGroupBy<PX.Objects.AR.CustomerPaymentMethod, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>>, Where<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>>, Aggregate<GroupBy<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>>.Config>.Select(graph, new object[1]
      {
        (object) processingCenter.ProcessingCenterID
      }))
      {
        PX.Objects.CA.PaymentMethod paymentMethod = PXResult<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.CA.PaymentMethod>.op_Implicit(pxResult);
        stringSet.Add(paymentMethod.Descr);
      }
    }
    if (stringSet.Count == 0)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string str in stringSet)
    {
      if (stringBuilder.Length > 0)
        stringBuilder.Append(", ");
      stringBuilder.Append(str);
    }
    return stringBuilder.ToString();
  }

  public static bool IsCreditCardCountEnough(int creditCardCount, int limit)
  {
    return creditCardCount != 0 && creditCardCount >= limit;
  }

  public static int CustomerProfileCountPerCustomer(
    PXGraph graph,
    int? aBAccountID,
    string aCCProcessingCenterID)
  {
    return ((PXResult) PXResultset<CustomerProcessingCenterID>.op_Implicit(PXSelectBase<CustomerProcessingCenterID, PXSelectGroupBy<CustomerProcessingCenterID, Where<CustomerProcessingCenterID.bAccountID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>, And<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>>>, Aggregate<Count<CustomerProcessingCenterID.customerCCPID>>>.Config>.Select(graph, new object[2]
    {
      (object) aBAccountID,
      (object) aCCProcessingCenterID
    }))).RowCount.GetValueOrDefault();
  }

  public static PX.Objects.AR.Customer GetCustomer(PXGraph graph, int? aBAccountID)
  {
    return PX.Objects.AR.Customer.PK.Find(graph, aBAccountID);
  }

  public static PX.Objects.AR.Customer GetCustomer(PXGraph graph, string acctCD)
  {
    return ((PXSelectBase<PX.Objects.AR.Customer>) new PXSelectReadonly<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.acctCD, Equal<Required<PX.Objects.AR.Customer.acctCD>>>>(graph)).SelectSingle(new object[1]
    {
      (object) acctCD
    });
  }

  public static string BuildPrefixForCustomerCD(
    int customerProfileCount,
    CCProcessingCenter processingCenter)
  {
    return (customerProfileCount + 1).ToString() + "__";
  }

  public static string ExtractStreetAddress(IAddressBase aAddress)
  {
    return ((IEnumerable<string>) new string[3]
    {
      aAddress.AddressLine1,
      aAddress.AddressLine2,
      aAddress.AddressLine3
    }).Where<string>((Func<string, bool>) (i => !string.IsNullOrWhiteSpace(i))).Aggregate<string, string>(string.Empty, (Func<string, string, string>) ((cur, next) => !string.IsNullOrEmpty(cur) ? $"{cur}, {next}" : next));
  }

  public static ProcessingStatus GetProcessingStatusByTranData(TransactionData tranData)
  {
    return ExtTransactionProcStatusCode.GetProcessingStatusByProcStatusStr(ExtTransactionProcStatusCode.GetStatusByTranStatusTranType(CCTranStatusCode.GetCode(V2Converter.ConvertTranStatus(tranData.TranStatus)), CCTranTypeCode.GetTypeCode(V2Converter.ConvertTranType(tranData.TranType.Value))));
  }

  public static bool IsV1ProcessingInterface(Type pluginType)
  {
    return CCPluginTypeHelper.CheckParentClass(pluginType, "PX.CCProcessingBase.ICCPaymentProcessing", 0, 3);
  }

  public static ICCProcessingPlugin IsV2ProcessingInterface(object pluginObject)
  {
    return pluginObject as ICCProcessingPlugin;
  }

  public static bool IsV2ProcessingInterface(Type pluginType)
  {
    return ((IEnumerable<Type>) pluginType.GetInterfaces()).Contains<Type>(typeof (ICCProcessingPlugin));
  }

  public static void CheckHttpsConnection()
  {
    if (HttpContext.Current?.Request != null && new Uri(HttpContext.Current.Request.GetWebsiteUrl()).Scheme != Uri.UriSchemeHttps)
      throw new PXException("For credit card processing, the HTTPS connection to the application must be established.");
  }

  private static PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail> GetSavedPaymentProfile(
    PXResultset<CustomerPaymentMethodDetail> items)
  {
    PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail> savedPaymentProfile = (PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail>) null;
    foreach (PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail> pxResult in items)
    {
      CustomerPaymentMethodDetail paymentMethodDetail = PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail>.op_Implicit(pxResult);
      if (PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail>.op_Implicit(pxResult).IsCCProcessingID.GetValueOrDefault() && !string.IsNullOrEmpty(paymentMethodDetail.Value))
      {
        savedPaymentProfile = pxResult;
        break;
      }
    }
    return savedPaymentProfile;
  }

  public static string DeleteCustomerPrefix(string customerID)
  {
    if (customerID == null)
      return customerID;
    int num = customerID.IndexOf("__");
    if (num >= 0)
      customerID = customerID.Substring(num + "__".Length);
    return customerID;
  }

  public static string GetTransactionTypeName(CCTranType tranType)
  {
    string transactionTypeName = string.Empty;
    if (tranType == CCTranType.AuthorizeOnly)
      transactionTypeName = PXMessages.LocalizeNoPrefix("authorization");
    if (CCTranTypeCode.IsCaptured(tranType))
      transactionTypeName = PXMessages.LocalizeNoPrefix("capture");
    if (tranType == CCTranType.Void)
      transactionTypeName = PXMessages.LocalizeNoPrefix("void");
    return transactionTypeName;
  }

  public static bool PaymentMethodSupportsIntegratedProcessing(PX.Objects.CA.PaymentMethod pm)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() && pm != null && EnumerableExtensions.IsIn<string>(pm.PaymentType, "CCD", "EFT", "POS") && pm.ARIsProcessingRequired.GetValueOrDefault();
  }

  public static bool IntegratedProcessingActivated(ARSetup setup)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() && setup != null && setup.IntegratedCCProcessing.GetValueOrDefault();
  }
}
