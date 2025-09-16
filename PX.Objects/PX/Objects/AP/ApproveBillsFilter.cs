// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.ApproveBillsFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AP;

[Serializable]
public class ApproveBillsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected System.DateTime? _SelectionDate;
  protected int? _VendorID;
  protected 
  #nullable disable
  string _VendorClassID;
  protected short? _PayInLessThan;
  protected bool? _ShowPayInLessThan;
  protected short? _DueInLessThan;
  protected bool? _ShowDueInLessThan;
  protected short? _DiscountExpiresInLessThan;
  protected bool? _ShowDiscountExpiresInLessThan;
  protected bool? _ShowApprovedForPayment;
  protected bool? _ShowNotApprovedForPayment;
  protected Decimal? _DocsTotal;
  protected long? _CuryInfoID;
  protected string _Days;

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Selection Date", Required = true, Visibility = PXUIVisibility.Visible)]
  public virtual System.DateTime? SelectionDate
  {
    get => this._SelectionDate;
    set => this._SelectionDate = value;
  }

  [Vendor(Visibility = PXUIVisibility.SelectorVisible, Required = false, DescriptionField = typeof (Vendor.acctName))]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (VendorClass.vendorClassID), DescriptionField = typeof (VendorClass.descr))]
  [PXUIField(DisplayName = "Vendor Class", Required = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string VendorClassID
  {
    get => this._VendorClassID;
    set => this._VendorClassID = value;
  }

  [PXDBShort]
  [PXUIField(Visibility = PXUIVisibility.Visible)]
  [PXDefault(typeof (IIf<Where<ApproveBillsFilter.showPayInLessThan, Equal<True>>, Current<APSetup.paymentLeadTime>, short0>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual short? PayInLessThan
  {
    get => this._PayInLessThan;
    set => this._PayInLessThan = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Pay Date Within", Visibility = PXUIVisibility.Visible)]
  public virtual bool? ShowPayInLessThan
  {
    get => this._ShowPayInLessThan;
    set => this._ShowPayInLessThan = value;
  }

  [PXDBShort]
  [PXUIField(Visibility = PXUIVisibility.Visible)]
  [PXDefault(typeof (IIf<Where<ApproveBillsFilter.showDueInLessThan, Equal<True>>, Current<APSetup.paymentLeadTime>, short0>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual short? DueInLessThan
  {
    get => this._DueInLessThan;
    set => this._DueInLessThan = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Due Date Within", Visibility = PXUIVisibility.Visible)]
  public virtual bool? ShowDueInLessThan
  {
    get => this._ShowDueInLessThan;
    set => this._ShowDueInLessThan = value;
  }

  [PXDBShort]
  [PXUIField(Visibility = PXUIVisibility.Visible)]
  [PXDefault(typeof (IIf<Where<ApproveBillsFilter.showDiscountExpiresInLessThan, Equal<True>>, Current<APSetup.paymentLeadTime>, short0>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual short? DiscountExpiresInLessThan
  {
    get => this._DiscountExpiresInLessThan;
    set => this._DiscountExpiresInLessThan = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cash Discount Expires Within", Visibility = PXUIVisibility.Visible)]
  public virtual bool? ShowDiscountExpiresInLessThan
  {
    get => this._ShowDiscountExpiresInLessThan;
    set => this._ShowDiscountExpiresInLessThan = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Show Approved for Payment", Visibility = PXUIVisibility.Visible)]
  public virtual bool? ShowApprovedForPayment
  {
    get => this._ShowApprovedForPayment;
    set => this._ShowApprovedForPayment = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Show Not Approved for Payment", Visibility = PXUIVisibility.Visible)]
  public virtual bool? ShowNotApprovedForPayment
  {
    get => this._ShowNotApprovedForPayment;
    set => this._ShowNotApprovedForPayment = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCury(typeof (ApproveBillsFilter.curyID))]
  [PXUIField(DisplayName = "Documents Total", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual Decimal? CuryDocsTotal { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCury(typeof (ApproveBillsFilter.curyID))]
  [PXUIField(DisplayName = "Approved for Payment", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual Decimal? CuryApprovedTotal { get; set; }

  [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  [PXDBDecimal(4)]
  public virtual Decimal? DocsTotal
  {
    get => this._DocsTotal;
    set => this._DocsTotal = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, Required = false)]
  [PXDefault]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID { get; set; }

  [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  [PXDBLong]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBString]
  [PXUIField]
  [PXDefault("Days", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string Days
  {
    get => this._Days;
    set => this._Days = value;
  }

  [PXBool]
  [PXUIField(Visibility = PXUIVisibility.Visible)]
  [PXDefault(true)]
  public virtual bool? PendingRefresh { get; set; }

  public abstract class selectionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    ApproveBillsFilter.selectionDate>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ApproveBillsFilter.vendorID>
  {
  }

  public abstract class vendorClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ApproveBillsFilter.vendorClassID>
  {
  }

  public abstract class payInLessThan : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    ApproveBillsFilter.payInLessThan>
  {
  }

  public abstract class showPayInLessThan : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ApproveBillsFilter.showPayInLessThan>
  {
  }

  public abstract class dueInLessThan : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    ApproveBillsFilter.dueInLessThan>
  {
  }

  public abstract class showDueInLessThan : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ApproveBillsFilter.showDueInLessThan>
  {
  }

  public abstract class discountExpiresInLessThan : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    ApproveBillsFilter.discountExpiresInLessThan>
  {
  }

  public abstract class showDiscountExpiresInLessThan : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ApproveBillsFilter.showDiscountExpiresInLessThan>
  {
  }

  public abstract class showApprovedForPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ApproveBillsFilter.showApprovedForPayment>
  {
  }

  public abstract class showNotApprovedForPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ApproveBillsFilter.showNotApprovedForPayment>
  {
  }

  public abstract class curyDocsTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ApproveBillsFilter.curyDocsTotal>
  {
  }

  public abstract class curyApprovedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ApproveBillsFilter.curyApprovedTotal>
  {
  }

  public abstract class docsTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ApproveBillsFilter.docsTotal>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ApproveBillsFilter.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ApproveBillsFilter.curyInfoID>
  {
  }

  public abstract class days : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ApproveBillsFilter.days>
  {
  }

  public abstract class pendingRefresh : IBqlField, IBqlOperand
  {
  }
}
