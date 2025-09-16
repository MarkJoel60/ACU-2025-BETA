// Decompiled with JetBrains decompiler
// Type: PX.SM.AUAuditFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class AUAuditFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(8, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Screen ID")]
  [PXSelector(typeof (Search<AUAuditSetup.screenID, Where<AUAuditSetup.isActive, Equal<PX.Data.True>>>), new System.Type[] {typeof (AUAuditSetup.virtualScreenID), typeof (AUAuditSetup.isActive), typeof (AUAuditSetup.description), typeof (AUAuditSetup.screenName)}, DescriptionField = typeof (AUAuditSetup.screenName))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Table Name")]
  [TableNameSelector(DescriptionField = typeof (AUAuditTable.tableDisplayName))]
  public virtual string TableName { get; set; }

  [PXGuid]
  [PXUIField(DisplayName = "User")]
  [PXSelector(typeof (Search<Users.pKID>), new System.Type[] {typeof (Users.username), typeof (Users.fullName)}, SubstituteKey = typeof (Users.username))]
  public virtual Guid? UserID { get; set; }

  [PXUIField(DisplayName = "Start Date")]
  [PXDateAndTime(UseTimeZone = true)]
  [PXDefault]
  public virtual System.DateTime? StartDate { get; set; }

  [PXUIField(DisplayName = "End Date")]
  [PXDateAndTime(UseTimeZone = true)]
  [PXDefault]
  public virtual System.DateTime? EndDate { get; set; }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditFilter.screenID>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditFilter.tableName>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUAuditFilter.userID>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  AUAuditFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  AUAuditFilter.endDate>
  {
  }
}
