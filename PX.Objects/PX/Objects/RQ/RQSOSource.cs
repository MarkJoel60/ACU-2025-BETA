// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQSOSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXHidden]
[Serializable]
public class RQSOSource : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _CustomerID;
  protected int? _CustomerLocationID;
  protected int? _LineNbr;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected 
  #nullable disable
  string _UOM;
  protected Decimal? _OrderQty;
  protected bool? _IsUseMarkup;
  protected Decimal? _MarkupPct;
  protected Decimal? _EstUnitCost;
  protected Decimal? _CuryEstUnitCost;

  [Customer]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDefault(typeof (Search<Customer.defLocationID, Where<Customer.bAccountID, Equal<Current<RQRequisition.customerID>>>>))]
  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public virtual int? CustomerLocationID
  {
    get => this._CustomerLocationID;
    set => this._CustomerLocationID = value;
  }

  [PXDBInt(IsKey = true)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [RQRequisitionInventoryItem(Filterable = true)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(typeof (RQRequisitionLine.inventoryID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [INUnit(typeof (RQSOSource.inventoryID), DisplayName = "UOM")]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (RQRequisitionLine.uOM), typeof (RQRequisitionLine.baseOrderQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBBool]
  public virtual bool? IsUseMarkup
  {
    get => this._IsUseMarkup;
    set => this._IsUseMarkup = value;
  }

  [PXDBPriceCost]
  public virtual Decimal? MarkupPct
  {
    get => this._MarkupPct;
    set => this._MarkupPct = value;
  }

  [PXDBDecimal(6)]
  public virtual Decimal? EstUnitCost
  {
    get => this._EstUnitCost;
    set => this._EstUnitCost = value;
  }

  [PXDBDecimal(6)]
  public virtual Decimal? CuryEstUnitCost
  {
    get => this._CuryEstUnitCost;
    set => this._CuryEstUnitCost = value;
  }

  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQSOSource.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQSOSource.customerLocationID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQSOSource.lineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQSOSource.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQSOSource.subItemID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQSOSource.uOM>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQSOSource.orderQty>
  {
  }

  public abstract class isUseMarkup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQSOSource.isUseMarkup>
  {
  }

  public abstract class markupPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQSOSource.markupPct>
  {
  }

  public abstract class estUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQSOSource.estUnitCost>
  {
  }

  public abstract class curyEstUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQSOSource.curyEstUnitCost>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQSOSource.description>
  {
  }
}
