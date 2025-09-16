// Decompiled with JetBrains decompiler
// Type: PX.Api.Payroll.AppLicenseInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update;
using System;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace PX.Api.Payroll;

[DataContract]
[Serializable]
public class AppLicenseInfo
{
  [NonSerialized]
  private XmlDocument _RestrictionXml;
  public const string TelemetryXmlTag = "telemetry";
  public const string ClientIDXmlAttribute = "clientID";
  public const string FeaturesXmlTag = "features";
  public const string FeatureNameXmlAttribute = "name";
  public const string PayrollModuleName = "PX.Objects.CS.FeaturesSet+PayrollModule";

  [DataMember]
  public virtual string Restriction { get; set; }

  [DataMember]
  public virtual string Signature { get; set; }

  [DataMember]
  public virtual bool Licensed { get; set; }

  public bool Validate()
  {
    if (!this.Licensed)
      return false;
    byte[] signatureHash = Convert.FromBase64String(this.Signature);
    return PXCriptoHelper.ValidateHash(PXCriptoHelper.CalculateSHA(this.Restriction), signatureHash);
  }

  public XmlDocument AsXml()
  {
    if (this._RestrictionXml != null)
      return this._RestrictionXml;
    if (string.IsNullOrWhiteSpace(this.Restriction))
      return (XmlDocument) null;
    this._RestrictionXml = new XmlDocument();
    this._RestrictionXml.LoadXml(this.Restriction);
    return this._RestrictionXml;
  }

  public string GetClientID()
  {
    XmlDocument xmlDocument = this.AsXml();
    if (xmlDocument != null)
    {
      xmlDocument.LoadXml(this.Restriction);
      XmlNodeList elementsByTagName = xmlDocument.DocumentElement.GetElementsByTagName("telemetry");
      if (elementsByTagName.Count > 0)
        return elementsByTagName.Item(0).Attributes["clientID"]?.Value;
    }
    return (string) null;
  }
}
