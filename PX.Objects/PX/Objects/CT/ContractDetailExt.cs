// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractDetailExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.CT;

[PXProjection(typeof (Select<ContractDetailExt>), Persistent = true)]
[PXBreakInheritance]
[PXHidden]
[Serializable]
public class ContractDetailExt : ContractDetail
{
  protected new int? _ContractDetailID;
  protected new int? _ContractID;
  protected new int? _LineNbr;
  protected new Decimal? _UsedTotal;

  [PXDBIdentity(IsKey = true)]
  public override int? ContractDetailID
  {
    get => this._ContractDetailID;
    set => this._ContractDetailID = value;
  }

  [PXDBInt]
  [PXLineNbr(typeof (Contract.lineCtr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public override int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(MinValue = 1, IsKey = true)]
  [PXDefault(typeof (Contract.revID))]
  public override int? RevID { get; set; }

  [PXQuantity]
  public override Decimal? LastQty { get; set; }

  [PXBool]
  [PXFormula(typeof (Selector<ContractDetailExt.contractItemID, ContractItem.deposit>))]
  public override bool? Deposit { get; set; }

  [PXUIField(DisplayName = "Included", Enabled = false)]
  [PXDecimal]
  [PXFormula(typeof (Switch<Case<Where<ContractDetailExt.deposit, Equal<True>>, ContractDetailExt.depositAmt>, ContractDetailExt.qty>))]
  public override Decimal? RecurringIncluded { get; set; }

  [PXUIField(DisplayName = "Used", Enabled = false)]
  [PXDecimal]
  [PXFormula(typeof (Switch<Case<Where<ContractDetailExt.deposit, Equal<True>>, ContractDetailExt.depositUsed>, ContractDetailExt.used>))]
  public override Decimal? RecurringUsed { get; set; }

  [PXUIField(DisplayName = "Used Total", Enabled = false)]
  [PXDecimal]
  [PXFormula(typeof (Switch<Case<Where<ContractDetailExt.deposit, Equal<True>>, ContractDetailExt.depositUsedTotal>, ContractDetailExt.usedTotal>))]
  public override Decimal? RecurringUsedTotal { get; set; }

  [PXDecimal]
  public override Decimal? LastBaseDiscountPct { get; set; }

  [PXDecimal]
  public override Decimal? LastRecurringDiscountPct { get; set; }

  [PXDecimal]
  public override Decimal? LastRenewalDiscountPct { get; set; }

  public new abstract class contractDetailID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    ContractDetailExt.contractDetailID>
  {
  }

  public new abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetailExt.contractID>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetailExt.lineNbr>
  {
  }

  public new abstract class revID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetailExt.revID>
  {
  }

  public new abstract class contractItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractDetailExt.contractItemID>
  {
  }

  public new abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetailExt.qty>
  {
  }

  public new abstract class used : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetailExt.used>
  {
  }

  public new abstract class usedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.usedTotal>
  {
  }

  public new abstract class lastQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetailExt.lastQty>
  {
  }

  public new abstract class deposit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractDetailExt.deposit>
  {
  }

  public new abstract class depositAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.depositAmt>
  {
  }

  public new abstract class depositUsed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.depositUsed>
  {
  }

  public new abstract class depositUsedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.depositUsedTotal>
  {
  }

  public new abstract class recurringIncluded : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.recurringIncluded>
  {
  }

  public new abstract class recurringUsed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.recurringUsed>
  {
  }

  public new abstract class recurringUsedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.recurringUsedTotal>
  {
  }

  public new abstract class baseDiscountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.baseDiscountPct>
  {
  }

  public new abstract class recurringDiscountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.recurringDiscountPct>
  {
  }

  public new abstract class renewalDiscountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.renewalDiscountPct>
  {
  }

  public new abstract class lastBaseDiscountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.lastBaseDiscountPct>
  {
  }

  public new abstract class lastRecurringDiscountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.lastRecurringDiscountPct>
  {
  }

  public new abstract class lastRenewalDiscountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailExt.lastRenewalDiscountPct>
  {
  }
}
