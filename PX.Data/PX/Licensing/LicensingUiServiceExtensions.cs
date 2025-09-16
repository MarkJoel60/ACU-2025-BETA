// Decompiled with JetBrains decompiler
// Type: PX.Licensing.LicensingUiServiceExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace PX.Licensing;

internal static class LicensingUiServiceExtensions
{
  internal static void CheckWarning(this ILicensingUiService licensingUiService, Control form)
  {
    string message;
    string navigation;
    string navScreenID;
    if (!licensingUiService.CheckWarning(out message, out navigation, out navScreenID))
      return;
    Panel child1 = new Panel();
    child1.CssClass = "LicenseBanner";
    if (!string.IsNullOrEmpty(message))
    {
      Label child2 = new Label();
      child2.Text = message;
      child2.CssClass = "LicenseMessage";
      child1.Controls.Add((Control) child2);
    }
    if (!string.IsNullOrEmpty(navigation))
    {
      HyperLink child3 = new HyperLink();
      child3.Text = PXMessages.LocalizeNoPrefix(navigation);
      PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(navScreenID);
      if (mapNodeByScreenId != null)
      {
        string str = child3.ResolveUrl(HttpUtility.UrlPathEncode("~/Main"));
        child3.NavigateUrl = $"{str}?ScreenId={mapNodeByScreenId.ScreenID}";
        child3.CssClass = "LicenseMessage";
        child1.Controls.Add((Control) child3);
      }
    }
    if (child1.Controls.Count <= 0)
      return;
    form.Controls.Add((Control) child1);
  }
}
