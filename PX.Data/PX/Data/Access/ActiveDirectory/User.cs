// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.User
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

[PXInternalUseOnly]
[Serializable]
public sealed class User
{
  private readonly string _sid;
  private readonly NameInfo _name;
  private readonly string _firstName;
  private readonly string _lastName;
  private readonly string _displayName;
  private readonly string _email;
  private readonly string _comment;
  private readonly System.DateTime _creationDate;
  private readonly System.DateTime _lastLogonDate;
  private readonly System.DateTime _lastPwdSetDate;
  private readonly Guid? _objectGUID;
  private readonly int? _source;

  public User(
    string sid,
    Guid? objectGUID,
    NameInfo name,
    string displayName,
    string firstName,
    string lastName,
    string email,
    string comment,
    System.DateTime creationDate,
    System.DateTime lastLogonDate,
    System.DateTime lastPwdSetDate)
  {
    this._sid = sid;
    this._name = name;
    this._displayName = displayName;
    this._firstName = firstName;
    this._lastName = lastName;
    this._email = email;
    this._comment = comment;
    this._creationDate = creationDate;
    this._lastLogonDate = lastLogonDate;
    this._lastPwdSetDate = lastPwdSetDate;
    this._objectGUID = objectGUID;
    this._source = new int?(1);
  }

  public User(
    string sid,
    Guid? objectGUID,
    NameInfo name,
    string displayName,
    string firstName,
    string lastName,
    string email)
  {
    this._sid = sid;
    this._name = name;
    this._displayName = displayName;
    this._firstName = firstName;
    this._lastName = lastName;
    this._email = email;
    this._creationDate = new System.DateTime(2000, 1, 1);
    this._lastLogonDate = new System.DateTime(2000, 1, 1);
    this._lastPwdSetDate = new System.DateTime(2000, 1, 1);
    this._objectGUID = objectGUID;
    this._source = new int?(2);
  }

  public string SID => this._sid;

  public Guid? ObjectGUID => this._objectGUID;

  public int? Source => this._source;

  public NameInfo Name => this._name;

  public string DisplayName => this._displayName;

  public string FirstName => this._firstName;

  public string LastName => this._lastName;

  public string Email => this._email;

  public string Comment => this._comment;

  public System.DateTime CreationDate => this._creationDate;

  public System.DateTime LastLogonDate => this._lastLogonDate;

  public System.DateTime LastPwdSetDate => this._lastPwdSetDate;
}
