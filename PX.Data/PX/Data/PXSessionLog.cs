// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSessionLog
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Web;

#nullable disable
namespace PX.Data;

internal class PXSessionLog
{
  public HashSet<string> Items = new HashSet<string>();

  public static PXSessionLog GetLog(HttpContext context)
  {
    PXSessionLog log = (PXSessionLog) context.Items[(object) nameof (PXSessionLog)];
    if (log == null)
    {
      log = new PXSessionLog();
      context.Items[(object) nameof (PXSessionLog)] = (object) log;
    }
    return log;
  }
}
