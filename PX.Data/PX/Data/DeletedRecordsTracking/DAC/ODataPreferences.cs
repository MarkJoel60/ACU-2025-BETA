// Decompiled with JetBrains decompiler
// Type: PX.Data.DeletedRecordsTracking.DAC.ODataPreferences
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.DeletedRecordsTracking.DAC;

[PXCacheName("OData Preferences")]
public class ODataPreferences : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  internal const int DefaultDaysRetentionHistoryDeletedRecords = 10;

  [PXDBInt(MinValue = 0)]
  [PXDefault(10)]
  [PXUIField(DisplayName = "Days to Keep Records")]
  public int? DaysRetentionHistoryDeletedRecords { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  public abstract class daysRetentionHistoryDeletedRecords : 
    BqlType<IBqlInt, int>.Field<ODataPreferences.daysRetentionHistoryDeletedRecords>
  {
  }

  /// <exclude />
  public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<ODataPreferences.noteID>
  {
  }

  public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<ODataPreferences.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<ODataPreferences.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<ODataPreferences.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<IBqlGuid, Guid>.Field<ODataPreferences.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<ODataPreferences.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<ODataPreferences.lastModifiedDateTime>
  {
  }
}
