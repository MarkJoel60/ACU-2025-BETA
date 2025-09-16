// Decompiled with JetBrains decompiler
// Type: PX.Data.PXContextCopyingRequiredAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>
/// Objects of all classes decorated by this attribute will be copied from PXContext slots to another threads.
/// </summary>
/// <remarks>
/// Bug 41770:
/// When QueueUserWorkItem is called, several settings are placed in arguments and later PerformOperation method restores them on background thread.
/// We have introduced this attribute, and inside StartOperation method, before QueueUserWorkItem call, iterating through all pxcontext slots,
/// and if an  object stored is decorated with this attribute, passing it via parameters and then restoring it on the background thread that will execute operation.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class PXContextCopyingRequiredAttribute : Attribute
{
  internal static IDictionary Capture(ISlotStorageProvider slots)
  {
    return PXContextCopyingRequiredAttribute.Capture(slots, (Func<string, object, object>) ((key, _) => (object) key));
  }

  internal static IDictionary Capture(Func<string, object, object> keyFactory)
  {
    return PXContextCopyingRequiredAttribute.Capture(SlotStore.Provider, keyFactory);
  }

  internal static IDictionary Capture(
    ISlotStorageProvider provider,
    Func<string, object, object> keyFactory)
  {
    return (IDictionary) provider.Items().Where<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>) (pair =>
    {
      object obj = pair.Value;
      return obj != null && Attribute.IsDefined((MemberInfo) obj.GetType(), typeof (PXContextCopyingRequiredAttribute));
    })).GroupBy<KeyValuePair<string, object>, object, object>((Func<KeyValuePair<string, object>, object>) (pair => keyFactory(pair.Key, pair.Value)), (Func<KeyValuePair<string, object>, object>) (pair => pair.Value)).ToDictionary<IGrouping<object, object>, object, object>((Func<IGrouping<object, object>, object>) (grouping => grouping.Key), (Func<IGrouping<object, object>, object>) (grouping => grouping.Last<object>()));
  }

  internal static void SetToStorage(IDictionary slots, ISlotStorageProvider slotStorage)
  {
    foreach (DictionaryEntry slot in slots)
    {
      object key = slot.Key;
      if (!(key is string str))
      {
        System.Type type = key as System.Type;
        if ((object) type != null)
        {
          object obj = slot.Value;
          if (obj != null && type != obj.GetType())
            throw new InvalidOperationException($"Defined key type ({type}) and actual value type ({obj.GetType()}) are not equal.");
          TypeKeyedOperationExtensions.Set((ISlotStore) slotStorage, type, obj);
        }
      }
      else
        ((ISlotStore) slotStorage).Set(str, slot.Value);
    }
  }
}
