// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Interfaces.IExternalTransaction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Interfaces;

public interface IExternalTransaction
{
  int? TransactionID { get; set; }

  string DocType { get; set; }

  string RefNbr { get; set; }

  string OrigDocType { get; set; }

  string OrigRefNbr { get; set; }

  string VoidDocType { get; set; }

  string VoidRefNbr { get; set; }

  string TranNumber { get; set; }

  string TranApiNumber { get; set; }

  string CommerceTranNumber { get; set; }

  string AuthNumber { get; set; }

  Decimal? Amount { get; set; }

  string ProcStatus { get; set; }

  string ProcessingCenterID { get; set; }

  DateTime? LastActivityDate { get; set; }

  string Direction { get; set; }

  bool? Active { get; set; }

  bool? Completed { get; set; }

  bool? NeedSync { get; set; }

  bool? SaveProfile { get; set; }

  int? ParentTranID { get; set; }

  DateTime? ExpirationDate { get; set; }

  string CVVVerification { get; set; }

  string SyncStatus { get; set; }

  string SyncMessage { get; set; }

  Guid? NoteID { get; set; }

  string LastDigits { get; set; }

  string CardType { get; set; }
}
