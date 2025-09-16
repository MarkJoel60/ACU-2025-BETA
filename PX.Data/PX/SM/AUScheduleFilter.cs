// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScheduleFilter
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

[PXCacheName("Schedule Filter")]
public class AUScheduleFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IHistoryFilter
{
  protected 
  #nullable disable
  string _ScreenID;
  protected int? _ScheduleID;
  protected short? _RowNbr;
  protected bool? _IsActive;
  protected int? _OpenBrackets;
  protected string _FieldName;
  protected int? _Condition;
  protected string _Value;
  protected string _Value2;
  protected int? _CloseBrackets;
  protected int? _Operator;
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
  [PXLineNbr(typeof (AUSchedule.filterCntr))]
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

  [PXDBInt]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5, 6, 7}, new string[] {"-", "(", "((", "(((", "((((", "(((((", "Activity Exists (", "Activity Not Exists ("})]
  [PXUIField(DisplayName = "Brackets")]
  [PXDefault(0)]
  public virtual int? OpenBrackets
  {
    get => this._OpenBrackets;
    set => this._OpenBrackets = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Field Name", Required = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string FieldName
  {
    get => this._FieldName;
    set => this._FieldName = value;
  }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Condition", Required = true)]
  [AUConditionType]
  public virtual int? Condition
  {
    get => this._Condition;
    set => this._Condition = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value 2")]
  public virtual string Value2
  {
    get => this._Value2;
    set => this._Value2 = value;
  }

  [PXDBInt]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5}, new string[] {"-", ")", "))", ")))", "))))", ")))))"})]
  [PXUIField(DisplayName = "Brackets")]
  [PXDefault(0)]
  public virtual int? CloseBrackets
  {
    get => this._CloseBrackets;
    set => this._CloseBrackets = value;
  }

  [PXDBInt]
  [PXIntList(new int[] {0, 1}, new string[] {"And", "Or"})]
  [PXUIField(FieldName = "Operator")]
  [PXDefault(0)]
  public virtual int? Operator
  {
    get => this._Operator;
    set => this._Operator = value;
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
    PrimaryKeyOf<AUScheduleFilter>.By<AUScheduleFilter.screenID, AUScheduleFilter.scheduleID, AUScheduleFilter.rowNbr>
  {
    public static AUScheduleFilter Find(
      PXGraph graph,
      int? scheduleID,
      short? rowNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<AUScheduleFilter>.By<AUScheduleFilter.screenID, AUScheduleFilter.scheduleID, AUScheduleFilter.rowNbr>.FindBy(graph, (object) scheduleID, (object) rowNbr, (object) options);
    }
  }

  public static class FK
  {
    public class SiteMap : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>.ForeignKeyOf<AUScheduleFilter>.By<AUScheduleFilter.screenID>
    {
    }

    public class PortalMap : 
      PrimaryKeyOf<PortalMap>.By<PortalMap.screenID>.ForeignKeyOf<AUScheduleFilter>.By<AUScheduleFilter.screenID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<AUSchedule>.By<AUSchedule.scheduleID>.ForeignKeyOf<AUScheduleFilter>.By<AUScheduleFilter.scheduleID>
    {
    }
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScheduleFilter.screenID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScheduleFilter.scheduleID>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUScheduleFilter.rowNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScheduleFilter.isActive>
  {
  }

  public abstract class openBrackets : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScheduleFilter.openBrackets>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScheduleFilter.fieldName>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScheduleFilter.condition>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScheduleFilter.value>
  {
  }

  public abstract class value2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScheduleFilter.value2>
  {
  }

  public abstract class closeBrackets : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScheduleFilter.closeBrackets>
  {
  }

  public abstract class operatoR : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScheduleFilter.operatoR>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScheduleFilter.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScheduleFilter.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScheduleFilter.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScheduleFilter.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScheduleFilter.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScheduleFilter.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScheduleFilter.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUScheduleFilter.tStamp>
  {
  }
}
