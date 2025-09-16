// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.WikiArticle
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>
/// Represents parsed article with all its elements in memory.
/// </summary>
public class WikiArticle
{
  private Stack<PXElement> elements = new Stack<PXElement>();
  private readonly PX.Data.Wiki.Parser.ParseContext _parseContext = new PX.Data.Wiki.Parser.ParseContext();
  public readonly List<TOCItem> TocItems = new List<TOCItem>();

  internal PX.Data.Wiki.Parser.ParseContext TypedContext => this._parseContext;

  public IDictionary<string, object> ParseContext
  {
    get => (IDictionary<string, object>) this._parseContext;
  }

  /// <summary>Adds an element to stack.</summary>
  /// <param name="elem">An element to add.</param>
  public void AddElement(PXElement elem) => this.elements.Push(elem);

  /// <summary>Adds an range of elements to stack.</summary>
  /// <param name="elems">An elements collection to add.</param>
  public void AddElementsRange(IEnumerable<PXElement> elems)
  {
    foreach (PXElement elem in elems)
      this.elements.Push(elem);
  }

  /// <summary>Adds an range of elements to stack.</summary>
  /// <param name="article">An article with elements collection to add.</param>
  public void AddElementsRange(WikiArticle article)
  {
    foreach (PXElement element in article.elements)
      this.elements.Push(element);
  }

  /// <summary>
  /// Returns a list of all elements of this WikiArticle in order of appearance.
  /// </summary>
  /// <returns>A list of all elements of this WikiArticle in order of appearance.</returns>
  public List<PXElement> GetAllElements()
  {
    List<PXElement> allElements = new List<PXElement>((IEnumerable<PXElement>) this.elements.ToArray());
    allElements.Reverse();
    return allElements;
  }

  /// <summary>
  /// Puts all elements from top of stack inside of nearest non-reduced container element.
  /// </summary>
  public void ReduceToContainer()
  {
    List<PXElement> elems = new List<PXElement>();
    PXElement current;
    for (current = this.Current; current != null && !this.IsStopReduction(current); current = this.Current)
    {
      PXElement pxElement = this.elements.Pop();
      elems.Add(pxElement);
    }
    elems.Reverse();
    if (current == null)
      return;
    ((PXContainerElement) current).AddChildren(elems);
    ((PXContainerElement) current).IsReduced = true;
  }

  /// <summary>Gets the last added element.</summary>
  public PXElement Current => this.elements.Count == 0 ? (PXElement) null : this.elements.Peek();

  private bool IsStopReduction(PXElement el)
  {
    return el is PXContainerElement && !((PXContainerElement) el).IsReduced;
  }
}
