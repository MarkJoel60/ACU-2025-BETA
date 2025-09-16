// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSPCommnDocResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class ARSPCommnDocResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected string _AdjdDocType;
  protected string _AdjdRefNbr;
  protected int? _AdjNbr;
  protected Decimal? _CommnPct;
  protected Decimal? _CommnAmt;
  protected Decimal? _CommnblAmt;
  protected int? _CustomerID;
  protected int? _CustomerLocationID;
  protected Decimal? _OrigDocAmt;
  protected int? _BranchID;

  [PXDBString(3, IsKey = true)]
  [PXUIField(DisplayName = "Doc. Type")]
  [ARDocType.List]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Reference Nbr.")]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBString(3, IsKey = true)]
  [PXUIField(DisplayName = "Original Doc. Type")]
  [ARDocType.List]
  public virtual string AdjdDocType
  {
    get => this._AdjdDocType;
    set => this._AdjdDocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Original Ref. Nbr.")]
  public virtual string AdjdRefNbr
  {
    get => this._AdjdRefNbr;
    set => this._AdjdRefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  public virtual int? AdjNbr
  {
    get => this._AdjNbr;
    set => this._AdjNbr = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commission %")]
  public virtual Decimal? CommnPct
  {
    get => this._CommnPct;
    set => this._CommnPct = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commission Amount")]
  public virtual Decimal? CommnAmt
  {
    get => this._CommnAmt;
    set => this._CommnAmt = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commissionable Amount")]
  public virtual Decimal? CommnblAmt
  {
    get => this._CommnblAmt;
    set => this._CommnblAmt = value;
  }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Currency")]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.multipleBaseCurrencies>))]
  public virtual string BaseCuryID { get; set; }

  [Customer]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARSPCommnDocResult.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public virtual int? CustomerLocationID
  {
    get => this._CustomerLocationID;
    set => this._CustomerLocationID = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Doc. Amount")]
  public virtual Decimal? OrigDocAmt
  {
    get => this._OrigDocAmt;
    set => this._OrigDocAmt = value;
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

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSPCommnDocResult.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSPCommnDocResult.refNbr>
  {
  }

  public abstract class adjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSPCommnDocResult.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSPCommnDocResult.adjdRefNbr>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSPCommnDocResult.adjNbr>
  {
  }

  public abstract class commnPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARSPCommnDocResult.commnPct>
  {
  }

  public abstract class commnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARSPCommnDocResult.commnAmt>
  {
  }

  public abstract class commnblAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARSPCommnDocResult.commnblAmt>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSPCommnDocResult.baseCuryID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSPCommnDocResult.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARSPCommnDocResult.customerLocationID>
  {
  }

  public abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARSPCommnDocResult.origDocAmt>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSPCommnDocResult.branchID>
  {
  }
}
