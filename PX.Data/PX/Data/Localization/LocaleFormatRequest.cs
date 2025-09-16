// Decompiled with JetBrains decompiler
// Type: PX.Data.Localization.LocaleFormatRequest
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Localization;

/// <exclude />
public class LocaleFormatRequest
{
  protected const string FORMAT = "{0}_{1}";
  protected readonly string _LocaleID;
  protected readonly Guid _UserID;
  protected readonly string _Key;

  public string LocaleID => this._LocaleID;

  public Guid UserID => this._UserID;

  public string Key => this._Key;

  public LocaleFormatRequest(string locale, Guid user)
  {
    this._LocaleID = locale;
    this._UserID = user;
    this._Key = $"{this._LocaleID}_{this._UserID}";
  }

  public override int GetHashCode() => this._Key.GetHashCode();

  public override string ToString() => this._Key;
}
