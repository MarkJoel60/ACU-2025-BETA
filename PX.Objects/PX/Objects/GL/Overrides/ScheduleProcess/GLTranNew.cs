// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Overrides.ScheduleProcess.GLTranNew
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL.Overrides.ScheduleProcess;

[Serializable]
public class GLTranNew : GLTran
{
  protected 
  #nullable disable
  string _RefBatchNbr;

  [PXDBString(2, IsKey = true, IsFixed = true)]
  public override string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXParent(typeof (Select<BatchNew, Where<BatchNew.module, Equal<Current<GLTranNew.module>>, And<BatchNew.batchNbr, Equal<Current<GLTranNew.batchNbr>>>>>))]
  public override string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [PXString(15, IsUnicode = true)]
  public virtual string RefBatchNbr
  {
    get => this._RefBatchNbr;
    set => this._RefBatchNbr = value;
  }

  [PXDBInt]
  public override int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  [PXDBInt]
  public override int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXDBInt]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBLong]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  public override string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [PXDBDate]
  public override DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDefault]
  [PX.Objects.GL.FinPeriodID(null, typeof (GLTran.branchID), null, null, null, null, true, false, null, typeof (GLTran.tranPeriodID), typeof (BatchNew.tranPeriodID), true, true)]
  [PXUIField(DisplayName = "Period ID", Enabled = false, Visible = false)]
  public override string FinPeriodID { get; set; }

  [PXDBString(60, IsUnicode = true)]
  public override string TaxID
  {
    get => base.TaxID;
    set => base.TaxID = value;
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranNew.module>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranNew.batchNbr>
  {
  }

  public abstract class refBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranNew.refBatchNbr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTranNew.curyInfoID>
  {
  }

  public new abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GLTranNew.tranDate>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranNew.taxID>
  {
  }

  public new abstract class finPeriodID : IBqlField, IBqlOperand
  {
  }
}
