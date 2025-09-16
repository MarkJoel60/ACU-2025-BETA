// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEquipmentTimeCardSpentTotals
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXProjection(typeof (Select4<EPEquipmentDetail, Aggregate<GroupBy<EPEquipmentDetail.timeCardCD, Sum<EPEquipmentDetail.setupTime, Sum<EPEquipmentDetail.runTime, Sum<EPEquipmentDetail.suspendTime>>>>>>))]
[PXHidden]
[Serializable]
public class EPEquipmentTimeCardSpentTotals : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RunTime;
  protected int? _SetupTime;
  protected int? _SuspendTime;

  [PXDBString(10, IsKey = true, BqlField = typeof (EPEquipmentDetail.timeCardCD))]
  public virtual 
  #nullable disable
  string TimeCardCD { get; set; }

  [PXDBInt(BqlField = typeof (EPEquipmentDetail.runTime))]
  [PXUIField(DisplayName = "Run Time")]
  public virtual int? RunTime
  {
    get => this._RunTime;
    set => this._RunTime = value;
  }

  [PXDBInt(BqlField = typeof (EPEquipmentDetail.setupTime))]
  [PXUIField(DisplayName = "Setup Time")]
  public virtual int? SetupTime
  {
    get => this._SetupTime;
    set => this._SetupTime = value;
  }

  [PXDBInt(BqlField = typeof (EPEquipmentDetail.suspendTime))]
  [PXUIField(DisplayName = "Suspend Time")]
  public virtual int? SuspendTime
  {
    get => this._SuspendTime;
    set => this._SuspendTime = value;
  }

  [PXInt]
  [PXUIField(DisplayName = "Total", Enabled = false)]
  public virtual int? TimeTotalCalc
  {
    get
    {
      int? nullable = this.RunTime;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.SetupTime;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int num = valueOrDefault1 + valueOrDefault2;
      nullable = this.SuspendTime;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      return new int?(num + valueOrDefault3);
    }
  }

  public abstract class timeCardCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipmentTimeCardSpentTotals.timeCardCD>
  {
  }

  public abstract class runTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipmentTimeCardSpentTotals.runTime>
  {
  }

  public abstract class setupTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentTimeCardSpentTotals.setupTime>
  {
  }

  public abstract class suspendTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentTimeCardSpentTotals.suspendTime>
  {
  }

  public abstract class timeTotalCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipmentTimeCardSpentTotals.timeTotalCalc>
  {
  }
}
