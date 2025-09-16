// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ManagementRoleType
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
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[XmlRoot("ManagementRole", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
[Serializable]
public class ManagementRoleType : SoapHeader
{
  private string[] userRolesField;
  private string[] applicationRolesField;

  /// <remarks />
  [XmlArrayItem("Role", IsNullable = false)]
  public string[] UserRoles
  {
    get => this.userRolesField;
    set => this.userRolesField = value;
  }

  /// <remarks />
  [XmlArrayItem("Role", IsNullable = false)]
  public string[] ApplicationRoles
  {
    get => this.applicationRolesField;
    set => this.applicationRolesField = value;
  }
}
