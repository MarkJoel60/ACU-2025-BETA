// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.CrossReferenceUniquenessAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public class CrossReferenceUniquenessAttribute : PXCheckUnique
{
  public CrossReferenceUniquenessAttribute(params Type[] fields)
    : base(fields)
  {
    this.UniqueKeyIsPartOfPrimaryKey = true;
    this.IgnoreDuplicatesOnCopyPaste = true;
    this.ErrorMessage = "The record cannot be saved because the alternate ID specified for the selected item is already assigned to another inventory item ({0}). Please specify another alternate ID.";
  }

  protected virtual string PrepareMessage(PXCache cache, object currentRow, object duplicateRow)
  {
    INItemXRef inItemXref = (INItemXRef) duplicateRow;
    InventoryItem inventoryItem = (InventoryItem) PXSelectorAttribute.Select<INItemXRef.inventoryID>(cache, (object) inItemXref);
    if (PXAccess.FeatureInstalled<FeaturesSet.subItem>() && inventoryItem.StkItem.GetValueOrDefault())
    {
      INSubItem inSubItem = (INSubItem) PXSelectorAttribute.Select<INItemXRef.subItemID>(cache, (object) inItemXref);
      if (!string.IsNullOrEmpty(inSubItem?.SubItemCD))
        return PXMessages.LocalizeFormatNoPrefix("The record cannot be saved because the alternate ID specified for the selected item is already assigned to another inventory item ({0}). Please specify another alternate ID.", new object[2]
        {
          (object) inventoryItem.InventoryCD.TrimEnd(),
          (object) inSubItem.SubItemCD
        });
    }
    return PXMessages.LocalizeFormatNoPrefix("The record cannot be saved because the alternate ID specified for the selected item is already assigned to another inventory item ({0}). Please specify another alternate ID.", new object[1]
    {
      (object) inventoryItem.InventoryCD.TrimEnd()
    });
  }

  protected virtual bool CanClearError(string errorText)
  {
    return ((IEnumerable<string>) PXMessages.LocalizeFormatNoPrefix("The record cannot be saved because the alternate ID specified for the selected item is already assigned to another inventory item ({0}). Please specify another alternate ID.", Array.Empty<object>()).Split(new string[1]
    {
      "{0}"
    }, StringSplitOptions.None)).All<string>(new Func<string, bool>(errorText.Contains));
  }

  [PXLocalizable]
  public static class Messages
  {
    public const string AnotherItemAlreadyHasTheReferenceToThisAlternateID = "The record cannot be saved because the alternate ID specified for the selected item is already assigned to another inventory item ({0}). Please specify another alternate ID.";
    public const string AnotherSubItemAlreadyHasTheReferenceToThisAlternateID = "The record cannot be saved because the alternate ID specified for the selected item is already assigned to another inventory item ({0}, subitem: {1}). Please specify another alternate ID.";
  }
}
