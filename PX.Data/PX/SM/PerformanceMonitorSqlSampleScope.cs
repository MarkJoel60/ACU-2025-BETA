// Decompiled with JetBrains decompiler
// Type: PX.SM.PerformanceMonitorSqlSampleScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data;
using System;
using System.Diagnostics;
using System.Reflection;

#nullable disable
namespace PX.SM;

internal sealed class PerformanceMonitorSqlSampleScope : IDisposable
{
  private readonly string _parentScopeBqlHash;
  private readonly string _parentScopeBqlHashViewName;
  private const string BqlHashSlotKey = "BqlHash";
  private const string BqlHashViewNameSlotKey = "BqlHashViewName";

  public PerformanceMonitorSqlSampleScope(BqlCommand cmd, PXView view)
  {
    string str1 = PXReflectionSerializer.GetStableHash(cmd.GetType().FullName.ToString()).ToString("X");
    string str2 = (string) null;
    if (view != null)
    {
      if (view.Name != null)
      {
        str2 = $"{CustomizedTypeManager.GetTypeNotCustomized(view.Graph.GetType().FullName)}+{view.Name}";
      }
      else
      {
        StackTrace stackTrace = PXStackTrace.GetStackTrace();
        string str3 = typeof (PXGraph).FullName + "`";
        for (int index = 0; index < stackTrace.FrameCount; ++index)
        {
          System.Diagnostics.StackFrame frame = stackTrace.GetFrame(index);
          if (frame.HasMethod())
          {
            MethodBase method = frame.GetMethod();
            System.Type declaringType = method.DeclaringType;
            if ((object) declaringType != null)
            {
              bool? nullable = declaringType.BaseType?.FullName?.StartsWith(str3);
              bool flag = true;
              if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
                goto label_8;
            }
            if (!typeof (Attribute).IsAssignableFrom(declaringType))
              continue;
label_8:
            str2 = $"{declaringType.FullName}+{method.ToString()}";
            break;
          }
        }
      }
    }
    if (str2 != null && str2.StartsWith("PX.Objects."))
      str2 = str2.Substring("PX.Objects.".Length);
    this._parentScopeBqlHash = PerformanceMonitorSqlSampleScope.BqlHash;
    this._parentScopeBqlHashViewName = PerformanceMonitorSqlSampleScope.BqlHashViewName;
    PXContext.SetSlot<string>(nameof (BqlHash), str1);
    PXContext.SetSlot<string>(nameof (BqlHashViewName), str2);
  }

  internal static string BqlHash => PXContext.GetSlot<string>(nameof (BqlHash));

  internal static string BqlHashViewName => PXContext.GetSlot<string>(nameof (BqlHashViewName));

  public void Dispose()
  {
    PXContext.SetSlot<string>("BqlHash", this._parentScopeBqlHash);
    PXContext.SetSlot<string>("BqlHashViewName", this._parentScopeBqlHashViewName);
  }
}
