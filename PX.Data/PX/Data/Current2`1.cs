// Decompiled with JetBrains decompiler
// Type: PX.Data.Current2`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Works similar to <tt>Current</tt>, but the default value will not be inserted if NULL was passed to the parameter.</summary>
/// <typeparam name="Field">The inserted field value.</typeparam>
public sealed class Current2<Field> : ParameterBase<Field> where Field : IBqlField
{
  /// <exclude />
  public override bool TryDefault => false;

  /// <exclude />
  public override bool HasDefault => true;

  /// <exclude />
  public override bool IsVisible => false;

  /// <exclude />
  public override bool IsArgument => false;
}
