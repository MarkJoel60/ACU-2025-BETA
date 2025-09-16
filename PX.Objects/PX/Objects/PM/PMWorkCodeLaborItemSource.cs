// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMWorkCodeLaborItemSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("Workers' Compensation Project Task Source")]
[Serializable]
public class PMWorkCodeLaborItemSource : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsKey = true)]
  [PXDBDefault(typeof (PMWorkCode.workCodeID))]
  [PXParent(typeof (PMWorkCodeLaborItemSource.FK.WorkCode))]
  public 
  #nullable disable
  string WorkCodeID { get; set; }

  [PMLaborItem(null, null, null, IsKey = true)]
  [PXForeignReference(typeof (PMWorkCodeLaborItemSource.FK.LaborItem))]
  [PXDefault]
  [PXCheckUnique(new Type[] {}, ErrorMessage = "One labor item cannot be associated with multiple workers' compensation codes.")]
  public virtual int? LaborItemID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<PMWorkCodeLaborItemSource>.By<PMWorkCodeLaborItemSource.workCodeID, PMWorkCodeLaborItemSource.laborItemID>
  {
    public static PMWorkCodeLaborItemSource Find(
      PXGraph graph,
      string workCodeID,
      int? laborItemID,
      PKFindOptions options = 0)
    {
      return (PMWorkCodeLaborItemSource) PrimaryKeyOf<PMWorkCodeLaborItemSource>.By<PMWorkCodeLaborItemSource.workCodeID, PMWorkCodeLaborItemSource.laborItemID>.FindBy(graph, (object) workCodeID, (object) laborItemID, options);
    }
  }

  public static class FK
  {
    public class WorkCode : 
      PrimaryKeyOf<PMWorkCode>.By<PMWorkCode.workCodeID>.ForeignKeyOf<PMWorkCodeLaborItemSource>.By<PMWorkCodeLaborItemSource.workCodeID>
    {
    }

    public class LaborItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<PMWorkCodeLaborItemSource>.By<PMWorkCodeLaborItemSource.laborItemID>
    {
    }
  }

  public abstract class workCodeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCodeLaborItemSource.workCodeID>
  {
  }

  public abstract class laborItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWorkCodeLaborItemSource.laborItemID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMWorkCodeLaborItemSource.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCodeLaborItemSource.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWorkCodeLaborItemSource.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMWorkCodeLaborItemSource.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCodeLaborItemSource.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWorkCodeLaborItemSource.lastModifiedDateTime>
  {
  }
}
