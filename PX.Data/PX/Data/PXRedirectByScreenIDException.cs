// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRedirectByScreenIDException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.ComponentModel;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public class PXRedirectByScreenIDException : PXRedirectToUrlException
{
  public PXRedirectByScreenIDException(
    string screenId,
    PXBaseRedirectException.WindowMode windowMode,
    object queryParameters)
    : base(PXRedirectByScreenIDException.GetUrl(screenId, queryParameters, false), windowMode, string.Empty)
  {
  }

  public PXRedirectByScreenIDException(
    string screenId,
    PXBaseRedirectException.WindowMode windowMode,
    PXRedirectByScreenIDException.FramesetBehavior framesetBehavior,
    object queryParameters)
    : base(PXRedirectByScreenIDException.GetUrl(screenId, queryParameters, framesetBehavior == PXRedirectByScreenIDException.FramesetBehavior.Refresh, framesetBehavior == PXRedirectByScreenIDException.FramesetBehavior.Supress), windowMode, framesetBehavior == PXRedirectByScreenIDException.FramesetBehavior.Supress, string.Empty)
  {
  }

  public PXRedirectByScreenIDException(
    string screenId,
    PXBaseRedirectException.WindowMode windowMode)
    : this(screenId, windowMode, (object) null)
  {
  }

  public PXRedirectByScreenIDException(
    string screenId,
    PXBaseRedirectException.WindowMode windowMode,
    bool supressFrameset,
    object queryParameters)
    : this(screenId, windowMode, supressFrameset ? PXRedirectByScreenIDException.FramesetBehavior.Supress : PXRedirectByScreenIDException.FramesetBehavior.Default, queryParameters)
  {
  }

  public PXRedirectByScreenIDException(
    string screenId,
    PXBaseRedirectException.WindowMode windowMode,
    PXRedirectByScreenIDException.FramesetBehavior framesetBehavior)
    : this(screenId, windowMode, framesetBehavior, (object) null)
  {
  }

  public PXRedirectByScreenIDException(
    string screenId,
    PXBaseRedirectException.WindowMode windowMode,
    bool supressFrameset)
    : this(screenId, windowMode, supressFrameset ? PXRedirectByScreenIDException.FramesetBehavior.Supress : PXRedirectByScreenIDException.FramesetBehavior.Default, (object) null)
  {
  }

  private static string GetUrl(string screenId, object queryParameters, bool refresh)
  {
    return PXRedirectByScreenIDException.GetUrl(screenId, queryParameters, refresh, false);
  }

  private static string GetUrl(
    string screenId,
    object queryParameters,
    bool refresh,
    bool supressFrameset)
  {
    PXSiteMapNode pxSiteMapNode = !string.IsNullOrEmpty(screenId) && screenId.Length == 8 ? PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId) : throw new PXArgumentException(nameof (screenId));
    if (pxSiteMapNode == null)
      throw new PXException(string.Format(ErrorMessages.GetLocal("You have insufficient rights to access the object ({0})."), (object) screenId));
    string url = !supressFrameset ? PXUrl.AppendUrlParameter("~/Main", "ScreenID", screenId.EncodeQueryStringPart()) : pxSiteMapNode.Url;
    if (queryParameters != null)
    {
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(queryParameters))
      {
        string key = property.Name.EncodeQueryStringPart();
        object obj = property.GetValue(queryParameters);
        string str = obj != null ? obj.ToString().EncodeQueryStringPart() : (string) null;
        url = PXUrl.AppendUrlParameter(url, key, str);
      }
    }
    if (refresh)
      url += "$target=_top";
    return url;
  }

  public enum FramesetBehavior
  {
    Default,
    Supress,
    Refresh,
  }
}
