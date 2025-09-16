// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionForXmlReference
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

#nullable disable
namespace PX.Data.SQLTree;

internal class ProjectionForXmlReference : ProjectionItem
{
  private readonly ProjectionItem _innerItem;
  private readonly bool _singleItem;

  public ProjectionForXmlReference(ProjectionItem innerItem, System.Type type)
  {
    this.type_ = type;
    this._innerItem = innerItem;
    this._singleItem = !typeof (IEnumerable).IsAssignableFrom(type);
  }

  public int Skip { get; set; }

  public int Top { get; set; }

  public override string ToString() => "FOR XML: " + this._innerItem?.ToString();

  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    XContainer xcontainer = data.GetXmlContainer(position++);
    IList emtpyList = this.GetEmtpyList();
    using (new PXInvariantCultureScope())
    {
      if (xcontainer != null)
      {
        if (xcontainer is XDocument xdocument)
          xcontainer = (XContainer) xdocument.Root;
        if (xcontainer != null)
        {
          int num = -1;
          foreach (XElement element in xcontainer.Elements())
          {
            ++num;
            if (this.Skip <= 0 || num >= this.Skip)
            {
              if (this.Top > 0)
              {
                if (num - this.Skip >= this.Top)
                  break;
              }
              PXDataRecord xmlRecord = PXDatabase.Provider.CreateXmlRecord(element);
              int position1 = 0;
              object obj = this._innerItem.GetValue(xmlRecord, ref position1, context);
              emtpyList.Add(obj);
              if (this._singleItem)
                break;
            }
          }
        }
      }
    }
    return this._singleItem ? (emtpyList.Count <= 0 ? (object) null : emtpyList[0]) : (typeof (IQueryable).IsAssignableFrom(this.type_) ? (object) emtpyList.AsQueryable() : (object) emtpyList);
  }

  protected override object CloneValueInternal(object value, CloneContext context)
  {
    if (value == null)
      return (object) null;
    if (this._singleItem)
      return this._innerItem.CloneValue(value, context);
    IList emtpyList = this.GetEmtpyList();
    foreach (object obj in (IEnumerable) value)
      emtpyList.Add(this._innerItem.CloneValue(obj, context));
    return typeof (IQueryable).IsAssignableFrom(this.type_) ? (object) emtpyList.AsQueryable() : (object) emtpyList;
  }

  public override IEnumerable<object> GetEmptyResult()
  {
    IList emtpyList = this.GetEmtpyList();
    return typeof (IQueryable).IsAssignableFrom(this.type_) ? (IEnumerable<object>) new object[1]
    {
      (object) emtpyList.AsQueryable()
    } : (IEnumerable<object>) new object[1]
    {
      (object) emtpyList
    };
  }

  private IList GetEmtpyList()
  {
    return (IList) ((IEnumerable<ConstructorInfo>) typeof (List<>).MakeGenericType(this._innerItem.type_).GetConstructors()).First<ConstructorInfo>((Func<ConstructorInfo, bool>) (c => c.GetParameters().Length == 0)).Invoke(new object[0]);
  }
}
