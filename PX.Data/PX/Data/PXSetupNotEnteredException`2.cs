// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSetupNotEnteredException`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public class PXSetupNotEnteredException<TDAC, TKeyField> : PXSetupNotEnteredException
{
  public PXSetupNotEnteredException(string format, string keyvalue, params object[] args)
  {
    string format1 = format;
    System.Type inpDAC = typeof (TDAC);
    Dictionary<System.Type, object> keyparams = new Dictionary<System.Type, object>();
    keyparams.Add(typeof (TKeyField), (object) keyvalue);
    object[] array = ((IEnumerable<object>) new string[1]
    {
      PXSetupNotEnteredException<TDAC, TKeyField>.GetDisplayName()
    }).Concat<object>((IEnumerable<object>) args).ToArray<object>();
    // ISSUE: explicit constructor call
    base.\u002Ector(format1, inpDAC, keyparams, array);
  }

  public PXSetupNotEnteredException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXSetupNotEnteredException<TDAC, TKeyField>>(this, info);
  }

  protected static string GetDisplayName()
  {
    string name = typeof (TDAC).Name;
    if (typeof (TDAC).IsDefined(typeof (PXCacheNameAttribute), true))
      name = ((PXNameAttribute) typeof (TDAC).GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0]).GetName();
    return name;
  }
}
