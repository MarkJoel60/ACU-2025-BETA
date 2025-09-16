// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionNew
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Reflection;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

internal class ProjectionNew : ProjectionItem
{
  private ConstructorInfo ctor_;
  private int size_;
  private ProjectionItem[] elements_;
  private string[] names_;

  public ProjectionNew(ConstructorInfo ctor, int size)
  {
    this.type_ = ctor.DeclaringType;
    this.size_ = size;
    this.ctor_ = ctor;
    this.elements_ = new ProjectionItem[this.size_];
    this.names_ = new string[this.size_];
  }

  public void SetElement(int idx, string name, ProjectionItem item)
  {
    this.names_[idx] = name;
    this.elements_[idx] = item;
  }

  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    object[] parameters = new object[this.elements_.Length];
    for (int index = 0; index < this.elements_.Length; ++index)
      parameters[index] = this.elements_[index].GetValue(data, ref position, context);
    return this.ctor_.Invoke(parameters);
  }

  protected override object CloneValueInternal(object value, CloneContext context)
  {
    object[] parameters = new object[this.elements_.Length];
    for (int index = 0; index < this.elements_.Length; ++index)
    {
      object obj = this.type_.GetProperty(this.names_[index]).GetValue(value);
      parameters[index] = this.elements_[index].CloneValue(obj, context);
    }
    return this.ctor_.Invoke(parameters);
  }

  internal override object Transform(
    object value,
    Func<System.Type, object, bool> predicate,
    Func<object, object> map)
  {
    if (predicate(this.type_, value))
      return map(value);
    object[] parameters = new object[this.elements_.Length];
    for (int index = 0; index < this.elements_.Length; ++index)
    {
      object obj = this.type_.GetProperty(this.names_[index]).GetValue(value);
      parameters[index] = this.elements_[index].Transform(obj, predicate, map);
    }
    return this.ctor_.Invoke(parameters);
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("new {");
    for (int index = 0; index < this.elements_.Length; ++index)
    {
      if (index > 0)
        stringBuilder.Append(", ");
      if (this.elements_[index] != null && this.names_[index] != null)
        stringBuilder.Append($"{this.names_[index]} = {this.elements_[index]}");
    }
    stringBuilder.Append("}");
    return stringBuilder.ToString();
  }
}
