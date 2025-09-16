// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRedirectToGIRequiredException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Maintenance.GI;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data;

public class PXRedirectToGIRequiredException : PXRedirectToUrlException
{
  private static StringBuilder GetBaseUrl()
  {
    return new StringBuilder("~/GenericInquiry/GenericInquiry.aspx").Append('?');
  }

  private static string BuildUrl(GIDesign design)
  {
    if (design == null)
      throw new ArgumentNullException(nameof (design));
    if (design.DesignID.HasValue)
      return PXRedirectToGIRequiredException.BuildUrl(design.DesignID.Value);
    return !string.IsNullOrEmpty(design.Name) ? PXRedirectToGIRequiredException.BuildUrl(design.Name) : throw new ArgumentException("The provided inquiry design has neither ID nor name.", nameof (design));
  }

  private static string BuildUrl(Guid designId)
  {
    return PXRedirectToGIRequiredException.GetBaseUrl().Append("id").Append("=").Append(designId.ToString()).ToString();
  }

  private static string BuildUrl(string name)
  {
    return PXRedirectToGIRequiredException.BuildUrl(name, (Dictionary<string, string>) null);
  }

  private static string BuildUrl(string name, Dictionary<string, string> parameters)
  {
    string str1 = name.EncodeQueryStringPart();
    StringBuilder stringBuilder = PXRedirectToGIRequiredException.GetBaseUrl().Append(nameof (name)).Append("=").Append(str1);
    if (parameters != null)
    {
      foreach (KeyValuePair<string, string> parameter in parameters)
      {
        string str2 = parameter.Key.EncodeQueryStringPart();
        string str3 = parameter.Value.EncodeQueryStringPart();
        stringBuilder.Append($"&{str2}={str3}");
      }
    }
    return stringBuilder.ToString();
  }

  public PXRedirectToGIRequiredException(
    Guid designId,
    PXBaseRedirectException.WindowMode windowMode = PXBaseRedirectException.WindowMode.Same,
    bool supressFrameset = false)
    : base(PXRedirectToGIRequiredException.BuildUrl(designId), windowMode, supressFrameset, string.Empty)
  {
  }

  public PXRedirectToGIRequiredException(
    string name,
    PXBaseRedirectException.WindowMode windowMode = PXBaseRedirectException.WindowMode.Same,
    bool supressFrameset = false)
    : base(PXRedirectToGIRequiredException.BuildUrl(name), windowMode, supressFrameset, string.Empty)
  {
  }

  public PXRedirectToGIRequiredException(
    GIDesign design,
    PXBaseRedirectException.WindowMode windowMode = PXBaseRedirectException.WindowMode.Same,
    bool supressFrameset = false)
    : base(PXRedirectToGIRequiredException.BuildUrl(design), windowMode, supressFrameset, string.Empty)
  {
  }

  public PXRedirectToGIRequiredException(
    string name,
    Dictionary<string, string> parameters,
    PXBaseRedirectException.WindowMode windowMode = PXBaseRedirectException.WindowMode.Same,
    bool supressFrameset = false)
    : base(PXRedirectToGIRequiredException.BuildUrl(name, parameters), windowMode, supressFrameset, string.Empty)
  {
  }
}
