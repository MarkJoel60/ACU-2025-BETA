// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Exceptions.UpdateQtyCostStatusImbalanceException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.IN.Exceptions;

public class UpdateQtyCostStatusImbalanceException : PXException
{
  public int? InventoryID { get; private set; }

  public int? SubItemID { get; private set; }

  public int? SiteID { get; private set; }

  public UpdateQtyCostStatusImbalanceException(
    int? inventoryID,
    int? subitemID,
    int? siteID,
    string message,
    params object[] args)
    : base(message, args)
  {
    this.InventoryID = inventoryID;
    this.SubItemID = subitemID;
    this.SiteID = siteID;
  }

  public UpdateQtyCostStatusImbalanceException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<UpdateQtyCostStatusImbalanceException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<UpdateQtyCostStatusImbalanceException>(this, info);
    base.GetObjectData(info, context);
  }
}
