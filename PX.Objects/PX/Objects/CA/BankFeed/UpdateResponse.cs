// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.UpdateResponse
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Text.Json.Serialization;

#nullable disable
namespace PX.Objects.CA.BankFeed;

internal class UpdateResponse
{
  [JsonPropertyName("updated")]
  public bool Updated { get; set; }

  [JsonPropertyName("error_reason")]
  public string ErrorReason { get; set; }
}
