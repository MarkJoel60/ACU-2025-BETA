// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ZoneZipRecord
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.TX;

[DebuggerDisplay("{ZoneID}-{ZipCode}")]
[PXHidden]
[Serializable]
public class ZoneZipRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ZoneID;
  protected string _ZipCode;

  [PXString(10, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Zone ID")]
  public virtual string ZoneID
  {
    get => this._ZoneID;
    set => this._ZoneID = value;
  }

  [PXString(10, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Zip Code")]
  public virtual string ZipCode
  {
    get => this._ZipCode;
    set => this._ZipCode = value;
  }

  public abstract class zoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZoneZipRecord.zoneID>
  {
  }

  public abstract class zipCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZoneZipRecord.zipCode>
  {
  }
}
