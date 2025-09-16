// Decompiled with JetBrains decompiler
// Type: PX.Data.DeletedRecordsTracking.DAC.SMDeletedRecordsTrackingTables
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.DeletedRecordsTracking.DAC;

[PXCacheName("Deleted Records Tracking Tables")]
public class SMDeletedRecordsTrackingTables : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Table")]
  [PXTablesSelectorForTrackingDeletion]
  public virtual string TableName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public string Description { get; set; }

  [PXDBDate(PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "Added On", Visible = true, Enabled = false)]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  public abstract class tableName : 
    BqlType<IBqlString, string>.Field<SMDeletedRecordsTrackingTables.tableName>
  {
  }

  public abstract class description : 
    BqlType<IBqlString, string>.Field<SMDeletedRecordsTrackingTables.description>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<SMDeletedRecordsTrackingTables.createdDateTime>
  {
  }

  public abstract class createdByID : 
    BqlType<IBqlGuid, Guid>.Field<SMDeletedRecordsTrackingTables.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<SMDeletedRecordsTrackingTables.createdByScreenID>
  {
  }
}
