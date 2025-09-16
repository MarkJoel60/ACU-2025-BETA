// Decompiled with JetBrains decompiler
// Type: PX.Api.Helpers.Error
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Data.OData;
using PX.Data;
using System;
using System.Configuration;

#nullable disable
namespace PX.Api.Helpers;

public static class Error
{
  private static string Format(string message, params object[] args)
  {
    return PXLocalizer.LocalizeFormat(message, args);
  }

  public static ODataException OData(string message, params object[] args)
  {
    return new ODataException(Error.Format(message, args));
  }

  public static ODataException OData(
    string message,
    Exception innerException,
    params object[] args)
  {
    return new ODataException(Error.Format(message, args), innerException);
  }

  public static ConfigurationErrorsException Configuration(string message, params object[] args)
  {
    return new ConfigurationErrorsException(Error.Format(message, args));
  }
}
