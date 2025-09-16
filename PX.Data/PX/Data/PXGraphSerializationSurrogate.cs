// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraphSerializationSurrogate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Common.Context;
using PX.Common.Session;
using System;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXGraphSerializationSurrogate : PXSerializationSurrogate
{
  private System.Type GraphType;
  private string[] Keys;
  private object[] Vals;

  private static IDisposable StubSessionState(IPXSessionState sessionState)
  {
    ISlotStore instance = SlotStore.Instance;
    PXSessionContextFactory.GetSessionContext(instance);
    return instance.UseSessionStandIn(sessionState);
  }

  public virtual object RestoreObject()
  {
    IPXSessionState pxSessionState = (IPXSessionState) new DictionarySessionState();
    for (int index = 0; index < this.Keys.Length; ++index)
      pxSessionState.Set(this.Keys[index], this.Vals[index]);
    using (PXGraphSerializationSurrogate.StubSessionState(pxSessionState))
    {
      using (new PXPreserveScope())
      {
        pxSessionState.CheckDynamicExtensions();
        PXGraph instance = PXGraph.CreateInstance(this.GraphType);
        instance.Load();
        foreach (System.Type key in instance.Views.Caches.ToArray())
        {
          PXCache cach = instance.Caches[key];
        }
        foreach (System.Type key in instance.Views.RestorableCaches.ToArray())
        {
          PXCache cach = instance.Caches[key];
        }
        return (object) instance;
      }
    }
  }

  public virtual void SaveObjectData(object src)
  {
    PXGraph g = (PXGraph) src;
    this.GraphType = CustomizedTypeManager.GetTypeNotCustomized(g);
    IPXSessionState sessionState = (IPXSessionState) new DictionarySessionState();
    using (PXGraphSerializationSurrogate.StubSessionState(sessionState))
    {
      g.UnattendedMode = false;
      g.Unload();
      this.Keys = sessionState.Keys.ToArray<string>();
      this.Vals = Array.ConvertAll<string, object>(this.Keys, new Converter<string, object>(sessionState.Get));
    }
  }
}
