// Decompiled with JetBrains decompiler
// Type: PX.SM.LicenseDetail
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace PX.SM;

[Serializable]
public class LicenseDetail
{
  public string LicenseKey { get; set; }

  public string LicenseType { get; set; }

  public string InstallationID { get; set; }

  public string Status { get; set; }

  public DateTime? ValidTo { get; set; }

  public string IssuedBy { get; set; }

  public bool? IsPortal { get; set; }

  public string ResourceLevel { get; set; }

  [XmlArray("features")]
  [XmlArrayItem("feature")]
  public LicenseFeatureInfo[] Features { get; set; }

  [XmlArray("restrictions")]
  [XmlArrayItem("restriction")]
  public LicenseRestriction[] Restrictions { get; set; }

  [XmlArray("resourceRestrictions")]
  [XmlArrayItem("restriction")]
  public LicenseRestriction[] ResourceRestrictions { get; set; }

  [XmlArray("featureRestrictions")]
  [XmlArrayItem("restriction")]
  public LicenseRestriction[] FeatureRestrictions { get; set; }

  [XmlArray("businessRestrictions")]
  [XmlArrayItem("restriction")]
  public LicenseRestriction[] BusinessRestrictions { get; set; }
}
