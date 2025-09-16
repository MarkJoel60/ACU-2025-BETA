// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.PXDescriptionColumn
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Search;

/// <exclude />
public class PXDescriptionColumn
{
  private List<PXResultButton> captionButtons = new List<PXResultButton>();
  private string text;

  public PXDescriptionColumn()
  {
  }

  public PXDescriptionColumn(string text) => this.text = text;

  public List<PXResultButton> CaptionButtons
  {
    get => this.captionButtons;
    set => this.captionButtons = value;
  }

  public virtual string Text
  {
    get => this.text;
    set => this.text = value;
  }
}
