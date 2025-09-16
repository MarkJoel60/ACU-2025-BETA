// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXElementRendererAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>
/// Represents information of elements available for rendering in specific renderer
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PXElementRendererAttribute : Attribute
{
  private readonly System.Type _renderer;
  private readonly System.Type[] _types;

  public PXElementRendererAttribute(System.Type renderer, params System.Type[] types)
  {
    this._renderer = renderer;
    this._types = types;
  }

  public System.Type[] Types => this._types;

  public System.Type Renderer => this._renderer;

  public virtual object CreateRenderer(System.Type type) => Activator.CreateInstance(type);
}
