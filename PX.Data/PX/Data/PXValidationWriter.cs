// Decompiled with JetBrains decompiler
// Type: PX.Data.PXValidationWriter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Web.Configuration;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXValidationWriter
{
  public static readonly bool? PageValidation = PXValidationWriter.GetPageValidation();
  public static readonly Dictionary<System.Type, HashSet<string>> TypeErrors = new Dictionary<System.Type, HashSet<string>>();

  private static bool? GetPageValidation()
  {
    string appSetting = WebConfigurationManager.AppSettings["PageValidation"];
    return string.IsNullOrEmpty(appSetting) ? new bool?() : new bool?(Convert.ToBoolean(appSetting));
  }

  public static void AddTypeError(System.Type t, string format, params object[] args)
  {
    bool? pageValidation = PXValidationWriter.PageValidation;
    bool flag = false;
    if (pageValidation.GetValueOrDefault() == flag & pageValidation.HasValue)
      return;
    if (!PXValidationWriter.TypeErrors.ContainsKey(t))
      PXValidationWriter.TypeErrors.Add(t, new HashSet<string>());
    PXValidationWriter.TypeErrors[t].Add(string.Format(format, args));
  }
}
