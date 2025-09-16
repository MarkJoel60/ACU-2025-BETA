// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.Mapping.PaidInvoiceMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.Mapping;

public class PaidInvoiceMapping : IBqlMapping
{
  /// <exclude />
  protected Type _table;
  /// <exclude />
  public Type BranchID = typeof (Document.branchID);
  /// <exclude />
  public Type HeaderDocDate = typeof (Document.headerDocDate);
  /// <exclude />
  public Type HeaderTranPeriodID = typeof (Document.headerTranPeriodID);
  /// <exclude />
  public Type HeaderFinPeriodID = typeof (Document.headerFinPeriodID);
  public Type DocType = typeof (InvoiceBase.docType);
  public Type RefNbr = typeof (InvoiceBase.refNbr);
  public Type CuryInfoID = typeof (InvoiceBase.curyInfoID);
  public Type Hold = typeof (InvoiceBase.hold);
  public Type Released = typeof (InvoiceBase.released);
  public Type Printed = typeof (InvoiceBase.printed);
  public Type OpenDoc = typeof (InvoiceBase.openDoc);
  public Type FinPeriodID = typeof (InvoiceBase.finPeriodID);
  public Type InvoiceNbr = typeof (InvoiceBase.invoiceNbr);
  public Type DocDesc = typeof (InvoiceBase.docDesc);
  public Type ContragentID = typeof (InvoiceBase.contragentID);
  public Type ContragentLocationID = typeof (InvoiceBase.contragentLocationID);
  public Type TaxZoneID = typeof (InvoiceBase.taxZoneID);
  public Type TaxCalcMode = typeof (InvoiceBase.taxCalcMode);
  public Type OrigModule = typeof (InvoiceBase.origModule);
  public Type OrigDocType = typeof (InvoiceBase.origDocType);
  public Type OrigRefNbr = typeof (InvoiceBase.origRefNbr);
  public Type CuryOrigDocAmt = typeof (InvoiceBase.curyOrigDocAmt);
  public Type CuryTaxAmt = typeof (InvoiceBase.curyTaxAmt);
  public Type CuryDocBal = typeof (InvoiceBase.curyDocBal);
  public Type CuryTaxTotal = typeof (InvoiceBase.curyTaxTotal);
  public Type CuryTaxRoundDiff = typeof (InvoiceBase.curyTaxRoundDiff);
  public Type CuryRoundDiff = typeof (InvoiceBase.curyRoundDiff);
  public Type TaxRoundDiff = typeof (InvoiceBase.taxRoundDiff);
  public Type RoundDiff = typeof (InvoiceBase.roundDiff);
  public Type TaxAmt = typeof (InvoiceBase.taxAmt);
  public Type DocBal = typeof (InvoiceBase.docBal);
  public Type Approved = typeof (InvoiceBase.approved);
  public Type CashAccountID = typeof (InvoiceBase.cashAccountID);
  public Type PaymentMethodID = typeof (InvoiceBase.paymentMethodID);
  public Type CATranID = typeof (InvoiceBase.cATranID);
  public Type ClearDate = typeof (InvoiceBase.clearDate);
  public Type Cleared = typeof (InvoiceBase.cleared);
  public Type ExtRefNbr = typeof (InvoiceBase.extRefNbr);

  /// <exclude />
  public Type Extension => typeof (PaidInvoice);

  /// <exclude />
  public Type Table => this._table;

  public PaidInvoiceMapping(Type table) => this._table = table;
}
