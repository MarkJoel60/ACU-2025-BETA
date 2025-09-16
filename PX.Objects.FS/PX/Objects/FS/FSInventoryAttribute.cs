// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSInventoryAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.FS;

[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>>>), "The inventory item is {0}.", new Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)})]
[PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>>), "The inventory item is a template item.", new Type[] {}, ShowWarning = true)]
public class FSInventoryAttribute : CrossItemAttribute
{
  public FSInventoryAttribute(
    Type searchType,
    Type substituteKey,
    Type descriptionField,
    Type[] listField)
    : base(searchType, substituteKey, descriptionField, INPrimaryAlternateType.CPN)
  {
  }

  public FSInventoryAttribute(Type searchType, Type substituteKey, Type descriptionField)
    : base(searchType, substituteKey, descriptionField, INPrimaryAlternateType.CPN)
  {
  }

  public FSInventoryAttribute()
    : this(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<Match<Current<AccessInfo.userName>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr))
  {
  }
}
