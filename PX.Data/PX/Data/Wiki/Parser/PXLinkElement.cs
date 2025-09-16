// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXLinkElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Represents a link element in memory.</summary>
public class PXLinkElement : PXElement, IWidthHeightSettable
{
  private List<PXElement> link = new List<PXElement>();
  private List<PXElement> caption = new List<PXElement>();
  private string props;
  private bool noicon;
  private bool isFileLink;
  private string wikiText;
  private bool isPopup;
  private int width;
  private int height;
  private bool isInNewWindow;
  private int? fileRevision;

  /// <summary>
  /// Returns an array of elements which are contained inside of this links value.
  /// </summary>
  /// <returns></returns>
  public PXElement[] GetLinkElements() => this.link.ToArray();

  /// <summary>
  /// Adds a new element to collection representing this links value.
  /// </summary>
  /// <param name="e">An element to add.</param>
  public void AddToLink(PXElement e) => this.link.Add(e);

  /// <summary>
  /// Adds a collection of new elements to collection representing this links value.
  /// </summary>
  /// <param name="elems">A collection of elements to add.</param>
  public void AddToLink(IEnumerable<PXElement> elems) => this.link.AddRange(elems);

  /// <summary>
  /// Returns an array of elements which are contained inside of this links caption.
  /// </summary>
  /// <returns>An array of elements which are contained inside of this links caption.</returns>
  public PXElement[] GetCaptionElements() => this.caption.ToArray();

  /// <summary>
  /// Adds a new element to collection representing this links caption.
  /// </summary>
  /// <param name="e">An element to add.</param>
  public void AddToCaption(PXElement e) => this.caption.Add(e);

  /// <summary>
  /// Adds a collection of new elements to collection representing this links caption.
  /// </summary>
  /// <param name="elems">A collection of elements to add.</param>
  public void AddToCaption(IEnumerable<PXElement> elems) => this.caption.AddRange(elems);

  /// <summary>Gets or sets additional link styles and attributes.</summary>
  public string Props
  {
    get => this.props;
    set => this.props = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether file icon should be displayed for file links
  /// </summary>
  public bool NoIcon
  {
    get => this.noicon;
    set => this.noicon = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether this is a file link.
  /// </summary>
  public bool IsFileLink
  {
    get => this.isFileLink;
    set => this.isFileLink = value;
  }

  public string WikiText
  {
    get => this.wikiText;
    set => this.wikiText = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether link will be displayed like a popup image when user clicks it.
  /// </summary>
  public bool IsPopup
  {
    get => this.isPopup;
    set => this.isPopup = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether link should be opened in a new window.
  /// </summary>
  public bool IsInNewWindow
  {
    get => this.isInNewWindow;
    set => this.isInNewWindow = value;
  }

  /// <summary>
  /// Gets or sets number of file revision (if this link points to file).
  /// </summary>
  public int? FileRevision
  {
    get => !this.IsFileLink ? new int?() : this.fileRevision;
    set => this.fileRevision = value;
  }

  /// <summary>Used for links to popup video (flash).</summary>
  public int Width
  {
    get => this.width;
    set => this.width = value;
  }

  public int Height
  {
    get => this.height;
    set => this.height = value;
  }
}
