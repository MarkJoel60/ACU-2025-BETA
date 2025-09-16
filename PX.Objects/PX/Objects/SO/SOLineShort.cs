// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLineShort
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.SO;

/// <summary>
/// This is a readonly DAC with limited fields of the <see cref="T:PX.Objects.SO.SOLine" /> DAC.
/// </summary>
[PXHidden]
[PXProjection(typeof (SelectFrom<SOLine>), Persistent = false)]
public class SOLineShort : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (SOLine.orderType))]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (SOLine.orderNbr))]
  public virtual string OrderNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (SOLine.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(BqlField = typeof (SOLine.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBInt(BqlField = typeof (SOLine.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXDBInt(BqlField = typeof (SOLine.siteID))]
  public virtual int? SiteID { get; set; }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa", BqlField = typeof (SOLine.uOM))]
  public virtual string UOM { get; set; }

  [PXDBInt(BqlField = typeof (SOLine.costCenterID))]
  public virtual int? CostCenterID { get; set; }

  public class PK : 
    PrimaryKeyOf<SOLineShort>.By<SOLineShort.orderType, SOLineShort.orderNbr, SOLineShort.lineNbr>
  {
    public static SOLineShort Find(PXGraph graph, string orderType, string orderNbr, int? lineNbr)
    {
      return (SOLineShort) PrimaryKeyOf<SOLineShort>.By<SOLineShort.orderType, SOLineShort.orderNbr, SOLineShort.lineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOLineShort>.By<SOLineShort.orderType, SOLineShort.orderNbr>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<SOLineShort>.By<SOLineShort.inventoryID, SOLineShort.subItemID, SOLineShort.siteID>
    {
    }

    public class SiteStatusByCostCenter : 
      PrimaryKeyOf<INSiteStatusByCostCenter>.By<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>.ForeignKeyOf<SOLineShort>.By<SOLineShort.inventoryID, SOLineShort.subItemID, SOLineShort.siteID, SOLineShort.costCenterID>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineShort.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineShort.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineShort.lineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineShort.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineShort.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineShort.siteID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineShort.uOM>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineShort.costCenterID>
  {
  }
}
