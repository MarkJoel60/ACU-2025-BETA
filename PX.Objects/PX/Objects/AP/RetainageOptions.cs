// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.RetainageOptions
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
namespace PX.Objects.AP;

[Serializable]
public class RetainageOptions : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? DocDate { get; set; }

  [APOpenPeriod(typeof (RetainageOptions.docDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null)]
  [PXDefault]
  [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual 
  #nullable disable
  string MasterFinPeriodID { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Retainage Vendor Ref.", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string InvoiceNbr { get; set; }

  [PXDBCurrency(typeof (APRegister.curyInfoID), typeof (RetainageOptions.retainageTotal))]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryRetainageTotal { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? RetainageTotal { get; set; }

  [DBRetainagePercent(typeof (True), typeof (decimal100), typeof (RetainageOptions.curyRetainageTotal), typeof (RetainageOptions.curyRetainageAmt), typeof (RetainageOptions.retainagePct), DisplayName = "Percent to Release")]
  public virtual Decimal? RetainagePct { get; set; }

  [DBRetainageAmount(typeof (APInvoice.curyInfoID), typeof (RetainageOptions.curyRetainageTotal), typeof (RetainageOptions.curyRetainageAmt), typeof (RetainageOptions.retainageAmt), typeof (RetainageOptions.retainagePct), DisplayName = "Retainage to Release")]
  public virtual Decimal? CuryRetainageAmt { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? RetainageAmt { get; set; }

  [PXDBCurrency(typeof (APInvoice.curyInfoID), typeof (RetainageOptions.retainageUnreleasedAmt))]
  [PXUIField(DisplayName = "Unreleased Retainage", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXFormula(typeof (Sub<RetainageOptions.curyRetainageTotal, RetainageOptions.curyRetainageAmt>))]
  public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? RetainageUnreleasedAmt { get; set; }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
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

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RetainageOptions.invoiceNbr>
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
