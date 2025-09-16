// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.CacheExtensions.PoOrderRsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.CN.Subcontracts.AP.CacheExtensions;

public sealed class PoOrderRsExt : PXCacheExtension<
#nullable disable
POOrderRS>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXDBString(BqlField = typeof (PX.Objects.PO.POOrder.orderNbr))]
  [PXUIField(DisplayName = "Subcontract Nbr.")]
  public string SubcontractNbr { get; set; }

  [PXDBCurrency(typeof (PX.Objects.PO.POOrder.curyInfoID), typeof (PX.Objects.PO.POOrder.orderTotal), BqlField = typeof (PX.Objects.PO.POOrder.curyOrderTotal))]
  [PXUIField(DisplayName = "Subcontract Total", Enabled = false)]
  public Decimal? SubcontractTotal { get; set; }

  [Obsolete]
  [PXString]
  [PXUIField(DisplayName = "Project")]
  public string ProjectCD { get; set; }

  [PXQuantity]
  [PXFormula(typeof (Sub<PX.Objects.PO.POOrder.orderQty, PX.Objects.PO.POOrder.unbilledOrderQty>))]
  [PXUIField(DisplayName = "Total Billed Qty.", Enabled = false)]
  public Decimal? SubcontractBilledQty { get; set; }

  [PXCurrency(typeof (PX.Objects.PO.POOrder.curyInfoID), typeof (PoOrderRsExt.subcontractBilledTotal))]
  [PXUIField(DisplayName = "Total Billed Amount", Enabled = false)]
  [PXFormula(typeof (Sub<PX.Objects.PO.POOrder.curyOrderTotal, PX.Objects.PO.POOrder.curyUnbilledOrderTotal>))]
  public Decimal? CurySubcontractBilledTotal { get; set; }

  [PXBaseCury]
  public Decimal? SubcontractBilledTotal { get; set; }

  public abstract class subcontractNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PoOrderRsExt.subcontractNbr>
  {
  }

  public abstract class subcontractTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PoOrderRsExt.subcontractTotal>
  {
  }

  [Obsolete]
  public abstract class projectCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PoOrderRsExt.subcontractNbr>
  {
  }

  public abstract class subcontractBilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PoOrderRsExt.subcontractBilledQty>
  {
  }

  public abstract class curySubcontractBilledTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PoOrderRsExt.curySubcontractBilledTotal>
  {
  }

  public abstract class subcontractBilledTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PoOrderRsExt.subcontractBilledTotal>
  {
  }
}
