// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.GenericInquiryDescriptionProviderExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.GenericInquiry;

internal static class GenericInquiryDescriptionProviderExtensions
{
  public static GIDescription Get(this IGenericInquiryDescriptionProvider provider, string designId)
  {
    Guid result;
    return !Guid.TryParse(designId, out result) ? (GIDescription) null : provider.Get(result);
  }
}
