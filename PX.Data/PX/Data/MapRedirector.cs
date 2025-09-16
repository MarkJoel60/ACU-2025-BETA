// Decompiled with JetBrains decompiler
// Type: PX.Data.MapRedirector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public abstract class MapRedirector
{
  public readonly string Title;

  public MapRedirector(string title) => this.Title = title;

  public abstract void ShowAddress(
    string country,
    string state,
    string city,
    string postalCode,
    string addressLine1,
    string addressLine2,
    string addressLine3);
}
