// Decompiled with JetBrains decompiler
// Type: PX.SM.FormFieldHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

internal static class FormFieldHelper
{
  /// <summary>
  /// Get DAC type and DAC field name from the form field name.
  /// </summary>
  public static bool TryGetFieldFromFormFieldName(
    PXGraph graph,
    string fieldName,
    out System.Type dacType,
    out string name)
  {
    dacType = (System.Type) null;
    name = (string) null;
    if (string.IsNullOrEmpty(fieldName))
      return false;
    int length = fieldName.LastIndexOf('.');
    if (length < 0)
    {
      dacType = graph.PrimaryItemType;
      name = fieldName;
      return true;
    }
    dacType = PXBuildManager.GetType(fieldName.Substring(0, length), false);
    name = fieldName.Substring(length + 1);
    return dacType != (System.Type) null;
  }
}
