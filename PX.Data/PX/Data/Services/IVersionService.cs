// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.IVersionService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Services;

/// <exclude />
[Obsolete("Will be replaced by the new PX.Version project in 2023R2")]
public interface IVersionService
{
  string ZeroVersion { get; }

  string MinimumVersion { get; }

  string DefaultVersion { get; }

  string VersionRegExp { get; }

  Version AssemblyVersion { get; }

  string GetVersionString(Version vers, bool pxFormat);
}
