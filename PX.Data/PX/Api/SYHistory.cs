// Decompiled with JetBrains decompiler
// Type: PX.Api.SYHistory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (SYMapping.mappingID))]
  public Guid? MappingID { get; set; }

  [PXDBDateWithTicks(IsKey = true, PreserveTime = true, UseSmallDateTime = false, UseTimeZone = true, DisplayMask = "g", InputMask = "g")]
  [PXUIField(DisplayName = "Status Date", Enabled = false, Visible = false)]
  [PXDefault]
  public virtual System.DateTime? StatusDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [HistoryStatus.StringList]
  [PXDefault]
  public virtual 
  #nullable disable
  string Status { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Number of Records", Enabled = false)]
  [PXDefault]
  public virtual int? NbrRecords { get; set; }

  [PXDBString(4000, IsUnicode = true)]
  [PXUIField(DisplayName = "Version")]
  public virtual string ImportTimeStamp { get; set; }

  [PXDBDate(PreserveTime = true, UseTimeZone = false, UseSmallDateTime = false, DisplayMask = "g", InputMask = "g")]
  [PXUIField(DisplayName = "Version", Visible = false)]
  public virtual System.DateTime? ExportTimeStamp { get; set; }

  [PXDBDate(PreserveTime = true, UseTimeZone = false, UseSmallDateTime = false, DisplayMask = "g", InputMask = "g")]
  [PXUIField(DisplayName = "UTC Time Stamp")]
  public virtual System.DateTime? ExportTimeStampUtc { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string Description { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDependsOnFields(new System.Type[] {typeof (SYHistory.statusDate)})]
  [PXDate]
  [PXUIField(DisplayName = "Status Date", Enabled = false)]
  public virtual System.DateTime? StatusDateToDisplay
  {
    get => this.StatusDate;
    set
    {
    }
  }

  public abstract class mappingID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYHistory.mappingID>
  {
  }

  public abstract class statusDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  SYHistory.statusDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYHistory.status>
  {
  }

  public abstract class nbrRecords : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYHistory.nbrRecords>
  {
  }

  public abstract class importTimeStamp : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYHistory.importTimeStamp>
  {
  }

  public abstract class exportTimeStamp : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYHistory.exportTimeStamp>
  {
  }

  public abstract class exportTimeStampUtc : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYHistory.exportTimeStampUtc>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYHistory.description>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYHistory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYHistory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYHistory.createdDateTime>
  {
  }

  public abstract class statusDateToDisplay : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYHistory.statusDate>
  {
  }
}
