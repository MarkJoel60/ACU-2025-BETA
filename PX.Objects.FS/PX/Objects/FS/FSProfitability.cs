// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSProfitability
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSProfitability : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Ref. Nbr.")]
  public virtual 
  #nullable disable
  string LineRef { get; set; }

  [PXString(5, IsFixed = true)]
  [PXUIField(DisplayName = "Line Type")]
  [ListField_LineType_Profitability.ListAtrribute]
  [PXDefault]
  public virtual string LineType { get; set; }

  [InventoryIDByLineType(typeof (FSProfitability.lineType), null)]
  public virtual int? ItemID { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr { get; set; }

  [PXInt]
  [PXDefault]
  [FSSelector_StaffMember_ServiceOrderProjectID]
  [PXUIField(DisplayName = "Staff Member")]
  public virtual int? EmployeeID { get; set; }

  [PXLong]
  [CurrencyInfo(typeof (FSAppointment.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price")]
  public virtual Decimal? UnitPrice { get; set; }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (FSProfitability.curyInfoID), typeof (FSProfitability.unitPrice))]
  [PXUIField(DisplayName = "Unit Price")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitPrice { get; set; }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Estimated Quantity")]
  public virtual Decimal? EstimatedQty { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Estimated Amount")]
  public virtual Decimal? EstimatedAmount { get; set; }

  [PXCurrency(typeof (FSProfitability.curyInfoID), typeof (FSProfitability.estimatedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Estimated Amount")]
  public virtual Decimal? CuryEstimatedAmount { get; set; }

  [FSDBTimeSpanLongAllowNegative]
  [PXUIField(DisplayName = "Actual Duration")]
  public virtual int? ActualDuration { get; set; }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Actual Quantity")]
  public virtual Decimal? ActualQty { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Amount")]
  public virtual Decimal? ActualAmount { get; set; }

  [PXCurrency(typeof (FSProfitability.curyInfoID), typeof (FSProfitability.actualAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Actual Amount")]
  public virtual Decimal? CuryActualAmount { get; set; }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Billable Quantity")]
  public virtual Decimal? BillableQty { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billable Amount")]
  public virtual Decimal? BillableAmount { get; set; }

  [PXCurrency(typeof (FSProfitability.curyInfoID), typeof (FSProfitability.billableAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billable Amount")]
  public virtual Decimal? CuryBillableAmount { get; set; }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Cost")]
  public virtual Decimal? UnitCost { get; set; }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (FSProfitability.curyInfoID), typeof (FSProfitability.unitCost))]
  [PXUIField(DisplayName = "Unit Cost")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Estimated Cost")]
  public virtual Decimal? EstimatedCost { get; set; }

  [PXCurrency(typeof (FSProfitability.curyInfoID), typeof (FSProfitability.estimatedCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Estimated Cost")]
  public virtual Decimal? CuryEstimatedCost { get; set; }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Cost")]
  public virtual Decimal? ExtCost { get; set; }

  [PXCurrency(typeof (FSProfitability.curyInfoID), typeof (FSProfitability.extCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Cost")]
  public virtual Decimal? CuryExtCost { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Profit")]
  public virtual Decimal? Profit { get; set; }

  [PXCurrency(typeof (FSProfitability.curyInfoID), typeof (FSProfitability.profit))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Profit")]
  public virtual Decimal? CuryProfit { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Profit Markup (%)")]
  public virtual Decimal? ProfitPercent { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Profit Margin (%)")]
  public virtual Decimal? ProfitMarginPercent { get; set; }

  public FSProfitability()
  {
  }

  public FSProfitability(FSSODet fsSODet, string billingBy)
  {
    this.LineRef = fsSODet.LineRef;
    this.LineType = fsSODet.LineType;
    this.ItemID = fsSODet.InventoryID;
    this.Descr = fsSODet.TranDesc;
    this.EmployeeID = new int?();
    this.CuryInfoID = fsSODet.CuryInfoID;
    this.CuryUnitPrice = fsSODet.CuryUnitPrice;
    this.EstimatedQty = fsSODet.EstimatedQty;
    this.CuryEstimatedAmount = fsSODet.CuryEstimatedTranAmt;
    this.ActualDuration = this.LineType == "SLPRO" ? new int?(0) : fsSODet.ApptDuration;
    this.ActualQty = billingBy == "AP" ? fsSODet.ApptQty : fsSODet.EstimatedQty;
    this.CuryActualAmount = billingBy == "AP" ? fsSODet.CuryApptTranAmt : fsSODet.CuryEstimatedTranAmt;
    this.BillableQty = billingBy == "AP" ? fsSODet.ApptQty : fsSODet.BillableQty;
    this.CuryBillableAmount = billingBy == "AP" ? fsSODet.CuryApptTranAmt : fsSODet.CuryBillableTranAmt;
    this.CuryUnitCost = fsSODet.CuryUnitCost;
    Decimal? estimatedQty = this.EstimatedQty;
    Decimal? curyUnitCost1 = this.CuryUnitCost;
    this.CuryEstimatedCost = estimatedQty.HasValue & curyUnitCost1.HasValue ? new Decimal?(estimatedQty.GetValueOrDefault() * curyUnitCost1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable1;
    if (!fsSODet.IsLinkedItem && !(billingBy == "SO"))
    {
      Decimal? actualQty = this.ActualQty;
      Decimal? curyUnitCost2 = this.CuryUnitCost;
      nullable1 = actualQty.HasValue & curyUnitCost2.HasValue ? new Decimal?(actualQty.GetValueOrDefault() * curyUnitCost2.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable1 = fsSODet.CuryExtCost;
    this.CuryExtCost = nullable1;
    Decimal? nullable2 = this.CuryBillableAmount;
    Decimal num1 = Math.Round(nullable2.Value, 2);
    nullable2 = this.CuryExtCost;
    Decimal num2 = Math.Round(nullable2.Value, 2);
    this.CuryProfit = new Decimal?(num1 - num2);
    nullable2 = this.CuryExtCost;
    Decimal num3 = 0.0M;
    Decimal? nullable3;
    if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
    {
      Decimal? curyProfit = this.CuryProfit;
      Decimal? curyExtCost = this.CuryExtCost;
      nullable2 = curyProfit.HasValue & curyExtCost.HasValue ? new Decimal?(curyProfit.GetValueOrDefault() / curyExtCost.GetValueOrDefault()) : new Decimal?();
      num3 = (Decimal) 100;
      nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num3) : new Decimal?();
    }
    else
      nullable3 = new Decimal?(0.0M);
    this.ProfitPercent = nullable3;
    nullable2 = this.CuryBillableAmount;
    num3 = 0.0M;
    Decimal? nullable4;
    if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
    {
      Decimal? curyProfit = this.CuryProfit;
      Decimal? curyBillableAmount = this.CuryBillableAmount;
      nullable2 = curyProfit.HasValue & curyBillableAmount.HasValue ? new Decimal?(curyProfit.GetValueOrDefault() / curyBillableAmount.GetValueOrDefault()) : new Decimal?();
      num3 = (Decimal) 100;
      nullable4 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num3) : new Decimal?();
    }
    else
      nullable4 = new Decimal?(0.0M);
    this.ProfitMarginPercent = nullable4;
  }

  public FSProfitability(FSAppointmentDet fsAppointmentDetRow)
  {
    this.LineRef = fsAppointmentDetRow.LineRef;
    this.LineType = fsAppointmentDetRow.LineType;
    this.ItemID = fsAppointmentDetRow.InventoryID;
    this.Descr = fsAppointmentDetRow.TranDesc;
    this.CuryInfoID = fsAppointmentDetRow.CuryInfoID;
    this.CuryUnitPrice = fsAppointmentDetRow.CuryUnitPrice;
    this.EstimatedQty = fsAppointmentDetRow.EstimatedQty;
    this.CuryEstimatedAmount = fsAppointmentDetRow.CuryEstimatedTranAmt;
    this.ActualDuration = fsAppointmentDetRow.IsService ? fsAppointmentDetRow.ActualDuration : new int?(0);
    this.ActualQty = fsAppointmentDetRow.ActualQty;
    this.CuryActualAmount = fsAppointmentDetRow.CuryTranAmt;
    this.BillableQty = fsAppointmentDetRow.BillableQty;
    this.CuryBillableAmount = fsAppointmentDetRow.CuryBillableTranAmt;
    this.CuryUnitCost = fsAppointmentDetRow.CuryUnitCost;
    Decimal? estimatedQty = this.EstimatedQty;
    Decimal? curyUnitCost = this.CuryUnitCost;
    this.CuryEstimatedCost = estimatedQty.HasValue & curyUnitCost.HasValue ? new Decimal?(estimatedQty.GetValueOrDefault() * curyUnitCost.GetValueOrDefault()) : new Decimal?();
    this.CuryExtCost = fsAppointmentDetRow.CuryExtCost;
    Decimal? nullable1 = this.CuryBillableAmount;
    Decimal num1 = Math.Round(nullable1.Value, 2);
    nullable1 = this.CuryExtCost;
    Decimal num2 = Math.Round(nullable1.Value, 2);
    this.CuryProfit = new Decimal?(num1 - num2);
    nullable1 = this.CuryExtCost;
    Decimal num3 = 0.0M;
    Decimal? nullable2;
    if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue))
    {
      Decimal? curyProfit = this.CuryProfit;
      Decimal? curyExtCost = this.CuryExtCost;
      nullable1 = curyProfit.HasValue & curyExtCost.HasValue ? new Decimal?(curyProfit.GetValueOrDefault() / curyExtCost.GetValueOrDefault()) : new Decimal?();
      num3 = (Decimal) 100;
      nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num3) : new Decimal?();
    }
    else
      nullable2 = new Decimal?(0.0M);
    this.ProfitPercent = nullable2;
    nullable1 = this.CuryBillableAmount;
    num3 = 0.0M;
    Decimal? nullable3;
    if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue))
    {
      Decimal? curyProfit = this.CuryProfit;
      Decimal? curyBillableAmount = this.CuryBillableAmount;
      nullable1 = curyProfit.HasValue & curyBillableAmount.HasValue ? new Decimal?(curyProfit.GetValueOrDefault() / curyBillableAmount.GetValueOrDefault()) : new Decimal?();
      num3 = (Decimal) 100;
      nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num3) : new Decimal?();
    }
    else
      nullable3 = new Decimal?(0.0M);
    this.ProfitMarginPercent = nullable3;
  }

  public FSProfitability(FSLog fsLogRow)
  {
    this.LineRef = fsLogRow.LineRef;
    this.LineType = "LABOR";
    this.CuryInfoID = fsLogRow.CuryInfoID;
    this.ItemID = fsLogRow.LaborItemID;
    this.EmployeeID = fsLogRow.BAccountID;
    this.ActualDuration = fsLogRow.TimeDuration;
    this.CuryUnitCost = fsLogRow.CuryUnitCost;
    this.ActualQty = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(Decimal.Divide((Decimal) fsLogRow.TimeDuration.GetValueOrDefault(), 60M))));
    Decimal? actualQty = this.ActualQty;
    Decimal? curyUnitCost1 = this.CuryUnitCost;
    this.CuryActualAmount = actualQty.HasValue & curyUnitCost1.HasValue ? new Decimal?(actualQty.GetValueOrDefault() * curyUnitCost1.GetValueOrDefault()) : new Decimal?();
    this.CuryExtCost = fsLogRow.CuryExtCost;
    this.BillableQty = new Decimal?(0M);
    this.CuryBillableAmount = new Decimal?(0M);
    if (fsLogRow.IsBillable.GetValueOrDefault())
    {
      this.BillableQty = fsLogRow.BillableQty;
      Decimal? billableQty = this.BillableQty;
      Decimal? curyUnitCost2 = this.CuryUnitCost;
      this.CuryBillableAmount = billableQty.HasValue & curyUnitCost2.HasValue ? new Decimal?(billableQty.GetValueOrDefault() * curyUnitCost2.GetValueOrDefault()) : new Decimal?();
    }
    this.CuryProfit = new Decimal?(Math.Round(this.CuryBillableAmount.Value, 2) - Math.Round(this.CuryExtCost.Value, 2));
    Decimal? nullable1 = this.CuryExtCost;
    Decimal num = 0.0M;
    Decimal? nullable2;
    Decimal? nullable3;
    if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
    {
      Decimal? curyProfit = this.CuryProfit;
      nullable2 = this.CuryExtCost;
      nullable1 = curyProfit.HasValue & nullable2.HasValue ? new Decimal?(curyProfit.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?();
      num = (Decimal) 100;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new Decimal?(nullable1.GetValueOrDefault() * num);
    }
    else
      nullable3 = new Decimal?(0.0M);
    this.ProfitPercent = nullable3;
    nullable1 = this.CuryBillableAmount;
    num = 0.0M;
    Decimal? nullable4;
    if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
    {
      nullable2 = this.CuryProfit;
      Decimal? nullable5 = this.CuryBillableAmount;
      nullable1 = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / nullable5.GetValueOrDefault()) : new Decimal?();
      num = (Decimal) 100;
      if (!nullable1.HasValue)
      {
        nullable5 = new Decimal?();
        nullable4 = nullable5;
      }
      else
        nullable4 = new Decimal?(nullable1.GetValueOrDefault() * num);
    }
    else
      nullable4 = new Decimal?(0.0M);
    this.ProfitMarginPercent = nullable4;
  }

  public abstract class lineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSProfitability.lineRef>
  {
  }

  public abstract class lineType : ListField_LineType_Profitability
  {
  }

  public abstract class itemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSProfitability.itemID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSProfitability.descr>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSProfitability.employeeID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSProfitability.curyInfoID>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSProfitability.unitPrice>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.curyUnitPrice>
  {
  }

  public abstract class estimatedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.estimatedQty>
  {
  }

  public abstract class estimatedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.estimatedAmount>
  {
  }

  public abstract class curyEstimatedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.curyEstimatedAmount>
  {
  }

  public abstract class actualDuration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSProfitability.actualDuration>
  {
  }

  public abstract class actualQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSProfitability.actualQty>
  {
  }

  public abstract class actualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.actualAmount>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.curyActualAmount>
  {
  }

  public abstract class billableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.billableQty>
  {
  }

  public abstract class billableAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.billableAmount>
  {
  }

  public abstract class curyBillableAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.curyBillableAmount>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSProfitability.unitCost>
  {
  }

  public abstract class curyUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.curyUnitCost>
  {
  }

  public abstract class estimatedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.estimatedCost>
  {
  }

  public abstract class curyEstimatedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.curyEstimatedCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSProfitability.extCost>
  {
  }

  public abstract class curyExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.curyExtCost>
  {
  }

  public abstract class profit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSProfitability.profit>
  {
  }

  public abstract class curyProfit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSProfitability.curyProfit>
  {
  }

  public abstract class profitPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.profitPercent>
  {
  }

  public abstract class profitMarginPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSProfitability.profitMarginPercent>
  {
  }
}
