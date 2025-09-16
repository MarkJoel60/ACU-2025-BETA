// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.PXRedirectToGIWithParametersRequiredException
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

#nullable disable
namespace PX.Objects.FS;

public class PXRedirectToGIWithParametersRequiredException : PXRedirectToUrlException
{
  public PXRedirectToGIWithParametersRequiredException(
    SerializationInfo info,
    StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXRedirectToGIWithParametersRequiredException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXRedirectToGIWithParametersRequiredException>(this, info);
    base.GetObjectData(info, context);
  }

  public PXRedirectToGIWithParametersRequiredException(
    string gIName,
    Dictionary<string, string> parameters,
    PXBaseRedirectException.WindowMode windowMode = 1,
    bool supressFrameset = false)
    : base(PXRedirectToGIWithParametersRequiredException.BuildUrl(gIName, parameters), windowMode, supressFrameset, string.Empty)
  {
  }

  public PXRedirectToGIWithParametersRequiredException(
    Guid designId,
    Dictionary<string, string> parameters,
    PXBaseRedirectException.WindowMode windowMode = 1,
    bool supressFrameset = false)
    : base(PXRedirectToGIWithParametersRequiredException.BuildUrl(designId, parameters), windowMode, supressFrameset, string.Empty)
  {
  }

  private static void AppendParameters(Dictionary<string, string> parameters, ref StringBuilder url)
  {
    foreach (KeyValuePair<string, string> parameter in parameters)
    {
      url.Append("&");
      url.Append(parameter.Key);
      url.Append("=");
      url.Append(parameter.Value.Trim());
    }
  }

  public static string BuildUrl(Guid designId, Dictionary<string, string> parameters)
  {
    StringBuilder url = new StringBuilder(PXRedirectToGIWithParametersRequiredException.BuildGenericInquiryUrlById(designId));
    if (parameters != null)
      PXRedirectToGIWithParametersRequiredException.AppendParameters(parameters, ref url);
    return url.ToString();
  }

  private static string BuildUrl(string baseUrl, string paramName, string paramValue)
  {
    return $"{baseUrl}?{paramName}={paramValue}";
  }

  internal static string BuildGenericInquiryUrlById(Guid designId)
  {
    return ((IEnumerable<string>) new string[2]
    {
      PXRedirectToGIWithParametersRequiredException.BuildUrl("~/GenericInquiry/GenericInquiry.aspx", "id", designId.ToString()),
      PXRedirectToGIWithParametersRequiredException.BuildUrl("~/Scripts/Screens/GenericInquiry.html", "id", designId.ToString())
    }).Select<string, PXSiteMapNode>((Func<string, PXSiteMapNode>) (x => PXSiteMapProviderExtensions.FindSiteMapNodeUnsecure(PXSiteMap.Provider, x))).FirstOrDefault<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (x => x != null))?.Url;
  }

  internal static string BuildGenericInquiryUrlByName(string giName)
  {
    return ((IEnumerable<string>) new string[2]
    {
      PXRedirectToGIWithParametersRequiredException.BuildUrl("~/GenericInquiry/GenericInquiry.aspx", "name", giName),
      PXRedirectToGIWithParametersRequiredException.BuildUrl("~/Scripts/Screens/GenericInquiry.html", "name", giName)
    }).Select<string, PXSiteMapNode>((Func<string, PXSiteMapNode>) (x => PXSiteMapProviderExtensions.FindSiteMapNodeUnsecure(PXSiteMap.Provider, x))).FirstOrDefault<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (x => x != null))?.Url;
  }

  private static string BuildUrl(string giName, Dictionary<string, string> parameters)
  {
    StringBuilder url = new StringBuilder(PXRedirectToGIWithParametersRequiredException.BuildGenericInquiryUrlByName(giName));
    if (parameters != null)
    {
      foreach (KeyValuePair<string, string> parameter in parameters)
        PXRedirectToGIWithParametersRequiredException.AppendParameters(parameters, ref url);
    }
    return url.ToString();
  }
}
