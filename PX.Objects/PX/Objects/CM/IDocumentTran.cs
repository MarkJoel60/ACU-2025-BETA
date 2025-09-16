// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.IDocumentTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CM;

public interface IDocumentTran
{
  string TranType { get; set; }

  string RefNbr { get; set; }

  int? LineNbr { get; set; }

  Decimal? TranAmt { get; set; }

  Decimal? CuryTranAmt { get; set; }

  string TranDesc { get; set; }

  DateTime? TranDate { get; set; }

  long? CuryInfoID { get; set; }

  Decimal? CuryCashDiscBal { get; set; }

  Decimal? CashDiscBal { get; set; }

  Decimal? CuryTranBal { get; set; }

  Decimal? TranBal { get; set; }
}
