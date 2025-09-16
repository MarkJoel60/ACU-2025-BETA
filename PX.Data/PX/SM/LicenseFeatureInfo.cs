// Decompiled with JetBrains decompiler
// Type: PX.SM.LicenseFeatureInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace PX.SM;

[Serializable]
public class LicenseFeatureInfo
{
  [XmlAttribute]
  public string Id { get; set; }

  [XmlAttribute]
  public string Name { get; set; }

  [XmlAttribute]
  public bool IsEnabled { get; set; }

  public LicenseFeatureInfo()
  {
  }

  public LicenseFeatureInfo(string id, string name, bool enabled)
  {
    this.Id = id;
    this.Name = name;
    this.IsEnabled = enabled;
  }
}
