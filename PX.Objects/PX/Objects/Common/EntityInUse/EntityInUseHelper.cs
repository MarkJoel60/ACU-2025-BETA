// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.EntityInUse.EntityInUseHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.Common.EntityInUse;

public static class EntityInUseHelper
{
  private static Lazy<PXGraph> lazyGraph = new Lazy<PXGraph>((Func<PXGraph>) (() => PXGraph.CreateInstance<PXGraph>()));

  public static void MarkEntityAsInUse<Table>(params object[] keys) where Table : IBqlTable
  {
    if (EntityInUseHelper.IsEntityInUse<Table>(keys))
      return;
    KeysCollection cacheKeys = EntityInUseHelper.GetCacheKeys<Table>();
    if (((IEnumerable<string>) cacheKeys).Count<string>() != keys.Length)
      throw new PXArgumentException(nameof (keys));
    PXDataFieldAssign[] pxDataFieldAssignArray = new PXDataFieldAssign[cacheKeys.Count];
    for (int index = 0; index < cacheKeys.Count; ++index)
      pxDataFieldAssignArray[index] = new PXDataFieldAssign(cacheKeys[index], keys[index]);
    try
    {
      PXDatabase.Insert<Table>(pxDataFieldAssignArray);
    }
    catch (PXDatabaseException ex) when (ex.ErrorCode == 4)
    {
    }
  }

  public static bool IsEntityInUse<Table>(params object[] keys) where Table : IBqlTable
  {
    if (Attribute.IsDefined((MemberInfo) typeof (Table), typeof (EntityInUseDBSlotOnAttribute)))
    {
      EntityInUseHelper.EntityInUseDefinition<Table> slot = PXDatabase.GetSlot<EntityInUseHelper.EntityInUseDefinition<Table>>(typeof (EntityInUseHelper.EntityInUseDefinition<Table>).FullName, new Type[1]
      {
        typeof (Table)
      });
      return keys.Length == 0 ? slot.EntitiesInUse.Any<string>() : slot.EntitiesInUse.Contains(EntityInUseHelper.GetHash<Table>(keys));
    }
    KeysCollection cacheKeys = EntityInUseHelper.GetCacheKeys<Table>();
    if (keys.Length != 0 && ((IEnumerable<string>) cacheKeys).Count<string>() != keys.Length)
      throw new PXArgumentException(nameof (keys));
    PXDataField[] pxDataFieldArray = new PXDataField[cacheKeys.Count * (keys.Length != 0 ? 2 : 1)];
    for (int index = 0; index < cacheKeys.Count; ++index)
    {
      string str = cacheKeys[index];
      pxDataFieldArray[index] = new PXDataField(str);
      if (keys.Length != 0)
        pxDataFieldArray[cacheKeys.Count + index] = (PXDataField) new PXDataFieldValue(str, keys[index]);
    }
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Table>(pxDataFieldArray))
      return pxDataRecord != null;
  }

  private static KeysCollection GetCacheKeys<Table>() where Table : IBqlTable
  {
    return EntityInUseHelper.lazyGraph.Value.Caches[typeof (Table)].Keys;
  }

  private static string GetHash<Table>(params object[] keys) where Table : IBqlTable
  {
    return string.Join("::", ((IEnumerable<object>) keys).Select<object, string>((Func<object, string>) (key => (key ?? (object) string.Empty).ToString())));
  }

  private class EntityInUseDefinition<Table> : IPrefetchable, IPXCompanyDependent where Table : IBqlTable
  {
    public readonly HashSet<string> EntitiesInUse = new HashSet<string>();

    public void Prefetch()
    {
      this.EntitiesInUse.Clear();
      using (new PXConnectionScope())
      {
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Table>(((IEnumerable<string>) EntityInUseHelper.GetCacheKeys<Table>()).Select<string, PXDataField>((Func<string, PXDataField>) (key => new PXDataField(key))).ToArray<PXDataField>()))
        {
          object[] objArray = new object[pxDataRecord.FieldCount];
          for (int index = 0; index < objArray.Length; ++index)
            objArray[index] = pxDataRecord.GetValue(index);
          this.EntitiesInUse.Add(EntityInUseHelper.GetHash<Table>(objArray));
        }
      }
    }
  }
}
