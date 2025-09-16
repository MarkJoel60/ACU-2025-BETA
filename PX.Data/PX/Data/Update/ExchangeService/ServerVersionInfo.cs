// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ServerVersionInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[XmlRoot(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
[Serializable]
public class ServerVersionInfo : SoapHeader
{
  private int majorVersionField;
  private bool majorVersionFieldSpecified;
  private int minorVersionField;
  private bool minorVersionFieldSpecified;
  private int majorBuildNumberField;
  private bool majorBuildNumberFieldSpecified;
  private int minorBuildNumberField;
  private bool minorBuildNumberFieldSpecified;
  private string versionField;

  /// <remarks />
  [XmlAttribute]
  public int MajorVersion
  {
    get => this.majorVersionField;
    set => this.majorVersionField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MajorVersionSpecified
  {
    get => this.majorVersionFieldSpecified;
    set => this.majorVersionFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public int MinorVersion
  {
    get => this.minorVersionField;
    set => this.minorVersionField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MinorVersionSpecified
  {
    get => this.minorVersionFieldSpecified;
    set => this.minorVersionFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public int MajorBuildNumber
  {
    get => this.majorBuildNumberField;
    set => this.majorBuildNumberField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MajorBuildNumberSpecified
  {
    get => this.majorBuildNumberFieldSpecified;
    set => this.majorBuildNumberFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public int MinorBuildNumber
  {
    get => this.minorBuildNumberField;
    set => this.minorBuildNumberField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MinorBuildNumberSpecified
  {
    get => this.minorBuildNumberFieldSpecified;
    set => this.minorBuildNumberFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string Version
  {
    get => this.versionField;
    set => this.versionField = value;
  }
}
