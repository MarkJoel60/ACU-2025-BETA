// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.GeneralInfoSelect
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.EP;

public class GeneralInfoSelect(PXGraph graph) : PXSelect<Access.AccessInfoNotification>(graph, (Delegate) new PXSelectDelegate(GeneralInfoSelect.SelectDelegate))
{
  public const string ViewName = "GeneralInfo";

  private static IEnumerable SelectDelegate()
  {
    Access.AccessInfoNotification infoNotification = new Access.AccessInfoNotification();
    infoNotification.ScreenID = PXContext.GetScreenID();
    infoNotification.UserName = PXAccess.GetUserName();
    infoNotification.DisplayName = PXAccess.GetUserDisplayName();
    infoNotification.UserID = PXAccess.GetUserID();
    infoNotification.BranchID = PXAccess.GetBranchID();
    infoNotification.CompanyName = PXDatabase.Provider.GetCompanyDisplayName();
    PreferencesEmail preferencesEmail = (PreferencesEmail) PXSelectBase<PreferencesEmail, PXSelect<PreferencesEmail>.Config>.SelectSingleBound(PXView.CurrentGraph, (object[]) null, (object[]) null);
    infoNotification.NotificationSiteUrl = preferencesEmail?.NotificationSiteUrl ?? PXUrl.SiteUrlWithPath();
    infoNotification.MailSignature = MailAccountManager.GetSignature(PXView.CurrentGraph, MailAccountManager.SignatureOptions.Default);
    System.DateTime now = PXTimeZoneInfo.Now;
    infoNotification.BusinessDate = new System.DateTime?(PXContext.GetBusinessDate() ?? new System.DateTime(now.Year, now.Month, now.Day));
    string companyName = PXAccess.GetCompanyName();
    string screenId = PXContext.GetScreenID();
    PXSiteMapNode mapNodeByScreenId = !string.IsNullOrEmpty(screenId) ? PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId.Replace(".", string.Empty)) : (PXSiteMapNode) null;
    if (!string.IsNullOrEmpty(mapNodeByScreenId?.ScreenID) && !string.IsNullOrEmpty(PXView.CurrentGraph.PrimaryView))
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("?");
      if (!string.IsNullOrEmpty(companyName))
      {
        stringBuilder.Append("CompanyID=" + HttpUtility.UrlEncode(companyName));
        stringBuilder.Append("&");
      }
      stringBuilder.Append("ScreenId=" + mapNodeByScreenId.ScreenID);
      PXCache cache = PXView.CurrentGraph.Views[PXView.CurrentGraph.PrimaryView].Cache;
      object current = cache.Current;
      if (current != null)
      {
        List<string> stringList = new List<string>();
        foreach (string key in (IEnumerable<string>) cache.Keys)
        {
          string str1 = key;
          object obj = cache.GetValue(current, key);
          if (obj != null)
          {
            if (string.Equals(key, "ScreenID", StringComparison.InvariantCultureIgnoreCase))
              str1 = "_" + key;
            if (string.Equals(key, "CompanyID", StringComparison.InvariantCultureIgnoreCase))
              str1 = "_" + key;
            string source = obj.ToString();
            if (!source.Contains<char>('<') && !source.Contains<char>('>'))
            {
              if (source.Contains<char>('\\'))
                source = source.Replace("\\", "%5C");
              string str2 = PXUrl.QuoteString(source);
              stringList.Add($"{str1}={HttpUtility.UrlEncode(str2)}");
            }
          }
        }
        if (stringList.Count > 0)
        {
          stringBuilder.Append("&");
          stringBuilder.Append(string.Join("&", stringList.ToArray()));
        }
      }
      infoNotification.LinkEntity = GeneralInfoSelect.Combine(infoNotification.NotificationSiteUrl, mapNodeByScreenId.Url) + stringBuilder?.ToString();
    }
    yield return (object) infoNotification;
  }

  public static string Combine(string uri1, string uri2)
  {
    uri1 = uri1.TrimEnd('/');
    uri2 = uri2.TrimStart('~');
    uri2 = uri2.TrimStart('/');
    return $"{uri1}/{uri2}";
  }
}
