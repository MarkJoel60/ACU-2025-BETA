// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.TemplateInventoryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// Provides a base selector for the Template Inventory Items. The list is filtered by the user access rights.
/// </summary>
[PXUIField]
[PXRestrictor(typeof (Where<InventoryItem.isTemplate, Equal<True>>), "The inventory item is not a template item.", new Type[] {}, ShowWarning = true)]
public class TemplateInventoryAttribute(Type SearchType, Type SubstituteKey, Type DescriptionField) : 
  BaseInventoryAttribute(SearchType, SubstituteKey, DescriptionField)
{
  public TemplateInventoryAttribute()
    : this(typeof (Search<InventoryItem.inventoryID, Where<Match<Current<AccessInfo.userName>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))
  {
  }
}
