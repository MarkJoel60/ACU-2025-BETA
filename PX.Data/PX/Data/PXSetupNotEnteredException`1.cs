// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSetupNotEnteredException`1
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
public class PXSetupNotEnteredException<TDAC> : PXSetupNotEnteredException
{
  public PXSetupNotEnteredException()
    : this("The required configuration data is not entered on the {0} form.")
  {
  }

  public PXSetupNotEnteredException(string format, params object[] args)
    : base(format, typeof (TDAC), (Dictionary<System.Type, object>) null, ((IEnumerable<object>) new string[1]
    {
      PXSetupNotEnteredException<TDAC>.GetDisplayName()
    }).Concat<object>((IEnumerable<object>) args).ToArray<object>())
  {
  }

  public PXSetupNotEnteredException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXSetupNotEnteredException<TDAC>>(this, info);
  }

  protected static string GetDisplayName()
  {
    string name = typeof (TDAC).Name;
    if (typeof (TDAC).IsDefined(typeof (PXCacheNameAttribute), true))
      name = ((PXNameAttribute) typeof (TDAC).GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0]).GetName();
    return name;
  }
}
