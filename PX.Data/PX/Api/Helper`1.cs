// Decompiled with JetBrains decompiler
// Type: PX.Api.Helper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Reflection;
using System.Web.Services.Protocols;

#nullable disable
namespace PX.Api;

public static class Helper<ServiceGate> where ServiceGate : SoapHttpClientProtocol, new()
{
  private static readonly MethodInfo invokeMethod = typeof (SoapHttpClientProtocol).GetMethod("Invoke", BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new System.Type[2]
  {
    typeof (string),
    typeof (object[])
  }, (ParameterModifier[]) null);

  public static object Invoke(ServiceGate gate, string method, params object[] args)
  {
    try
    {
      return ((object[]) Helper<ServiceGate>.invokeMethod.Invoke((object) gate, new object[2]
      {
        (object) method,
        (object) args
      }))[0];
    }
    catch (TargetInvocationException ex)
    {
      throw PXException.ExtractInner((Exception) ex);
    }
  }
}
