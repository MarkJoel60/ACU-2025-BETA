// Decompiled with JetBrains decompiler
// Type: PX.SM.BaseExtraActionRunHandler`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Data;
using System.Collections;

#nullable disable
namespace PX.SM;

public abstract class BaseExtraActionRunHandler<T> : IExtraActionRunHandler where T : class
{
  protected abstract IEnumerable HandlerImplementation(PXAdapter adapter, T parameters);

  public PXButtonDelegate GetHandler(ScreenActionExtraData action)
  {
    return (PXButtonDelegate) (adapter =>
    {
      T parameters = string.IsNullOrEmpty(action.Settings) ? default (T) : this.DeserializeParameters(action.Settings);
      return this.HandlerImplementation(adapter, parameters);
    });
  }

  public virtual T DeserializeParameters(string value) => JsonConvert.DeserializeObject<T>(value);
}
