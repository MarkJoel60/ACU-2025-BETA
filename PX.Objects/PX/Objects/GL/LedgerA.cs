// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.LedgerA
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXHidden]
[Serializable]
public class LedgerA : Ledger
{
  public new abstract class baseCuryID : BqlType<IBqlString, string>.Field<
  #nullable disable
  LedgerA.baseCuryID>
  {
  }

  public new abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LedgerA.ledgerID>
  {
  }
}
