// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNameAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>The base class for <tt>PXCacheName</tt>
/// and <tt>PXViewName</tt> attributes. Do not use
/// this attribute directly.</summary>
public class PXNameAttribute : Attribute
{
  protected string _name;

  /// <summary>Gets the value specified as the name in the
  /// constructor.</summary>
  public string Name => this._name;

  /// <exclude />
  public virtual string GetName() => PXMessages.Localize(this._name, out string _);

  /// <summary>
  /// Initializes a new instance of the attribute that assigns the provided name to the object.
  /// </summary>
  /// <param name="name">The value used as the name of the object.</param>
  public PXNameAttribute(string name) => this._name = name;
}
