// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.PropertyAssigmentProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using System;
using System.Reflection;

#nullable disable
namespace PX.Api.Reports;

internal abstract class PropertyAssigmentProcessor : CommandProcessor<PX.Api.Models.Field>
{
  protected readonly Type TargetType;

  protected PropertyAssigmentProcessor(Type targetType) => this.TargetType = targetType;

  public override bool CanExecute(Command cmd)
  {
    if (!base.CanExecute(cmd))
      return false;
    object propertyOwner = this.GetPropertyOwner(cmd);
    return propertyOwner != null && propertyOwner.GetType().GetProperty(cmd.FieldName, BindingFlags.Instance | BindingFlags.Public) != (PropertyInfo) null;
  }

  protected abstract object GetPropertyOwner(Command cmd);

  public override void Execute(PX.Api.Models.Field cmd)
  {
    object propertyOwner = this.GetPropertyOwner((Command) cmd);
    string str = FieldDecoder.UnpackValue(cmd);
    PropertyInfo property = propertyOwner.GetType().GetProperty(cmd.FieldName, BindingFlags.Instance | BindingFlags.Public);
    property.SetValue(propertyOwner, this.ParsePrimitiveValue(property.PropertyType, str), (object[]) null);
  }

  private object ParsePrimitiveValue(Type t, string value)
  {
    if (t == typeof (string))
      return (object) value;
    if (typeof (DateTime?).IsAssignableFrom(t))
      return (object) DateTime.Parse(value);
    if (typeof (bool?).IsAssignableFrom(t))
      return (object) bool.Parse(value);
    if (typeof (int?).IsAssignableFrom(t))
      return (object) int.Parse(value);
    if (!t.IsEnum)
      throw new Exception("Unknown type to parse:" + t.Name);
    try
    {
      return Enum.Parse(t, value, true);
    }
    catch (ArgumentException ex)
    {
      throw new ArgumentException($"Can't parse the value {value} for the type {t}. ", (Exception) ex);
    }
  }
}
