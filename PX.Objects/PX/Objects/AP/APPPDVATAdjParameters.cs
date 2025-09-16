// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPPDVATAdjParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// Pending Prompt Payment Discount (PPD) VAT Adjustment Parameters - Filter DAC
/// </summary>
[PXHidden]
[Serializable]
public class APPPDVATAdjParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected System.DateTime? _ApplicationDate;
  protected int? _BranchID;
  protected System.DateTime? _DebitAdjDate;
  protected 
  #nullable disable
  string _FinPeriodID;

  /// <summary>
  /// Application date, it is a filter field for selecting documents to the grid.
  /// </summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.Visible, Required = true)]
  public virtual System.DateTime? ApplicationDate
  {
    get => this._ApplicationDate;
    set => this._ApplicationDate = value;
  }

  /// <summary>
  /// Branch, it is a filter field for selecting documents to the grid.
  /// </summary>
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// Vendor, it is a filter field for selecting documents to the grid.
  /// Related to the <see cref="T:PX.Objects.AP.Vendor.bAccountID" />
  /// </summary>
  [Vendor]
  public virtual int? VendorID { get; set; }

  /// <summary>
  /// GenerateOnePerVendor, when this value is true then the processing Generate VAT Adj. will generate consolidated by Vendor adjustments.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Consolidate Tax Adjustments by Vendor", Visibility = PXUIVisibility.Visible)]
  public virtual bool? GenerateOnePerVendor { get; set; }

  /// <summary>
  /// VAT Adjustment date for the Consolidated VAT Adjustments generation
  /// </summary>
  [PXDBDate]
  [PXFormula(typeof (Switch<Case<Where<APPPDVATAdjParameters.generateOnePerVendor, Equal<True>>, Current<AccessInfo.businessDate>>, Null>))]
  [PXUIField(DisplayName = "Tax Adjustment Date", Visibility = PXUIVisibility.Visible)]
  public virtual System.DateTime? DebitAdjDate
  {
    get => this._DebitAdjDate;
    set => this._DebitAdjDate = value;
  }

  /// <summary>
  /// Fin. Period for the Consolidated VAT Adjustments generation
  /// </summary>
  [APOpenPeriod(typeof (APPPDVATAdjParameters.debitAdjDate), typeof (APPPDVATAdjParameters.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null)]
  [PXUIField(DisplayName = "Fin. Period", Visibility = PXUIVisibility.Visible)]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public abstract class applicationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPPDVATAdjParameters.applicationDate>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPPDVATAdjParameters.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPPDVATAdjParameters.vendorID>
  {
  }

  public abstract class generateOnePerVendor : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPPDVATAdjParameters.generateOnePerVendor>
  {
  }

  public abstract class debitAdjDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPPDVATAdjParameters.debitAdjDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPPDVATAdjParameters.finPeriodID>
  {
  }
}
