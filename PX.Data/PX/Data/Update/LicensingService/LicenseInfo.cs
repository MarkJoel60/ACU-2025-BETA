// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.LicensingService.LicenseInfo
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
public class LicenseInfo
{
  private bool validField;
  private string errorField;
  private string signatureField;
  private string restrictionField;

  public bool Valid
  {
    get => this.validField;
    set => this.validField = value;
  }

  public string Error
  {
    get => this.errorField;
    set => this.errorField = value;
  }

  public string Signature
  {
    get => this.signatureField;
    set => this.signatureField = value;
  }

  public string Restriction
  {
    get => this.restrictionField;
    set => this.restrictionField = value;
  }
}
