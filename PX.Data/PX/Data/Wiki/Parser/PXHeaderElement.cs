// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXHeaderElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Represents a header element in memory.</summary>
internal class PXHeaderElement : PXContainerElement
{
  private SectionLevel level;
  private int id;
  private bool hasExpTag;
  private bool hasCollapsedTag;
  private bool nestedTopics;
  private bool collapsable;
  private bool defaultexpanded;

  /// <summary>Initializes a new PXHeaderElement object.</summary>
  /// <param name="level">Header level.</param>
  public PXHeaderElement(SectionLevel level) => this.level = level;

  /// <summary>
  /// Gets name of this PXHeaderElement object (H1, H2, H3 or H4).
  /// </summary>
  public string Name => this.level.ToString();

  /// <summary>
  /// Gets a SectionLevel value representing level of this PXHeaderElement object.
  /// </summary>
  public SectionLevel Level => this.level;

  /// <summary>
  /// Gets or sets an ID of section preceeded with this header.
  /// </summary>
  public int SectionID
  {
    get => this.id;
    set => this.id = value;
  }

  /// <summary>
  /// Gets a value indicating whether this header should be expandable.
  /// </summary>
  public bool HasExpTag => this.hasExpTag;

  /// <summary>
  /// Gets a value indicating whether this header should be initially collapsed.
  /// </summary>
  public bool HasCollapsedTag => this.hasCollapsedTag;

  /// <summary>
  /// Gets a value indicating whether this header should be initially collapsed.
  /// </summary>
  public bool IsCollapsable => this.collapsable;

  /// <summary>
  /// Gets a value indicating whether this header should be initially collapsed.
  /// </summary>
  public bool IsDefaultExpanded => this.defaultexpanded;

  /// <summary>
  /// Gets a value indicating whether the section under this header was automatically filled with nested topics.
  /// </summary>
  public bool IsNestedTopics => this.nestedTopics;

  /// <summary>Gets a string representing header caption.</summary>
  public string Value => this.GetValue(this.Children);

  /// <summary>
  /// Parses header caption and determines whether it can be expandable and
  /// whether succeeding section should be initially collapsed.
  /// </summary>
  public void ExtractExpandProps()
  {
    foreach (PXElement child in this.Children)
    {
      if (child is PXTextElement)
        this.ExtractExpandProps((PXTextElement) child);
    }
  }

  private void ExtractExpandProps(PXTextElement e)
  {
    string str1 = "{collapsed}";
    string str2 = "{exp}";
    string str3 = "{collapsable}";
    string str4 = "{defaultexpanded}";
    string str5 = "{nestedtopics}";
    string lower1 = e.Value.ToLower();
    if (lower1.Length >= str2.Length && lower1.Contains(str2))
    {
      int startIndex = lower1.IndexOf(str2);
      this.hasExpTag = true;
      e.Value = e.Value.Remove(startIndex, str2.Length);
    }
    string lower2 = e.Value.ToLower();
    if (lower2.Length >= str1.Length && lower2.Contains(str1))
    {
      int startIndex = lower2.IndexOf(str1);
      this.hasCollapsedTag = true;
      e.Value = e.Value.Remove(startIndex, str1.Length);
    }
    string lower3 = e.Value.ToLower();
    if (lower3.Length >= str3.Length && lower3.Contains(str3))
    {
      int startIndex = lower3.IndexOf(str3);
      this.collapsable = true;
      e.Value = e.Value.Remove(startIndex, str3.Length);
    }
    string lower4 = e.Value.ToLower();
    if (lower4.Length >= str4.Length && lower4.Contains(str4))
    {
      int startIndex = lower4.IndexOf(str4);
      this.defaultexpanded = true;
      e.Value = e.Value.Remove(startIndex, str4.Length);
    }
    string lower5 = e.Value.ToLower();
    if (lower5.Length < str5.Length || !lower5.Contains(str5))
      return;
    int startIndex1 = lower5.IndexOf(str5);
    this.nestedTopics = true;
    e.Value = e.Value.Remove(startIndex1, str5.Length);
  }

  private string GetValue(PXElement[] children)
  {
    string str = "";
    foreach (PXElement child in children)
    {
      switch (child)
      {
        case PXContainerElement _:
          str += this.GetValue(((PXContainerElement) child).Children);
          break;
        case PXLinkElement _:
          PXLinkElement pxLinkElement = (PXLinkElement) child;
          PXElement[] captionElements = pxLinkElement.GetCaptionElements();
          str += this.GetValue(captionElements.Length == 0 ? pxLinkElement.GetLinkElements() : captionElements);
          break;
        case PXTextElement _:
          str += ((PXTextElement) child).Value;
          break;
      }
    }
    return str;
  }
}
