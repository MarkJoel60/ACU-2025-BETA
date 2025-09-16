// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.RetainageOptions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class RetainageOptions : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [AROpenPeriod(typeof (RetainageOptions.docDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null)]
  [PXDefault]
  [PXUIField]
  public virtual 
  #nullable disable
  string MasterFinPeriodID { get; set; }

  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (RetainageOptions.retainageTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainageTotal { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageTotal { get; set; }

  [DBRetainagePercent(typeof (True), typeof (decimal100), typeof (RetainageOptions.curyRetainageTotal), typeof (RetainageOptions.curyRetainageAmt), typeof (RetainageOptions.retainagePct), DisplayName = "Percent to Release")]
  public virtual Decimal? RetainagePct { get; set; }

  [DBRetainageAmount(typeof (ARInvoice.curyInfoID), typeof (RetainageOptions.curyRetainageTotal), typeof (RetainageOptions.curyRetainageAmt), typeof (RetainageOptions.retainageAmt), typeof (RetainageOptions.retainagePct), DisplayName = "Retainage to Release")]
  public virtual Decimal? CuryRetainageAmt { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageAmt { get; set; }

  [PXDBCurrency(typeof (ARInvoice.curyInfoID), typeof (RetainageOptions.retainageUnreleasedAmt))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Sub<RetainageOptions.curyRetainageTotal, RetainageOptions.curyRetainageAmt>))]
  public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageUnreleasedAmt { get; set; }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  RetainageOptions.docDate>
  {
  }

  public abstract class masterFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RetainageOptions.masterFinPeriodID>
  {
  }

  public abstract class curyRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RetainageOptions.curyRetainageTotal>
  {
  }

  public abstract class retainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RetainageOptions.retainageTotal>
  {
  }

  public abstract class retainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RetainageOptions.retainagePct>
  {
  }

  public abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RetainageOptions.curyRetainageAmt>
  {
  }

  public abstract class retainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RetainageOptions.retainageAmt>
  {
  }

  public abstract class curyRetainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RetainageOptions.curyRetainageUnreleasedAmt>
  {
  }

  public abstract class retainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RetainageOptions.retainageUnreleasedAmt>
  {
  }
}
