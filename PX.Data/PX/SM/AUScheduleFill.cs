// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScheduleFill
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Schedule Filter Values")]
public class AUScheduleFill : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IHistoryFilter
{
  protected 
  #nullable disable
  string _ScreenID;
  protected int? _ScheduleID;
  protected short? _RowNbr;
  protected bool? _IsActive;
  protected string _FieldName;
  protected string _Value;
  protected bool? _IgnoreError;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault(typeof (AUSchedule.screenID), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (AUSchedule.scheduleID))]
  public virtual int? ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (Select<AUSchedule, Where<AUSchedule.screenID, Equal<Current<AUScheduleFill.screenID>>, And<AUSchedule.scheduleID, Equal<Current<AUScheduleFill.scheduleID>>>>>), LeaveChildren = true)]
  [PXLineNbr(typeof (AUSchedule))]
  public virtual short? RowNbr
  {
    get => this._RowNbr;
    set => this._RowNbr = value;
  }

  [Obsolete("This DAC field is obsolete and will be removed in future Acumatica versions")]
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active", Visible = false, Visibility = PXUIVisibility.Invisible)]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Field Name", Required = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  [PXCheckDacFieldNameUnique(Where = typeof (Where<AUScheduleFill.screenID, Equal<Current<AUScheduleFill.screenID>>, And<AUScheduleFill.scheduleID, Equal<Current<AUScheduleFill.scheduleID>>, And<AUScheduleFill.isActive, Equal<PX.Data.True>>>>), ClearOnDuplicate = false)]
  public virtual string FieldName
  {
    get => this._FieldName;
    set => this._FieldName = value;
  }

  [PXDBString(4000, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  public virtual string Value2 { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ignore Error")]
  public virtual bool? IgnoreError
  {
    get => this._IgnoreError;
    set => this._IgnoreError = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  public class PK : 
    PrimaryKeyOf<AUScheduleFill>.By<AUScheduleFill.screenID, AUScheduleFill.scheduleID, AUScheduleFill.rowNbr>
  {
    public static AUScheduleFill Find(
      PXGraph graph,
      int? scheduleID,
      short? rowNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<AUScheduleFill>.By<AUScheduleFill.screenID, AUScheduleFill.scheduleID, AUScheduleFill.rowNbr>.FindBy(graph, (object) scheduleID, (object) rowNbr, (object) options);
    }
  }

  public static class FK
  {
    public class SiteMap : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>.ForeignKeyOf<AUScheduleFilter>.By<AUScheduleFill.screenID>
    {
    }

    public class PortalMap : 
      PrimaryKeyOf<PortalMap>.By<PortalMap.screenID>.ForeignKeyOf<AUScheduleFilter>.By<AUScheduleFill.screenID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<AUSchedule>.By<AUSchedule.scheduleID>.ForeignKeyOf<AUScheduleFilter>.By<AUScheduleFill.scheduleID>
    {
    }
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScheduleFill.screenID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScheduleFill.scheduleID>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUScheduleFill.rowNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScheduleFill.isActive>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScheduleFill.fieldName>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScheduleFill.value>
  {
  }

  public abstract class ignoreError : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScheduleFill.ignoreError>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScheduleFill.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScheduleFill.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScheduleFill.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScheduleFill.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScheduleFill.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScheduleFill.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScheduleFill.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUScheduleFill.tStamp>
  {
  }
}
