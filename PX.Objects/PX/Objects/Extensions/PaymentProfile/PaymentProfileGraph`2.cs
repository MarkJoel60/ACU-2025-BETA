// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentProfile.PaymentProfileGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.Repositories;
using PX.Objects.CA;
using PX.Objects.CC.PaymentProcessing;
using System;
using System.Collections;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Objects.Extensions.PaymentProfile;

public abstract class PaymentProfileGraph<TGraph, TPrimary> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  public PXSelectExtension<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod> CustomerPaymentMethod;
  public PXSelectExtension<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail> CustomerPaymentMethodDetail;
  public PXSelectExtension<PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail> PaymentMethodDetail;
  public PXAction<TPrimary> createCCPaymentMethodHF;
  public PXAction<TPrimary> syncCCPaymentMethods;
  public PXAction<TPrimary> manageCCPaymentMethodHF;
  private CustomerRepository customerRepo;

  [InjectDependency]
  internal PaymentConnectorCallbackService PaymentCallbackService { get; set; }

  [PXUIField(DisplayName = "Create New", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton(DisplayOnMainToolbar = false, PopupCommand = "syncCCPaymentMethods")]
  public virtual IEnumerable CreateCCPaymentMethodHF(PXAdapter adapter)
  {
    this.Base.Caches[typeof (PX.Objects.AR.Customer)].Clear();
    this.Base.Caches[typeof (PX.Objects.AR.Customer)].ClearQueryCache();
    PXTrace.WriteInformation(this.GetClassMethodName(nameof (CreateCCPaymentMethodHF)) + " started.");
    PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod current = this.CustomerPaymentMethod.Current;
    if (!CCProcessingHelper.IsHFPaymentMethod((PXGraph) this.Base, current.PMInstanceID, true))
      throw new PXException("The processing plug-in does not support the creation or editing of payment profiles by using hosted forms. Please configure processing centers to use processing plug-ins that support hosted forms.");
    if (current.CCProcessingCenterID == null)
    {
      this.CustomerPaymentMethod.Cache.SetDefaultExt<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.cCProcessingCenterID>((object) current);
      this.CustomerPaymentMethod.Cache.SetDefaultExt<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.customerCCPID>((object) current);
    }
    this.CheckBeforeProcessing();
    PXGraph.CreateInstance<CCCustomerInformationManagerGraph>().GetCreatePaymentProfileForm((PXGraph) this.Base, (ICCPaymentProfileAdapter) new GenericCCPaymentProfileAdapter<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod>((PXSelectBase<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod>) this.CustomerPaymentMethod));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Sync", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable SyncCCPaymentMethods(PXAdapter adapter)
  {
    string classMethodName = this.GetClassMethodName(nameof (SyncCCPaymentMethods));
    PXTrace.WriteInformation(classMethodName + " started.");
    PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod current = this.CustomerPaymentMethod.Current;
    PXTrace.WriteInformation($"{classMethodName}. CCProcessingCenterID:{current.CCProcessingCenterID}; UserName:{this.Base.Accessinfo.UserName}");
    IEnumerable enumerable = adapter.Get();
    if (this.PaymentCallbackService.FromCCPaymentPanelCallback().IsCancelled.GetValueOrDefault() || adapter.CommandArguments == "__CLOSECCHFORM")
      return enumerable;
    if (current.CCProcessingCenterID == null)
    {
      this.CustomerPaymentMethod.Cache.SetDefaultExt<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.cCProcessingCenterID>((object) current);
      this.CustomerPaymentMethod.Cache.SetDefaultExt<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.customerCCPID>((object) current);
    }
    CCCustomerInformationManagerGraph instance = PXGraph.CreateInstance<CCCustomerInformationManagerGraph>();
    ICCPaymentProfileAdapter payment = (ICCPaymentProfileAdapter) new GenericCCPaymentProfileAdapter<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod>((PXSelectBase<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod>) this.CustomerPaymentMethod);
    ICCPaymentProfileDetailAdapter paymentDetail = (ICCPaymentProfileDetailAdapter) new GenericCCPaymentProfileDetailAdapter<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail, PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail>((PXSelectBase<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail>) this.CustomerPaymentMethodDetail, (PXSelectBase<PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail>) this.PaymentMethodDetail);
    if (CCProcessingFeatureHelper.IsFeatureSupported(this.GetProcessingCenterById(current.CCProcessingCenterID), CCProcessingFeature.ProfileForm, false))
    {
      string commandArguments = adapter.CommandArguments;
      instance.ProcessProfileFormResponse((PXGraph) this.Base, payment, paymentDetail, commandArguments);
    }
    else if (!CCProcessingHelper.IsCCPIDFilled((PXGraph) this.Base, this.CustomerPaymentMethod.Current.PMInstanceID))
      instance.GetNewPaymentProfiles((PXGraph) this.Base, payment, paymentDetail);
    else
      instance.GetPaymentProfile((PXGraph) this.Base, payment, paymentDetail);
    this.Base.Persist();
    return enumerable;
  }

  [PXUIField(DisplayName = "Edit", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ManageCCPaymentMethodHF(PXAdapter adapter)
  {
    this.Base.Caches[typeof (PX.Objects.AR.Customer)].Clear();
    this.Base.Caches[typeof (PX.Objects.AR.Customer)].ClearQueryCache();
    ICCPaymentProfile current = (ICCPaymentProfile) this.CustomerPaymentMethod.Current;
    string classMethodName = this.GetClassMethodName(nameof (ManageCCPaymentMethodHF));
    PXTrace.WriteInformation(classMethodName + " started.");
    PXTrace.WriteInformation($"{classMethodName}; CCProcessingCenterID:{current.CCProcessingCenterID}.");
    if (!CCProcessingHelper.IsHFPaymentMethod((PXGraph) this.Base, current.PMInstanceID, true))
      throw new PXException("The processing plug-in does not support the creation or editing of payment profiles by using hosted forms. Please configure processing centers to use processing plug-ins that support hosted forms.");
    PXGraph.CreateInstance<CCCustomerInformationManagerGraph>().GetManagePaymentProfileForm((PXGraph) this.Base, current);
    return adapter.Get();
  }

  protected virtual void RefreshCreatePaymentAction(bool enable, bool visible)
  {
    this.createCCPaymentMethodHF.SetVisible(visible);
    this.createCCPaymentMethodHF.SetEnabled(enable);
  }

  protected virtual void RefreshSyncPaymentAction(bool enable)
  {
    this.syncCCPaymentMethods.SetEnabled(enable);
  }

  private string GetClassMethodName([CallerMemberName] string methodName = "")
  {
    return $"{this.GetType().Name}.{methodName}";
  }

  protected virtual void RefreshManagePaymentAction(bool enable, bool visible)
  {
    if (!CCProcessingFeatureHelper.IsFeatureSupported(this.GetProcessingCenterById(this.CustomerPaymentMethod.Current?.CCProcessingCenterID), CCProcessingFeature.ProfileEditForm, false))
      enable = false;
    this.manageCCPaymentMethodHF.SetVisible(visible);
    this.manageCCPaymentMethodHF.SetEnabled(enable);
  }

  [PXOverride]
  public virtual void Persist(System.Action @base)
  {
    bool flag1 = this.CustomerPaymentMethod.Cache.Deleted.Count() != 0L;
    bool flag2 = this.CustomerPaymentMethodDetail.Cache.Inserted.Count() != 0L;
    CCCustomerInformationManagerGraph instance = PXGraph.CreateInstance<CCCustomerInformationManagerGraph>();
    ICCPaymentProfileAdapter paymentProfileAdapter = (ICCPaymentProfileAdapter) new GenericCCPaymentProfileAdapter<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod>((PXSelectBase<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod>) this.CustomerPaymentMethod);
    ICCPaymentProfileDetailAdapter profileDetailAdapter = (ICCPaymentProfileDetailAdapter) new GenericCCPaymentProfileDetailAdapter<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail, PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail>((PXSelectBase<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail>) this.CustomerPaymentMethodDetail, (PXSelectBase<PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail>) this.PaymentMethodDetail);
    if (!flag1 & flag2 && !string.IsNullOrEmpty(this.CustomerPaymentMethod.Current.CCProcessingCenterID) && CCProcessingHelper.IsTokenizedPaymentMethod((PXGraph) this.Base, this.CustomerPaymentMethod.Current.PMInstanceID, true))
    {
      this.CheckCustomerProfileId();
      this.CheckBeforeProcessing();
      instance.GetOrCreatePaymentProfile((PXGraph) this.Base, paymentProfileAdapter, profileDetailAdapter);
    }
    else if (flag1 && CCProcessingHelper.IsTokenizedPaymentMethod((PXGraph) this.Base, new int?(), true))
      instance.DeletePaymentProfile((PXGraph) this.Base, paymentProfileAdapter, profileDetailAdapter);
    @base();
  }

  public override void Initialize()
  {
    base.Initialize();
    this.customerRepo = new CustomerRepository((PXGraph) this.Base);
    this.MapViews(this.Base);
  }

  protected virtual void RowSelected(PX.Data.Events.RowSelected<TPrimary> e)
  {
  }

  protected void CheckCustomerProfileId()
  {
    PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod current = this.CustomerPaymentMethod.Current;
    if (string.IsNullOrEmpty(current.CustomerCCPID))
    {
      string displayName = PXUIFieldAttribute.GetDisplayName(this.CustomerPaymentMethod.Cache, "CustomerCCPID");
      this.CustomerPaymentMethod.Cache.RaiseExceptionHandling<PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.customerCCPID>((object) current, (object) current.CustomerCCPID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) displayName
      }));
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) displayName
      });
    }
  }

  protected void CheckBeforeProcessing()
  {
    PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod current = this.CustomerPaymentMethod.Current;
    string processingCenterId = current.CCProcessingCenterID;
    if (processingCenterId != null)
    {
      bool? allowSaveProfile = this.GetProcessingCenterById(processingCenterId).AllowSaveProfile;
      bool flag = false;
      if (allowSaveProfile.GetValueOrDefault() == flag & allowSaveProfile.HasValue)
        throw new PXException("Saving payment profiles is not allowed for the {0} processing center.", new object[1]
        {
          (object) processingCenterId
        });
    }
    int? baccountId = current.BAccountID;
    if (!baccountId.HasValue || this.Base.IsContractBasedAPI)
      return;
    Tuple<CustomerClass, PX.Objects.AR.Customer> customerAndClassById = this.customerRepo.GetCustomerAndClassById(baccountId);
    if (customerAndClassById == null)
      return;
    CustomerClass customerClass = customerAndClassById.Item1;
    if (customerClass.SavePaymentProfiles == "P")
    {
      PX.Objects.AR.Customer customer = customerAndClassById.Item2;
      throw new PXException("Saving payment profiles is not allowed for the {0} customer class that is selected for the {1} customer.", new object[2]
      {
        (object) customerClass.CustomerClassID,
        (object) customer.AcctName
      });
    }
  }

  protected abstract PaymentProfileGraph<TGraph, TPrimary>.CustomerPaymentMethodMapping GetCustomerPaymentMethodMapping();

  protected abstract PaymentProfileGraph<TGraph, TPrimary>.CustomerPaymentMethodDetailMapping GetCusotmerPaymentMethodDetailMapping();

  protected abstract PaymentProfileGraph<TGraph, TPrimary>.PaymentmethodDetailMapping GetPaymentMethodDetailMapping();

  protected virtual void MapViews(TGraph graph)
  {
  }

  protected CCProcessingCenter GetProcessingCenterById(string id)
  {
    return (CCProcessingCenter) PXSelectBase<CCProcessingCenter, PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Config>.Select((PXGraph) this.Base, (object) id);
  }

  protected class CustomerPaymentMethodMapping : IBqlMapping
  {
    public System.Type CCProcessingCenterID = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.cCProcessingCenterID);
    public System.Type CustomerCCPID = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.customerCCPID);
    public System.Type BAccountID = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.bAccountID);
    public System.Type PMInstanceID = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.pMInstanceID);
    public System.Type PaymentMethodID = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.paymentMethodID);
    public System.Type CashAccountID = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.cashAccountID);
    public System.Type Descr = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.descr);
    public System.Type ExpirationDate = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod.expirationDate);

    public System.Type Extension => typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethod);

    public System.Type Table { get; private set; }

    public CustomerPaymentMethodMapping(System.Type table) => this.Table = table;
  }

  protected class PaymentmethodDetailMapping : IBqlMapping
  {
    public System.Type PaymentMethodID = typeof (PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail.paymentMethodID);
    public System.Type UseFor = typeof (PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail.useFor);
    public System.Type DetailID = typeof (PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail.detailID);
    public System.Type Descr = typeof (PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail.descr);
    public System.Type IsEncrypted = typeof (PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail.isEncrypted);
    public System.Type IsRequired = typeof (PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail.isRequired);
    public System.Type IsIdentifier = typeof (PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail.isIdentifier);
    public System.Type IsExpirationDate = typeof (PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail.isExpirationDate);
    public System.Type IsOwnerName = typeof (PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail.isOwnerName);
    public System.Type IsCCProcessingID = typeof (PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail.isCCProcessingID);
    public System.Type IsCVV = typeof (PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail.isCVV);

    public System.Type Table { get; private set; }

    public System.Type Extension => typeof (PX.Objects.Extensions.PaymentProfile.PaymentMethodDetail);

    public PaymentmethodDetailMapping(System.Type table) => this.Table = table;
  }

  protected class CustomerPaymentMethodDetailMapping : IBqlMapping
  {
    public System.Type PMInstanceID = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail.pMInstanceID);
    public System.Type PaymentMethodID = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail.paymentMethodID);
    public System.Type DetailID = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail.detailID);
    public System.Type Value = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail.value);
    public System.Type IsIdentifier = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail.isIdentifier);
    public System.Type IsCCProcessingID = typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail.isCCProcessingID);

    public System.Type Extension => typeof (PX.Objects.Extensions.PaymentProfile.CustomerPaymentMethodDetail);

    public System.Type Table { get; private set; }

    public CustomerPaymentMethodDetailMapping(System.Type table) => this.Table = table;
  }
}
