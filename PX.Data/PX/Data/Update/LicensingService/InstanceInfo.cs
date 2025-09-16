// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.LicensingService.InstanceInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.LicensingService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18047")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://acumatica.com/")]
[Serializable]
public class InstanceInfo
{
  private string licenseKeyField;
  private string installationIDField;
  private string configurationField;

  public string LicenseKey
  {
    get => this.licenseKeyField;
    set => this.licenseKeyField = value;
  }

  public string InstallationID
  {
    get => this.installationIDField;
    set => this.installationIDField = value;
  }

  public string Configuration
  {
    get => this.configurationField;
    set => this.configurationField = value;
  }

  public InstanceStatistics Statistic { get; set; }

  public LicenseCustInfo[] CustProjects { get; set; }
}
