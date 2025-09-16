// Decompiled with JetBrains decompiler
// Type: PX.Data.Optional2`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Same as <tt>Optional</tt>, but in case the null value is passed to the parameter, doesn't insert the default value.</summary>
/// <typeparam name="Field">The inserted field value.</typeparam>
public sealed class Optional2<Field> : ParameterBase<Field> where Field : IBqlField
{
  /// <exclude />
  public override bool TryDefault => false;

  /// <exclude />
  public override bool HasDefault => true;

  /// <exclude />
  public override bool IsVisible => true;

  /// <exclude />
  public override bool IsArgument => false;
}
