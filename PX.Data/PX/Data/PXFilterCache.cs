// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFilterCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

internal sealed class PXFilterCache(PXGraph graph) : PXCache<FilterHeader>(graph)
{
  private string _ViewName;
  private string _ScreenID;

  public string ViewName
  {
    set => this._ViewName = value;
  }

  public string ScreenID
  {
    set => this._ScreenID = value;
  }

  private void clearDefault(Guid filterID)
  {
    foreach (PXResult<FilterHeader> pxResult in PXSelectBase<FilterHeader, PXSelect<FilterHeader, Where<FilterHeader.screenID, Equal<Required<FilterHeader.screenID>>, And<FilterHeader.viewName, Equal<Required<FilterHeader.viewName>>, And<FilterHeader.userName, Equal<Required<FilterHeader.userName>>>>>>.Config>.Select(this._Graph, (object) this._ScreenID, (object) this._ViewName, (object) this._Graph.Accessinfo.UserName))
    {
      FilterHeader filterHeader = (FilterHeader) pxResult;
      Guid guid = filterID;
      Guid? filterId = filterHeader.FilterID;
      if ((filterId.HasValue ? (guid != filterId.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      {
        bool? isDefault = filterHeader.IsDefault;
        bool flag = true;
        if (isDefault.GetValueOrDefault() == flag & isDefault.HasValue)
        {
          FilterHeader copy = PXCache<FilterHeader>.CreateCopy(filterHeader);
          copy.IsDefault = new bool?(false);
          this.Update(copy);
        }
      }
    }
  }

  public override object Delete(object data)
  {
    if (data is FilterHeader filterHeader)
    {
      bool? isSystem = filterHeader.IsSystem;
      bool flag = true;
      if (isSystem.GetValueOrDefault() == flag & isSystem.HasValue)
        throw new InvalidOperationException("The system filter cannot be deleted.");
      filterHeader.UserName = this._Graph.Accessinfo.UserName;
      filterHeader.ScreenID = this._ScreenID;
      filterHeader.ViewName = this._ViewName;
    }
    return base.Delete(data);
  }

  public override int Delete(IDictionary keys, IDictionary values)
  {
    if (keys != null)
    {
      keys[(object) "UserName"] = (object) this._Graph.Accessinfo.UserName;
      keys[(object) "ScreenID"] = (object) this._ScreenID;
      keys[(object) "ViewName"] = (object) this._ViewName;
      if (this.IsSystem(keys[(object) "FilterID"]))
        throw new InvalidOperationException("The system filter cannot be deleted.");
    }
    return base.Delete(keys, values);
  }

  public override object Update(object data)
  {
    if (data is FilterHeader filterHeader1)
    {
      if (this.IsSystem((object) filterHeader1.FilterID))
        throw new InvalidOperationException("The system filter cannot be updated.");
      filterHeader1.UserName = this._Graph.Accessinfo.UserName;
      filterHeader1.ScreenID = this._ScreenID;
      filterHeader1.ViewName = this._ViewName;
    }
    FilterHeader filterHeader2 = (FilterHeader) base.Update(data);
    if (filterHeader2 != null && filterHeader2.IsDefault.Value)
      this.clearDefault(filterHeader2.FilterID.Value);
    return (object) filterHeader2;
  }

  public override int Update(IDictionary keys, IDictionary values)
  {
    if (keys != null)
    {
      string[] array = keys.Keys.ToArray<string>();
      if (this.IsSystem(keys[(object) "FilterID"]))
        throw new InvalidOperationException("The system filter cannot be updated.");
      if (!((IEnumerable<string>) array).Contains<string>("FilterID"))
        keys[(object) "UserName"] = (object) this._Graph.Accessinfo.UserName;
      if (!((IEnumerable<string>) array).Contains<string>("ScreenID"))
        keys[(object) "ScreenID"] = (object) this._ScreenID;
      if (!((IEnumerable<string>) array).Contains<string>("ViewName"))
        keys[(object) "ViewName"] = (object) this._ViewName;
    }
    values?.Remove((object) "IsSystem");
    int num1 = base.Update(keys, values);
    if (num1 <= 0)
      return num1;
    PXFieldState pxFieldState = (PXFieldState) values[(object) "IsDefault"];
    bool flag = false;
    if (pxFieldState != null)
      flag = (bool) pxFieldState.Value;
    if (!flag)
      return num1;
    Guid filterID = (Guid) ((PXFieldState) values[(object) "FilterID"]).Value;
    int num2 = !values.Contains((object) "IsShared") ? 0 : ((values[(object) "IsShared"] as PXFieldState).Return<PXFieldState, bool>((Func<PXFieldState, bool>) (_ => (bool) _.Value), false) ? 1 : 0);
    this.clearDefault(filterID);
    return num1;
  }

  public override object Insert(object data)
  {
    if (data is FilterHeader filterHeader)
    {
      filterHeader.UserName = this._Graph.Accessinfo.UserName;
      filterHeader.ScreenID = this._ScreenID;
      filterHeader.ViewName = this._ViewName;
      filterHeader.IsSystem = new bool?(false);
    }
    object obj = base.Insert(data);
    if (obj != null && ((FilterHeader) obj).IsDefault.Value)
      this.clearDefault(((FilterHeader) obj).FilterID.Value);
    return obj;
  }

  public override int Insert(IDictionary values)
  {
    if (values != null)
    {
      values[(object) "UserName"] = (object) this._Graph.Accessinfo.UserName;
      values[(object) "ScreenID"] = (object) this._ScreenID;
      values[(object) "ViewName"] = (object) this._ViewName;
    }
    values?.Remove((object) "IsSystem");
    int num = base.Insert(values);
    if (num > 0)
    {
      PXFieldState pxFieldState = (PXFieldState) values[(object) "IsDefault"];
      bool flag = false;
      if (pxFieldState != null)
        flag = (bool) pxFieldState.Value;
      Guid filterID = (Guid) ((PXFieldState) values[(object) "FilterID"]).Value;
      FilterHeader data = (FilterHeader) null;
      foreach (FilterHeader filterHeader in this.Inserted)
      {
        Guid? filterId = filterHeader.FilterID;
        Guid guid = filterID;
        if ((filterId.HasValue ? (filterId.HasValue ? (filterId.GetValueOrDefault() == guid ? 1 : 0) : 1) : 0) != 0)
          data = filterHeader;
      }
      if (flag)
        this.clearDefault(filterID);
      if (data != null)
        values[(object) "FilterID"] = this.GetValueExt((object) data, "FilterID");
    }
    return num;
  }

  private bool IsSystem(object filterId)
  {
    return FilterHeader.Definition.Get().Any<FilterHeader>((Func<FilterHeader, bool>) (f =>
    {
      Guid? filterId1 = f.FilterID;
      Guid? nullable = (Guid?) filterId;
      return (filterId1.HasValue == nullable.HasValue ? (filterId1.HasValue ? (filterId1.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && f.IsSystem.GetValueOrDefault();
    }));
  }
}
