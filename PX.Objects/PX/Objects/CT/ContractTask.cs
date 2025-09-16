// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractTask
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.DR;
using System;

#nullable enable
namespace PX.Objects.CT;

[PXHidden]
[Serializable]
public class ContractTask : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ContractID;
  protected int? _TaskID;
  protected 
  #nullable disable
  string _TaskCD;
  protected string _TaskDescr;
  protected string _DeferredCode;
  protected Decimal? _TaskProgress;
  protected string _RecognitionMethod;
  protected byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (Contract.contractID))]
  public virtual int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  [PXDBInt]
  [PXLineNbr(typeof (Contract))]
  [PXParent(typeof (Select<Contract, Where<Contract.contractID, Equal<Current<ContractTask.contractID>>>>), LeaveChildren = true)]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDefault]
  public virtual string TaskCD
  {
    get => this._TaskCD;
    set => this._TaskCD = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string TaskDescr
  {
    get => this._TaskDescr;
    set => this._TaskDescr = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID>))]
  [PXUIField(DisplayName = "Deferral Code")]
  public virtual string DeferredCode
  {
    get => this._DeferredCode;
    set => this._DeferredCode = value;
  }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Progress, (%%)")]
  public virtual Decimal? TaskProgress
  {
    get => this._TaskProgress;
    set => this._TaskProgress = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Recognition Method")]
  [PXDefault("C")]
  [PXStringList(new string[] {"P", "C"}, new string[] {"On Progress", "On Completion"})]
  public virtual string RecognitionMethod
  {
    get => this._RecognitionMethod;
    set => this._RecognitionMethod = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<ContractTask>.By<ContractTask.contractID, ContractTask.taskID>
  {
    public static ContractTask Find(
      PXGraph graph,
      int? contractID,
      int? taskID,
      PKFindOptions options = 0)
    {
      return (ContractTask) PrimaryKeyOf<ContractTask>.By<ContractTask.contractID, ContractTask.taskID>.FindBy(graph, (object) contractID, (object) taskID, options);
    }
  }

  public static class FK
  {
    public class Contract : 
      PrimaryKeyOf<Contract>.By<Contract.contractID>.ForeignKeyOf<ContractTask>.By<ContractTask.contractID>
    {
    }
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTask.contractID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractTask.taskID>
  {
  }

  public abstract class taskCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractTask.taskCD>
  {
  }

  public abstract class taskDescr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractTask.taskDescr>
  {
  }

  public abstract class deferredCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractTask.deferredCode>
  {
  }

  public abstract class taskProgress : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractTask.taskProgress>
  {
  }

  public abstract class recognitionMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractTask.recognitionMethod>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ContractTask.Tstamp>
  {
  }
}
