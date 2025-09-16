// Decompiled with JetBrains decompiler
// Type: SW.Objects.AR.Descriptor.Messages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace SW.Objects.AR.Descriptor;

/// <summary>
///   A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Messages
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Messages()
  {
  }

  /// <summary>
  ///   Returns the cached ResourceManager instance used by this class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (Messages.resourceMan == null)
        Messages.resourceMan = new ResourceManager("SW.Objects.AR.Descriptor.Messages", typeof (Messages).Assembly);
      return Messages.resourceMan;
    }
  }

  /// <summary>
  ///   Overrides the current thread's CurrentUICulture property for all
  ///   resource lookups using this strongly typed resource class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Messages.resourceCulture;
    set => Messages.resourceCulture = value;
  }

  /// <summary>
  ///   Looks up a localized string similar to {0} cannot be greater than Document Date..
  /// </summary>
  internal static string ApplDate_Greater_DocDate
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (ApplDate_Greater_DocDate), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to {0} cannot be less than Document Date..
  /// </summary>
  internal static string ApplDate_Less_DocDate
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (ApplDate_Less_DocDate), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to {0} cannot be greater than Document Period..
  /// </summary>
  internal static string ApplPeriod_Greater_DocPeriod
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (ApplPeriod_Greater_DocPeriod), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to {0} cannot be less than Document Period..
  /// </summary>
  internal static string ApplPeriod_Less_DocPeriod
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (ApplPeriod_Less_DocPeriod), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Customer Balance Enquiry.
  /// </summary>
  internal static string ARCustomerBalanceEnq
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (ARCustomerBalanceEnq), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Customer Documents Enquiry.
  /// </summary>
  internal static string ARDocumentEnq
  {
    get => Messages.ResourceManager.GetString(nameof (ARDocumentEnq), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Release AR documents.
  /// </summary>
  internal static string ARDocumentRelease
  {
    get => Messages.ResourceManager.GetString(nameof (ARDocumentRelease), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Due Invoices Enquiry.
  /// </summary>
  internal static string ARDueInvoicesEnq
  {
    get => Messages.ResourceManager.GetString(nameof (ARDueInvoicesEnq), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to AR Invoice Entry.
  /// </summary>
  internal static string ARInvoiceEntry
  {
    get => Messages.ResourceManager.GetString(nameof (ARInvoiceEntry), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to AR Payment Entry.
  /// </summary>
  internal static string ARPaymentEntry
  {
    get => Messages.ResourceManager.GetString(nameof (ARPaymentEntry), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to AR Release Process.
  /// </summary>
  internal static string ARReleaseProcess
  {
    get => Messages.ResourceManager.GetString(nameof (ARReleaseProcess), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Required configuration data is not entered into AR Setup..
  /// </summary>
  internal static string ARSetupNotFound
  {
    get => Messages.ResourceManager.GetString(nameof (ARSetupNotFound), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Process AR Statements.
  /// </summary>
  internal static string ARStatementProcess
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (ARStatementProcess), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to An error occured during the process of statement regeneration.
  /// </summary>
  internal static string ARStatementRegenerationError
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (ARStatementRegenerationError), Messages.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Balanced.</summary>
  internal static string Balanced
  {
    get => Messages.ResourceManager.GetString(nameof (Balanced), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Once applications are entered this field cannot be changed..
  /// </summary>
  internal static string Cannot_Change_Details_Exists
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (Cannot_Change_Details_Exists), Messages.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Closed.</summary>
  internal static string Closed
  {
    get => Messages.ResourceManager.GetString(nameof (Closed), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Credit Days Past Due Were Exceeded!.
  /// </summary>
  internal static string CreditDaysPastDueWereExceeded
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (CreditDaysPastDueWereExceeded), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Customer Credit Limit Was Exceeded!.
  /// </summary>
  internal static string CreditLimitWasExceeded
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (CreditLimitWasExceeded), Messages.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Credit Memo.</summary>
  internal static string CreditMemo
  {
    get => Messages.ResourceManager.GetString(nameof (CreditMemo), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Customer Class.
  /// </summary>
  internal static string CustomerClassMaint
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (CustomerClassMaint), Messages.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Customer.</summary>
  internal static string CustomerMaint
  {
    get => Messages.ResourceManager.GetString(nameof (CustomerMaint), Messages.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Debit Memo.</summary>
  internal static string DebitMemo
  {
    get => Messages.ResourceManager.GetString(nameof (DebitMemo), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Document is On Hold and cannot be released..
  /// </summary>
  internal static string Document_OnHold_CannotRelease
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (Document_OnHold_CannotRelease), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Document Status is invalid for processing..
  /// </summary>
  internal static string Document_Status_Invalid
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (Document_Status_Invalid), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Entry must be less or equal {0}.
  /// </summary>
  internal static string Entry_LE
  {
    get => Messages.ResourceManager.GetString(nameof (Entry_LE), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Overdue Charge.
  /// </summary>
  internal static string FinCharge
  {
    get => Messages.ResourceManager.GetString(nameof (FinCharge), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Financial charge can not be created in this form! Use form Process Overdue Charges!.
  /// </summary>
  internal static string FinChargeCanNotBeDeleted
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (FinChargeCanNotBeDeleted), Messages.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to On Hold.</summary>
  internal static string Hold
  {
    get => Messages.ResourceManager.GetString(nameof (Hold), Messages.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Invoice.</summary>
  internal static string Invoice
  {
    get => Messages.ResourceManager.GetString(nameof (Invoice), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Multiply Installments.
  /// </summary>
  internal static string MultiplyInstallmentsTranDesc
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (MultiplyInstallmentsTranDesc), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Enter New Invoice.
  /// </summary>
  internal static string NewInvoice
  {
    get => Messages.ResourceManager.GetString(nameof (NewInvoice), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Enter New Payment.
  /// </summary>
  internal static string NewPayment
  {
    get => Messages.ResourceManager.GetString(nameof (NewPayment), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Only Invoices and Credit adjustments may be payed.
  /// </summary>
  internal static string Only_Invoices_MayBe_Payed
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (Only_Invoices_MayBe_Payed), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Only documents having status 'open' can be processed.
  /// </summary>
  internal static string Only_Open_Documents_MayBe_Processed
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (Only_Open_Documents_MayBe_Processed), Messages.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Open.</summary>
  internal static string Open
  {
    get => Messages.ResourceManager.GetString(nameof (Open), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Customer Over Limit Amount Was Exceeded!.
  /// </summary>
  internal static string OverLimitWasExceeded
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (OverLimitWasExceeded), Messages.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Payment.</summary>
  internal static string Payment
  {
    get => Messages.ResourceManager.GetString(nameof (Payment), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to customer Refund.
  /// </summary>
  internal static string Refund
  {
    get => Messages.ResourceManager.GetString(nameof (Refund), Messages.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to SalesPerson.</summary>
  internal static string SalesPerson
  {
    get => Messages.ResourceManager.GetString(nameof (SalesPerson), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to SalesPerson Maintenance.
  /// </summary>
  internal static string SalesPersonMaint
  {
    get => Messages.ResourceManager.GetString(nameof (SalesPersonMaint), Messages.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Scheduled.</summary>
  internal static string Scheduled
  {
    get => Messages.ResourceManager.GetString(nameof (Scheduled), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Start date must be equal ot later then end date!.
  /// </summary>
  internal static string TempCrLimitInvalidDate
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (TempCrLimitInvalidDate), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Effective periods in temporary credit limit history must not be crossed!.
  /// </summary>
  internal static string TempCrLimitPeriodsCrossed
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (TempCrLimitPeriodsCrossed), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Unknown document type  - unable to process..
  /// </summary>
  internal static string UnknownDocumentType
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (UnknownDocumentType), Messages.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Void Check Payment Reference must match original payment..
  /// </summary>
  internal static string VoidAppl_CheckNbr_NotMatchOrigPayment
  {
    get
    {
      return Messages.ResourceManager.GetString(nameof (VoidAppl_CheckNbr_NotMatchOrigPayment), Messages.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Voided.</summary>
  internal static string Voided
  {
    get => Messages.ResourceManager.GetString(nameof (Voided), Messages.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Void Payment.
  /// </summary>
  internal static string VoidPayment
  {
    get => Messages.ResourceManager.GetString(nameof (VoidPayment), Messages.resourceCulture);
  }
}
