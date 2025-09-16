// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ZoneDetailRecord
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

[DebuggerDisplay("{ZoneID}-{TaxID}")]
[Serializable]
public class ZoneDetailRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ZoneID;
  protected string _TaxID;

  [PXString(10, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Zone ID")]
  public virtual string ZoneID
  {
    get => this._ZoneID;
    set => this._ZoneID = value;
  }

  [PXString(60, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax ID")]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  public abstract class zoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZoneDetailRecord.zoneID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZoneDetailRecord.taxID>
  {
  }
}
