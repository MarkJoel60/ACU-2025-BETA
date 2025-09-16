// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.CompleteNameType
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
public class CompleteNameType
{
  private string titleField;
  private string firstNameField;
  private string middleNameField;
  private string lastNameField;
  private string suffixField;
  private string initialsField;
  private string fullNameField;
  private string nicknameField;
  private string yomiFirstNameField;
  private string yomiLastNameField;

  /// <remarks />
  public string Title
  {
    get => this.titleField;
    set => this.titleField = value;
  }

  /// <remarks />
  public string FirstName
  {
    get => this.firstNameField;
    set => this.firstNameField = value;
  }

  /// <remarks />
  public string MiddleName
  {
    get => this.middleNameField;
    set => this.middleNameField = value;
  }

  /// <remarks />
  public string LastName
  {
    get => this.lastNameField;
    set => this.lastNameField = value;
  }

  /// <remarks />
  public string Suffix
  {
    get => this.suffixField;
    set => this.suffixField = value;
  }

  /// <remarks />
  public string Initials
  {
    get => this.initialsField;
    set => this.initialsField = value;
  }

  /// <remarks />
  public string FullName
  {
    get => this.fullNameField;
    set => this.fullNameField = value;
  }

  /// <remarks />
  public string Nickname
  {
    get => this.nicknameField;
    set => this.nicknameField = value;
  }

  /// <remarks />
  public string YomiFirstName
  {
    get => this.yomiFirstNameField;
    set => this.yomiFirstNameField = value;
  }

  /// <remarks />
  public string YomiLastName
  {
    get => this.yomiLastNameField;
    set => this.yomiLastNameField = value;
  }
}
