// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Attributes.DBMatrixLocalizableDescriptionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN.Matrix.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Matrix.Attributes;

/// <exclude />
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class DBMatrixLocalizableDescriptionAttribute(int length) : PXDBLocalizableStringAttribute(length)
{
  public bool CopyTranslationsToInventoryItem { get; set; }

  public static void SetTranslations<TDestinationField>(
    PXCache destinationCache,
    object destinationData,
    Func<string, string> processTranslation)
    where TDestinationField : IBqlField
  {
    if (!PXDBLocalizableStringAttribute.IsEnabled)
      return;
    string[] translations = new string[PXDBLocalizableStringAttribute.EnabledLocales.Count];
    for (int index = 0; index < PXDBLocalizableStringAttribute.EnabledLocales.Count; ++index)
    {
      string enabledLocale = PXDBLocalizableStringAttribute.EnabledLocales[index];
      translations[index] = processTranslation(enabledLocale);
    }
    PXCacheEx.Adjust<DBMatrixLocalizableDescriptionAttribute>(destinationCache, destinationData).For<TDestinationField>((Action<DBMatrixLocalizableDescriptionAttribute>) (a => a.SetTranslations(destinationCache, destinationData, translations)));
  }

  protected virtual void Translations_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!this.CopyTranslationsToInventoryItem)
    {
      base.Translations_FieldUpdating(sender, e);
    }
    else
    {
      int? inventoryID = (int?) sender.GetValue<InventoryItem.inventoryID>(e.Row);
      if (!inventoryID.HasValue)
      {
        base.Translations_FieldUpdating(sender, e);
      }
      else
      {
        string[] first = (string[]) this.GetTranslations(sender, e.Row)?.Clone();
        base.Translations_FieldUpdating(sender, e);
        string[] translations = this.GetTranslations(sender, e.Row);
        if ((first == null || translations == null || ((IEnumerable<string>) first).SequenceEqual<string>((IEnumerable<string>) translations)) && (first != null || translations == null) && (first == null || translations != null))
          return;
        InventoryItem dirty = InventoryItem.PK.FindDirty(sender.Graph, inventoryID);
        foreach (PXDBLocalizableStringAttribute localizableStringAttribute in ((PXCache) GraphHelper.Caches<InventoryItem>(sender.Graph)).GetAttributes<InventoryItem.descr>((object) dirty).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attribute => attribute is PXDBLocalizableStringAttribute)))
        {
          localizableStringAttribute.SetTranslations((PXCache) GraphHelper.Caches<InventoryItem>(sender.Graph), (object) dirty, translations);
          if (sender.Graph is TemplateInventoryItemMaint graph)
            graph.UpdateChild(dirty);
          else
            GraphHelper.Caches<InventoryItem>(sender.Graph).Update(dirty);
        }
      }
    }
  }
}
