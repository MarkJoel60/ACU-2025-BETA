// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CARegister
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

[Serializable]
public class CARegister : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected long? _TranID;
  protected bool? _Selected = new bool?(false);
  protected bool? _Hold = new bool?(false);
  protected bool? _Released = new bool?(false);
  protected 
  #nullable disable
  string _Module;
  protected string _TranType;
  protected string _ReferenceNbr;
  protected string _Description;
  protected DateTime? _DocDate;
  protected string _FinPeriodID;
  protected Guid? _NoteID;
  protected string _CuryID;
  protected Decimal? _TranAmt;
  protected Decimal? _CuryTranAmt;
  protected int? _BranchID;

  [PXLong(IsKey = true)]
  [PXUIField(DisplayName = "Transaction Num.")]
  public virtual long? TranID
  {
    get => this._TranID;
    set => this._TranID = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hold")]
  public bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released")]
  public bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXString(3)]
  [BatchModule.List]
  [PXUIField(DisplayName = "Module")]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXString(3)]
  [CAAPARTranType.ListByModule(typeof (CARegister.module))]
  [PXUIField(DisplayName = "Transaction Type")]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string ReferenceNbr
  {
    get => this._ReferenceNbr;
    set => this._ReferenceNbr = value;
  }

  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDate]
  [PXUIField(DisplayName = "Doc. Date")]
  public virtual DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDefault]
  [CashAccount(true, null, null)]
  public virtual int? CashAccountID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tran. Amount", Enabled = false)]
  public virtual Decimal? TranAmt
  {
    get => this._TranAmt;
    set => this._TranAmt = value;
  }

  [PXDBCury(typeof (CARecon.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Enabled = false)]
  public virtual Decimal? CuryTranAmt
  {
    get => this._CuryTranAmt;
    set => this._CuryTranAmt = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, to which the batch belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CARegister.tranID>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CARegister.selected>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CARegister.hold>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CARegister.released>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CARegister.module>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CARegister.tranType>
  {
  }

  public abstract class referenceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CARegister.referenceNbr>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CARegister.description>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CARegister.docDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CARegister.finPeriodID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CARegister.noteID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CARegister.cashAccountID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CARegister.curyID>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CARegister.tranAmt>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CARegister.curyTranAmt>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CARegister.branchID>
  {
  }
}
