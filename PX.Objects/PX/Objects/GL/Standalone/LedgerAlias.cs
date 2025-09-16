// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Standalone.LedgerAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL.Standalone;

[PXCacheName("Ledger")]
[Serializable]
public class LedgerAlias : Ledger
{
  public new abstract class ledgerID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  LedgerAlias.ledgerID>
  {
  }

  public new abstract class ledgerCD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LedgerAlias.ledgerCD>
  {
  }

  public new abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LedgerAlias.organizationID>
  {
  }

  public new abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LedgerAlias.baseCuryID>
  {
  }

  public new abstract class descr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LedgerAlias.descr>
  {
  }

  public new abstract class balanceType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LedgerAlias.balanceType>
  {
  }
}
