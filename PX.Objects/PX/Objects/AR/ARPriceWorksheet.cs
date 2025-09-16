// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPriceWorksheet
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// An accounts receivable price worksheet. Price worksheets are used
/// to conveniently make mass changes to <see cref="T:PX.Objects.AR.ARSalesPrice">sales prices</see>.
/// Prices defined in the worksheet (<see cref="T:PX.Objects.AR.ARPriceWorksheetDetail" />) update sales
/// prices upon worksheet release. The entities of this type can be edited on the
/// Sales Price Worksheets (AR202010) form, which corresponds to the <see cref="T:PX.Objects.AR.ARPriceWorksheetMaint" /> graph.
/// </summary>
[PXPrimaryGraph(typeof (ARPriceWorksheetMaint))]
[PXCacheName("AR Price Worksheet")]
[Serializable]
public class ARPriceWorksheet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _RefNbr;
  protected string _Status;
  protected bool? _Hold;
  protected bool? _Approved;
  protected string _Descr;
  protected DateTime? _EffectiveDate;
  protected DateTime? _ExpirationDate;
  protected bool? _IsPromotional;
  protected bool? _OverwriteOverlapping;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<ARPriceWorksheet.refNbr>), new Type[] {typeof (ARPriceWorksheet.refNbr), typeof (ARPriceWorksheet.status), typeof (ARPriceWorksheet.descr), typeof (ARPriceWorksheet.effectiveDate)})]
  [ARPriceWorksheet.refNbr.Numbering]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField]
  [SPWorksheetStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Approved
  {
    get => this._Approved;
    set => this._Approved = value;
  }

  [PXDBString(150, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? EffectiveDate
  {
    get => this._EffectiveDate;
    set => this._EffectiveDate = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? ExpirationDate
  {
    get => this._ExpirationDate;
    set => this._ExpirationDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Promotional")]
  public virtual bool? IsPromotional
  {
    get => this._IsPromotional;
    set => this._IsPromotional = value;
  }

  /// <summary>
  /// Default value for the Skip Line Discounts check box in the lines.
  /// </summary>
  [PXDBBool]
  [PXFormula(typeof (ARPriceWorksheet.isPromotional))]
  [PXUIField(DisplayName = "Ignore Automatic Line Discounts")]
  [PXDefault]
  public virtual bool? SkipLineDiscounts { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the price
  /// will be used in revenue reallocaiton process
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsFairValue { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the price
  /// will be increased or decreased proportionaly the term
  /// specified on invoice line
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsProrated { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the discount can be applied
  /// to a fair value price (that is, when the <see cref="P:PX.Objects.AR.ARPriceWorksheet.IsFairValue" /> property is <see langword="true" />)
  /// in the revenue reallocation process in a deferral schedule.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Discountable { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Overwrite Overlapping Prices")]
  public virtual bool? OverwriteOverlapping
  {
    get => this._OverwriteOverlapping;
    set => this._OverwriteOverlapping = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("U")]
  [PriceTaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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

  public class PK : PrimaryKeyOf<ARPriceWorksheet>.By<ARPriceWorksheet.refNbr>
  {
    public static ARPriceWorksheet Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (ARPriceWorksheet) PrimaryKeyOf<ARPriceWorksheet>.By<ARPriceWorksheet.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPriceWorksheet.refNbr>
  {
    public class NumberingAttribute : AutoNumberAttribute
    {
      public NumberingAttribute()
        : base(typeof (ARSetup.priceWSNumberingID), typeof (ARPriceWorksheet.effectiveDate))
      {
      }
    }
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPriceWorksheet.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPriceWorksheet.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPriceWorksheet.approved>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPriceWorksheet.descr>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPriceWorksheet.effectiveDate>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPriceWorksheet.expirationDate>
  {
  }

  public abstract class isPromotional : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPriceWorksheet.isPromotional>
  {
  }

  public abstract class skipLineDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPriceWorksheet.skipLineDiscounts>
  {
  }

  public abstract class isFairValue : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPriceWorksheet.isFairValue>
  {
  }

  public abstract class isProrated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPriceWorksheet.isProrated>
  {
  }

  public abstract class discountable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPriceWorksheet.discountable>
  {
  }

  public abstract class overwriteOverlapping : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPriceWorksheet.overwriteOverlapping>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPriceWorksheet.taxCalcMode>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARPriceWorksheet.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARPriceWorksheet.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARPriceWorksheet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPriceWorksheet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPriceWorksheet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARPriceWorksheet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPriceWorksheet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPriceWorksheet.lastModifiedDateTime>
  {
  }
}
