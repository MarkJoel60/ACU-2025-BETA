// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceState
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AP;

public class APInvoiceState
{
  public bool DontApprove;
  public bool HasPOLink;
  public bool IsDocumentPrepayment;
  public bool IsDocumentInvoice;
  public bool IsDocumentPrepaymentInvoice;
  public bool IsDocumentDebitAdjustment;
  public bool IsDocumentCreditAdjustment;
  public bool IsDocumentOnHold;
  public bool IsDocumentScheduled;
  public bool IsDocumentPrebookedNotCompleted;
  public bool IsDocumentReleasedOrPrebooked;
  public bool IsDocumentVoided;
  public bool IsDocumentRejected;
  public bool RetainageApply;
  public bool IsRetainageDocument;
  public bool IsRetainageDebAdj;
  public bool IsRetainageReversing;
  public bool IsRetainageApplyDocument;
  public bool IsPrepaymentInvoiceReversing;
  public bool IsDocumentRejectedOrPendingApproval;
  public bool IsDocumentApprovedBalanced;
  public bool PaymentsByLinesAllowed;
  public bool LandedCostEnabled;
  public bool IsFromExpenseClaims;
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025R2. For the details see AC-306704.")]
  public bool AllowAddPOByProject;
  public bool IsCuryEnabled;
  public bool IsFromPO;
  public bool IsAssignmentEnabled;

  public bool IsPrepaymentRequestFromPO => this.IsDocumentPrepayment && this.IsFromPO;

  public bool IsDocumentEditable
  {
    get
    {
      return !this.IsDocumentReleasedOrPrebooked && !this.IsDocumentRejectedOrPendingApproval && !this.IsDocumentApprovedBalanced;
    }
  }
}
