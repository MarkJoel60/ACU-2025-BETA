// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTranR
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXProjection(typeof (Select2<GLTran, LeftJoin<PX.Objects.GL.ADL.Batch, On<GLTran.module, Equal<PX.Objects.GL.ADL.Batch.module>, And<GLTran.batchNbr, Equal<PX.Objects.GL.ADL.Batch.batchNbr>>>>>))]
[Serializable]
public class GLTranR : GLTran, ISignedBalances
{
  protected Decimal? _BegBalance;
  protected 
  #nullable disable
  string _CuryID;
  protected Decimal? _CuryBegBalance;
  protected string _Type;

  [Inventory(Visible = false)]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (Batch.dateEntered))]
  [PXUIField]
  public override DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault("", typeof (Search<GLTran.refNbr, Where<GLTran.module, Equal<Current<GLTran.module>>, And<GLTran.batchNbr, Equal<Current<GLTran.batchNbr>>>>>))]
  [PXUIField]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <inheritdoc cref="P:PX.Objects.GL.Batch.Description" />
  [PXString(512 /*0x0200*/)]
  [PXUIField]
  [PXDBCalced(typeof (PX.Objects.GL.ADL.Batch.description), typeof (string))]
  public virtual string BatchDescription { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault(typeof (Search<GLTran.tranDesc, Where<GLTran.module, Equal<Current<GLTran.module>>, And<GLTran.batchNbr, Equal<Current<GLTran.batchNbr>>>>>))]
  [PXUIField]
  public override string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [PXBaseCury(typeof (GLTranR.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? BegBalance
  {
    get => this._BegBalance;
    set => this._BegBalance = value;
  }

  [PXDBBaseCury(typeof (GLTranR.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? DebitAmt
  {
    get => this._DebitAmt;
    set => this._DebitAmt = value;
  }

  [PXDBBaseCury(typeof (GLTranR.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CreditAmt
  {
    get => this._CreditAmt;
    set => this._CreditAmt = value;
  }

  [PXDBCury(typeof (GLTranR.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryDebitAmt
  {
    get => this._CuryDebitAmt;
    set => this._CuryDebitAmt = value;
  }

  [PXDBCury(typeof (GLTranR.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryCreditAmt
  {
    get => this._CuryCreditAmt;
    set => this._CuryCreditAmt = value;
  }

  [SubAccount]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency")]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXBaseCury(typeof (GLTranR.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? EndBalance
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranR.begBalance), typeof (GLTranR.type), typeof (GLTranR.debitAmt), typeof (GLTranR.creditAmt)})] get
    {
      if (this.Type == null)
        return new Decimal?();
      Decimal? begBalance = this._BegBalance;
      Decimal num = AccountRules.CalcSaldo(this.Type, this.DebitAmt.GetValueOrDefault(), this.CreditAmt.GetValueOrDefault());
      return !begBalance.HasValue ? new Decimal?() : new Decimal?(begBalance.GetValueOrDefault() + num);
    }
  }

  [PXCury(typeof (GLTranR.curyID))]
  [PXUIField(DisplayName = "Curr. Beg. Balance", Visible = true)]
  public virtual Decimal? CuryBegBalance
  {
    get => this._CuryBegBalance;
    set => this._CuryBegBalance = value;
  }

  [PXCury(typeof (GLTranR.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Curr. Ending Balance", Visible = true)]
  public virtual Decimal? CuryEndBalance
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLTranR.curyBegBalance), typeof (GLTranR.type), typeof (GLTranR.curyDebitAmt), typeof (GLTranR.curyCreditAmt)})] get
    {
      if (this.Type == null)
        return new Decimal?();
      Decimal? curyBegBalance = this._CuryBegBalance;
      Decimal num = AccountRules.CalcSaldo(this.Type, this.CuryDebitAmt.GetValueOrDefault(), this.CuryCreditAmt.GetValueOrDefault());
      return !curyBegBalance.HasValue ? new Decimal?() : new Decimal?(curyBegBalance.GetValueOrDefault() + num);
    }
  }

  [PXBaseCury(typeof (GLTranR.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beg. Balance")]
  public virtual Decimal? SignBegBalance { get; set; }

  [PXBaseCury(typeof (GLTranR.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ending Balance")]
  public virtual Decimal? SignEndBalance { get; set; }

  [PXCury(typeof (GLTranR.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Curr. Beg. Balance")]
  public virtual Decimal? SignCuryBegBalance { get; set; }

  [PXCury(typeof (GLTranR.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Curr. Ending Balance")]
  public virtual Decimal? SignCuryEndBalance { get; set; }

  [PXString(1)]
  [PXFormula(typeof (Selector<GLTran.accountID, PX.Objects.GL.ADL.Account.type>))]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBInt]
  [PXDimensionSelector("BIZACCT", typeof (Search<BAccountR.bAccountID>), typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName), DirtyRead = true)]
  [PXUIField(DisplayName = "Business Account", Enabled = false, Visible = false)]
  public override int? ReferenceID
  {
    get => this._ReferenceID;
    set => this._ReferenceID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.PM.PMProject" />, that is related to the document.
  /// </summary>
  [GLProjectDefault(typeof (GLTran.ledgerID))]
  [ActiveProjectOrContractForGL(AccountFieldType = typeof (GLTranR.accountID), Visible = false, FieldClass = "PROJECT")]
  [PXForeignReference(typeof (Field<GLTranR.projectID>.IsRelatedTo<PMProject.contractID>))]
  public override int? ProjectID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.PM.PMTask" />, that is related to the document.
  /// </summary>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<GLTranR.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [PXForeignReference(typeof (CompositeKey<Field<GLTranR.projectID>.IsRelatedTo<PMTask.projectID>, Field<GLTranR.taskID>.IsRelatedTo<PMTask.taskID>>))]
  [BaseProjectTask(typeof (GLTran.projectID), "GL", DisplayName = "Project Task", AllowInactive = false, Visible = false)]
  public override int? TaskID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.PM.PMCostCode" />, that is related to the document.
  /// </summary>
  [CostCode(typeof (GLTranR.accountID), typeof (GLTranR.taskID), ReleasedField = typeof (GLTranR.released), Visible = false)]
  [PXForeignReference(typeof (Field<GLTranR.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public override int? CostCodeID { get; set; }

  [PXDBString(BqlField = typeof (Batch.batchType))]
  public virtual string BatchType { get; set; }

  public Decimal? OrigDebitAmt { get; set; }

  public Decimal? OrigCreditAmt { get; set; }

  public Decimal? CuryOrigDebitAmt { get; set; }

  public Decimal? CuryOrigCreditAmt { get; set; }

  public new abstract class selected : IBqlField, IBqlOperand
  {
  }

  public new abstract class branchID : IBqlField, IBqlOperand
  {
  }

  public new abstract class tranDate : IBqlField, IBqlOperand
  {
  }

  public new abstract class module : IBqlField, IBqlOperand
  {
  }

  public new abstract class refNbr : IBqlField, IBqlOperand
  {
  }

  public new abstract class lineNbr : IBqlField, IBqlOperand
  {
  }

  public new abstract class batchNbr : IBqlField, IBqlOperand
  {
  }

  public new abstract class debitAmt : IBqlField, IBqlOperand
  {
  }

  public new abstract class creditAmt : IBqlField, IBqlOperand
  {
  }

  public new abstract class curyDebitAmt : IBqlField, IBqlOperand
  {
  }

  public new abstract class curyCreditAmt : IBqlField, IBqlOperand
  {
  }

  public new abstract class curyReclassRemainingAmt : IBqlField, IBqlOperand
  {
  }

  public new abstract class subID : IBqlField, IBqlOperand
  {
  }

  public new abstract class tranDesc : IBqlField, IBqlOperand
  {
  }

  public new abstract class tranType : IBqlField, IBqlOperand
  {
  }

  public new abstract class ledgerID : IBqlField, IBqlOperand
  {
  }

  public new abstract class accountID : IBqlField, IBqlOperand
  {
  }

  public new abstract class finPeriodID : IBqlField, IBqlOperand
  {
  }

  public new abstract class tranPeriodID : IBqlField, IBqlOperand
  {
  }

  public new abstract class posted : IBqlField, IBqlOperand
  {
  }

  public new abstract class released : IBqlField, IBqlOperand
  {
  }

  public new abstract class reclassified : IBqlField, IBqlOperand
  {
  }

  public new abstract class isReclassReverse : IBqlField, IBqlOperand
  {
  }

  public new abstract class reclassOrigTranDate : IBqlField, IBqlOperand
  {
  }

  public new abstract class reclassBatchNbr : IBqlField, IBqlOperand
  {
  }

  public new abstract class noteID : IBqlField, IBqlOperand
  {
  }

  public abstract class batchDescription : IBqlField, IBqlOperand
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranR.inventoryID>
  {
  }

  public abstract class begBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranR.begBalance>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranR.curyID>
  {
  }

  public abstract class endBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranR.endBalance>
  {
  }

  public abstract class curyBegBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranR.curyBegBalance>
  {
  }

  public abstract class curyEndBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranR.curyEndBalance>
  {
  }

  public abstract class signBegBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranR.signBegBalance>
  {
  }

  public abstract class signEndBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranR.signEndBalance>
  {
  }

  public abstract class signCuryBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranR.signCuryBegBalance>
  {
  }

  public abstract class signCuryEndBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranR.signCuryEndBalance>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranR.type>
  {
  }

  public new abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranR.referenceID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranR.projectID>
  {
  }

  public new abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranR.taskID>
  {
  }

  public new abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranR.costCodeID>
  {
  }
}
