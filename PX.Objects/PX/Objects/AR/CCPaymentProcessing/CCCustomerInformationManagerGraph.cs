// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.CCCustomerInformationManagerGraph
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing;

public class CCCustomerInformationManagerGraph : PXGraph<CCCustomerInformationManagerGraph>
{
  public virtual void GetOrCreatePaymentProfile(
    PXGraph graph,
    ICCPaymentProfileAdapter paymentProfileAdapter,
    ICCPaymentProfileDetailAdapter profileDetailAdapter)
  {
    CCCustomerInformationManager.GetOrCreatePaymentProfile(graph, paymentProfileAdapter, profileDetailAdapter);
  }

  public virtual void GetCreatePaymentProfileForm(
    PXGraph graph,
    ICCPaymentProfileAdapter ccPaymentProfileAdapter)
  {
    CCCustomerInformationManager.GetCreatePaymentProfileForm(graph, ccPaymentProfileAdapter);
  }

  public virtual void GetManagePaymentProfileForm(PXGraph graph, ICCPaymentProfile paymentProfile)
  {
    CCCustomerInformationManager.GetManagePaymentProfileForm(graph, paymentProfile);
  }

  public virtual void GetNewPaymentProfiles(
    PXGraph graph,
    ICCPaymentProfileAdapter payment,
    ICCPaymentProfileDetailAdapter paymentDetail)
  {
    CCCustomerInformationManager.GetNewPaymentProfiles(graph, payment, paymentDetail);
  }

  public virtual void GetPaymentProfile(
    PXGraph graph,
    ICCPaymentProfileAdapter payment,
    ICCPaymentProfileDetailAdapter paymentDetail)
  {
    CCCustomerInformationManager.GetPaymentProfile(graph, payment, paymentDetail);
  }

  public virtual PXResultset<CustomerPaymentMethodDetail> GetAllCustomersCardsInProcCenter(
    PXGraph graph,
    int? BAccountID,
    string CCProcessingCenterID)
  {
    return CCCustomerInformationManager.GetAllCustomersCardsInProcCenter(graph, BAccountID, CCProcessingCenterID);
  }

  public virtual void DeletePaymentProfile(
    PXGraph graph,
    ICCPaymentProfileAdapter payment,
    ICCPaymentProfileDetailAdapter paymentDetail)
  {
    CCCustomerInformationManager.DeletePaymentProfile(graph, payment, paymentDetail);
  }

  public virtual TranProfile GetOrCreatePaymentProfileByTran(
    PXGraph graph,
    ICCPaymentProfileAdapter payment,
    string tranId)
  {
    return CCCustomerInformationManager.GetOrCreatePaymentProfileByTran(graph, payment, tranId);
  }

  public virtual void ProcessProfileFormResponse(
    PXGraph graph,
    ICCPaymentProfileAdapter payment,
    ICCPaymentProfileDetailAdapter paymentDetail,
    string response)
  {
    CCCustomerInformationManager.ProcessProfileFormResponse(graph, payment, paymentDetail, response);
  }
}
