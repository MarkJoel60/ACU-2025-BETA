// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRedirectToGIDesignerRequiredException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Maintenance.GI;
using System;
using System.Linq;

#nullable disable
namespace PX.Data;

public class PXRedirectToGIDesignerRequiredException : PXRedirectToUrlException
{
  private const string DesignIdRedirectParameter = "DesignID";
  private static readonly string[] RedirectParams = new string[2]
  {
    "id",
    "name"
  };
  private static readonly string[] DesignerRedirectParams = new string[2]
  {
    "DesignID",
    "Name"
  };

  public static string GetUrl(string giScreenID)
  {
    if (string.IsNullOrEmpty(giScreenID))
      throw new ArgumentNullException(nameof (giScreenID));
    return PXRedirectToGIDesignerRequiredException.GetUrl(PXSiteMap.Provider.FindSiteMapNodesByScreenIDUnsecure(giScreenID).FirstOrDefault<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (n => PXSiteMap.IsGenericInquiry(n.Url))) ?? throw new ArgumentException($"The screen '{giScreenID}' is not a generic inquiry.", nameof (giScreenID)));
  }

  /// <summary>
  /// Returns URL for Generic Inquiry Designer with parameters for defined GI site map node.
  /// </summary>
  /// <param name="giNode"></param>
  /// <returns>URL; null, if Generic Inquiry Designer site map is not accessible or does not exist.</returns>
  public static string GetUrl(PXSiteMapNode giNode)
  {
    if (giNode == null)
      throw new ArgumentNullException(nameof (giNode));
    if (!PXSiteMap.IsGenericInquiry(giNode.Url))
      throw new ArgumentException($"The screen '{giNode.ScreenID}' is not a generic inquiry.", nameof (giNode));
    PXSiteMapNode siteMapNode = PXSiteMap.Provider.FindSiteMapNode(typeof (GenericInquiryDesigner));
    if (siteMapNode == null || string.IsNullOrEmpty(siteMapNode.Url))
      return (string) null;
    string url = siteMapNode.Url;
    for (int index = 0; index < PXRedirectToGIDesignerRequiredException.RedirectParams.Length && index < PXRedirectToGIDesignerRequiredException.DesignerRedirectParams.Length; ++index)
    {
      string parameter = PXUrl.GetParameter(giNode.Url, PXRedirectToGIDesignerRequiredException.RedirectParams[index]);
      if (!string.IsNullOrEmpty(parameter))
        url = url.AppendUrlParam(PXRedirectToGIDesignerRequiredException.DesignerRedirectParams[index], parameter);
    }
    return url;
  }

  internal static string GetUrl(Guid designId)
  {
    string url = PXSiteMap.Provider.FindSiteMapNode(typeof (GenericInquiryDesigner))?.Url;
    return string.IsNullOrEmpty(url) ? (string) null : url.AppendUrlParam("DesignID", designId.ToString());
  }

  public PXRedirectToGIDesignerRequiredException(
    string giScreenID,
    PXBaseRedirectException.WindowMode windowMode = PXBaseRedirectException.WindowMode.Same,
    bool supressFrameset = false)
    : base(PXRedirectToGIDesignerRequiredException.GetUrl(giScreenID), windowMode, supressFrameset, string.Empty)
  {
  }

  public PXRedirectToGIDesignerRequiredException(
    PXSiteMapNode giNode,
    PXBaseRedirectException.WindowMode windowMode = PXBaseRedirectException.WindowMode.Same,
    bool supressFrameset = false)
    : base(PXRedirectToGIDesignerRequiredException.GetUrl(giNode), windowMode, supressFrameset, string.Empty)
  {
  }

  internal PXRedirectToGIDesignerRequiredException(
    Guid designId,
    PXBaseRedirectException.WindowMode windowMode = PXBaseRedirectException.WindowMode.Same,
    bool supressFrameset = false)
    : base(PXRedirectToGIDesignerRequiredException.GetUrl(designId), windowMode, supressFrameset, string.Empty)
  {
  }
}
