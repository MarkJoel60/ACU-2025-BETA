// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.decimal1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

public class decimal1 : BqlType<IBqlDecimal, Decimal>.Constant<
#nullable disable
decimal1>
{
  public decimal1()
    : base(1M)
  {
  }
}
