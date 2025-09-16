// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;

#nullable enable
namespace PX.Objects.RQ;

public class RQRequisitionOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault(typeof (RQRequisition.reqNbr))]
  [PXForeignReference(typeof (RQRequisitionOrder.FK.Requisition))]
  public virtual 
  #nullable disable
  string ReqNbr { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true, InputMask = ">aa")]
  [PXDefault]
  public virtual string OrderCategory { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true, InputMask = ">aa")]
  [PXDefault]
  public virtual string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string OrderNbr { get; set; }

  public class PK : 
    PrimaryKeyOf<RQRequisitionOrder>.By<RQRequisitionOrder.reqNbr, RQRequisitionOrder.orderCategory, RQRequisitionOrder.orderType, RQRequisitionOrder.orderNbr>
  {
    public static RQRequisitionOrder Find(
      PXGraph graph,
      string reqNbr,
      string orderCategory,
      string orderType,
      string orderNbr,
      PKFindOptions options = 0)
    {
      return (RQRequisitionOrder) PrimaryKeyOf<RQRequisitionOrder>.By<RQRequisitionOrder.reqNbr, RQRequisitionOrder.orderCategory, RQRequisitionOrder.orderType, RQRequisitionOrder.orderNbr>.FindBy(graph, (object) reqNbr, (object) orderCategory, (object) orderType, (object) orderNbr, options);
    }
  }

  public static class FK
  {
    public class Requisition : 
      PrimaryKeyOf<RQRequisition>.By<RQRequisition.reqNbr>.ForeignKeyOf<RQRequisitionOrder>.By<RQRequisitionOrder.reqNbr>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<RQRequisitionOrder>.By<RQRequisitionOrder.orderType, RQRequisitionOrder.orderNbr>
    {
    }

    public class POOrder : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<RQRequisitionOrder>.By<RQRequisitionOrder.orderType, RQRequisitionOrder.orderNbr>
    {
    }
  }

  public class Events : PXEntityEventBase<RQRequisitionOrder>.Container<RQRequisitionOrder.Events>
  {
    public PXEntityEvent<RQRequisitionOrder, PX.Objects.SO.SOOrder> SOOrderUnlinked;
    public PXEntityEvent<RQRequisitionOrder, PX.Objects.PO.POOrder> POOrderUnlinked;
  }

  public abstract class reqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionOrder.reqNbr>
  {
  }

  public abstract class orderCategory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionOrder.orderCategory>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionOrder.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionOrder.orderNbr>
  {
  }
}
