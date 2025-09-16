// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMInventorySelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Inventory Selector that allows to specify an OTHER InventoryID saving 0 in the table.
/// </summary>
[Serializable]
public class PMInventorySelectorAttribute : PXDimensionSelectorAttribute
{
  public const string DimensionName = "INVENTORY";
  public const string EmptyComponentCD = "<N/A>";

  public PMInventorySelectorAttribute()
    : this(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr))
  {
  }

  public PMInventorySelectorAttribute(Type searchType)
    : this(searchType, typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr))
  {
  }

  public PMInventorySelectorAttribute(Type searchType, Type substituteKey, Type descriptionField)
    : base("INVENTORY", searchType, substituteKey)
  {
    this.CacheGlobal = true;
    this.DescriptionField = descriptionField;
  }

  public static int EmptyInventoryID
  {
    get => ServiceLocator.Current.GetInstance<IProjectSettingsManager>().EmptyInventoryID;
  }
}
