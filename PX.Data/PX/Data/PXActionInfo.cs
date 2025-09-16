// Decompiled with JetBrains decompiler
// Type: PX.Data.PXActionInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Reflection;

#nullable enable
namespace PX.Data;

[PXInternalUseOnly]
public class PXActionInfo(string name, string? display) : PXInfo(name, display)
{
  public string? Icon;
  public System.Type? ViewType;

  public PXActionInfo(string name, string? display, System.Type viewType)
    : this(name, display)
  {
    this.ViewType = viewType;
  }

  public PXActionInfo(System.Reflection.FieldInfo field)
    : this(field.Name, (string) null)
  {
    MethodInfo method = field.DeclaringType.GetMethod(field.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    if (method == (MethodInfo) null)
      method = field.FieldType.GetMethod("handler", BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    if (method != (MethodInfo) null)
    {
      foreach (PXEventSubscriberAttribute customAttribute in method.GetCustomAttributes(typeof (PXEventSubscriberAttribute), false))
      {
        switch (customAttribute)
        {
          case IPXInterfaceField _:
            this.DisplayName = PXLocalizer.Localize(((IPXInterfaceField) customAttribute).DisplayName, (string) null);
            break;
          case PXButtonAttribute _:
            this.Icon = ((PXButtonAttribute) customAttribute).ImageUrl;
            break;
        }
      }
    }
    if (this.DisplayName != null)
      return;
    this.DisplayName = field.Name;
  }
}
