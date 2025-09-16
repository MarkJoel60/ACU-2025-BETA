// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.GraphExtensions.MyProfileMaintEPPExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.PaymentProcessor.Common;
using PX.Objects.AP.PaymentProcessor.DAC;
using PX.Objects.AP.PaymentProcessor.Managers;
using PX.SM;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.GraphExtensions;

public class MyProfileMaintEPPExt : PXGraphExtension<MyProfileMaint>
{
  public FbqlSelect<
  #nullable disable
  SelectFromBase<
  #nullable enable
  APPaymentProcessorAccount, 
  #nullable disable
  TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<
  #nullable enable
  APPaymentProcessorAccountUser>.On<BqlChainableConditionBase<
  #nullable disable
  TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorAccount.externalAccountID, 
  #nullable disable
  Equal<
  #nullable enable
  APPaymentProcessorAccountUser.externalAccountID>>>>>.And<BqlChainableConditionBase<
  #nullable disable
  TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorAccount.organizationID, 
  #nullable disable
  Equal<
  #nullable enable
  APPaymentProcessorAccountUser.organizationID>>>>>.And<BqlOperand<APPaymentProcessorAccount.externalPaymentProcessorID, IBqlString>.IsEqual<APPaymentProcessorAccountUser.externalPaymentProcessorID>>>>>, 
  #nullable disable
  FbqlJoins.Inner<
  #nullable enable
  APPaymentProcessorUser>.On<BqlChainableConditionMirror<
  #nullable disable
  TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorAccountUser.externalUserID, 
  #nullable disable
  Equal<
  #nullable enable
  APPaymentProcessorUser.externalUserID>>>>, 
  #nullable disable
  PX.Data.And<
  #nullable enable
  BqlOperand<APPaymentProcessorAccountUser.organizationID, IBqlInt>.IsEqual<APPaymentProcessorUser.organizationID>>>>.And<BqlOperand<APPaymentProcessorAccountUser.externalPaymentProcessorID, IBqlString>.IsEqual<APPaymentProcessorUser.externalPaymentProcessorID>>>>, 
  #nullable disable
  FbqlJoins.Inner<
  #nullable enable
  APExternalPaymentProcessor>.On<BqlOperand<APPaymentProcessorAccount.externalPaymentProcessorID, IBqlString>.IsEqual<APExternalPaymentProcessor.externalPaymentProcessorID>>>>.Where<BqlChainableConditionMirror<
  #nullable disable
  TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorUser.userID, 
  #nullable disable
  Equal<
  #nullable enable
  BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>>, 
  #nullable disable
  PX.Data.And<
  #nullable enable
  BqlChainableConditionBase<
  #nullable disable
  TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<
  #nullable enable
  APPaymentProcessorAccount.status, 
  #nullable disable
  In3<
  #nullable enable
  AccountStatus.active, AccountStatus.pending>>>>>.And<BqlOperand<APPaymentProcessorAccountUser.verificationStatus, IBqlString>.IsNotEqual<VerificationStatus.undefined>>>>>.And<BqlOperand<APExternalPaymentProcessor.isActive, IBqlBool>.IsEqual<PX.Data.True>>>.Order<
  #nullable disable
  By<
  #nullable enable
  BqlField<APPaymentProcessorAccount.externalAccountBank, IBqlString>.Asc, BqlField<APPaymentProcessorAccount.externalAccountType, IBqlString>.Asc>>, APPaymentProcessorAccount>.View PaymentProcessorAccounts;
  public PXAction<Users> manageFundingAccounts;
  public PXAction<APExternalPaymentProcessor> syncManageFundingAccounts;

  [InjectDependency]
  internal Func<string, PaymentProcessorManager> PaymentProcessorManagerProvider { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>();

  [PXUIField(DisplayName = "Verify")]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable ManageFundingAccounts(PXAdapter adapter)
  {
    IEnumerable enumerable = adapter.Get();
    APPaymentProcessorAccount current = this.PaymentProcessorAccounts.Current;
    if (current == null)
      return enumerable;
    (APExternalPaymentProcessor paymentProcessor, APPaymentProcessorOrganization processorOrganization) = this.Base.FindImplementation<MyProfileMaintEPPExt.MyProfileMaintExternalPaymentProcessorHelper>().GetExternalProcessorWithOrganizationDetails(current);
    PaymentProcessorManager manager = this.PaymentProcessorManagerProvider(paymentProcessor.Type);
    PaymentProcessorSessionHelper.SetUserSessionStore(paymentProcessor);
    PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() =>
    {
      try
      {
        manager.ManageFundingAccountsAsync(paymentProcessor, new OrganizationUserData()
        {
          OrganizationId = processorOrganization.OrganizationID
        }).GetAwaiter().GetResult();
      }
      catch (PXPluginRedirectException ex)
      {
        PXLongOperation.SetCustomInfo((object) ex);
      }
    }));
    PXLongOperation.WaitCompletion(this.Base.UID);
    if (PXLongOperation.GetCustomInfo(this.Base.UID) is PXPluginRedirectException customInfo)
      throw customInfo;
    return enumerable;
  }

  [PXUIField(DisplayName = "", Visible = false)]
  [PXButton]
  public virtual IEnumerable SyncManageFundingAccounts(PXAdapter adapter)
  {
    string responseStr = adapter.CommandArguments;
    IEnumerable enumerable = adapter.Get();
    APPaymentProcessorAccount current = this.PaymentProcessorAccounts.Current;
    if (current == null)
      return enumerable;
    APExternalPaymentProcessor extProcessor = this.Base.FindImplementation<MyProfileMaintEPPExt.MyProfileMaintExternalPaymentProcessorHelper>().GetExternalProcessorWithOrganizationDetails(current).Item1;
    PaymentProcessorManager manager = this.PaymentProcessorManagerProvider(extProcessor.Type);
    PaymentProcessorSessionHelper.SetUserSessionStore(extProcessor);
    PXLongOperation.StartOperation((PXGraph) this.Base, (PXToggleAsyncDelegate) (() => manager.ProcessFundingAccountsResponseAsync(responseStr, extProcessor).GetAwaiter().GetResult()));
    return adapter.Get();
  }

  public override void Initialize()
  {
    base.Initialize();
    this.PaymentProcessorAccounts.AllowUpdate = false;
  }

  protected virtual void _(PX.Data.Events.RowSelected<Users> e)
  {
    if (e.Row == null)
      return;
    this.manageFundingAccounts.SetEnabled(this.PaymentProcessorAccounts.Select().Count > 0);
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Company", Visible = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<APPaymentProcessorAccount.organizationID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Account Status", Visible = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<APPaymentProcessorAccount.status> e)
  {
  }

  public class MyProfileMaintExternalPaymentProcessorHelper : 
    ExternalPaymentProcessorHelper<MyProfileMaint>
  {
  }
}
