// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Interfaces.ICCPaymentTransaction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Interfaces;

public interface ICCPaymentTransaction
{
  int? TransactionID { get; set; }

  int? TranNbr { get; set; }

  int? PMInstanceID { get; set; }

  string ProcessingCenterID { get; set; }

  string DocType { get; set; }

  string RefNbr { get; set; }

  int? RefTranNbr { get; set; }

  string OrigDocType { get; set; }

  string OrigRefNbr { get; set; }

  string PCTranNumber { get; set; }

  string PCTranApiNumber { get; set; }

  string AuthNumber { get; set; }

  string TranType { get; set; }

  string ProcStatus { get; set; }

  string TranStatus { get; set; }

  DateTime? ExpirationDate { get; set; }

  Decimal? Amount { get; set; }
}
