// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestLineBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXHidden]
[Serializable]
public class RQRequestLineBudget : RQRequestLine
{
  protected Decimal? _AprovedAmt;
  protected Decimal? _UnaprovedAmt;
  protected Decimal? _CuryAprovedAmt;
  protected Decimal? _CuryUnaprovedAmt;

  [PXDBCalced(typeof (Switch<Case<Where<RQRequest.approved, Equal<boolTrue>>, RQRequestLineBudget.estExtCost>, decimal0>), typeof (Decimal))]
  public virtual Decimal? AprovedAmt
  {
    get => this._AprovedAmt;
    set => this._AprovedAmt = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<RQRequest.approved, Equal<boolFalse>>, RQRequestLineBudget.estExtCost>, decimal0>), typeof (Decimal))]
  public virtual Decimal? UnaprovedAmt
  {
    get => this._UnaprovedAmt;
    set => this._UnaprovedAmt = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<RQRequest.approved, Equal<boolTrue>>, RQRequestLineBudget.curyEstExtCost>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryAprovedAmt
  {
    get => this._CuryAprovedAmt;
    set => this._CuryAprovedAmt = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<RQRequest.approved, Equal<boolFalse>>, RQRequestLineBudget.curyEstExtCost>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryUnaprovedAmt
  {
    get => this._CuryUnaprovedAmt;
    set => this._CuryUnaprovedAmt = value;
  }

  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    RQRequestLineBudget>.By<RQRequestLineBudget.orderNbr, RQRequestLine.lineNbr>
  {
    public static RQRequestLineBudget Find(
      PXGraph graph,
      string orderNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (RQRequestLineBudget) PrimaryKeyOf<RQRequestLineBudget>.By<RQRequestLineBudget.orderNbr, RQRequestLine.lineNbr>.FindBy(graph, (object) orderNbr, (object) lineNbr, options);
    }
  }

  public new static class FK
  {
    public class Request : 
      PrimaryKeyOf<RQRequest>.By<RQRequest.orderNbr>.ForeignKeyOf<RQRequestLineBudget>.By<RQRequestLineBudget.orderNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<RQRequestLineBudget>.By<RQRequestLine.inventoryID>
    {
    }
  }

  public new abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestLineBudget.orderNbr>
  {
  }

  public new abstract class estExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLineBudget.estExtCost>
  {
  }

  public new abstract class curyEstExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLineBudget.curyEstExtCost>
  {
  }

  public new abstract class expenseAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequestLineBudget.expenseAcctID>
  {
  }

  public new abstract class expenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequestLineBudget.expenseSubID>
  {
  }

  public abstract class aprovedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLineBudget.aprovedAmt>
  {
  }

  public abstract class unaprovedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLineBudget.unaprovedAmt>
  {
  }

  public abstract class curyAprovedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLineBudget.curyAprovedAmt>
  {
  }

  public abstract class curyUnaprovedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLineBudget.curyUnaprovedAmt>
  {
  }
}
