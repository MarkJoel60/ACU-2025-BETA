// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDACDescriptionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class PXDACDescriptionAttribute : Attribute
{
  private readonly System.Type _target;
  private readonly Attribute _attribute;

  public PXDACDescriptionAttribute(System.Type target, Attribute attribute)
  {
    if (target == (System.Type) null)
      throw new ArgumentNullException(nameof (target));
    if (attribute == null)
      throw new ArgumentNullException(nameof (attribute));
    this._target = target;
    this._attribute = attribute;
  }

  /// <summary>Get.</summary>
  public System.Type Target => this._target;

  /// <summary>Get.</summary>
  public Attribute Attribute => this._attribute;
}
