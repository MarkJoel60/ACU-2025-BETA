// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPriceWorksheet
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXPrimaryGraph(typeof (APPriceWorksheetMaint))]
[PXCacheName("AP Price Worksheet")]
[Serializable]
public class APPriceWorksheet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _RefNbr;
  protected string _Status;
  protected bool? _Hold;
  protected bool? _Approved;
  protected string _Descr;
  protected System.DateTime? _EffectiveDate;
  protected System.DateTime? _ExpirationDate;
  protected bool? _IsPromotional;
  protected bool? _OverwriteOverlapping;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
  [PXSelector(typeof (Search<APPriceWorksheet.refNbr>), new System.Type[] {typeof (APPriceWorksheet.refNbr), typeof (APPriceWorksheet.status), typeof (APPriceWorksheet.descr), typeof (APPriceWorksheet.effectiveDate)})]
  [APPriceWorksheet.refNbr.Numbering]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [SPWorksheetStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Hold", Visibility = PXUIVisibility.Visible)]
  [PXDefault(true)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Approved", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual bool? Approved
  {
    get => this._Approved;
    set => this._Approved = value;
  }

  [PXDBString(150, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDefault(typeof (AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.NullOrBlank)]
  [PXDBDate]
  [PXUIField(DisplayName = "Effective Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? EffectiveDate
  {
    get => this._EffectiveDate;
    set => this._EffectiveDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Expiration Date", Visibility = PXUIVisibility.Visible)]
  public virtual System.DateTime? ExpirationDate
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

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Overwrite Overlapping Prices")]
  public virtual bool? OverwriteOverlapping
  {
    get => this._OverwriteOverlapping;
    set => this._OverwriteOverlapping = value;
  }

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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<APPriceWorksheet>.By<APPriceWorksheet.refNbr>
  {
    public static APPriceWorksheet Find(PXGraph graph, string refNbr, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPriceWorksheet>.By<APPriceWorksheet.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPriceWorksheet.refNbr>
  {
    public class NumberingAttribute : AutoNumberAttribute
    {
      public NumberingAttribute()
        : base(typeof (APSetup.priceWSNumberingID), typeof (APPriceWorksheet.effectiveDate))
      {
      }
    }
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPriceWorksheet.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPriceWorksheet.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPriceWorksheet.approved>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPriceWorksheet.descr>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPriceWorksheet.effectiveDate>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPriceWorksheet.expirationDate>
  {
  }

  public abstract class isPromotional : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPriceWorksheet.isPromotional>
  {
  }

  public abstract class overwriteOverlapping : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPriceWorksheet.overwriteOverlapping>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APPriceWorksheet.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APPriceWorksheet.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APPriceWorksheet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPriceWorksheet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPriceWorksheet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APPriceWorksheet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPriceWorksheet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPriceWorksheet.lastModifiedDateTime>
  {
  }
}
