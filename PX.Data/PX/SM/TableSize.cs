// Decompiled with JetBrains decompiler
// Type: PX.SM.TableSize
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Table Size")]
[Serializable]
public class TableSize : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Table Name", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual 
  #nullable disable
  string TableName { get; set; }

  [PXUIField(DisplayName = "Size in DB", Enabled = false)]
  [PXDBLong]
  public virtual long? SizeByCompany { get; set; }

  [PXUIField(DisplayName = "Index Size in DB", Enabled = false)]
  [PXDBLong]
  public virtual long? IndexSizeByCompany { get; set; }

  [PXUIField(DisplayName = "Size in DB (including indexes)")]
  [PXDBCalced(typeof (Add<TableSize.sizeByCompany, TableSize.indexSizeByCompany>), typeof (long))]
  public virtual long? FullSizeByCompany { get; set; }

  [PXUIField(DisplayName = "Size in DB (MB)")]
  [PXDecimal(2)]
  public virtual Decimal? FullSizeByCompanyMB
  {
    get
    {
      return new Decimal?(this.FullSizeByCompany.HasValue ? Decimal.Round(Convert.ToDecimal(this.FullSizeByCompany.Value) / 1048576M, 2, MidpointRounding.AwayFromZero) : 0M);
    }
    set
    {
    }
  }

  [PXUIField(DisplayName = "Number of Records", Enabled = false)]
  [PXDBLong]
  public virtual long? CountOfCompanyRecords { get; set; }

  [PXUIField(DisplayName = "Real Size", Enabled = false)]
  [PXDBLong]
  public virtual long? RealSize { get; set; }

  [PXUIField(DisplayName = "CompanyId", Enabled = false)]
  [PXDBInt(IsKey = true)]
  public virtual int? Company { get; set; }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TableSize.tableName>
  {
  }

  public abstract class sizeByCompany : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  TableSize.sizeByCompany>
  {
  }

  public abstract class indexSizeByCompany : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    TableSize.indexSizeByCompany>
  {
  }

  public abstract class fullSizeByCompany : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    TableSize.fullSizeByCompany>
  {
  }

  public abstract class fullSizeByCompanyMB : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TableSize.fullSizeByCompanyMB>
  {
  }

  public abstract class countOfCompanyRecords : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    TableSize.countOfCompanyRecords>
  {
  }

  public abstract class realSize : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  TableSize.realSize>
  {
  }

  public abstract class company : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TableSize.company>
  {
  }
}
