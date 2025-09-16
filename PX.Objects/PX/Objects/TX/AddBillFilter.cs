// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.AddBillFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.TX;

[Serializable]
public class AddBillFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TaxID;
  protected int? _VendorID;
  protected string _InvoiceNbr;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<Tax.taxID, Where<Tax.directTax, Equal<True>>>))]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [VendorActive]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField]
  public virtual string InvoiceNbr
  {
    get => this._InvoiceNbr;
    set => this._InvoiceNbr = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Date From")]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Date To")]
  [PXDefault(typeof (APInvoice.docDate))]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddBillFilter.taxID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddBillFilter.vendorID>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddBillFilter.invoiceNbr>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  AddBillFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  AddBillFilter.endDate>
  {
  }
}
