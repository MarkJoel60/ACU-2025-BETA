// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.PXResultButton
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Search;

/// <exclude />
public class PXResultButton
{
  private string caption;
  private string navigateUrl;
  private PXResultClickHandler clickHandler;

  public PXResultButton(string caption) => this.caption = caption;

  public string Caption => this.caption;

  public string NavigateUrl
  {
    get => this.navigateUrl;
    set => this.navigateUrl = value;
  }

  public PXResultClickHandler ClickHandler
  {
    get => this.clickHandler;
    set => this.clickHandler = value;
  }

  public void DoClickHandling(PXSearchResult result)
  {
    if (this.clickHandler == null)
      return;
    this.clickHandler(this, result);
  }
}
