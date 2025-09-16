// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXPrimaryGraph(typeof (INLotSerClassMaint))]
[PXCacheName]
[Serializable]
public class INLotSerClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private const 
  #nullable disable
  string DfltLotSerialClass = "DEFAULT";
  protected string _LotSerClassID;
  protected string _Descr;
  protected string _LotSerTrack;
  protected string _LotSerAssign;
  protected string _LotSerIssueMethod;
  protected bool? _LotSerNumShared;
  protected string _LotSerFormatStr;
  protected bool? _LotSerTrackExpiration;
  protected bool? _AutoNextNbr;
  protected int? _AutoSerialMaxCount;
  protected bool? _RequiredForDropship;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  public static string GetDefaultLotSerClass(PXGraph graph)
  {
    INLotSerClass inLotSerClass = PXResultset<INLotSerClass>.op_Implicit(PXSelectBase<INLotSerClass, PXSelect<INLotSerClass, Where<INLotSerClass.lotSerTrack, Equal<INLotSerTrack.notNumbered>>>.Config>.Select(graph, Array.Empty<object>()));
    if (inLotSerClass != null)
      return inLotSerClass.LotSerClassID;
    PXCache<INLotSerClass> pxCache = GraphHelper.Caches<INLotSerClass>(graph);
    INLotSerClass instance = (INLotSerClass) ((PXCache) pxCache).CreateInstance();
    instance.LotSerClassID = "DEFAULT";
    instance.LotSerTrack = "N";
    ((PXCache) pxCache).Insert((object) instance);
    return instance.LotSerClassID;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<INLotSerClass.lotSerClassID>))]
  [PXFieldDescription]
  public virtual string LotSerClassID
  {
    get => this._LotSerClassID;
    set => this._LotSerClassID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField]
  [INLotSerTrack.List]
  public virtual string LotSerTrack
  {
    get => this._LotSerTrack;
    set => this._LotSerTrack = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("R")]
  [PXUIField]
  [PXUIEnabled(typeof (Where<INLotSerClass.lotSerTrack, In3<INLotSerTrack.lotNumbered, INLotSerTrack.serialNumbered>>))]
  [PXFormula(typeof (Switch<Case<Where<INLotSerClass.lotSerTrack, Equal<INLotSerTrack.notNumbered>>, INLotSerAssign.whenReceived>, INLotSerClass.lotSerAssign>))]
  [INLotSerAssign.List]
  public virtual string LotSerAssign
  {
    get => this._LotSerAssign;
    set => this._LotSerAssign = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("F")]
  [PXUIField]
  [PXUIEnabled(typeof (Where<INLotSerClass.lotSerTrack, In3<INLotSerTrack.lotNumbered, INLotSerTrack.serialNumbered>>))]
  [PXFormula(typeof (Switch<Case<Where<INLotSerClass.lotSerTrack, Equal<INLotSerTrack.notNumbered>>, INLotSerIssueMethod.fIFO>, INLotSerClass.lotSerIssueMethod>))]
  [INLotSerIssueMethod.List]
  public virtual string LotSerIssueMethod
  {
    get => this._LotSerIssueMethod;
    set => this._LotSerIssueMethod = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Share Auto-Incremental Value Between All Class Items")]
  public virtual bool? LotSerNumShared
  {
    get => this._LotSerNumShared;
    set => this._LotSerNumShared = value;
  }

  [PXDBString(60)]
  public virtual string LotSerFormatStr
  {
    get => this._LotSerFormatStr;
    set => this._LotSerFormatStr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Track Expiration Date")]
  [PXFormula(typeof (Switch<Case<Where<INLotSerClass.lotSerTrack, Equal<INLotSerTrack.notNumbered>>, False>, INLotSerClass.lotSerTrackExpiration>))]
  public virtual bool? LotSerTrackExpiration
  {
    get => this._LotSerTrackExpiration;
    set => this._LotSerTrackExpiration = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Auto-Generate Next Number")]
  public virtual bool? AutoNextNbr
  {
    get => this._AutoNextNbr;
    set => this._AutoNextNbr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Max. Auto-Generate Numbers")]
  public virtual int? AutoSerialMaxCount
  {
    get => this._AutoSerialMaxCount;
    set => this._AutoSerialMaxCount = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Required for Drop-ship")]
  public virtual bool? RequiredForDropship
  {
    get => this._RequiredForDropship;
    set => this._RequiredForDropship = value;
  }

  /// <summary>
  /// Lot/Serial number is not assigned automatically and requires user interaction.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Where<INLotSerClass.lotSerTrack, IsNotNull, And<INLotSerClass.lotSerTrack, NotEqual<INLotSerTrack.notNumbered>, And<Where<INLotSerClass.lotSerAssign, Equal<INLotSerAssign.whenReceived>, And<INLotSerClass.lotSerIssueMethod, Equal<INLotSerIssueMethod.userEnterable>, Or<INLotSerClass.lotSerAssign, Equal<INLotSerAssign.whenUsed>, And<INLotSerClass.autoNextNbr, NotEqual<True>>>>>>>>))]
  public virtual bool? IsManualAssignRequired { get; set; }

  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Specify Lot/Serial Price and Description", FieldClass = "LotSerialAttributes")]
  [PXUIEnabled(typeof (Where<INLotSerClass.lotSerAssign, Equal<INLotSerAssign.whenReceived>, And<INLotSerClass.lotSerTrack, NotEqual<INLotSerTrack.notNumbered>>>))]
  public virtual bool? UseLotSerSpecificDetails { get; set; }

  [PXNote(DescriptionField = typeof (INLotSerClass.lotSerClassID), Selector = typeof (INLotSerClass.lotSerClassID))]
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

  public class dfltLotSerialClass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INLotSerClass.dfltLotSerialClass>
  {
    public dfltLotSerialClass()
      : base("DEFAULT")
    {
    }
  }

  public class PK : PrimaryKeyOf<INLotSerClass>.By<INLotSerClass.lotSerClassID>
  {
    public static INLotSerClass Find(PXGraph graph, string lotSerClassID, PKFindOptions options = 0)
    {
      return (INLotSerClass) PrimaryKeyOf<INLotSerClass>.By<INLotSerClass.lotSerClassID>.FindBy(graph, (object) lotSerClassID, options);
    }

    internal static INLotSerClass FindDirty(PXGraph graph, string lotSerClassID)
    {
      return PXResultset<INLotSerClass>.op_Implicit(PXSelectBase<INLotSerClass, PXSelect<INLotSerClass, Where<INLotSerClass.lotSerClassID, Equal<Required<INLotSerClass.lotSerClassID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
      {
        (object) lotSerClassID
      }));
    }
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClass.lotSerClassID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INLotSerClass.descr>
  {
  }

  public abstract class lotSerTrack : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INLotSerClass.lotSerTrack>
  {
  }

  public abstract class lotSerAssign : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INLotSerClass.lotSerAssign>
  {
  }

  public abstract class lotSerIssueMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClass.lotSerIssueMethod>
  {
  }

  public abstract class lotSerNumShared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INLotSerClass.lotSerNumShared>
  {
  }

  public abstract class lotSerFormatStr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClass.lotSerFormatStr>
  {
  }

  public abstract class lotSerTrackExpiration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INLotSerClass.lotSerTrackExpiration>
  {
  }

  public abstract class autoNextNbr : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INLotSerClass.autoNextNbr>
  {
  }

  public abstract class autoSerialMaxCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLotSerClass.autoSerialMaxCount>
  {
  }

  public abstract class requiredForDropship : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INLotSerClass.requiredForDropship>
  {
  }

  public abstract class isManualAssignRequired : IBqlField, IBqlOperand
  {
  }

  public abstract class useLotSerSpecificDetails : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INLotSerClass.useLotSerSpecificDetails>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INLotSerClass.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INLotSerClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INLotSerClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerClass.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INLotSerClass.Tstamp>
  {
  }
}
