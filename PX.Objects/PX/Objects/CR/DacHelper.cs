// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.DacHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.CR;

[PXInternalUseOnly]
public static class DacHelper
{
  public static string GetExplicitField<TField>() where TField : IBqlField
  {
    return DacHelper.GetExplicitField(typeof (TField).DeclaringType?.Name, typeof (TField).Name);
  }

  public static string GetExplicitField<TField>(string tableAlias) where TField : IBqlField
  {
    return DacHelper.GetExplicitField(tableAlias, typeof (TField).Name);
  }

  public static string GetExplicitField(string table, string field)
  {
    return table != null ? $"{table}_{field}" : field;
  }
}
