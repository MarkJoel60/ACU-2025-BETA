// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UntypedService.LoginResult
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
namespace PX.Data.Update.UntypedService;

[GeneratedCode("System.Xml", "4.0.30319.233")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://www.acumatica.com/untyped/")]
[Serializable]
public class LoginResult
{
  private ErrorCode codeField;
  private string messageField;
  private string sessionField;

  public ErrorCode Code
  {
    get => this.codeField;
    set => this.codeField = value;
  }

  public string Message
  {
    get => this.messageField;
    set => this.messageField = value;
  }

  public string Session
  {
    get => this.sessionField;
    set => this.sessionField = value;
  }
}
