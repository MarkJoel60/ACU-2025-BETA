// Decompiled with JetBrains decompiler
// Type: PX.Common.PXStackTrace
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

#nullable enable
namespace PX.Common;

public static class PXStackTrace
{
  public static Func<int, object> GetThreadLockObject = new Func<int, object>(PXStackTrace.\u0002.\u0002.\u0002);
  private static Action<StackTrace, int>? \u0002 = (Action<StackTrace, int>) null;

  public static StackTrace GetStackTrace(int skipFrames = 0, bool needFileInfo = false)
  {
    lock (PXStackTrace.GetThreadLockObject(Thread.CurrentThread.ManagedThreadId))
      return new StackTrace(skipFrames, needFileInfo);
  }

  public static StackTrace? GetStackTrace(Thread? targetThread)
  {
    if (targetThread == null || !targetThread.IsAlive)
      return (StackTrace) null;
    StackTrace stackTrace = (StackTrace) null;
    lock (PXStackTrace.GetThreadLockObject(targetThread.ManagedThreadId))
    {
      targetThread.Suspend();
      try
      {
        stackTrace = new StackTrace(targetThread, true);
      }
      catch
      {
      }
      finally
      {
        try
        {
          targetThread.Resume();
        }
        catch
        {
          stackTrace = (StackTrace) null;
        }
      }
    }
    return stackTrace;
  }

  public static StackTrace GetStackTrace(Exception ex)
  {
    lock (PXStackTrace.GetThreadLockObject(Thread.CurrentThread.ManagedThreadId))
      return new StackTrace(ex);
  }

  internal static string RemoveSystemMethods(StackTrace _param0)
  {
    if (PXStackTrace.\u0002 == null)
    {
      try
      {
        ParameterExpression parameterExpression;
        PXStackTrace.\u0002 = ((Expression<Action<StackTrace, int>>) ((target, value) => Expression.Assign((Expression) Expression.Field((Expression) parameterExpression, typeof (StackTrace).GetField("m_iNumOfFrames", BindingFlags.Instance | BindingFlags.NonPublic)), value))).Compile();
      }
      catch
      {
        PXStackTrace.\u0002 = PXStackTrace.\u0002.\u000E ?? (PXStackTrace.\u0002.\u000E = new Action<StackTrace, int>(PXStackTrace.\u0002.\u0002.\u0002));
      }
    }
    _param0.GetFrames();
    int index;
    for (index = _param0.FrameCount - 1; index >= 0; --index)
    {
      Type declaringType = _param0.GetFrame(index).GetMethod().DeclaringType;
      if (!(declaringType != (Type) null) || string.IsNullOrEmpty(declaringType.FullName) || !declaringType.FullName.StartsWith("System."))
        break;
    }
    PXStackTrace.\u0002(_param0, index + 1);
    return _param0.ToString();
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly 
    #nullable disable
    PXStackTrace.\u0002 \u0002 = new PXStackTrace.\u0002();
    public static Action<
    #nullable enable
    StackTrace, int> \u000E;

    internal void \u0002(StackTrace _param1, int _param2)
    {
    }

    internal object \u0002(int _param1) => new object();
  }
}
