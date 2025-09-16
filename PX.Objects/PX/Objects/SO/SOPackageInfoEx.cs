// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPackageInfoEx
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

[PXProjection(typeof (Select2<SOPackageInfo, InnerJoin<PX.Objects.CS.CSBox, On<PX.Objects.CS.CSBox.boxID, Equal<SOPackageInfo.boxID>>, CrossJoin<CommonSetup>>>), new Type[] {typeof (SOPackageInfo)})]
[Serializable]
public class SOPackageInfoEx : SOPackageInfo
{
  protected 
  #nullable disable
  string _Description;
  protected Decimal? _BoxWeight;

  [PXDefault(typeof (Search<PX.Objects.CS.CSBox.description, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageInfoEx.boxID>>>>))]
  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.CS.CSBox.description))]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDefault(typeof (Search<PX.Objects.CS.CSBox.boxWeight, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageInfoEx.boxID>>>>))]
  [PXDBDecimal(4, MinValue = 0.0, BqlField = typeof (PX.Objects.CS.CSBox.boxWeight))]
  [PXUIField(DisplayName = "Box Weight", Enabled = false)]
  public virtual Decimal? BoxWeight
  {
    get => this._BoxWeight;
    set => this._BoxWeight = value;
  }

  [PXDefault(typeof (Search<PX.Objects.CS.CSBox.maxWeight, Where<PX.Objects.CS.CSBox.boxID, Equal<Current<SOPackageInfoEx.boxID>>>>))]
  [PXDBDecimal(MinValue = 0.0, BqlField = typeof (PX.Objects.CS.CSBox.maxWeight))]
  [PXUIField(DisplayName = "Max Weight", Enabled = false)]
  public virtual Decimal? MaxWeight { get; set; }

  [PXDBDecimal(4, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Weight")]
  [PXFormula(typeof (Add<SOPackageInfo.weight, SOPackageInfoEx.boxWeight>), typeof (SumCalc<SOOrder.packageWeight>))]
  public override Decimal? GrossWeight
  {
    get => this._GrossWeight;
    set => this._GrossWeight = value;
  }

  [PXDefault(typeof (Search<CommonSetup.linearUOM>))]
  [PXDBString(IsUnicode = true, BqlField = typeof (CommonSetup.linearUOM))]
  [PXUIField(DisplayName = "Linear UOM", Enabled = false)]
  public virtual string LinearUOM { get; set; }

  public virtual SOPackageDetailEx ToPackageDetail(string packageType)
  {
    SOPackageDetailEx packageDetail = new SOPackageDetailEx();
    packageDetail.BoxID = this.BoxID;
    packageDetail.NetWeight = this.Weight;
    packageDetail.Weight = this.GrossWeight;
    packageDetail.WeightUOM = this.WeightUOM;
    packageDetail.Qty = this.Qty;
    packageDetail.QtyUOM = this.QtyUOM;
    packageDetail.InventoryID = this.InventoryID;
    packageDetail.DeclaredValue = this.DeclaredValue;
    packageDetail.COD = this.COD.GetValueOrDefault() ? new Decimal?(this.DeclaredValue ?? 1M) : new Decimal?();
    packageDetail.PackageType = packageType;
    packageDetail.BoxWeight = this.BoxWeight;
    packageDetail.BoxDescription = this.Description;
    packageDetail.Height = this.Height;
    packageDetail.Length = this.Length;
    packageDetail.Width = this.Width;
    packageDetail.MaxWeight = this.MaxWeight;
    return packageDetail;
  }

  public new class PK : 
    PrimaryKeyOf<SOPackageInfoEx>.By<SOPackageInfoEx.orderType, SOPackageInfoEx.orderNbr, SOPackageInfoEx.lineNbr>
  {
    public static SOPackageInfoEx Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (SOPackageInfoEx) PrimaryKeyOf<SOPackageInfoEx>.By<SOPackageInfoEx.orderType, SOPackageInfoEx.orderNbr, SOPackageInfoEx.lineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, options);
    }
  }

  public new static class FK
  {
    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOPackageInfoEx>.By<SOPackageInfoEx.orderType, SOPackageInfoEx.orderNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOPackageInfoEx>.By<SOPackageInfoEx.inventoryID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOPackageInfoEx>.By<SOPackageInfoEx.siteID>
    {
    }

    public class Box : 
      PrimaryKeyOf<PX.Objects.CS.CSBox>.By<PX.Objects.CS.CSBox.boxID>.ForeignKeyOf<SOPackageInfoEx>.By<SOPackageInfoEx.boxID>
    {
    }
  }

  public new abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageInfoEx.orderType>
  {
  }

  public new abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageInfoEx.orderNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPackageInfoEx.lineNbr>
  {
  }

  public new abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageInfoEx.operation>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPackageInfoEx.siteID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPackageInfoEx.inventoryID>
  {
  }

  public new abstract class boxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageInfoEx.boxID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPackageInfoEx.description>
  {
  }

  public abstract class boxWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageInfoEx.boxWeight>
  {
  }

  public abstract class maxWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPackageInfoEx.maxWeight>
  {
  }

  public new abstract class grossWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPackageInfoEx.grossWeight>
  {
  }

  public abstract class linearUOM : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPackageInfoEx.linearUOM>
  {
  }
}
