// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POTaxTranImported
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXVirtual]
[PXBreakInheritance]
[Serializable]
public class POTaxTranImported : POTaxTran
{
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDBDefault(typeof (POOrder.orderType))]
  [PXUIField(DisplayName = "Order Type", Enabled = false, Visible = false)]
  public override 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXDBDefault(typeof (POOrder.orderNbr))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false, Visible = false)]
  public override string OrderNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(2147483647 /*0x7FFFFFFF*/)]
  [PXUIField]
  [PXParent(typeof (POTaxTranImported.FK.Order))]
  public override int? LineNbr { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr), DirtyRead = true, ValidateValue = false)]
  public override string TaxID { get; set; }

  [PXDBLong]
  public override long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (POTaxTran.curyInfoID), typeof (POTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public new Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  public new static class FK
  {
    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POTaxTranImported>.By<POTaxTranImported.orderType, POTaxTranImported.orderNbr>
    {
    }
  }

  public new abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POTaxTranImported.orderType>
  {
  }

  public new abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POTaxTranImported.orderNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POTaxTranImported.lineNbr>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POTaxTranImported.taxID>
  {
  }

  public new abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POTaxTranImported.curyTaxableAmt>
  {
  }
}
