// Decompiled with JetBrains decompiler
// Type: PX.Data.DeletedRecordsTracking.DAC.SMDeletedRecordsTrackingHistory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.DeletedRecordsTracking.DAC;

[PXHidden]
[PXInternalUseOnly]
public class SMDeletedRecordsTrackingHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? DeletedRecordsTrackingHistoryID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string TableName { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  public virtual string DacName { get; set; }

  [PXDefault]
  [PXDBGuid(false)]
  public virtual Guid? RefNoteID { get; set; }

  [PXDefault]
  [PXDBDate(UseTimeZone = true, PreserveTime = true)]
  public virtual System.DateTime? DeleteDate { get; set; }

  public abstract class deletedRecordsTrackingHistoryiD : 
    BqlType<IBqlGuid, Guid>.Field<SMDeletedRecordsTrackingHistory.deletedRecordsTrackingHistoryiD>
  {
  }

  public abstract class tableName : 
    BqlType<IBqlString, string>.Field<SMDeletedRecordsTrackingHistory.tableName>
  {
  }

  public abstract class dacName : 
    BqlType<IBqlString, string>.Field<SMDeletedRecordsTrackingHistory.dacName>
  {
  }

  public abstract class refNoteID : 
    BqlType<IBqlGuid, Guid>.Field<SMDeletedRecordsTrackingHistory.refNoteID>
  {
  }

  public abstract class deleteDate : 
    BqlType<IBqlDateTime, System.DateTime>.Field<SMDeletedRecordsTrackingHistory.deleteDate>
  {
  }
}
