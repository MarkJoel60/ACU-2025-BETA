// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.Mapping.InvoiceMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.Mapping;

public class InvoiceMapping : IBqlMapping
{
  /// <exclude />
  protected Type _table;
  /// <exclude />
  public Type BranchID = typeof (Document.branchID);
  /// <exclude />
  public Type HeaderDocDate = typeof (Document.headerDocDate);
  /// <exclude />
  public Type HeaderTranPeriodID = typeof (Document.headerTranPeriodID);
  public Type DocType = typeof (InvoiceBase.docType);
  public Type RefNbr = typeof (InvoiceBase.refNbr);
  public Type CuryInfoID = typeof (InvoiceBase.curyInfoID);
  public Type CuryID = typeof (InvoiceBase.curyID);
  public Type ModuleAccountID = typeof (InvoiceBase.moduleAccountID);
  public Type ModuleSubID = typeof (InvoiceBase.moduleSubID);
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

  /// <exclude />
  public Type Extension => typeof (Invoice);

  /// <exclude />
  public Type Table => this._table;

  /// <summary>Creates the default mapping of the <see cref="!:DocumentWithLines" /> mapped cache extension to the specified table.</summary>
  /// <param name="table">A DAC.</param>
  public InvoiceMapping(Type table) => this._table = table;
}
