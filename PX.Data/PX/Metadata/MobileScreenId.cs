// Decompiled with JetBrains decompiler
// Type: PX.Metadata.MobileScreenId
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Metadata;

/// <summary>
///     Contains a screen id for mobile screens.
///     We need this class because of the temporary solution for the mobile screens to get their legacy schema.
/// </summary>
/// <summary>
///     Contains a screen id for mobile screens.
///     We need this class because of the temporary solution for the mobile screens to get their legacy schema.
/// </summary>
internal sealed class MobileScreenId(string screenId)
{
  public string Id { get; } = screenId;
}
