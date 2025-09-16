// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARFinChargeTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// Represents an overdue charge detail associated with an overdue charge
/// invoice (see <see cref="T:PX.Objects.AR.ARInvoice" />). The entity encapsulates information
/// about the invoice to which a particular payment charge has been applied.
/// The records of this type are created during the Calculate Overdue Charges
/// (AR507000) processing, which corresponds to the <see cref="T:PX.Objects.AR.ARFinChargesApplyMaint" /> graph.
/// </summary>
[PXCacheName("AR Financial Charge Transaction")]
[Serializable]
public class ARFinChargeTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TranType;
  protected string _RefNbr;
  protected int? _LineNbr;
  protected string _OrigDocType;
  protected string _OrigRefNbr;
  protected int? _CustomerID;
  protected DateTime? _DocDate;
  protected string _FinChargeID;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (ARInvoice.docType))]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (ARInvoice.refNbr))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (Select<ARTran, Where<ARTran.tranType, Equal<Current<ARFinChargeTran.tranType>>, And<ARTran.refNbr, Equal<Current<ARFinChargeTran.refNbr>>, And<ARTran.lineNbr, Equal<Current<ARFinChargeTran.lineNbr>>>>>>))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("")]
  public virtual string OrigDocType
  {
    get => this._OrigDocType;
    set => this._OrigDocType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault("")]
  public virtual string OrigRefNbr
  {
    get => this._OrigRefNbr;
    set => this._OrigRefNbr = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (ARRegister.customerID))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (ARRegister.docDate))]
  public virtual DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  public virtual string FinChargeID
  {
    get => this._FinChargeID;
    set => this._FinChargeID = value;
  }

  public class PK : 
    PrimaryKeyOf<ARFinChargeTran>.By<ARFinChargeTran.tranType, ARFinChargeTran.refNbr, ARFinChargeTran.lineNbr>
  {
    public static ARFinChargeTran Find(
      PXGraph graph,
      string tranType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (ARFinChargeTran) PrimaryKeyOf<ARFinChargeTran>.By<ARFinChargeTran.tranType, ARFinChargeTran.refNbr, ARFinChargeTran.lineNbr>.FindBy(graph, (object) tranType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARFinChargeTran.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARFinChargeTran.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARFinChargeTran.lineNbr>
  {
  }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARFinChargeTran.origDocType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARFinChargeTran.origRefNbr>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARFinChargeTran.customerID>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARFinChargeTran.docDate>
  {
  }

  public abstract class finChargeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARFinChargeTran.finChargeID>
  {
  }
}
