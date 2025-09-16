// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DACHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.FS;

public class DACHelper
{
  public static List<string> GetFieldsName(Type dacType)
  {
    List<string> fieldsName = new List<string>();
    foreach (PropertyInfo property in dacType.GetProperties())
    {
      if (((IEnumerable<object>) property.GetCustomAttributes(true)).Where<object>((Func<object, bool>) (atr => atr is SkipSetExtensionVisibleInvisibleAttribute)).Count<object>() == 0)
        fieldsName.Add(property.Name);
    }
    return fieldsName;
  }

  public static void SetExtensionVisibleInvisible(
    Type dacType,
    PXCache cache,
    PXRowSelectedEventArgs e,
    bool isVisible,
    bool isGrid)
  {
    foreach (string str in DACHelper.GetFieldsName(dacType))
      PXUIFieldAttribute.SetVisible(cache, (object) null, str, isVisible);
  }

  public static string GetDisplayName(Type inpDAC)
  {
    string name = inpDAC.Name;
    if (inpDAC.IsDefined(typeof (PXCacheNameAttribute), true))
      name = ((PXNameAttribute) inpDAC.GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0]).GetName();
    return name;
  }
}
