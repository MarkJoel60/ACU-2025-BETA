// Decompiled with JetBrains decompiler
// Type: PX.Metadata.UiTypeIndependentGiId
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Metadata;

/// <summary>
///     Represents a generic inquiry id that is not tied to a specific UI type.
/// </summary>
/// <summary>
///     Represents a generic inquiry id that is not tied to a specific UI type.
/// </summary>
internal sealed class UiTypeIndependentGiId(Guid genericInquiryId) : IScreenInfoDataId
{
  public Guid Id { get; } = genericInquiryId;

  public string GetCacheKey() => this.Id.ToString();

  public static implicit operator UiTypeIndependentGiId(Guid genericInquiryId)
  {
    return new UiTypeIndependentGiId(genericInquiryId);
  }
}
