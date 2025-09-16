// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CA;

public sealed class CABankTranSplit : PXCacheExtension<
#nullable disable
CABankTran>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.bankTransactionSplits>();

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this bank transaction is splitted.
  /// That is, the bank transaction has been matched to an existing transaction in the system, or details of a new document that matches this transaction have been specified.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Split", Visible = true, Enabled = false, IsReadOnly = true)]
  public bool? Splitted { get; set; }

  /// <summary>
  /// The unique identifier of the CA bank transaction.
  /// This field is the key field.
  /// </summary>
  [PXDBInt]
  [PXSelector(typeof (Search<CABankTran.tranID, Where<CABankTran.tranID, NotEqual<Current<CABankTran.tranID>>>>))]
  [PXUIField(DisplayName = "ID", Visible = false)]
  public int? ParentTranID { get; set; }

  /// <summary>The balance type of the original bank transaction.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"D"</c>: Receipt,
  /// <c>"C"</c>: Disbursement
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [CADrCr.List]
  [PXUIField(DisplayName = "DrCr")]
  public string OrigDrCr { get; set; }

  /// <summary>
  /// The amount of the original bank transaction in the selected currency.
  /// </summary>
  [PXDBCury(typeof (CABankTran.curyID))]
  [PXUIField(DisplayName = "CuryOrigTranAmt")]
  public Decimal? CuryOrigTranAmt { get; set; }

  /// <summary>
  /// The amount of the original receipt in the selected currency.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField(DisplayName = "Orig. Receipt", Enabled = false, Visible = false)]
  public Decimal? CuryOrigDebitAmt
  {
    [PXDependsOnFields(new Type[] {typeof (CABankTran.drCr), typeof (CABankTranSplit.curyOrigTranAmt)})] get
    {
      Decimal? curyOrigDebitAmt;
      if (this.OrigDrCr == "D")
      {
        curyOrigDebitAmt = this.CuryOrigTranAmt;
        Decimal num = 0M;
        if (!(curyOrigDebitAmt.GetValueOrDefault() == num & curyOrigDebitAmt.HasValue))
          return this.CuryOrigTranAmt;
      }
      curyOrigDebitAmt = new Decimal?();
      return curyOrigDebitAmt;
    }
    set
    {
    }
  }

  /// <summary>
  /// The amount of the original disbursement in the selected currency.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField(DisplayName = "Orig. Disbursement", Enabled = false, Visible = false)]
  public Decimal? CuryOrigCreditAmt
  {
    [PXDependsOnFields(new Type[] {typeof (CABankTran.drCr), typeof (CABankTranSplit.curyOrigTranAmt)})] get
    {
      Decimal? curyOrigCreditAmt;
      if (this.OrigDrCr == "C")
      {
        curyOrigCreditAmt = this.CuryOrigTranAmt;
        Decimal num = 0M;
        if (!(curyOrigCreditAmt.GetValueOrDefault() == num & curyOrigCreditAmt.HasValue))
        {
          curyOrigCreditAmt = this.CuryOrigTranAmt;
          return !curyOrigCreditAmt.HasValue ? new Decimal?() : new Decimal?(-curyOrigCreditAmt.GetValueOrDefault());
        }
      }
      curyOrigCreditAmt = new Decimal?();
      return curyOrigCreditAmt;
    }
    set
    {
    }
  }

  /// <summary>
  /// The amount of the receipt in the selected currency.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField(DisplayName = "Orig. Receipt", Enabled = true)]
  public Decimal? CuryDisplayDebitAmt
  {
    [PXDependsOnFields(new Type[] {typeof (CABankTran.drCr), typeof (CABankTran.curyTranAmt), typeof (CABankTranSplit.origDrCr), typeof (CABankTranSplit.curyOrigTranAmt)})] get
    {
      return this.Splitted.GetValueOrDefault() ? (!(this.OrigDrCr == "D") ? new Decimal?(0M) : this.CuryOrigTranAmt) : (!(this.Base.DrCr == "D") ? new Decimal?(0M) : this.Base.CuryTranAmt);
    }
    set
    {
      if (this.Splitted.GetValueOrDefault())
      {
        Decimal? nullable = value;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        {
          this.CuryOrigTranAmt = value;
          this.OrigDrCr = "D";
        }
        else
        {
          if (!(this.Base.DrCr == "D"))
            return;
          this.CuryOrigTranAmt = new Decimal?(0M);
        }
      }
      else
      {
        Decimal? nullable = value;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        {
          this.Base.CuryTranAmt = value;
          this.Base.DrCr = "D";
        }
        else
        {
          if (!(this.Base.DrCr == "D"))
            return;
          this.Base.CuryTranAmt = new Decimal?(0M);
        }
      }
    }
  }

  /// <summary>
  /// The amount of the disbursement in the selected currency.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField(DisplayName = "Orig. Disbursement", Enabled = true)]
  public Decimal? CuryDisplayCreditAmt
  {
    [PXDependsOnFields(new Type[] {typeof (CABankTran.drCr), typeof (CABankTran.curyTranAmt), typeof (CABankTranSplit.origDrCr), typeof (CABankTranSplit.curyOrigTranAmt)})] get
    {
      if (this.Splitted.GetValueOrDefault())
      {
        if (!(this.OrigDrCr == "C"))
          return new Decimal?(0M);
        Decimal? curyOrigTranAmt = this.CuryOrigTranAmt;
        return !curyOrigTranAmt.HasValue ? new Decimal?() : new Decimal?(-curyOrigTranAmt.GetValueOrDefault());
      }
      if (!(this.Base.DrCr == "C"))
        return new Decimal?(0M);
      Decimal? curyTranAmt = this.Base.CuryTranAmt;
      return !curyTranAmt.HasValue ? new Decimal?() : new Decimal?(-curyTranAmt.GetValueOrDefault());
    }
    set
    {
      if (this.Splitted.GetValueOrDefault())
      {
        Decimal? nullable1 = value;
        Decimal num = 0M;
        if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
        {
          Decimal? nullable2 = value;
          this.CuryOrigTranAmt = nullable2.HasValue ? new Decimal?(-nullable2.GetValueOrDefault()) : new Decimal?();
          this.OrigDrCr = "C";
        }
        else
        {
          if (!(this.Base.DrCr == "C"))
            return;
          this.CuryOrigTranAmt = new Decimal?(0M);
        }
      }
      else
      {
        Decimal? nullable3 = value;
        Decimal num = 0M;
        if (!(nullable3.GetValueOrDefault() == num & nullable3.HasValue))
        {
          CABankTran caBankTran = this.Base;
          Decimal? nullable4 = value;
          Decimal? nullable5 = nullable4.HasValue ? new Decimal?(-nullable4.GetValueOrDefault()) : new Decimal?();
          caBankTran.CuryTranAmt = nullable5;
          this.Base.DrCr = "C";
        }
        else
        {
          if (!(this.Base.DrCr == "C"))
            return;
          this.Base.CuryTranAmt = new Decimal?(0M);
        }
      }
    }
  }

  [PXUIField(DisplayName = "Split", IsReadOnly = true, Visible = false)]
  [PXImage]
  public string SplittedIcon
  {
    [PXDependsOnFields(new Type[] {typeof (CABankTranSplit.splitted), typeof (CABankTranSplit.parentTranID)})] get
    {
      if (this.Splitted.GetValueOrDefault())
        return "~/Icons/parent_cc.svg";
      return this.ParentTranID.HasValue ? "~/Icons/subdirectory_arrow_right_cc.svg" : (string) null;
    }
    set
    {
    }
  }

  [PXDBInt(MinValue = 0)]
  public int? ChildsCount { get; set; }

  [PXDBInt(MinValue = 0)]
  public int? UnmatchedChilds { get; set; }

  [PXDBInt(MinValue = 0)]
  public int? UnprocessedChilds { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this bank transaction is processed.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<CABankTranSplit.unprocessedChilds, IBqlInt>.IsGreater<Zero>>, False>, CABankTran.processed>))]
  [PXUIField(DisplayName = "Processed", Visible = true, Enabled = false, IsReadOnly = true)]
  public bool? FullProcessed { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the payment and ready to be processed.
  /// That is, the bank transaction has been matched to an existing transaction in the system, or details of a new document that matches this transaction have been specified.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<CABankTranSplit.unmatchedChilds, IBqlInt>.IsGreater<Zero>>, False>, CABankTran.documentMatched>))]
  [PXUIField(DisplayName = "Matched", Visible = true, Enabled = false, IsReadOnly = true)]
  public bool? FullDocumentMatched { get; set; }

  public abstract class splitted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranSplit.splitted>
  {
  }

  public abstract class parentTranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranSplit.parentTranID>
  {
  }

  public abstract class origDrCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranSplit.origDrCr>
  {
  }

  public abstract class curyOrigTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranSplit.curyOrigTranAmt>
  {
  }

  public abstract class curyOrigDebitAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranSplit.curyOrigDebitAmt>
  {
  }

  public abstract class curyOrigCreditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranSplit.curyOrigCreditAmt>
  {
  }

  public abstract class curyDisplayDebitAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranSplit.curyDisplayDebitAmt>
  {
  }

  public abstract class curyDisplayCreditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranSplit.curyDisplayCreditAmt>
  {
  }

  public abstract class splittedIcon : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranSplit.splittedIcon>
  {
  }

  public abstract class childsCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranSplit.childsCount>
  {
  }

  public abstract class unmatchedChilds : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankTranSplit.unmatchedChilds>
  {
  }

  public abstract class unprocessedChilds : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankTranSplit.unprocessedChilds>
  {
  }

  public abstract class fullProcessed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranSplit.fullProcessed>
  {
  }

  public abstract class fullDocumentMatched : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CABankTranSplit.fullDocumentMatched>
  {
  }
}
