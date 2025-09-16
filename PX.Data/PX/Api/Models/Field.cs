// Decompiled with JetBrains decompiler
// Type: PX.Api.Models.Field
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Api.Models;

public class Field : Command
{
  public bool IsSelectorField
  {
    get
    {
      return this.Descriptor.ElementType == ElementTypes.ExplicitSelector || this.Descriptor.ElementType == ElementTypes.StringSelector;
    }
  }

  public Field SelectorKeyField()
  {
    try
    {
      return this.Descriptor.Container.SelectorKeyField();
    }
    catch (Exception ex)
    {
      string format = "Unable to get SelectorKeyField from field " + this.Name;
      object[] objArray = Array.Empty<object>();
      PXTrace.WriteError(ex, format, objArray);
      throw;
    }
  }

  public Field SelectorDescriptionField(bool throwIfNotFound = true)
  {
    try
    {
      return this.Descriptor.Container.SelectorDescriptionField(throwIfNotFound);
    }
    catch (Exception ex)
    {
      string format = "Unable to get SelectorKeyField from field " + this.Name;
      object[] objArray = Array.Empty<object>();
      PXTrace.WriteError(ex, format, objArray);
      throw;
    }
  }

  public bool HasSelectorKey()
  {
    ElementDescriptor descriptor = this.Descriptor;
    if (descriptor == null)
      return false;
    bool? nullable = descriptor.Container?.HasSelectorKey();
    bool flag = true;
    return nullable.GetValueOrDefault() == flag & nullable.HasValue;
  }

  public bool HasSelectorDescription()
  {
    ElementDescriptor descriptor = this.Descriptor;
    if (descriptor == null)
      return false;
    bool? nullable = descriptor.Container?.HasSelectorDescription();
    bool flag = true;
    return nullable.GetValueOrDefault() == flag & nullable.HasValue;
  }
}
