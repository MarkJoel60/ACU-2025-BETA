// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.INTransferInTransitSO
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select4<INTransitLineStatusSO, Where<INTransitLineStatusSO.qtyOnHand, Greater<Zero>>, Aggregate<GroupBy<INTransitLineStatusSO.sOShipmentNbr, GroupBy<INTransitLineStatusSO.sOOrderType, GroupBy<INTransitLineStatusSO.sOOrderNbr, GroupBy<INTransitLineStatusSO.fromSiteID, GroupBy<INTransitLineStatusSO.toSiteID, GroupBy<INTransitLineStatusSO.origModule>>>>>>>>))]
[PXHidden]
[Serializable]
public class INTransferInTransitSO : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _SOShipmentNbr;
  protected string _SOOrderType;
  protected string _SOOrderNbr;
  protected Guid? _RefNoteID;

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (INTransitLineStatusSO.sOShipmentNbr))]
  public virtual string SOShipmentNbr
  {
    get => this._SOShipmentNbr;
    set => this._SOShipmentNbr = value;
  }

  [PXDBString(2, IsKey = true, BqlField = typeof (INTransitLineStatusSO.sOOrderType))]
  public virtual string SOOrderType
  {
    get => this._SOOrderType;
    set => this._SOOrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (INTransitLineStatusSO.sOOrderNbr))]
  public virtual string SOOrderNbr
  {
    get => this._SOOrderNbr;
    set => this._SOOrderNbr = value;
  }

  [PXNote(BqlField = typeof (INTransitLineStatusSO.refNoteID))]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [Site(DisplayName = "From Warehouse", DescriptionField = typeof (INSite.descr), BqlField = typeof (INTransitLineStatusSO.fromSiteID))]
  public virtual int? FromSiteID { get; set; }

  [ToSite(DisplayName = "To Warehouse", DescriptionField = typeof (INSite.descr), BqlField = typeof (INTransitLineStatusSO.toSiteID))]
  public virtual int? ToSiteID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (INTransitLineStatusSO.origModule))]
  public virtual string OrigModule { get; set; }

  /// <summary>The date of the transaction.</summary>
  [PXDBDate(BqlField = typeof (INTransitLineStatusSO.tranDate))]
  [PXUIField]
  public virtual DateTime? TranDate { get; set; }

  public abstract class sOShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransferInTransitSO.sOShipmentNbr>
  {
  }

  public abstract class sOOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransferInTransitSO.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransferInTransitSO.sOOrderNbr>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTransferInTransitSO.refNoteID>
  {
  }

  public abstract class fromSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransferInTransitSO.fromSiteID>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransferInTransitSO.toSiteID>
  {
  }

  public abstract class origModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransferInTransitSO.origModule>
  {
  }

  public abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTransferInTransitSO.tranDate>
  {
  }
}
