// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ActiveSchedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class ActiveSchedule : FSSchedule
{
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FSSchedule.refNbr>))]
  public override 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = false)]
  [PXUIField(DisplayName = "Recurrence Description", Enabled = false)]
  public override string RecurrenceDescription { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Change Recurrence")]
  public virtual bool? ChangeRecurrence { get; set; }

  [PXDate]
  [PXUIField]
  [PXUIEnabled(typeof (Where<ActiveSchedule.changeRecurrence, Equal<True>>))]
  public virtual DateTime? EffectiveRecurrenceStartDate { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? NextExecution { get; set; }

  public abstract class changeRecurrence : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ActiveSchedule.changeRecurrence>
  {
  }

  public abstract class effectiveRecurrenceStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ActiveSchedule.effectiveRecurrenceStartDate>
  {
  }

  public abstract class nextExecution : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ActiveSchedule.nextExecution>
  {
  }
}
