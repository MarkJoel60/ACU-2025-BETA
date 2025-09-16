// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POOrderPM
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("Purchase Order")]
[PXProjection(typeof (Select<PX.Objects.PO.POOrder>))]
[Serializable]
public class POOrderPM : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _VendorID;
  protected string _CuryID;

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.PO.POOrder.orderType))]
  [PXDefault]
  [PXUIField(DisplayName = "PO Type", Enabled = false)]
  [POOrderType.List]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.PO.POOrder.orderNbr))]
  [PXDefault]
  [PXUIField(DisplayName = "PO Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<POLinePM.orderType>>>>), DescriptionField = typeof (PX.Objects.PO.POOrder.orderDesc))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [Vendor(typeof (Search<BAccountR.bAccountID, Where<PX.Objects.AP.Vendor.type, NotEqual<BAccountType.employeeType>>>), BqlField = typeof (PX.Objects.PO.POOrder.vendorID), Enabled = false)]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (PX.Objects.PO.POOrder.curyID))]
  [PXUIField(DisplayName = "Currency")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPM.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPM.orderNbr>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrderPM.vendorID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPM.curyID>
  {
  }
}
