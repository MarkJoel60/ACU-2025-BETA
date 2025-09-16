// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorBalanceSummaryByBaseCurrency
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXCacheName("Balance Summary by Base Currency")]
[Serializable]
public class VendorBalanceSummaryByBaseCurrency : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  [PXDefault]
  public virtual int? VendorID { get; set; }

  [PXDBString(5, IsKey = true, IsUnicode = true, BqlTable = typeof (PX.Objects.GL.Branch))]
  [PXUIField(DisplayName = "Currency")]
  public virtual 
  #nullable disable
  string BaseCuryID { get; set; }

  [CurySymbol(null, null, typeof (Vendor.baseCuryID), null, null, null, null, true, true)]
  [PXBaseCury(null, typeof (Vendor.baseCuryID))]
  [PXUIField(DisplayName = "Balance", Visible = true, Enabled = false)]
  public virtual Decimal? Balance { get; set; }

  [CurySymbol(null, null, typeof (Vendor.baseCuryID), null, null, null, null, true, true)]
  [PXBaseCury(null, typeof (Vendor.baseCuryID))]
  [PXUIField(DisplayName = "Prepayment Balance", Enabled = false)]
  public virtual Decimal? DepositsBalance { get; set; }

  [CurySymbol(null, null, typeof (Vendor.baseCuryID), null, null, null, null, true, true)]
  [PXBaseCury(null, typeof (Vendor.baseCuryID))]
  [PXUIField(DisplayName = "Retained Balance", Visibility = PXUIVisibility.Visible, Enabled = false, FieldClass = "Retainage")]
  public virtual Decimal? RetainageBalance { get; set; }

  public abstract class vendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorBalanceSummaryByBaseCurrency.vendorID>
  {
  }

  public abstract class baseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorBalanceSummaryByBaseCurrency.baseCuryID>
  {
  }

  public abstract class balance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    VendorBalanceSummaryByBaseCurrency.balance>
  {
  }

  public abstract class depositsBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    VendorBalanceSummaryByBaseCurrency.depositsBalance>
  {
  }

  public abstract class retainageBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    VendorBalanceSummaryByBaseCurrency.retainageBalance>
  {
  }
}
