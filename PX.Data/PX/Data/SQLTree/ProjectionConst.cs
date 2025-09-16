// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionConst
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.SQLTree;

internal class ProjectionConst : ProjectionItem
{
  public ProjectionConst(System.Type t) => this.type_ = t;

  public override string ToString()
  {
    int length = this.type_.FullName.IndexOf(", ");
    return length <= 0 ? this.type_.Name : this.type_.FullName.Substring(0, length);
  }

  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    if (this.type_ == typeof (bool) || this.type_ == typeof (bool?))
      return (object) data.GetBoolean(position++);
    object obj = data.GetValue(position++);
    if (obj == null || obj.GetType() == this.type_)
      return obj;
    if (this.type_.IsEnum)
      return Enum.ToObject(this.type_, obj);
    System.Type type = Nullable.GetUnderlyingType(this.type_);
    if ((object) type == null)
      type = this.type_;
    System.Type conversionType = type;
    if (obj is IConvertible && (conversionType.IsPrimitive || conversionType.IsValueType || conversionType == typeof (string)))
      return conversionType == typeof (Guid) && obj is string input ? (object) Guid.Parse(input) : Convert.ChangeType(obj, conversionType, (IFormatProvider) PXCultureInfo.InvariantCulture);
    if (conversionType == typeof (string))
      return (object) obj.ToString();
    return Activator.CreateInstance(this.type_, obj);
  }

  protected override object CloneValueInternal(object value, CloneContext context)
  {
    return value is ICloneable cloneable ? cloneable.Clone() : value;
  }
}
