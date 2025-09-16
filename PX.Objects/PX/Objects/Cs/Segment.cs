// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Segment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXPrimaryGraph(typeof (SegmentMaint))]
[PXCacheName("Segment")]
[Serializable]
public class Segment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DimensionID;
  protected short? _SegmentID;
  protected short? _Length;
  protected short? _Align;
  protected string _FillCharacter;
  protected string _PromptCharacter;
  protected string _EditMask;
  protected short? _CaseConvert;
  protected bool? _Validate;
  protected bool? _AutoNumber;
  protected string _Separator;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected short? _ConsolOrder;
  protected short? _ConsolNumChar;
  protected bool? _IsCosted;

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (Dimension.dimensionID))]
  [PXUIField]
  [PXSelector(typeof (Search<Dimension.dimensionID, Where<Dimension.dimensionID, InFieldClassActivated>>))]
  public virtual string DimensionID
  {
    get => this._DimensionID;
    set => this._DimensionID = value;
  }

  [PXDBShort(IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (Search<Segment.segmentID, Where<Segment.dimensionID, Equal<Current<Segment.dimensionID>>>>))]
  [PXParent(typeof (Select<Dimension, Where<Dimension.dimensionID, Equal<Current<Segment.dimensionID>>>>))]
  public virtual short? SegmentID
  {
    get => this._SegmentID;
    set => this._SegmentID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField]
  public virtual short? Length
  {
    get => this._Length;
    set => this._Length = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField]
  [PXIntList(new int[] {0, 1}, new string[] {"Left", "Right"})]
  public virtual short? Align
  {
    get => this._Align;
    set => this._Align = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(" ")]
  [PXUIField]
  [PXStringList(" ;Blanks,0;Zero")]
  public virtual string FillCharacter
  {
    get => this._FillCharacter;
    set => this._FillCharacter = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("_")]
  [PXUIField(DisplayName = "Prompt Character", Visible = false)]
  public virtual string PromptCharacter
  {
    get => this._PromptCharacter;
    set => this._PromptCharacter = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  [PXUIField]
  [SegmentEditMask]
  public virtual string EditMask
  {
    get => this._EditMask;
    set => this._EditMask = value;
  }

  [PXDBShort]
  [PXDefault(1)]
  [PXUIField]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"No Change", "Uppercase", "Lowercase"})]
  public virtual short? CaseConvert
  {
    get => this._CaseConvert;
    set => this._CaseConvert = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Validate
  {
    get => this._Validate;
    set => this._Validate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? AutoNumber
  {
    get => this._AutoNumber;
    set => this._AutoNumber = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("-")]
  [PXUIField]
  public virtual string Separator
  {
    get => this._Separator;
    set => this._Separator = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDefault(0)]
  [PXDBShort]
  [PXUIField(DisplayName = "Consol. Order", Visible = false)]
  public virtual short? ConsolOrder
  {
    get => this._ConsolOrder;
    set => this._ConsolOrder = value;
  }

  [PXDefault(0)]
  [PXDBShort]
  [PXUIField(DisplayName = "Number of characters", Visible = false)]
  public virtual short? ConsolNumChar
  {
    get => this._ConsolNumChar;
    set => this._ConsolNumChar = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include in Cost", Visible = false)]
  public virtual bool? IsCosted
  {
    get => this._IsCosted;
    set => this._IsCosted = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string ParentDimensionID { get; set; }

  [PXBool]
  public virtual bool? Inherited { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Override", Visible = false, IsReadOnly = true)]
  public virtual bool? IsOverrideForUI
  {
    [PXDependsOnFields(new Type[] {typeof (Segment.inherited)})] get
    {
      return new bool?(!this.Inherited.GetValueOrDefault());
    }
  }

  public class PK : PrimaryKeyOf<Segment>.By<Segment.dimensionID, Segment.segmentID>
  {
    public static Segment Find(
      PXGraph graph,
      string dimensionID,
      short? segmentID,
      PKFindOptions options = 0)
    {
      return (Segment) PrimaryKeyOf<Segment>.By<Segment.dimensionID, Segment.segmentID>.FindBy(graph, (object) dimensionID, (object) segmentID, options);
    }
  }

  public static class FK
  {
    public class Dimension : 
      PrimaryKeyOf<Dimension>.By<Dimension.dimensionID>.ForeignKeyOf<Segment>.By<Segment.dimensionID>
    {
    }
  }

  public abstract class dimensionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Segment.dimensionID>
  {
  }

  public abstract class segmentID : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Segment.segmentID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Segment.descr>
  {
  }

  public abstract class length : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Segment.length>
  {
  }

  public abstract class align : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Segment.align>
  {
  }

  public abstract class fillCharacter : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Segment.fillCharacter>
  {
  }

  public abstract class promptCharacter : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Segment.promptCharacter>
  {
  }

  public abstract class editMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Segment.editMask>
  {
  }

  public abstract class caseConvert : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Segment.caseConvert>
  {
  }

  public abstract class validate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Segment.validate>
  {
  }

  public abstract class autoNumber : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Segment.autoNumber>
  {
  }

  public abstract class separator : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Segment.separator>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Segment.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Segment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Segment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Segment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Segment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Segment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Segment.lastModifiedDateTime>
  {
  }

  public abstract class consolOrder : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Segment.consolOrder>
  {
  }

  public abstract class consolNumChar : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Segment.consolNumChar>
  {
  }

  public abstract class isCosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Segment.isCosted>
  {
  }

  public abstract class parentDimensionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Segment.parentDimensionID>
  {
  }

  public abstract class inherited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Segment.inherited>
  {
  }

  public abstract class isOverrideForUI : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Segment.isOverrideForUI>
  {
  }
}
