// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ZoneRecord
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

[DebuggerDisplay("{ZoneID}-{Description}")]
[Serializable]
public class ZoneRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ZoneID;
  protected string _Description;
  protected Decimal? _CombinedRate;

  [PXString(10, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Zone ID")]
  public virtual string ZoneID
  {
    get => this._ZoneID;
    set => this._ZoneID = value;
  }

  [PXString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDecimal(5)]
  [PXUIField(DisplayName = "Combined Rate")]
  public virtual Decimal? CombinedRate
  {
    get => this._CombinedRate;
    set => this._CombinedRate = value;
  }

  public abstract class zoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZoneRecord.zoneID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZoneRecord.description>
  {
  }

  public abstract class combinedRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZoneRecord.combinedRate>
  {
  }
}
