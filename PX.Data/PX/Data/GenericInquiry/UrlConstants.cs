// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.UrlConstants
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.GenericInquiry;

[PXInternalUseOnly]
public static class UrlConstants
{
  internal const string GenericInquiryCommonScreenId = "GenericInquiry";

  [PXInternalUseOnly]
  public static class Path
  {
    [PXInternalUseOnly]
    public const string GenericInquiry = "/GenericInquiry/GenericInquiry.aspx";
    [PXInternalUseOnly]
    public const string NewUiGenericInquiry = "/Scripts/Screens/GenericInquiry.html";
    internal static readonly IEnumerable<string> GenericInquiryPaths = (IEnumerable<string>) new string[2]
    {
      "/GenericInquiry/GenericInquiry.aspx",
      "/Scripts/Screens/GenericInquiry.html"
    };
  }

  [PXInternalUseOnly]
  public static class QueryString
  {
    [PXInternalUseOnly]
    public const string DesignId = "id";
    [PXInternalUseOnly]
    public const string Name = "name";
    [PXInternalUseOnly]
    public const string Dac = "dac";
  }
}
