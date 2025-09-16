// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.Interfaces.IUrlResolver
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Services.Interfaces;

public interface IUrlResolver
{
  bool IsAbsolute(string url);

  string ToAbsolute(string relativeUrl);

  string ToRelative(string absoluteUrl);

  string SecureQueryString(string queryString);

  string IgnoreSystemQueryParameters(string url);
}
