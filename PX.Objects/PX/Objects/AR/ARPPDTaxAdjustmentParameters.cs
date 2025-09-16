// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPPDTaxAdjustmentParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// Represents the processing parameters for the Generate AR Tax
/// Adjustments (AR504500) process, which corresponds to the <see cref="T:PX.Objects.AR.ARPPDCreditMemoProcess" /> graph.
/// </summary>
[PXCacheName("AR Tax Adjustment Parameters")]
[Serializable]
public class ARPPDTaxAdjustmentParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Application Date</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? ApplicationDate { get; set; }

  /// <summary>Branch ID</summary>
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  /// <summary>Customer ID</summary>
  [Customer]
  public virtual int? CustomerID { get; set; }

  /// <summary>Generate one document per customer</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? GenerateOnePerCustomer { get; set; }

  /// <summary>Tax adjustment date</summary>
  [PXDBDate]
  [PXFormula(typeof (Switch<Case<Where<ARPPDTaxAdjustmentParameters.generateOnePerCustomer, Equal<True>>, Current<AccessInfo.businessDate>>, Null>))]
  [PXUIField]
  public virtual DateTime? TaxAdjustmentDate { get; set; }

  /// <summary>Fin Period ID</summary>
  [AROpenPeriod(typeof (ARPPDTaxAdjustmentParameters.taxAdjustmentDate), typeof (ARPPDTaxAdjustmentParameters.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null)]
  [PXUIField]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  public abstract class applicationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPPDTaxAdjustmentParameters.applicationDate>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPPDTaxAdjustmentParameters.branchID>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARPPDTaxAdjustmentParameters.customerID>
  {
  }

  public abstract class generateOnePerCustomer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPPDTaxAdjustmentParameters.generateOnePerCustomer>
  {
  }

  public abstract class taxAdjustmentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPPDTaxAdjustmentParameters.taxAdjustmentDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPPDTaxAdjustmentParameters.finPeriodID>
  {
  }
}
