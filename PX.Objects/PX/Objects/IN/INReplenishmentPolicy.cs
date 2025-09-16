// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReplenishmentPolicy
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXPrimaryGraph(typeof (INReplenishmentPolicyMaint))]
[PXCacheName]
[Serializable]
public class INReplenishmentPolicy : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReplenishmentPolicyID;
  protected string _Descr;
  protected string _CalendarID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDefault]
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField]
  [PXSelector(typeof (Search<INReplenishmentPolicy.replenishmentPolicyID>))]
  [PXFieldDescription]
  public virtual string ReplenishmentPolicyID
  {
    get => this._ReplenishmentPolicyID;
    set => this._ReplenishmentPolicyID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<CSCalendar.calendarID>), DescriptionField = typeof (CSCalendar.description))]
  public virtual string CalendarID
  {
    get => this._CalendarID;
    set => this._CalendarID = value;
  }

  [PXNote(DescriptionField = typeof (INReplenishmentPolicy.replenishmentPolicyID), Selector = typeof (Search<INReplenishmentPolicy.replenishmentPolicyID>), FieldList = new Type[] {typeof (INReplenishmentPolicy.replenishmentPolicyID), typeof (INReplenishmentPolicy.descr)})]
  public virtual Guid? NoteID { get; set; }

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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INReplenishmentPolicy>.By<INReplenishmentPolicy.replenishmentPolicyID>
  {
    public static INReplenishmentPolicy Find(
      PXGraph graph,
      string replenishmentPolicyID,
      PKFindOptions options = 0)
    {
      return (INReplenishmentPolicy) PrimaryKeyOf<INReplenishmentPolicy>.By<INReplenishmentPolicy.replenishmentPolicyID>.FindBy(graph, (object) replenishmentPolicyID, options);
    }
  }

  public static class FK
  {
    public class Calendar : 
      PrimaryKeyOf<CSCalendar>.By<CSCalendar.calendarID>.ForeignKeyOf<INReplenishmentPolicy>.By<INReplenishmentPolicy.calendarID>
    {
    }
  }

  public abstract class replenishmentPolicyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentPolicy.replenishmentPolicyID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentPolicy.descr>
  {
  }

  public abstract class calendarID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentPolicy.calendarID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INReplenishmentPolicy.noteID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INReplenishmentPolicy.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentPolicy.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentPolicy.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INReplenishmentPolicy.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentPolicy.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentPolicy.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INReplenishmentPolicy.Tstamp>
  {
  }
}
