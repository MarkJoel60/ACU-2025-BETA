// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.NativeMethods
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.InteropServices;
using System.Transactions;

#nullable disable
namespace PX.Data.PushNotifications;

/// <exclude />
public static class NativeMethods
{
  public const int MQ_MOVE_ACCESS = 4;
  public const int MQ_DENY_NONE = 0;
  public const byte VT_NULL = 1;
  public const byte VT_UI4 = 19;
  public const int PROPID_MGMT_QUEUE_MESSAGE_COUNT = 7;
  public const int PROPID_MGMT_QUEUE_BYTES_IN_QUEUE = 8;

  [DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
  public static extern int MQOpenQueue(
    string formatName,
    int access,
    int shareMode,
    ref IntPtr hQueue);

  [DllImport("mqrt.dll")]
  public static extern int MQCloseQueue(IntPtr queue);

  [DllImport("mqrt.dll")]
  public static extern int MQMoveMessage(
    IntPtr sourceQueue,
    IntPtr targetQueue,
    long lookupID,
    IDtcTransaction transaction);

  [DllImport("mqrt.dll")]
  internal static extern int MQMgmtGetInfo(
    [MarshalAs(UnmanagedType.BStr)] string computerName,
    [MarshalAs(UnmanagedType.BStr)] string objectName,
    ref NativeMethods.MQMGMTPROPS mgmtProps);

  internal struct MQPROPVariant
  {
    public byte vt;
    public byte spacer;
    public short spacer2;
    public int spacer3;
    public uint ulVal;
    public int spacer4;
  }

  internal struct MQMGMTPROPS
  {
    public uint cProp;
    public IntPtr aPropID;
    public IntPtr aPropVar;
    public IntPtr status;
  }
}
