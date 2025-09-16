// Decompiled with JetBrains decompiler
// Type: PX.Data.PXConnectionList
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXConnectionList
{
  public static readonly bool IsStackTraceEnabled = WebConfig.GetBool("IsConnectionStackTraceEnabled", false);
  private readonly Dictionary<DbConnection, StackTrace> Items = new Dictionary<DbConnection, StackTrace>();

  internal static bool IsEnabled { get; set; } = WebConfig.GetBool("IsConnectionTraceEnabled", false);

  private void ThrowIfNotDisposed()
  {
    if (this.Items.Count != 0)
      throw new PXException($"SqlConnection has not been disposed. {(PXConnectionList.IsStackTraceEnabled ? (object) $"Connection has been created {this.Items.First<KeyValuePair<DbConnection, StackTrace>>().Value}" : (object) "Use the IsConnectionStackTraceEnabled parameter in web.config to get allocation stack.")}");
  }

  private void Add(DbConnection c)
  {
    StackTrace stackTrace = (StackTrace) null;
    if (PXConnectionList.IsStackTraceEnabled)
      stackTrace = PXStackTrace.GetStackTrace();
    this.Items.Add(c, stackTrace);
    c.Disposed += (EventHandler) ((_param1, _param2) => this.Items.Remove(c));
  }

  internal static void AddConnection(DbConnection result)
  {
    if (!PXConnectionList.IsEnabled)
      return;
    PXConnectionList pxConnectionList = PXContext.GetSlot<PXConnectionList>();
    if (pxConnectionList == null)
    {
      pxConnectionList = new PXConnectionList();
      PXContext.SetSlot<PXConnectionList>(pxConnectionList);
    }
    pxConnectionList.Add(result);
  }

  public static void CheckConnections()
  {
    if (!PXConnectionList.IsEnabled)
      return;
    PXContext.GetSlot<PXConnectionList>()?.ThrowIfNotDisposed();
  }
}
