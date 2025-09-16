// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.StoragePlace
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName]
[PXProjection(typeof (SelectFromBase<INCostSite, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INLocation>.On<BqlOperand<INLocation.locationID, IBqlInt>.IsEqual<INCostSite.costSiteID>>>, FbqlJoins.Left<INCart>.On<BqlOperand<INCart.cartID, IBqlInt>.IsEqual<INCostSite.costSiteID>>>, FbqlJoins.Left<INSite>.On<BqlOperand<IsNull<INLocation.siteID, INCart.siteID>, IBqlInt>.IsEqual<INSite.siteID>>>>.Where<BqlOperand<INCostSite.costSiteType, IBqlString>.IsIn<NameOf<INLocation>, NameOf<INCart>>>), Persistent = false)]
[PXPrimaryGraph(new Type[] {typeof (INSiteMaint)}, new Type[] {typeof (SelectFromBase<INSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INSite.siteID, IBqlInt>.IsEqual<BqlField<StoragePlace.siteID, IBqlInt>.FromCurrent>>)})]
public class StoragePlace : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlTable = typeof (INSite))]
  public int? SiteID { get; set; }

  [PXDBString(BqlTable = typeof (INSite))]
  [PXUIField]
  public 
  #nullable disable
  string SiteCD { get; set; }

  [Location(typeof (StoragePlace.siteID), BqlField = typeof (INLocation.locationID), Enabled = false)]
  public virtual int? LocationID { get; set; }

  [PXDBInt(BqlField = typeof (INCart.cartID))]
  [PXUIField(DisplayName = "Cart ID", IsReadOnly = true)]
  [PXSelector(typeof (SearchFor<INCart.cartID>.In<SelectFromBase<INCart, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INCart.active, IBqlBool>.IsEqual<True>>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  public int? CartID { get; set; }

  [PXInt(IsKey = true)]
  [PXDBCalced(typeof (BqlOperand<INLocation.locationID, IBqlInt>.IfNullThen<INCart.cartID>), typeof (int))]
  public virtual int? StorageID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Storage ID", IsReadOnly = true)]
  [PXDBCalced(typeof (BqlOperand<INLocation.locationCD, IBqlString>.IfNullThen<INCart.cartCD>), typeof (string))]
  public virtual string StorageCD { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Description", IsReadOnly = true)]
  [PXDBCalced(typeof (BqlOperand<INLocation.descr, IBqlString>.IfNullThen<INCart.descr>), typeof (string))]
  public virtual string Descr { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Cart", IsReadOnly = true, FieldClass = "Carts")]
  [PXDBCalced(typeof (BqlOperand<True, IBqlBool>.When<BqlOperand<INCart.cartID, IBqlInt>.IsNotNull>.NoDefault.Else<False>), typeof (bool))]
  public virtual bool? IsCart { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Active", IsReadOnly = true)]
  [PXDBCalced(typeof (BqlOperand<INLocation.active, IBqlBool>.IfNullThen<INCart.active>), typeof (bool))]
  public virtual bool? Active { get; set; }

  public class PK : PrimaryKeyOf<StoragePlace>.By<StoragePlace.siteID, StoragePlace.storageID>
  {
    public static StoragePlace Find(
      PXGraph graph,
      int? siteID,
      int? storageID,
      PKFindOptions options = 0)
    {
      return (StoragePlace) PrimaryKeyOf<StoragePlace>.By<StoragePlace.siteID, StoragePlace.storageID>.FindBy(graph, (object) siteID, (object) storageID, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<StoragePlace>.By<StoragePlace.siteID>
    {
    }

    public class Cart : 
      PrimaryKeyOf<INCart>.By<INCart.siteID, INCart.cartID>.ForeignKeyOf<StoragePlace>.By<StoragePlace.siteID, StoragePlace.cartID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<StoragePlace>.By<StoragePlace.locationID>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlace.siteID>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StoragePlace.siteCD>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlace.locationID>
  {
  }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlace.cartID>
  {
  }

  public abstract class storageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlace.storageID>
  {
  }

  public abstract class storageCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StoragePlace.storageCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StoragePlace.descr>
  {
  }

  public abstract class isCart : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  StoragePlace.isCart>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  StoragePlace.active>
  {
  }
}
