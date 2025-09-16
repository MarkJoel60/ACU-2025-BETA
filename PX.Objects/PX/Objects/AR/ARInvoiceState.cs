// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceState
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

/// <exclude />
public class ARInvoiceState
{
  public bool PaymentsByLinesAllowed;
  public bool RetainageApply;
  public bool IsRetainageDocument;
  public bool IsDocumentReleased;
  public bool IsDocumentInvoice;
  public bool IsDocumentPrepaymentInvoice;
  public bool IsDocumentCreditMemo;
  public bool IsPrepaymentInvoiceReversing;
  public bool IsDocumentDebitMemo;
  public bool IsDocumentFinCharge;
  public bool IsDocumentSmallCreditWO;
  public bool IsRetainageReversing;
  public bool RetainTaxes;
  public bool IsDocumentOnHold;
  public bool IsDocumentOnCreditHold;
  public bool IsDocumentScheduled;
  public bool IsDocumentVoided;
  public bool IsDocumentRejected;
  public bool InvoiceUnreleased;
  public bool IsRetainageApplyDocument;
  public bool IsDocumentRejectedOrPendingApproval;
  public bool IsDocumentApprovedBalanced;
  public bool IsUnreleasedWO;
  public bool IsUnreleasedPPD;
  public bool IsMigratedDocument;
  public bool IsUnreleasedMigratedDocument;
  public bool IsReleasedMigratedDocument;
  public bool IsMigrationMode;
  public bool IsCancellationDocument;
  public bool IsCorrectionDocument;
  public bool IsRegularBalancedDocument;
  public bool CuryEnabled;
  public bool ShouldDisableHeader;
  public bool AllowDeleteDocument;
  public bool DocumentHoldEnabled;
  public bool DocumentDateEnabled;
  public bool DocumentDescrEnabled;
  public bool EditCustomerEnabled;
  public bool AddressValidationEnabled;
  public bool IsTaxZoneIDEnabled;
  public bool IsAvalaraCustomerUsageTypeEnabled;
  public bool ApplyFinChargeVisible;
  public bool ApplyFinChargeEnable;
  public bool ShowCashDiscountInfo;
  public bool ShowCommissionsInfo;
  public bool IsAssignmentEnabled;
  public bool BalanceBaseCalc;
  public bool AllowDeleteTransactions;
  public bool AllowInsertTransactions;
  public bool AllowUpdateTransactions;
  public bool AllowDeleteTaxes;
  public bool AllowInsertTaxes;
  public bool AllowUpdateTaxes;
  public bool AllowDeleteDiscounts;
  public bool AllowInsertDiscounts;
  public bool AllowUpdateDiscounts;
  public bool LoadDocumentsEnabled;
  public bool AutoApplyEnabled;
  public bool AllowUpdateAdjustments;
  public bool AllowDeleteAdjustments;
  public bool AllowUpdateCMAdjustments;
  public IList<string> ExplicitlyEnabledTranFields = (IList<string>) new List<string>();
}
