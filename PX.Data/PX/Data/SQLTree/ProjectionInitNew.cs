// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionInitNew
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PX.Data.SQLTree;

internal class ProjectionInitNew : ProjectionItem
{
  private readonly ProjectionNew _projectionNew;
  private readonly ProjectionItem[] _elements;
  private readonly MemberInfo[] _members;

  public IDictionary<string, object> Constants { get; }

  public ProjectionInitNew(ProjectionNew projectionNew, int assigningsCount)
  {
    this.type_ = projectionNew.GetResultType();
    this._projectionNew = projectionNew;
    this._elements = new ProjectionItem[assigningsCount];
    this._members = new MemberInfo[assigningsCount];
    this.Constants = (IDictionary<string, object>) new Dictionary<string, object>();
  }

  public void SetMember(int idx, MemberInfo member, ProjectionItem item)
  {
    this._members[idx] = member;
    this._elements[idx] = item;
  }

  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    object obj1 = this._projectionNew.GetValue(data, ref position, context);
    for (int index = 0; index < this._members.Length; ++index)
    {
      object obj2;
      if (this.Constants.TryGetValue(this._members[index].Name, out obj2))
        ++position;
      else
        obj2 = this._elements[index].GetValue(data, ref position, context);
      PropertyInfo member = this._members[index] as PropertyInfo;
      if ((object) member != null)
        member.SetValue(obj1, obj2);
      else
        (this._members[index] as System.Reflection.FieldInfo ?? throw new PXNotSupportedException("MemberInfo of type {0} not supported", new object[1]
        {
          (object) this._members[index].GetType()
        })).SetValue(obj1, obj2);
    }
    return obj1;
  }

  protected override object CloneValueInternal(object value, CloneContext context)
  {
    object obj1 = this._projectionNew.CloneValue(value, context);
    for (int index = 0; index < this._members.Length; ++index)
    {
      PropertyInfo member1 = this._members[index] as PropertyInfo;
      if ((object) member1 != null)
      {
        object obj2 = member1.GetValue(value);
        object obj3 = this._elements[index].CloneValue(obj2, context);
        member1.SetValue(obj1, obj3);
      }
      else
      {
        System.Reflection.FieldInfo member2 = this._members[index] as System.Reflection.FieldInfo;
        object obj4 = (object) member2 != null ? member2.GetValue(value) : throw new PXNotSupportedException("MemberInfo of type {0} not supported", new object[1]
        {
          (object) this._members[index].GetType()
        });
        object obj5 = this._elements[index].CloneValue(obj4, context);
        member2.SetValue(obj1, obj5);
      }
    }
    return obj1;
  }

  internal override object Transform(
    object value,
    Func<System.Type, object, bool> predicate,
    Func<object, object> map)
  {
    object obj1 = base.Transform(value, predicate, map);
    for (int index = 0; index < this._members.Length; ++index)
    {
      PropertyInfo member1 = this._members[index] as PropertyInfo;
      if ((object) member1 != null)
      {
        object obj2 = member1.GetValue(value);
        object obj3 = this._elements[index].Transform(obj2, predicate, map);
        member1.SetValue(obj1, obj3);
      }
      else
      {
        System.Reflection.FieldInfo member2 = this._members[index] as System.Reflection.FieldInfo;
        object obj4 = (object) member2 != null ? member2.GetValue(value) : throw new PXNotSupportedException("MemberInfo of type {0} not supported", new object[1]
        {
          (object) this._members[index].GetType()
        });
        object obj5 = this._elements[index].Transform(obj4, predicate, map);
        member2.SetValue(obj1, obj5);
      }
    }
    return obj1;
  }

  public override string ToString() => this._projectionNew.ToString();
}
