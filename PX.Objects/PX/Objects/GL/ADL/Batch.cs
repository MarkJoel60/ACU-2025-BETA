// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ADL.Batch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL.ADL;

public class Batch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _Module;
  protected string _BatchNbr;
  protected DateTime? _DateEntered;
  protected string _FinPeriodID;
  protected string _BatchType;
  protected bool? _Scheduled;
  protected bool? _Voided;

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  [BatchModule.List]
  [PXFieldDescription]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [BatchModule.Numbering]
  [PXFieldDescription]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  /// <inheritdoc cref="P:PX.Objects.GL.Batch.Description" />
  [PXUIField]
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  public virtual string Description { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DateEntered
  {
    get => this._DateEntered;
    set => this._DateEntered = value;
  }

  [OpenPeriod(typeof (Batch.dateEntered))]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBString(3)]
  [PXDefault("H")]
  [PXUIField(DisplayName = "Type")]
  [BatchTypeCode.List]
  public virtual string BatchType
  {
    get => this._BatchType;
    set => this._BatchType = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Scheduled
  {
    get => this._Scheduled;
    set => this._Scheduled = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Voided")]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  public class PK : PrimaryKeyOf<Batch>.By<Batch.module, Batch.batchNbr>
  {
    public static Batch Find(PXGraph graph, string module, string batchNbr, PKFindOptions options = 0)
    {
      return (Batch) PrimaryKeyOf<Batch>.By<Batch.module, Batch.batchNbr>.FindBy(graph, (object) module, (object) batchNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<Batch>.By<Batch.branchID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Batch.branchID>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.module>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.batchNbr>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.description>
  {
  }

  public abstract class dateEntered : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  Batch.dateEntered>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.finPeriodID>
  {
  }

  public abstract class batchType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Batch.batchType>
  {
  }

  public abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.scheduled>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Batch.voided>
  {
  }
}
