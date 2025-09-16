// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessingGraph
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.CC.PaymentProcessing;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing;

public class CCPaymentProcessingGraph : PXGraph<CCPaymentProcessingGraph>, ICCPaymentProcessor
{
  public ICCPaymentProcessingRepository Repository { get; set; }

  public virtual bool TestCredentials(PXGraph callerGraph, string processingCenterID)
  {
    return new PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing(callerGraph).TestCredentials(callerGraph, processingCenterID);
  }

  public virtual void ValidateSettings(
    PXGraph callerGraph,
    string processingCenterID,
    PluginSettingDetail settingDetail)
  {
    new PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing(callerGraph).ValidateSettings(callerGraph, processingCenterID, settingDetail);
  }

  public virtual IList<PluginSettingDetail> ExportSettings(
    PXGraph callerGraph,
    string processingCenterID)
  {
    return new PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing(callerGraph).ExportSettings(callerGraph, processingCenterID);
  }

  public virtual TranOperationResult Authorize(ICCPayment aPmtInfo, bool aCapture)
  {
    return this.GetPaymentProcessing().Authorize(aPmtInfo, aCapture);
  }

  public virtual TranOperationResult IncreaseAuthorizedAmount(
    ICCPayment aPmtInfo,
    int? transactionId)
  {
    return this.GetPaymentProcessing().IncreaseAuthorizedAmount(aPmtInfo, transactionId);
  }

  public virtual TranOperationResult Capture(ICCPayment payment, int? transactionId)
  {
    return this.GetPaymentProcessing().Capture(payment, transactionId);
  }

  public virtual TranOperationResult CaptureOnly(ICCPayment payment, string aAuthorizationNbr)
  {
    return this.GetPaymentProcessing().CaptureOnly(payment, aAuthorizationNbr);
  }

  public virtual TranOperationResult Void(ICCPayment payment, int? aRefTranNbr)
  {
    return this.GetPaymentProcessing().Void(payment, aRefTranNbr);
  }

  public virtual TranOperationResult VoidOrCredit(ICCPayment payment, int? transactionId)
  {
    return this.GetPaymentProcessing().VoidOrCredit(payment, transactionId);
  }

  public virtual TranOperationResult Credit(
    ICCPayment aPmtInfo,
    string aExtRefTranNbr,
    string procCenterId)
  {
    return this.GetPaymentProcessing().Credit(aPmtInfo, aExtRefTranNbr, procCenterId);
  }

  public virtual TranOperationResult Credit(ICCPayment aPmtInfo, int? transactionId)
  {
    return this.GetPaymentProcessing().Credit(aPmtInfo, transactionId);
  }

  public virtual TranOperationResult Credit(
    ICCPayment payment,
    int? transactionId,
    string aCuryID,
    Decimal? aAmount)
  {
    return this.GetPaymentProcessing().Credit(payment, transactionId, aCuryID, aAmount);
  }

  public virtual TranOperationResult UpdateLevel3Data(PX.Objects.Extensions.PaymentTransaction.Payment payment, int? transactionId)
  {
    return this.GetLevel3Processing().UpdateLevel3Data(payment, transactionId);
  }

  public virtual bool RecordAuthorization(ICCPayment payment, TranRecordData data)
  {
    return this.GetPaymentProcessing().RecordAuthorization(payment, data);
  }

  public virtual bool RecordCapture(ICCPayment payment, TranRecordData data)
  {
    return this.GetPaymentProcessing().RecordCapture(payment, data);
  }

  public virtual bool RecordPriorAuthorizedCapture(ICCPayment payment, TranRecordData data)
  {
    return this.GetPaymentProcessing().RecordPriorAuthorizedCapture(payment, data);
  }

  public virtual bool RecordCaptureOnly(ICCPayment payment, TranRecordData data)
  {
    return this.GetPaymentProcessing().RecordCaptureOnly(payment, data);
  }

  public virtual bool RecordVoid(ICCPayment payment, TranRecordData data)
  {
    return this.GetPaymentProcessing().RecordVoid(payment, data);
  }

  public virtual bool RecordCredit(ICCPayment payment, TranRecordData data)
  {
    return this.GetPaymentProcessing().RecordCredit(payment, data);
  }

  public virtual bool RecordUnknown(ICCPayment payment, TranRecordData data)
  {
    return this.GetPaymentProcessing().RecordUnknown(payment, data);
  }

  protected virtual PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing GetPaymentProcessing()
  {
    return this.Repository == null ? new PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing() : new PX.Objects.AR.CCPaymentProcessing.CCPaymentProcessing(this.Repository);
  }

  protected virtual Level3Processing GetLevel3Processing()
  {
    return this.Repository != null ? new Level3Processing(this.Repository) : new Level3Processing();
  }
}
