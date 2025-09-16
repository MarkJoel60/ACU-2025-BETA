// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSContractGenerationHistory
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Contract Generation History")]
[PXPrimaryGraph(typeof (ContractGenerationHistoryMaint))]
[Serializable]
public class FSContractGenerationHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual int? ContractGenerationHistoryID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Generation ID")]
  public virtual int? GenerationID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "ScheduleID")]
  [PXParent(typeof (Select<FSSchedule, Where<FSSchedule.scheduleID, Equal<Current<FSContractGenerationHistory.scheduleID>>>>))]
  public virtual int? ScheduleID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_Schedule_EntityType.ListAtrribute]
  [PXUIField(DisplayName = "Entity Type")]
  public virtual 
  #nullable disable
  string EntityType { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Generated Element")]
  public virtual DateTime? LastGeneratedElementDate { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Up to Date")]
  public virtual DateTime? LastProcessedDate { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Previous Generated Element")]
  public virtual DateTime? PreviousGeneratedElementDate { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Previous Last Processed")]
  public virtual DateTime? PreviousProcessedDate { get; set; }

  [PXDBString(4, IsUnicode = false)]
  [PXDefault("NRSC")]
  [ListField_RecordType_ContractSchedule.ListAtrribute]
  public virtual string RecordType { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Generation Date")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<FSContractGenerationHistory>.By<FSContractGenerationHistory.contractGenerationHistoryID>
  {
    public static FSContractGenerationHistory Find(
      PXGraph graph,
      int? contractGenerationHistoryID,
      PKFindOptions options = 0)
    {
      return (FSContractGenerationHistory) PrimaryKeyOf<FSContractGenerationHistory>.By<FSContractGenerationHistory.contractGenerationHistoryID>.FindBy(graph, (object) contractGenerationHistoryID, options);
    }
  }

  public static class FK
  {
    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<FSContractGenerationHistory>.By<FSContractGenerationHistory.scheduleID>
    {
    }
  }

  public abstract class contractGenerationHistoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractGenerationHistory.contractGenerationHistoryID>
  {
  }

  public abstract class generationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractGenerationHistory.generationID>
  {
  }

  public abstract class scheduleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractGenerationHistory.scheduleID>
  {
  }

  public abstract class entityType : ListField_Schedule_EntityType
  {
  }

  public abstract class lastGeneratedElementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractGenerationHistory.lastGeneratedElementDate>
  {
  }

  public abstract class lastProcessedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractGenerationHistory.lastProcessedDate>
  {
  }

  public abstract class previousGeneratedElementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractGenerationHistory.previousGeneratedElementDate>
  {
  }

  public abstract class previousProcessedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractGenerationHistory.previousProcessedDate>
  {
  }

  public abstract class recordType : ListField_RecordType_ContractSchedule
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSContractGenerationHistory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractGenerationHistory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractGenerationHistory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSContractGenerationHistory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractGenerationHistory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractGenerationHistory.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    FSContractGenerationHistory.Tstamp>
  {
  }
}
