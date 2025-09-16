// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BudgetLedger
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

[Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
public class BudgetLedger : BqlType<IBqlString, string>.Constant<
#nullable disable
BudgetLedger>
{
  public BudgetLedger()
    : base("B")
  {
  }
}
