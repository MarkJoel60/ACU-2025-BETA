// Decompiled with JetBrains decompiler
// Type: PX.SM.AuditHistory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Process;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class AuditHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBLong(IsKey = true)]
  [PXUIField(DisplayName = "Batch ID", Visible = false)]
  public virtual long? BatchID { get; set; }

  [PXDBLong(IsKey = true)]
  [PXUIField(DisplayName = "Change ID", Visible = false)]
  public virtual long? ChangeID { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Screen Name", Visible = false)]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "User")]
  public virtual Guid? UserID { get; set; }

  [PXUIField(DisplayName = "Date and Time")]
  [PXDBDateAndTime(InputMask = "g", UseTimeZone = true, PreserveTime = true)]
  public virtual System.DateTime? ChangeDate { get; set; }

  [PXDBString(1)]
  [PXUIField(DisplayName = "Operation")]
  [PXAuditOperationsList]
  public virtual string Operation { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "TableName", Visible = false)]
  public virtual string TableName { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string CombinedKey { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string ModifiedFields { get; set; }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  AuditHistory.batchID>
  {
  }

  public abstract class changeID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  AuditHistory.changeID>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AuditHistory.screenID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AuditHistory.userID>
  {
  }

  public abstract class changeDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  AuditHistory.changeDate>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AuditHistory.operation>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AuditHistory.tableName>
  {
  }

  public abstract class combinedKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AuditHistory.combinedKey>
  {
  }

  public abstract class modifiedFields : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AuditHistory.modifiedFields>
  {
  }
}
