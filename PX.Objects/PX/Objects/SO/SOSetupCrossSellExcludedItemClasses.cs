// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOSetupCrossSellExcludedItemClasses
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <exclude />
[PXCacheName]
public class SOSetupCrossSellExcludedItemClasses : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<INItemClass.itemClassID>), new Type[] {typeof (INItemClass.itemClassCD), typeof (INItemClass.descr)}, SubstituteKey = typeof (INItemClass.itemClassCD))]
  [PXUIField]
  public virtual int? ItemClassID { get; set; }

  [PXDBTimestamp]
  public virtual 
  #nullable disable
  byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<SOSetupCrossSellExcludedItemClasses>.By<SOSetupCrossSellExcludedItemClasses.itemClassID>
  {
    public static SOSetupCrossSellExcludedItemClasses Find(
      PXGraph graph,
      int? itemClassID,
      PKFindOptions options = 0)
    {
      return (SOSetupCrossSellExcludedItemClasses) PrimaryKeyOf<SOSetupCrossSellExcludedItemClasses>.By<SOSetupCrossSellExcludedItemClasses.itemClassID>.FindBy(graph, (object) itemClassID, options);
    }
  }

  public static class FK
  {
    public class ItemClassID : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<SOSetupCrossSellExcludedItemClasses>.By<SOSetupCrossSellExcludedItemClasses.itemClassID>
    {
    }
  }

  public abstract class itemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOSetupCrossSellExcludedItemClasses.itemClassID>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    SOSetupCrossSellExcludedItemClasses.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOSetupCrossSellExcludedItemClasses.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetupCrossSellExcludedItemClasses.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOSetupCrossSellExcludedItemClasses.createdDateTime>
  {
  }
}
