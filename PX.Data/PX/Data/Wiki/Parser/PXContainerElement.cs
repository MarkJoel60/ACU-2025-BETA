// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXContainerElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>
/// Represents a base class for all container elements (i.e. elements which can contain other elements).
/// </summary>
public class PXContainerElement : PXElement
{
  protected List<PXElement> children = new List<PXElement>();
  protected bool isReduced;

  /// <summary>Adds a new child element to this container.</summary>
  /// <param name="elem">A child element to add.</param>
  public void AddChild(PXElement elem)
  {
    this.children.Add(elem);
    elem.parent = (PXElement) this;
  }

  /// <summary>Adds new children elements to this container.</summary>
  /// <param name="elems">A list of children to add.</param>
  public void AddChildren(List<PXElement> elems)
  {
    this.children.AddRange((IEnumerable<PXElement>) elems);
    foreach (PXElement elem in elems)
      elem.parent = (PXElement) this;
  }

  /// <summary>Gets an array containing child elements.</summary>
  public PXElement[] Children => this.children.ToArray();

  /// <summary>
  /// Gets or sets value indicating whether this container is reduced (
  /// includes child elements it should have had contained).
  /// </summary>
  public bool IsReduced
  {
    get => this.isReduced;
    set => this.isReduced = value;
  }
}
