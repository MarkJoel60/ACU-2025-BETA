// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Overrides.ScheduleMaint.BatchSelection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL.Overrides.ScheduleMaint;

[PXCacheName("Batch to Process")]
[Serializable]
public class BatchSelection : Batch
{
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault("GL")]
  [PXUIField]
  public override 
  #nullable disable
  string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.released, Equal<False>, And<Batch.hold, Equal<False>, And<Batch.voided, Equal<False>, And<Batch.rejected, NotEqual<True>, And<Batch.module, Equal<BatchModule.moduleGL>, And<Batch.batchType, NotEqual<BatchTypeCode.allocation>, And<Batch.batchType, NotEqual<BatchTypeCode.reclassification>, And<Batch.batchType, NotEqual<BatchTypeCode.trialBalance>>>>>>>>>>))]
  [PXUIField]
  public override string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXDBDefault(typeof (Schedule.scheduleID))]
  [PXParent(typeof (Select<Schedule, Where<Schedule.scheduleID, Equal<Current<Batch.scheduleID>>>>), LeaveChildren = true)]
  public override string ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [PXDBLong]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PX.Objects.GL.FinPeriodID(typeof (Batch.dateEntered), typeof (Batch.branchID), null, null, null, null, true, false, null, typeof (Batch.tranPeriodID), null, true, true, IsHeader = true)]
  [PXDefault]
  [PXUIField]
  public override string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BatchSelection.module>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BatchSelection.batchNbr>
  {
  }

  public new abstract class scheduleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BatchSelection.scheduleID>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  BatchSelection.curyInfoID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BatchSelection.finPeriodID>
  {
  }

  [PeriodID(null, null, null, true)]
  public new abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BatchSelection.tranPeriodID>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BatchSelection.released>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BatchSelection.hold>
  {
  }

  public new abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BatchSelection.scheduled>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BatchSelection.voided>
  {
  }

  public new abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BatchSelection.createdByID>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    BatchSelection.lastModifiedByID>
  {
  }
}
