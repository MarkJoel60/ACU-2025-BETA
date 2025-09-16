// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSGenerationLogError
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

[PXCacheName("Generation Log Error")]
[Serializable]
public class FSGenerationLogError : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _ErrorDate;

  [PXDBIdentity(IsKey = true)]
  public virtual int? LogID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Generation ID")]
  public virtual int? GenerationID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Schedule ID")]
  public virtual int? ScheduleID { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Error Message")]
  public virtual 
  #nullable disable
  string ErrorMessage { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Generation Date")]
  [PXUIField]
  public virtual DateTime? ErrorDate
  {
    get => this._ErrorDate;
    set
    {
      this.ErrorDateUTC = value;
      this._ErrorDate = value;
    }
  }

  [PXDBString(4, IsFixed = true)]
  public virtual string ProcessType { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Ignore")]
  public virtual bool? Ignore { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "tstamp")]
  public virtual byte[] tstamp { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Generation Date")]
  [PXUIField]
  public virtual DateTime? ErrorDateUTC { get; set; }

  public class PK : PrimaryKeyOf<FSGenerationLogError>.By<FSGenerationLogError.logID>
  {
    public static FSGenerationLogError Find(PXGraph graph, int? logID, PKFindOptions options = 0)
    {
      return (FSGenerationLogError) PrimaryKeyOf<FSGenerationLogError>.By<FSGenerationLogError.logID>.FindBy(graph, (object) logID, options);
    }
  }

  public static class FK
  {
    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<FSGenerationLogError>.By<FSGenerationLogError.scheduleID>
    {
    }
  }

  public abstract class logID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSGenerationLogError.logID>
  {
  }

  public abstract class generationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSGenerationLogError.generationID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSGenerationLogError.scheduleID>
  {
  }

  public abstract class errorMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGenerationLogError.errorMessage>
  {
  }

  public abstract class errorDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSGenerationLogError.errorDate>
  {
  }

  public abstract class processType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGenerationLogError.processType>
  {
  }

  public abstract class ignore : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSGenerationLogError.ignore>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSGenerationLogError.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGenerationLogError.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSGenerationLogError.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSGenerationLogError.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGenerationLogError.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSGenerationLogError.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSGenerationLogError.Tstamp>
  {
  }

  public abstract class errorDateUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSGenerationLogError.errorDateUTC>
  {
  }
}
