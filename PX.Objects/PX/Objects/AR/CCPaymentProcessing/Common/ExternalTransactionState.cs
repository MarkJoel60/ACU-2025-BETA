// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.ExternalTransactionState
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.AR.CCPaymentProcessing.Interfaces;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

public class ExternalTransactionState
{
  public IExternalTransaction ExternalTransaction { get; private set; }

  public ProcessingStatus ProcessingStatus { get; set; }

  public bool IsActive { get; private set; }

  public bool IsCompleted { get; private set; }

  public bool NeedSync { get; private set; }

  public bool CreateProfile { get; private set; }

  public bool IsVoided { get; private set; }

  public bool IsCaptured { get; set; }

  public bool IsPreAuthorized { get; set; }

  public bool IsRefunded { get; set; }

  public bool IsOpenForReview { get; set; }

  public bool IsDeclined { get; set; }

  public bool IsExpired { get; set; }

  public string Description { get; set; }

  public bool HasErrors { get; set; }

  public bool SyncFailed { get; set; }

  /// <summary>
  /// Flag that indicates that credit card transaction was submitted for settlement.
  /// </summary>
  public bool IsSettlementDue => this.IsCaptured || this.IsRefunded;

  public bool IsImportedUnknown
  {
    get
    {
      if (this.ProcessingStatus != ProcessingStatus.Unknown || this.ExternalTransaction == null)
        return false;
      return this.IsActive || this.NeedSync;
    }
  }

  public ExternalTransactionState(IExternalTransaction extTran)
  {
    this.ExternalTransaction = extTran;
    this.SetProps(extTran);
  }

  public ExternalTransactionState()
  {
  }

  private void SetProps(IExternalTransaction extTran)
  {
    this.ProcessingStatus = ExtTransactionProcStatusCode.GetProcessingStatusByProcStatusStr(extTran.ProcStatus);
    this.IsActive = extTran.Active.GetValueOrDefault();
    this.IsCompleted = extTran.Completed.GetValueOrDefault();
    this.NeedSync = extTran.NeedSync.GetValueOrDefault();
    this.CreateProfile = extTran.SaveProfile.GetValueOrDefault();
    this.SyncFailed = extTran.SyncStatus == "E";
    this.IsVoided = this.ProcessingStatus == ProcessingStatus.VoidSuccess || this.ProcessingStatus == ProcessingStatus.VoidHeldForReview || this.ProcessingStatus == ProcessingStatus.RejectSuccess;
    this.IsCaptured = this.ProcessingStatus == ProcessingStatus.CaptureSuccess || this.ProcessingStatus == ProcessingStatus.CaptureHeldForReview;
    this.IsPreAuthorized = this.ProcessingStatus == ProcessingStatus.AuthorizeSuccess || this.ProcessingStatus == ProcessingStatus.AuthorizeHeldForReview;
    this.IsRefunded = this.ProcessingStatus == ProcessingStatus.CreditSuccess || this.ProcessingStatus == ProcessingStatus.CreditHeldForReview;
    this.IsOpenForReview = this.ProcessingStatus == ProcessingStatus.AuthorizeHeldForReview || this.ProcessingStatus == ProcessingStatus.CaptureHeldForReview || this.ProcessingStatus == ProcessingStatus.AuthorizeHeldForReview || this.ProcessingStatus == ProcessingStatus.VoidHeldForReview || this.ProcessingStatus == ProcessingStatus.CreditHeldForReview;
    this.IsDeclined = this.ProcessingStatus == ProcessingStatus.AuthorizeDecline || this.ProcessingStatus == ProcessingStatus.CaptureDecline || this.ProcessingStatus == ProcessingStatus.VoidDecline || this.ProcessingStatus == ProcessingStatus.CreditDecline;
    this.IsExpired = this.ProcessingStatus == ProcessingStatus.AuthorizeExpired || this.ProcessingStatus == ProcessingStatus.CaptureExpired;
    this.HasErrors = this.ProcessingStatus == ProcessingStatus.AuthorizeFail || this.ProcessingStatus == ProcessingStatus.AuthorizeIncreaseFail || this.ProcessingStatus == ProcessingStatus.CaptureFail || this.ProcessingStatus == ProcessingStatus.VoidFail || this.ProcessingStatus == ProcessingStatus.CreditFail;
  }
}
