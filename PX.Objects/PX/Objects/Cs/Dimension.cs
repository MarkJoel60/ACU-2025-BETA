// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Dimension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Dimension")]
[PXPrimaryGraph(new System.Type[] {typeof (DimensionMaint)}, new System.Type[] {typeof (Select<Dimension, Where<Dimension.dimensionID, Equal<Current<Dimension.dimensionID>>>>)})]
[Serializable]
public class Dimension : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DimensionID;
  protected string _Descr;
  protected short? _Length;
  protected int? _maxLength;
  protected short? _Segments;
  protected bool? _Internal;
  protected string _NumberingID;
  protected string _LookupMode;
  protected bool? _Validate;
  protected string _SpecificModule;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<Dimension.dimensionID, Where<Dimension.dimensionID, InFieldClassActivated>>))]
  public virtual string DimensionID
  {
    get => this._DimensionID;
    set => this._DimensionID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField]
  public virtual short? Length
  {
    get => this._Length;
    set => this._Length = value;
  }

  [PXInt]
  [PXUIField(DisplayName = "Max Length", Enabled = false)]
  public virtual int? MaxLength
  {
    get => this._maxLength;
    set => this._maxLength = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField]
  public virtual short? Segments
  {
    get => this._Segments;
    set => this._Segments = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Internal
  {
    get => this._Internal;
    set => this._Internal = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Numbering.numberingID))]
  public virtual string NumberingID
  {
    get => this._NumberingID;
    set => this._NumberingID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("K0")]
  [DimensionLookupMode.List]
  [PXUIField(DisplayName = "Lookup Mode")]
  public virtual string LookupMode
  {
    get => this._LookupMode;
    set => this._LookupMode = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (IIf<Where<Dimension.lookupMode, Equal<DimensionLookupMode.bySegmentsAndAllAvailableSegmentValues>>, True, False>), KeepIdleSelfUpdates = true)]
  [PXUIField(DisplayName = "Allow Adding New Values On the Fly")]
  public virtual bool? Validate
  {
    get => this._Validate;
    set => this._Validate = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [RelationGroup.ModuleAll]
  [PXUIField(DisplayName = "Specific Module")]
  public virtual string SpecificModule
  {
    get => this._SpecificModule;
    set => this._SpecificModule = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Parent", Enabled = false)]
  [PXNavigateSelector(typeof (Dimension.dimensionID))]
  public virtual string ParentDimensionID { get; set; }

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

  public class PK : PrimaryKeyOf<Dimension>.By<Dimension.dimensionID>
  {
    public static Dimension Find(PXGraph graph, string dimensionID, PKFindOptions options = 0)
    {
      return (Dimension) PrimaryKeyOf<Dimension>.By<Dimension.dimensionID>.FindBy(graph, (object) dimensionID, options);
    }
  }

  public static class FK
  {
    public class ParentDimension : 
      PrimaryKeyOf<Dimension>.By<Dimension.dimensionID>.ForeignKeyOf<Dimension>.By<Dimension.parentDimensionID>
    {
    }
  }

  public abstract class dimensionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Dimension.dimensionID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Dimension.descr>
  {
  }

  public abstract class length : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Dimension.length>
  {
  }

  public abstract class maxLength : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Dimension.maxLength>
  {
  }

  public abstract class segments : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Dimension.segments>
  {
  }

  public abstract class @internal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Dimension.@internal>
  {
  }

  public abstract class numberingID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Dimension.numberingID>
  {
  }

  public abstract class lookupMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Dimension.lookupMode>
  {
  }

  public abstract class validate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Dimension.validate>
  {
  }

  public abstract class specificModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Dimension.specificModule>
  {
  }

  public abstract class parentDimensionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Dimension.parentDimensionID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Dimension.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Dimension.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Dimension.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Dimension.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Dimension.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Dimension.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Dimension.lastModifiedDateTime>
  {
  }
}
