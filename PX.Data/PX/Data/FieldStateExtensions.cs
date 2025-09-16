// Decompiled with JetBrains decompiler
// Type: PX.Data.FieldStateExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Data;

public static class FieldStateExtensions
{
  /// <summary>Indicates whether input control of this field has a fixed list of values.</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool HasFixedValuesList(this PXStringState state) => state.AllowedValues != null;

  /// <summary>Indicates whether input control of this field has a fixed list of values.</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool HasFixedValuesList(this PXIntState state) => state.AllowedValues != null;
}
