// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.MsmqExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Messaging;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace PX.Data.PushNotifications;

internal static class MsmqExtensions
{
  public static bool PeekWithCancellation(
    this MessageQueue queue,
    CancellationToken cancellationToken,
    out Message message,
    TimeSpan? timeout = null,
    System.Action<long> heartBeatDelegate = null)
  {
    int seconds = 5;
    double num = timeout.HasValue ? timeout.GetValueOrDefault().TotalSeconds / (double) seconds : (double) int.MaxValue;
    message = (Message) null;
    for (int index = 0; (double) index <= num; ++index)
    {
      if (heartBeatDelegate != null)
        heartBeatDelegate(System.DateTime.UtcNow.Ticks);
      if (queue.GetCount() > 0)
      {
        message = queue.Peek(new TimeSpan(0, 0, seconds));
        return true;
      }
      if (cancellationToken.WaitHandle.WaitOne(new TimeSpan(0, 0, seconds)))
        return false;
    }
    return false;
  }

  public static bool MoveNextWithCancellation(
    this MessageEnumerator queueObserver,
    CancellationToken cancellationToken,
    TimeSpan? timeout,
    System.Action<long> heartBeatDelegate)
  {
    double num = timeout.HasValue ? timeout.GetValueOrDefault().TotalSeconds / 10.0 : (double) int.MaxValue;
    for (int index = 0; (double) index < num; ++index)
    {
      if (heartBeatDelegate != null)
        heartBeatDelegate(System.DateTime.UtcNow.Ticks);
      if (queueObserver.MoveNext(new TimeSpan(0, 0, 10)))
        return true;
      if (cancellationToken.IsCancellationRequested)
        return false;
    }
    return false;
  }

  internal static bool IsPathLocal(string queuePath)
  {
    string a = queuePath.Substring(0, queuePath.IndexOf('\\'));
    return string.Equals(a, ".", StringComparison.OrdinalIgnoreCase) || string.Equals(a, Environment.MachineName, StringComparison.OrdinalIgnoreCase);
  }

  public static int GetCount(this MessageQueue self)
  {
    string str = $"{self.MachineName}\\{self.QueueName}";
    if (!MsmqExtensions.IsPathLocal(str))
      return 1;
    if (!MessageQueue.Exists(str))
      return 0;
    NativeMethods.MQMGMTPROPS mgmtProps = new NativeMethods.MQMGMTPROPS()
    {
      cProp = 1
    };
    try
    {
      mgmtProps.aPropID = Marshal.AllocHGlobal(4);
      Marshal.WriteInt32(mgmtProps.aPropID, 7);
      mgmtProps.aPropVar = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (NativeMethods.MQPROPVariant)));
      Marshal.StructureToPtr<NativeMethods.MQPROPVariant>(new NativeMethods.MQPROPVariant()
      {
        vt = (byte) 1
      }, mgmtProps.aPropVar, false);
      mgmtProps.status = Marshal.AllocHGlobal(4);
      Marshal.WriteInt32(mgmtProps.status, 0);
      if (NativeMethods.MQMgmtGetInfo((string) null, "queue=" + self.FormatName, ref mgmtProps) != 0 || Marshal.ReadInt32(mgmtProps.status) != 0)
        return 0;
      NativeMethods.MQPROPVariant structure = (NativeMethods.MQPROPVariant) Marshal.PtrToStructure(mgmtProps.aPropVar, typeof (NativeMethods.MQPROPVariant));
      return structure.vt != (byte) 19 ? 0 : Convert.ToInt32(structure.ulVal);
    }
    finally
    {
      Marshal.FreeHGlobal(mgmtProps.aPropID);
      Marshal.FreeHGlobal(mgmtProps.aPropVar);
      Marshal.FreeHGlobal(mgmtProps.status);
    }
  }

  public static long GetSize(this MessageQueue self)
  {
    string str = $"{self.MachineName}\\{self.QueueName}";
    if (!MsmqExtensions.IsPathLocal(str))
      return 1;
    if (!MessageQueue.Exists(str))
      return 0;
    NativeMethods.MQMGMTPROPS mgmtProps = new NativeMethods.MQMGMTPROPS()
    {
      cProp = 1
    };
    try
    {
      mgmtProps.aPropID = Marshal.AllocHGlobal(4);
      Marshal.WriteInt32(mgmtProps.aPropID, 8);
      mgmtProps.aPropVar = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (NativeMethods.MQPROPVariant)));
      Marshal.StructureToPtr<NativeMethods.MQPROPVariant>(new NativeMethods.MQPROPVariant()
      {
        vt = (byte) 1
      }, mgmtProps.aPropVar, false);
      mgmtProps.status = Marshal.AllocHGlobal(4);
      Marshal.WriteInt32(mgmtProps.status, 0);
      if (NativeMethods.MQMgmtGetInfo((string) null, "queue=" + self.FormatName, ref mgmtProps) != 0 || Marshal.ReadInt32(mgmtProps.status) != 0)
        return 0;
      NativeMethods.MQPROPVariant structure = (NativeMethods.MQPROPVariant) Marshal.PtrToStructure(mgmtProps.aPropVar, typeof (NativeMethods.MQPROPVariant));
      return structure.vt != (byte) 19 ? 0L : Convert.ToInt64(structure.ulVal);
    }
    finally
    {
      Marshal.FreeHGlobal(mgmtProps.aPropID);
      Marshal.FreeHGlobal(mgmtProps.aPropVar);
      Marshal.FreeHGlobal(mgmtProps.status);
    }
  }
}
