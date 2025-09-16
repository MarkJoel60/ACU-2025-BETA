// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ZoneZipPlusRecord
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

[DebuggerDisplay("{ZoneID}-{ZipCode} [{ZipMin}-{ZipMax}]")]
[PXHidden]
[Serializable]
public class ZoneZipPlusRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ZoneID;
  protected string _ZipCode;
  protected int? _ZipMin;
  protected int? _ZipMax;

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

  [PXInt(IsKey = true)]
  [PXDefault]
  public virtual int? ZipMin
  {
    get => this._ZipMin;
    set => this._ZipMin = value;
  }

  [PXInt]
  [PXDefault]
  public virtual int? ZipMax
  {
    get => this._ZipMax;
    set => this._ZipMax = value;
  }

  public abstract class zoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZoneZipPlusRecord.zoneID>
  {
  }

  public abstract class zipCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZoneZipPlusRecord.zipCode>
  {
  }

  public abstract class zipMin : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ZoneZipPlusRecord.zipMin>
  {
  }

  public abstract class zipMax : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ZoneZipPlusRecord.zipMax>
  {
  }
}
