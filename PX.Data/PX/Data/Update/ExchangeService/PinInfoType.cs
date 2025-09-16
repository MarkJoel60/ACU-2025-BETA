// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.PinInfoType
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
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class PinInfoType
{
  private string pINField;
  private bool isValidField;
  private bool pinExpiredField;
  private bool lockedOutField;
  private bool firstTimeUserField;

  /// <remarks />
  public string PIN
  {
    get => this.pINField;
    set => this.pINField = value;
  }

  /// <remarks />
  public bool IsValid
  {
    get => this.isValidField;
    set => this.isValidField = value;
  }

  /// <remarks />
  public bool PinExpired
  {
    get => this.pinExpiredField;
    set => this.pinExpiredField = value;
  }

  /// <remarks />
  public bool LockedOut
  {
    get => this.lockedOutField;
    set => this.lockedOutField = value;
  }

  /// <remarks />
  public bool FirstTimeUser
  {
    get => this.firstTimeUserField;
    set => this.firstTimeUserField = value;
  }
}
