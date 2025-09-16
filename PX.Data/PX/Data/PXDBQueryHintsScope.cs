// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBQueryHintsScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

[Obsolete("This class is obsolete and will be removed. Rewrite your code without this class or contact your partner for assistance.")]
public class PXDBQueryHintsScope : IDisposable
{
  private QueryHints _queryHints;
  public static QueryHints DefaultHints = WebConfig.SqlOptimizeForUnknown ? QueryHints.SqlServerOptimizeForUnknown : QueryHints.None;

  [Obsolete("This method is obsolete and will be removed. Rewrite your code without this method or contact your partner for assistance.")]
  public PXDBQueryHintsScope(QueryHints hints)
  {
    this._queryHints = hints;
    PXContext.SetSlot<PXDBQueryHintsScope>(this);
  }

  [Obsolete("This method is obsolete and will be removed. Rewrite your code without this method or contact your partner for assistance.")]
  public void Dispose() => PXContext.SetSlot<PXDBQueryHintsScope>((PXDBQueryHintsScope) null);
}
