// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXHtmlTagElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Represents a single html-tag element in memory</summary>
public class PXHtmlTagElement : PXElement
{
  private string tagName;
  private List<PXElement> tagValue;
  private List<PXHtmlAttribute> attributes;

  /// <summary>Gets inner tag elements.</summary>
  public List<PXElement> TagValue
  {
    get => this.tagValue;
    set => this.tagValue = value;
  }

  /// <summary>Gets or sets tag name.</summary>
  public string TagName
  {
    get => this.tagName;
    set => this.tagName = value;
  }

  /// <summary>Gets or sets an attribute list for this HTML tag.</summary>
  public List<PXHtmlAttribute> Attributes
  {
    get => this.attributes;
    set => this.attributes = value;
  }
}
