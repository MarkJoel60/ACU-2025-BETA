// Decompiled with JetBrains decompiler
// Type: PX.CS.RMPreviewAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Text;

#nullable disable
namespace PX.CS;

public abstract class RMPreviewAttribute : PXStringAttribute
{
  private object SubstituteWithLabel(PXFieldState fieldState)
  {
    if (fieldState == null || fieldState.Value == null)
      return (object) null;
    switch (fieldState)
    {
      case PXStringState pxStringState:
        if (pxStringState.AllowedLabels == null)
          return pxStringState.Value;
        int index1 = Array.IndexOf<string>(pxStringState.AllowedValues, pxStringState.Value as string);
        return index1 < 0 ? pxStringState.Value : (object) pxStringState.AllowedLabels[index1];
      case PXIntState pxIntState:
        if (pxIntState.AllowedLabels == null)
          return pxIntState.Value;
        int[] allowedValues = pxIntState.AllowedValues;
        short? nullable = pxIntState.Value as short?;
        // ISSUE: variable of a boxed type
        __Boxed<int?> local = (ValueType) (nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : pxIntState.Value as int?);
        int index2 = Array.IndexOf((Array) allowedValues, (object) local);
        return index2 < 0 ? pxIntState.Value : (object) pxIntState.AllowedLabels[index2];
      default:
        return fieldState.Value;
    }
  }

  public string GetPreviewText(
    PXCache cache,
    RMDataSource dataSource,
    RMPreviewAttribute.PreviewItemDescriptor[] descriptors,
    string prefix = null)
  {
    StringBuilder descriptor1 = new StringBuilder("");
    if (!string.IsNullOrEmpty(prefix))
      RMPreviewAttribute.AppendProperty(descriptor1, "", prefix, (string) null);
    foreach (RMPreviewAttribute.PreviewItemDescriptor descriptor2 in descriptors)
    {
      PXFieldState stateExt1 = cache.GetStateExt((object) dataSource, descriptor2.FieldFrom) as PXFieldState;
      if (stateExt1.Value != null && !stateExt1.Value.Equals(descriptor2.FieldFromDefault))
      {
        PXFieldState stateExt2 = descriptor2.FieldTo != null ? cache.GetStateExt((object) dataSource, descriptor2.FieldTo) as PXFieldState : (PXFieldState) null;
        object obj1 = this.SubstituteWithLabel(stateExt1);
        object obj2 = this.SubstituteWithLabel(stateExt2);
        RMPreviewAttribute.AppendProperty(descriptor1, descriptor2.Label, obj1.ToString(), obj2?.ToString());
      }
    }
    return $"[{descriptor1}]";
  }

  public static void AppendProperty(
    StringBuilder descriptor,
    string field,
    string valueFrom,
    string valueTo)
  {
    if (string.IsNullOrEmpty(valueFrom))
      return;
    string str1;
    if (!string.IsNullOrEmpty(valueTo) && !(valueFrom == valueTo))
      str1 = $"{{\"{field}\": \"{string.Format(PXMessages.LocalizeNoPrefix("{0}-{1}"), (object) valueFrom.Trim(), (object) valueTo.Trim())}\"}}";
    else
      str1 = $"{{\"{field}\": \"{valueFrom.Trim()}\"}}";
    string str2 = str1;
    if (descriptor.Length > 0)
      descriptor.Append(",");
    descriptor.Append(str2);
  }

  public readonly struct PreviewItemDescriptor(
    string label,
    string fieldFrom,
    string fieldTo = null,
    object fieldFromDefault = null)
  {
    public string Label { get; } = label;

    public string FieldFrom { get; } = fieldFrom;

    public string FieldTo { get; } = fieldTo;

    public object FieldFromDefault { get; } = fieldFromDefault;
  }
}
