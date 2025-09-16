// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiCssMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Web;

#nullable disable
namespace PX.SM;

/// <exclude />
public class WikiCssMaint : PXGraph<WikiCssMaint, WikiCss>
{
  public PXSelect<WikiCss> WikiStyles;

  public WikiCssMaint() => Wiki.BlockIfOnlineHelpIsOn();

  protected void WikiCss_CssID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  protected void WikiCss_RowSelecting(PXCache cache, PXRowSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    this.ReplaceForFirefox((WikiCss) e.Row);
  }

  protected void WikiCss_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    WikiCss row = e.Row as WikiCss;
    if (e.Operation == PXDBOperation.Delete || row.Style == null)
      return;
    row.Style = row.Style.Replace("\n", Environment.NewLine).Replace("\r\r", "\r");
  }

  protected void WikiCss_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    this.ReplaceForFirefox((WikiCss) e.Row);
  }

  private void ReplaceForFirefox(WikiCss row)
  {
    if (HttpContext.Current == null || HttpContext.Current.Request == null || HttpContext.Current.Request.Browser == null || !(HttpContext.Current.Request.Browser.Browser == "Firefox") || row.Style == null)
      return;
    row.Style = row.Style.Replace(Environment.NewLine, "\n");
  }
}
