// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.GraphExtensions.ExternalPaymentProcessorHelper`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.PaymentProcessor.DAC;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.GraphExtensions;

public class ExternalPaymentProcessorHelper<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public static bool IsActive() => true;

  public virtual APPaymentProcessorAccount GetExternalProcessorAccount(
    PX.Objects.CA.CashAccount? cashAccount,
    PX.Objects.GL.Branch? branch,
    APExternalPaymentProcessor? externalProcessor)
  {
    return (APPaymentProcessorAccount) PXSelectBase<APPaymentProcessorAccount, PXViewOf<APPaymentProcessorAccount>.BasedOn<SelectFromBase<APPaymentProcessorAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorAccount.externalPaymentProcessorID, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorAccount.organizationID, Equal<P.AsInt>>>>>.And<BqlOperand<APPaymentProcessorAccount.cashAccountID, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.Select((PXGraph) this.Base, (object) externalProcessor?.ExternalPaymentProcessorID, (object) (int?) branch?.OrganizationID, (object) (int?) cashAccount?.CashAccountID);
  }

  public virtual APPaymentProcessorAccountUser GetExternalProcessorAccountUser(
    APExternalPaymentProcessor? externalProcessor,
    string? externalAccountID,
    string? externalUserID)
  {
    return (APPaymentProcessorAccountUser) PXSelectBase<APPaymentProcessorAccountUser, PXViewOf<APPaymentProcessorAccountUser>.BasedOn<SelectFromBase<APPaymentProcessorAccountUser, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorAccountUser.externalPaymentProcessorID, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorAccountUser.externalAccountID, Equal<P.AsString>>>>>.And<BqlOperand<APPaymentProcessorAccountUser.externalUserID, IBqlString>.IsEqual<P.AsString>>>>>.Config>.Select((PXGraph) this.Base, (object) externalProcessor?.ExternalPaymentProcessorID, (object) externalAccountID, (object) externalUserID);
  }

  public virtual (APExternalPaymentProcessor, APPaymentProcessorOrganization) GetExternalProcessorWithOrganizationDetails(
    APPaymentProcessorAccount processorAccount)
  {
    PXResult<APExternalPaymentProcessor, APPaymentProcessorOrganization> pxResult = (PXResult<APExternalPaymentProcessor, APPaymentProcessorOrganization>) (PXResult<APExternalPaymentProcessor>) PXSelectBase<APExternalPaymentProcessor, PXViewOf<APExternalPaymentProcessor>.BasedOn<SelectFromBase<APExternalPaymentProcessor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<APPaymentProcessorOrganization>.On<BqlOperand<APExternalPaymentProcessor.externalPaymentProcessorID, IBqlString>.IsEqual<APPaymentProcessorOrganization.externalPaymentProcessorID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorOrganization.externalPaymentProcessorID, Equal<P.AsString>>>>>.And<BqlOperand<APPaymentProcessorOrganization.organizationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, (object) processorAccount.ExternalPaymentProcessorID, (object) processorAccount.OrganizationID);
    return ((APExternalPaymentProcessor) pxResult, (APPaymentProcessorOrganization) pxResult);
  }

  public virtual (APExternalPaymentProcessor, APPaymentProcessorOrganization) GetExternalProcessorWithOrganization(
    string externalProcessorId,
    int organizationId)
  {
    PXResult<APExternalPaymentProcessor, APPaymentProcessorOrganization> pxResult = (PXResult<APExternalPaymentProcessor, APPaymentProcessorOrganization>) (PXResult<APExternalPaymentProcessor>) PXSelectBase<APExternalPaymentProcessor, PXViewOf<APExternalPaymentProcessor>.BasedOn<SelectFromBase<APExternalPaymentProcessor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<APPaymentProcessorOrganization>.On<BqlOperand<APExternalPaymentProcessor.externalPaymentProcessorID, IBqlString>.IsEqual<APPaymentProcessorOrganization.externalPaymentProcessorID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPaymentProcessorOrganization.externalPaymentProcessorID, Equal<P.AsString>>>>>.And<BqlOperand<APPaymentProcessorOrganization.organizationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, (object) externalProcessorId, (object) organizationId);
    return ((APExternalPaymentProcessor) pxResult, (APPaymentProcessorOrganization) pxResult);
  }
}
