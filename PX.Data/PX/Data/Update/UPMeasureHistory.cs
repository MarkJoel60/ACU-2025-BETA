// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UPMeasureHistory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Update;

[Serializable]
public class UPMeasureHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Endpoint ID")]
  [PXDBString(16 /*0x10*/, IsKey = true)]
  public virtual 
  #nullable disable
  string EndpointID { get; set; }

  [PXUIField(DisplayName = "Measure ID", Enabled = false)]
  [PXDBIdentity(IsKey = true)]
  public virtual int? MeasureID { get; set; }

  [PXUIField(DisplayName = "Date", Enabled = false)]
  [PXDBDateAndTime]
  public virtual System.DateTime? Date { get; set; }

  [PXUIField(DisplayName = "Response Time, ms", Enabled = false)]
  [PXDBInt]
  public virtual int? NetworkTime { get; set; }

  [PXUIField(DisplayName = "Operation Time, ms", Enabled = false)]
  [PXDBInt]
  public virtual int? OperationTime { get; set; }

  [PXUIField(DisplayName = "User Count", Enabled = false)]
  [PXDBInt]
  public virtual int? UsersCount { get; set; }

  [PXUIField(DisplayName = "Date")]
  [PXDate]
  public virtual System.DateTime? DateOnly => new System.DateTime?(this.Date.Value.Date);

  [PXUIField(DisplayName = "Time")]
  [PXTimeSpan]
  public virtual int? TimeOnly => new int?((int) this.Date.Value.TimeOfDay.TotalMinutes);

  public abstract class endpointID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPMeasureHistory.endpointID>
  {
  }

  public abstract class measureID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPMeasureHistory.measureID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  UPMeasureHistory.date>
  {
  }

  public abstract class networkTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPMeasureHistory.networkTime>
  {
  }

  public abstract class operationTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPMeasureHistory.operationTime>
  {
  }

  public abstract class usersCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPMeasureHistory.usersCount>
  {
  }

  public abstract class dateOnly : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  UPMeasureHistory.dateOnly>
  {
  }

  public abstract class timeOnly : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPMeasureHistory.timeOnly>
  {
  }
}
