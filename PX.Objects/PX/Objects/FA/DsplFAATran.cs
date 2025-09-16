// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DsplFAATran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class DsplFAATran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _TranID;
  protected Decimal? _GLTranQty;
  protected Decimal? _GLTranAmt;
  protected Decimal? _SelectedQty;
  protected Decimal? _SelectedAmt;
  protected Decimal? _OpenQty;
  protected Decimal? _OpenAmt;
  protected Decimal? _ClosedAmt;
  protected Decimal? _ClosedQty;
  protected Decimal? _UnitCost;
  protected bool? _Component = new bool?(false);
  protected int? _ClassID;

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? TranID
  {
    get => this._TranID;
    set => this._TranID = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Orig. Quantity", Enabled = false)]
  public virtual Decimal? GLTranQty
  {
    get => this._GLTranQty;
    set => this._GLTranQty = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Orig. Amount", Enabled = false)]
  public virtual Decimal? GLTranAmt
  {
    get => this._GLTranAmt;
    set => this._GLTranAmt = value;
  }

  [PXDBQuantity]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Selected Quantity")]
  public virtual Decimal? SelectedQty
  {
    get => this._SelectedQty;
    set => this._SelectedQty = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Selected Amount")]
  public virtual Decimal? SelectedAmt
  {
    get => this._SelectedAmt;
    set => this._SelectedAmt = value;
  }

  [PXQuantity]
  [PXFormula(typeof (Sub<DsplFAATran.gLTranQty, Add<DsplFAATran.closedQty, DsplFAATran.selectedQty>>))]
  [PXUIField(DisplayName = "Open Quantity", Enabled = false)]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  [PXBaseCury]
  [PXFormula(typeof (Sub<DsplFAATran.gLTranAmt, Add<DsplFAATran.closedAmt, DsplFAATran.selectedAmt>>))]
  [PXUIField(DisplayName = "Open Amount", Enabled = false)]
  public virtual Decimal? OpenAmt
  {
    get => this._OpenAmt;
    set => this._OpenAmt = value;
  }

  [PXDBBaseCury(null, null)]
  public virtual Decimal? ClosedAmt
  {
    get => this._ClosedAmt;
    set => this._ClosedAmt = value;
  }

  [PXDBQuantity]
  public virtual Decimal? ClosedQty
  {
    get => this._ClosedQty;
    set => this._ClosedQty = value;
  }

  [PXDBBaseCury(null, null)]
  [PXFormula(typeof (Switch<Case<Where<DsplFAATran.gLTranQty, LessEqual<decimal0>>, DsplFAATran.gLTranAmt>, Div<DsplFAATran.gLTranAmt, DsplFAATran.gLTranQty>>))]
  [PXUIField(DisplayName = "Unit Cost", Enabled = false)]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Reconciled")]
  [PXDefault(typeof (Search<FAAccrualTran.reconciled, Where<FAAccrualTran.tranID, Equal<Current<DsplFAATran.tranID>>>>))]
  public bool? Reconciled { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Component")]
  public virtual bool? Component
  {
    get => this._Component;
    set => this._Component = value;
  }

  [PXInt]
  [PXSelector(typeof (Search2<FixedAsset.assetID, LeftJoin<FABookSettings, On<FixedAsset.assetID, Equal<FABookSettings.assetID>>, LeftJoin<FABook, On<FABookSettings.bookID, Equal<FABook.bookID>>>>, Where<FixedAsset.recordType, Equal<FARecordType.classType>, And<FABook.updateGL, Equal<True>>>>), SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXUIField(DisplayName = "Asset Class")]
  public virtual int? ClassID
  {
    get => this._ClassID;
    set => this._ClassID = value;
  }

  public abstract class selected : BqlType<IBqlBool, bool>.Field<
  #nullable disable
  DsplFAATran.selected>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DsplFAATran.tranID>
  {
  }

  public abstract class gLTranQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DsplFAATran.gLTranQty>
  {
  }

  public abstract class gLTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DsplFAATran.gLTranAmt>
  {
  }

  public abstract class selectedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DsplFAATran.selectedQty>
  {
  }

  public abstract class selectedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DsplFAATran.selectedAmt>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DsplFAATran.openQty>
  {
  }

  public abstract class openAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DsplFAATran.openAmt>
  {
  }

  public abstract class closedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DsplFAATran.closedAmt>
  {
  }

  public abstract class closedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DsplFAATran.closedQty>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DsplFAATran.unitCost>
  {
  }

  public abstract class reconciled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DsplFAATran.reconciled>
  {
  }

  public abstract class component : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DsplFAATran.component>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DsplFAATran.classID>
  {
  }
}
