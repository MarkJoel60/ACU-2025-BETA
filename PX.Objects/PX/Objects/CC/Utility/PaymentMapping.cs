// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.Utility.PaymentMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.PaymentTransaction;
using System;

#nullable disable
namespace PX.Objects.CC.Utility;

public class PaymentMapping : IBqlMapping
{
  public Type PMInstanceID = typeof (Payment.pMInstanceID);
  public Type CashAccountID = typeof (Payment.cashAccountID);
  public Type ProcessingCenterID = typeof (Payment.processingCenterID);
  public Type CuryDocBal = typeof (Payment.curyDocBal);
  public Type CuryID = typeof (Payment.curyID);
  public Type DocType = typeof (Payment.docType);
  public Type RefNbr = typeof (Payment.refNbr);
  public Type OrigDocType = typeof (Payment.origDocType);
  public Type OrigRefNbr = typeof (Payment.origRefNbr);
  public Type RefTranExtNbr = typeof (Payment.refTranExtNbr);
  public Type Released = typeof (Payment.released);
  public Type SaveCard = typeof (Payment.saveCard);
  public Type CCTransactionRefund = typeof (Payment.cCTransactionRefund);
  public Type CCPaymentStateDescr = typeof (Payment.cCPaymentStateDescr);
  public Type CCActualExternalTransactionID = typeof (Payment.cCActualExternalTransactionID);

  public Type Table { get; private set; }

  public Type Extension => typeof (Payment);

  public PaymentMapping(Type table) => this.Table = table;
}
