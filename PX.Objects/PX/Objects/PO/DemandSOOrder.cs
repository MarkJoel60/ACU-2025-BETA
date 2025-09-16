// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.DemandSOOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.SO;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
[PXProjection(typeof (Select<PX.Objects.SO.SOOrder>))]
public class DemandSOOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (PX.Objects.SO.SOOrder.orderType))]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDefault]
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.SO.SOOrder.orderNbr))]
  public virtual string OrderNbr { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOOrder.status))]
  [PXUIField]
  [SOOrderStatus.List]
  public virtual string Status { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOOrder.hold))]
  public virtual bool? Hold { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOOrder.approved))]
  public virtual bool? Approved { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOOrder.cancelled))]
  public virtual bool? Cancelled { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOOrder.prepaymentReqSatisfied))]
  public virtual bool? PrepaymentReqSatisfied { get; set; }

  public class PK : PrimaryKeyOf<DemandSOOrder>.By<DemandSOOrder.orderType, DemandSOOrder.orderNbr>
  {
    public static DemandSOOrder Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      PKFindOptions options = 0)
    {
      return (DemandSOOrder) PrimaryKeyOf<DemandSOOrder>.By<DemandSOOrder.orderType, DemandSOOrder.orderNbr>.FindBy(graph, (object) orderType, (object) orderNbr, options);
    }

    public static DemandSOOrder FindDirty(PXGraph graph, string orderType, string orderNbr)
    {
      return PXResultset<DemandSOOrder>.op_Implicit(PXSelectBase<DemandSOOrder, PXSelect<DemandSOOrder, Where<DemandSOOrder.orderType, Equal<Required<DemandSOOrder.orderType>>, And<DemandSOOrder.orderNbr, Equal<Required<DemandSOOrder.orderNbr>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
      {
        (object) orderType,
        (object) orderNbr
      }));
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DemandSOOrder.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DemandSOOrder.orderNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DemandSOOrder.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DemandSOOrder.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DemandSOOrder.approved>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DemandSOOrder.cancelled>
  {
  }

  public abstract class prepaymentReqSatisfied : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DemandSOOrder.prepaymentReqSatisfied>
  {
  }
}
