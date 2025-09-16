// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldUpdateMode
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Specifies possible options for <see cref="P:PX.Data.PXDBDefaultAttribute.FieldUpdateMode" /> property.
/// </summary>
public enum PXFieldUpdateMode
{
  /// <summary>
  /// Default option: field is updated on <tt>RowPersisting</tt> event of dependent DAC.
  /// </summary>
  Default = 0,
  /// <summary>
  /// Field is updated on <tt>RowPersisting</tt> event of dependent DAC.
  /// </summary>
  OnChildPersisting = 0,
  /// <summary>
  /// Field is updated on <tt>RowPersisted</tt> event of the source DAC.
  /// </summary>
  OnParentPersisted = 1,
}
