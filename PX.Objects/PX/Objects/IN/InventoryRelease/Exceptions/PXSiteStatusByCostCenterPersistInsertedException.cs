// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Exceptions.PXSiteStatusByCostCenterPersistInsertedException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.IN.InventoryRelease.Exceptions;

public class PXSiteStatusByCostCenterPersistInsertedException : PXException
{
  public int? InventoryID { get; private set; }

  public int? SubItemID { get; private set; }

  public int? SiteID { get; private set; }

  public int? CostCenterID { get; private set; }

  public PXSiteStatusByCostCenterPersistInsertedException(
    int? inventoryID,
    int? subitemID,
    int? siteID,
    int? costCenterID,
    string message,
    params object[] args)
    : base(message, args)
  {
    this.InventoryID = inventoryID;
    this.SubItemID = subitemID;
    this.SiteID = siteID;
    this.CostCenterID = costCenterID;
  }

  public PXSiteStatusByCostCenterPersistInsertedException(
    SerializationInfo info,
    StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXSiteStatusByCostCenterPersistInsertedException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXSiteStatusByCostCenterPersistInsertedException>(this, info);
    base.GetObjectData(info, context);
  }
}
