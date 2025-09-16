// Decompiled with JetBrains decompiler
// Type: PX.Data.GIOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

internal class GIOptions
{
  /// <summary>
  /// Defines if the RowSelecting optimization is enabled.
  /// If it is enabled, RowSelecting event handlers are discarded for the fields that are not used in the query.
  /// </summary>
  /// <seealso cref="T:PX.Data.PXGenericInqGrph.PXRowSelectingFieldScope" />
  public bool OptimizeRowSelecting { get; set; } = true;
}
