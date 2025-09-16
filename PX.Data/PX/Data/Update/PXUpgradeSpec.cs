// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXUpgradeSpec
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Update;

[Serializable]
public class PXUpgradeSpec
{
  public byte[] Launcher { get; set; }

  public Version CurrentVersion { get; set; }

  public Version DestinationVersion { get; set; }

  public string ZipPath { get; set; }

  public Dictionary<string, string> ZipFiles { get; set; }

  public Dictionary<string, string> Parameters { get; set; }
}
