// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Overrides.ScheduleProcess.BatchNew
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.GL.Overrides.ScheduleProcess;

[PXCacheName("GL Batch New")]
[Serializable]
public class BatchNew : Batch
{
  protected 
  #nullable disable
  string _RefBatchNbr;

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Module")]
  public override string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [AutoNumber(typeof (GLSetup.batchNumberingID), typeof (BatchNew.dateEntered))]
  [PXUIField(DisplayName = "Batch Number")]
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

  [PXDBDate]
  public override DateTime? DateEntered
  {
    get => this._DateEntered;
    set => this._DateEntered = value;
  }

  [PeriodID(null, null, null, true)]
  public override string TranPeriodID { get; set; }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BatchNew.module>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BatchNew.batchNbr>
  {
  }

  public abstract class refBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BatchNew.refBatchNbr>
  {
  }

  public new abstract class dateEntered : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  BatchNew.dateEntered>
  {
  }

  public new abstract class origBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BatchNew.origBatchNbr>
  {
  }

  public new abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BatchNew.origModule>
  {
  }

  public new abstract class tranPeriodID : IBqlField, IBqlOperand
  {
  }
}
