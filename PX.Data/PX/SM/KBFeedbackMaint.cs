// Decompiled with JetBrains decompiler
// Type: PX.SM.KBFeedbackMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.SM;

/// <exclude />
public class KBFeedbackMaint : PXGraph<KBFeedbackMaint>
{
  public PXSelect<KBFeedback> Responses;
  public PXAction<KBFeedback> submit;
  public PXAction<KBFeedback> close;

  [PXUIField(DisplayName = "Submit", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXButton]
  public virtual void Submit()
  {
    this.Responses.Cache.CreateCopy((object) this.Responses.Current);
    this.Responses.Current.UserID = new Guid?(this.Accessinfo.UserID);
    this.Responses.Current.Date = new System.DateTime?(PXTimeZoneInfo.Now);
    this.Actions.PressSave();
    string str = PXUrl.SiteUrlWithPath();
    throw new PXRedirectToUrlException($"{str + (str.EndsWith("/") ? "" : "/")}Wiki/{"ShowWiki.aspx"}?pageid={this.Responses.Current.PageID}" + "&feedbackid=1", PXBaseRedirectException.WindowMode.Same, "");
  }

  [PXUIField(DisplayName = "Close", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual void Close() => throw new PXClosePopupException("");

  public override IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }
}
