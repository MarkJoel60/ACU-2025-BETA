// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPackageDetailEx
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
namespace PX.Objects.SO;

[PXProjection(typeof (Select2<SOPackageDetail, LeftJoin<PX.Objects.CS.CSBox, On<PX.Objects.CS.CSBox.boxID, Equal<SOPackageDetail.boxID>>, CrossJoin<CommonSetup>>>), new Type[] {typeof (SOPackageDetail)})]
public class SOPackageDetailEx : SOPackageDetail
{
  [PXDefault(typeof (Search<PX.Objects.CS.CSBox.description, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageDetailEx.boxID>>>>))]
  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.CS.CSBox.description))]
  [PXUIField]
  public virtual 
  #nullable disable
  string BoxDescription { get; set; }

  [PXDefault(typeof (Search<PX.Objects.CS.CSBox.boxWeight, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageDetailEx.boxID>>>>))]
  [PXDBDecimal(4, MinValue = 0.0, BqlField = typeof (PX.Objects.CS.CSBox.boxWeight))]
  [PXUIField(DisplayName = "Box Weight", Enabled = false)]
  public virtual Decimal? BoxWeight { get; set; }

  [PXDefault(typeof (Search<PX.Objects.CS.CSBox.maxWeight, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageDetailEx.boxID>>>>))]
  [PXDBDecimal(4, BqlField = typeof (PX.Objects.CS.CSBox.maxWeight))]
  [PXUIField(DisplayName = "Max Weight", Enabled = false)]
  public virtual Decimal? MaxWeight { get; set; }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Net Weight", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<SOPackageDetail.weight, GreaterEqual<SOPackageDetailEx.boxWeight>>, Sub<SOPackageDetail.weight, SOPackageDetailEx.boxWeight>>, decimal0>))]
  public virtual Decimal? NetWeight { get; set; }

  [PXDefault(typeof (Search<CommonSetup.linearUOM>))]
  [PXDBString(IsUnicode = true, BqlField = typeof (CommonSetup.linearUOM))]
  [PXUIField(DisplayName = "Linear UOM", Enabled = false)]
  public virtual string LinearUOM { get; set; }

  public virtual SOPackageInfoEx ToPackageInfo(int? siteID)
  {
    SOPackageInfoEx packageInfo = new SOPackageInfoEx();
    packageInfo.BoxID = this.BoxID;
    packageInfo.LineNbr = this.LineNbr;
    packageInfo.Weight = this.NetWeight;
    packageInfo.GrossWeight = this.Weight;
    packageInfo.WeightUOM = this.WeightUOM;
    packageInfo.Qty = this.Qty;
    packageInfo.QtyUOM = this.QtyUOM;
    packageInfo.InventoryID = this.InventoryID;
    packageInfo.DeclaredValue = this.DeclaredValue;
    Decimal? cod = this.COD;
    Decimal num = 0M;
    packageInfo.COD = new bool?(cod.GetValueOrDefault() > num & cod.HasValue);
    packageInfo.SiteID = siteID;
    packageInfo.BoxWeight = this.BoxWeight;
    packageInfo.Description = this.BoxDescription;
    packageInfo.Height = this.Height;
    packageInfo.Length = this.Length;
    packageInfo.Width = this.Width;
    packageInfo.MaxWeight = this.MaxWeight;
    return packageInfo;
  }

  public new class PK : 
    PrimaryKeyOf<SOPackageDetailEx>.By<SOPackageDetailEx.shipmentNbr, SOPackageDetailEx.lineNbr>
  {
    public static SOPackageDetailEx Find(
      PXGraph graph,
      string shipmentNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (SOPackageDetailEx) PrimaryKeyOf<SOPackageDetailEx>.By<SOPackageDetailEx.shipmentNbr, SOPackageDetailEx.lineNbr>.FindBy(graph, (object) shipmentNbr, (object) lineNbr, options);
    }
  }

  public new static class FK
  {
    public class Box : 
      PrimaryKeyOf<PX.Objects.CS.CSBox>.By<PX.Objects.CS.CSBox.boxID>.ForeignKeyOf<SOPackageDetailEx>.By<SOPackageDetailEx.boxID>
    {
    }

    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.ForeignKeyOf<SOPackageDetailEx>.By<SOPackageDetailEx.shipmentNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOPackageDetailEx>.By<SOPackageDetailEx.inventoryID>
    {
    }
  }

  public new abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackageDetailEx.shipmentNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPackageDetailEx.lineNbr>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPackageDetailEx.inventoryID>
  {
  }

  public new abstract class boxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageDetailEx.boxID>
  {
  }

  public new abstract class packageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackageDetailEx.packageType>
  {
  }

  public abstract class boxDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPackageDetailEx.boxDescription>
  {
  }

  public abstract class boxWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageDetailEx.boxWeight>
  {
  }

  public abstract class maxWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageDetailEx.maxWeight>
  {
  }

  public abstract class netWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageDetailEx.netWeight>
  {
  }

  public abstract class linearUOM : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPackageDetailEx.linearUOM>
  {
  }
}
