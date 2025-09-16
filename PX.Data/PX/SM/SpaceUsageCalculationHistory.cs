// Decompiled with JetBrains decompiler
// Type: PX.SM.SpaceUsageCalculationHistory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Space Usage Calculation History")]
[Serializable]
public class SpaceUsageCalculationHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _PkID;

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Guid? PkID
  {
    get => this._PkID;
    set => this._PkID = value;
  }

  [PXUIField(DisplayName = "Space Limit", Enabled = false)]
  [PXDBLong]
  public virtual long? QuotaSize { get; set; }

  [PXUIField(DisplayName = "By Tenants", Enabled = false)]
  [PXDBLong]
  public virtual long? UsedByCompanies { get; set; }

  [PXUIField(DisplayName = "By Snapshots", Enabled = false)]
  [PXDBLong]
  public virtual long? UsedBySnapshots { get; set; }

  [PXUIField(DisplayName = "Total", Enabled = false)]
  [PXLong]
  public virtual long? UsedTotal
  {
    get
    {
      long? usedByCompanies = this.UsedByCompanies;
      long? usedBySnapshots = this.UsedBySnapshots;
      return !(usedByCompanies.HasValue & usedBySnapshots.HasValue) ? new long?() : new long?(usedByCompanies.GetValueOrDefault() + usedBySnapshots.GetValueOrDefault());
    }
  }

  [PXUIField(DisplayName = "Free Space", Enabled = false)]
  [PXLong]
  public virtual long? FreeSpace
  {
    get
    {
      long? quotaSize = this.QuotaSize;
      long? nullable1 = this.UsedTotal;
      long? nullable2 = quotaSize.HasValue & nullable1.HasValue ? new long?(quotaSize.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new long?();
      nullable1 = nullable2;
      long num = 0;
      return nullable1.GetValueOrDefault() < num & nullable1.HasValue ? new long?(0L) : nullable2;
    }
  }

  [PXUIField(DisplayName = "Last Calculated", Enabled = false)]
  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = true)]
  [PXDefault]
  public virtual System.DateTime? CalculationDate { get; set; }

  [PXUIField(DisplayName = "Usage Status", Enabled = false)]
  [PXLong]
  public virtual long? CurrentStatus
  {
    get
    {
      long? quotaSize1 = this.QuotaSize;
      long num1 = 0;
      if (quotaSize1.GetValueOrDefault() <= num1 & quotaSize1.HasValue)
        return new long?(-1L);
      long num2 = 100;
      long? currentStatus = this.UsedTotal;
      long? nullable = currentStatus.HasValue ? new long?(num2 * currentStatus.GetValueOrDefault()) : new long?();
      long? quotaSize2 = this.QuotaSize;
      if (nullable.HasValue & quotaSize2.HasValue)
        return new long?(nullable.GetValueOrDefault() / quotaSize2.GetValueOrDefault());
      currentStatus = new long?();
      return currentStatus;
    }
  }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual 
  #nullable disable
  string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created DateTime")]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] TStamp { get; set; }

  public abstract class pkID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SpaceUsageCalculationHistory.pkID>
  {
  }

  public abstract class quotaSize : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.quotaSize>
  {
  }

  public abstract class usedByCompanies : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.usedByCompanies>
  {
  }

  public abstract class usedBySnapshots : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.usedBySnapshots>
  {
  }

  public abstract class usedTotal : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.usedTotal>
  {
  }

  public abstract class freeSpace : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.freeSpace>
  {
  }

  public abstract class calculationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.calculationDate>
  {
  }

  public abstract class currentStatus : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.currentStatus>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    SpaceUsageCalculationHistory.tStamp>
  {
  }
}
