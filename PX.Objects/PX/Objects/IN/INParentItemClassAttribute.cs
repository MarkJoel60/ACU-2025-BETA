// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INParentItemClassAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// The attribute is supposed to find and assign Parent Item Class for a newly created Child Item Class.
/// </summary>
public class INParentItemClassAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldDefaultingSubscriber,
  IPXRowInsertedSubscriber
{
  protected readonly bool _DefaultStkItemFromParent;

  public bool InsertCurySettings { get; set; }

  public INParentItemClassAttribute(bool defaultStkItemFromParent = false)
  {
    this._DefaultStkItemFromParent = defaultStkItemFromParent;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!this._DefaultStkItemFromParent)
      return;
    PXGraph.FieldDefaultingEvents fieldDefaulting = sender.Graph.FieldDefaulting;
    INParentItemClassAttribute itemClassAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) itemClassAttribute, __vmethodptr(itemClassAttribute, StkItemDefaulting));
    fieldDefaulting.AddHandler<INItemClass.stkItem>(pxFieldDefaulting);
  }

  protected virtual void StkItemDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    INItemClass inItemClass = this.LookupNearestParent(sender, e);
    e.NewValue = (object) (inItemClass != null ? inItemClass.StkItem : new bool?(true));
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    INItemClass inItemClass = this.LookupNearestParent(sender, e);
    if (inItemClass != null)
      e.NewValue = (object) inItemClass.ItemClassID;
    else
      this.ItemClassDefaulting(sender, e);
  }

  protected virtual void ItemClassDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    INSetup inSetup = PXResultset<INSetup>.op_Implicit(PXSelectBase<INSetup, PXSelectReadonly<INSetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
    if (inSetup == null)
      return;
    bool? nullable1 = (bool?) sender.GetValue<INItemClass.stkItem>(e.Row);
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    bool? nullable2 = nullable1;
    bool flag = false;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local = (ValueType) (nullable2.GetValueOrDefault() == flag & nullable2.HasValue ? inSetup.DfltNonStkItemClassID : inSetup.DfltStkItemClassID);
    defaultingEventArgs.NewValue = (object) local;
  }

  protected virtual INItemClass LookupNearestParent(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    string str1 = ((string) sender.GetValue<INItemClass.itemClassCD>(e.Row) ?? string.Empty).Trim();
    if (string.IsNullOrEmpty(str1))
      return (INItemClass) null;
    Segment[] array = GraphHelper.RowCast<Segment>((IEnumerable) PXSelectBase<Segment, PXSelectReadonly<Segment, Where<Segment.dimensionID, Equal<INItemClass.dimension>>, OrderBy<Asc<Segment.segmentID>>>.Config>.Select(sender.Graph, Array.Empty<object>())).ToArray<Segment>();
    if (array.Length == 0)
      return (INItemClass) null;
    int[] numArray = new int[array.Length];
    int num = 0;
    for (int index = 0; index < array.Length; ++index)
    {
      numArray[index] = (int) array[index].Length.Value + (index == 0 ? 0 : numArray[index - 1]);
      if (str1.Length > numArray[index])
        ++num;
    }
    INItemClass inItemClass;
    for (inItemClass = (INItemClass) null; inItemClass == null && num > 0; --num)
    {
      int length = numArray[num - 1];
      string str2 = str1.Substring(0, length);
      inItemClass = PXResultset<INItemClass>.op_Implicit(PXSelectBase<INItemClass, PXSelectReadonly<INItemClass, Where<INItemClass.itemClassCD, Equal<Required<INItemClass.itemClassCD>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) str2
      }));
    }
    return inItemClass;
  }

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!this.InsertCurySettings)
      return;
    this.CopyCurySettings(sender.Graph, e.Row as INItemClass);
  }

  public virtual void CopyCurySettings(PXGraph graph, INItemClass itemClass)
  {
    if (string.IsNullOrEmpty(itemClass?.ItemClassCD))
      return;
    FbqlSelect<SelectFromBase<INItemClassCurySettings, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClassCurySettings.itemClassID, IBqlInt>.IsEqual<P.AsInt>>, INItemClassCurySettings>.View view = new FbqlSelect<SelectFromBase<INItemClassCurySettings, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClassCurySettings.itemClassID, IBqlInt>.IsEqual<P.AsInt>>, INItemClassCurySettings>.View(graph);
    PXCache cache = ((PXSelectBase) view).Cache;
    using (new ReadOnlyScope(new PXCache[1]{ cache }))
    {
      foreach (PXResult<INItemClassCurySettings> pxResult in ((PXSelectBase<INItemClassCurySettings>) view).Select(new object[1]
      {
        (object) itemClass.ItemClassID
      }))
        cache.Delete((object) pxResult);
      List<INItemClassCurySettings> list = GraphHelper.RowCast<INItemClassCurySettings>((IEnumerable) ((PXSelectBase<INItemClassCurySettings>) view).Select(new object[1]
      {
        (object) itemClass.ParentItemClassID
      })).ToList<INItemClassCurySettings>();
      if (!list.Any<INItemClassCurySettings>((Func<INItemClassCurySettings, bool>) (s => string.Equals(s.CuryID, graph.Accessinfo.BaseCuryID, StringComparison.OrdinalIgnoreCase))))
        list.Add(new INItemClassCurySettings()
        {
          CuryID = graph.Accessinfo.BaseCuryID
        });
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PXFieldVerifying pxFieldVerifying = INParentItemClassAttribute.\u003C\u003Ec.\u003C\u003E9__12_1 ?? (INParentItemClassAttribute.\u003C\u003Ec.\u003C\u003E9__12_1 = new PXFieldVerifying((object) INParentItemClassAttribute.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCopyCurySettings\u003Eb__12_1)));
      graph.FieldVerifying.AddHandler<INItemClassCurySettings.dfltSiteID>(pxFieldVerifying);
      try
      {
        foreach (INItemClassCurySettings classCurySettings in list)
        {
          INItemClassCurySettings copy = (INItemClassCurySettings) cache.CreateCopy((object) classCurySettings);
          copy.ItemClassID = itemClass.ItemClassID;
          if ((INItemClassCurySettings) cache.Insert((object) copy) == null)
            throw new PXException("Copying settings from the selected item class has completed with errors; some settings have not been copied. Try to select the item class again and save the changes.");
        }
      }
      finally
      {
        graph.FieldVerifying.RemoveHandler<INItemClassCurySettings.dfltSiteID>(pxFieldVerifying);
      }
    }
  }
}
