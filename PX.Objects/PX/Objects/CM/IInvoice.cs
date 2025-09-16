// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.IInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Common.Abstractions;
using System;

#nullable disable
namespace PX.Objects.CM;

public interface IInvoice : IRegister, IDocumentKey
{
  Decimal? CuryDocBal { get; set; }

  Decimal? DocBal { get; set; }

  Decimal? CuryDiscBal { get; set; }

  Decimal? DiscBal { get; set; }

  Decimal? CuryWhTaxBal { get; set; }

  Decimal? WhTaxBal { get; set; }

  DateTime? DiscDate { get; set; }

  bool? Released { get; set; }
}
