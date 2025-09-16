// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.PXSearchResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Search;

/// <summary>Represents search results.</summary>
public class PXSearchResult
{
  protected List<PXResultButton> captionButtons = new List<PXResultButton>();
  protected List<PXDescriptionColumn> descriptionColumns = new List<PXDescriptionColumn>();

  public List<PXResultButton> CaptionButtons
  {
    get => this.captionButtons;
    set => this.captionButtons = value;
  }

  public List<PXDescriptionColumn> Description
  {
    get => this.descriptionColumns;
    set => this.descriptionColumns = value;
  }
}
