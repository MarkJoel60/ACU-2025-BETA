// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Consolidation.ConsolidationItemAPI
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.GL.Consolidation;

internal class ConsolidationItemAPI
{
  public virtual string AccountCD { get; set; }

  public virtual Decimal? ConsolAmtCredit { get; set; }

  public virtual Decimal? ConsolAmtDebit { get; set; }

  public virtual string FinPeriodID { get; set; }

  public virtual string MappedValue { get; set; }

  public virtual int? MappedValueLength { get; set; }
}
