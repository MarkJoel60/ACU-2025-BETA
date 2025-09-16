// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSubItemSegmentValueList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.IN;

public class INSubItemSegmentValueList : 
  PXSelect<
  #nullable disable
  INSubItemSegmentValue, Where<INSubItemSegmentValue.inventoryID, Equal<Current<InventoryItem.inventoryID>>>>
{
  /// <summary>String pattern for dynamic Subitem views</summary>
  public const string SubItemViewsPattern = "SubItem_";

  /// <summary>
  /// Gets the number of Subitem segments if the appropriate feature is on,
  /// otherwise returns <c>null</c>
  /// </summary>
  public int? SegmentsNumber { get; protected set; }

  public INSubItemSegmentValueList(PXGraph graph)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    INSubItemSegmentValueList.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new INSubItemSegmentValueList.\u003C\u003Ec__DisplayClass6_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass60.graph = graph;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: explicit constructor call
    base.\u002Ector(cDisplayClass60.graph);
    List<int> utilizedSegmentNumbers = new List<int>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass60.graph.Caches[typeof (INSubItemSegmentValueList.SValue)].AllowInsert = cDisplayClass60.graph.Caches[typeof (INSubItemSegmentValueList.SValue)].AllowDelete = false;
    if (!PXAccess.FeatureInstalled<FeaturesSet.subItem>())
    {
      ((PXSelectBase) this).AllowSelect = false;
      // ISSUE: reference to a compiler-generated field
      INSubItemSegmentValueList.GenerateDummyViews(cDisplayClass60.graph, utilizedSegmentNumbers);
    }
    else
    {
      this.SegmentsNumber = new int?(0);
      // ISSUE: reference to a compiler-generated field
      foreach (PXResult<Segment> pxResult in PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<SubItemAttribute.dimensionName>>, OrderBy<Asc<Segment.segmentID>>>.Config>.Select(cDisplayClass60.graph, Array.Empty<object>()))
      {
        Segment segment = PXResult<Segment>.op_Implicit(pxResult);
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        INSubItemSegmentValueList.\u003C\u003Ec__DisplayClass6_1 cDisplayClass61 = new INSubItemSegmentValueList.\u003C\u003Ec__DisplayClass6_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass61.CS\u0024\u003C\u003E8__locals1 = cDisplayClass60;
        int? segmentsNumber = this.SegmentsNumber;
        this.SegmentsNumber = segmentsNumber.HasValue ? new int?(segmentsNumber.GetValueOrDefault() + 1) : new int?();
        short? segmentId = segment.SegmentID;
        int? nullable = segmentId.HasValue ? new int?((int) segmentId.GetValueOrDefault()) : new int?();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass61.segmentID = nullable;
        // ISSUE: reference to a compiler-generated field
        utilizedSegmentNumbers.Add(cDisplayClass61.segmentID.GetValueOrDefault());
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        cDisplayClass61.CS\u0024\u003C\u003E8__locals1.graph.Views.Add("DimensionsSubItem", new PXView(cDisplayClass61.CS\u0024\u003C\u003E8__locals1.graph, false, BqlCommand.CreateInstance(new Type[1]
        {
          typeof (Select<Segment, Where<Segment.dimensionID, Equal<SubItemAttribute.dimensionName>>, OrderBy<Asc<Segment.segmentID>>>)
        })));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        PXView pxView1 = new PXView(cDisplayClass61.CS\u0024\u003C\u003E8__locals1.graph, false, BqlCommand.CreateInstance(new Type[1]
        {
          typeof (Select<INSubItemSegmentValueList.SValue>)
        }), (Delegate) new PXSelectDelegate((object) cDisplayClass61, __methodptr(\u003C\u002Ector\u003Eb__0)));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        PXViewCollection views = cDisplayClass61.CS\u0024\u003C\u003E8__locals1.graph.Views;
        segmentId = segment.SegmentID;
        string str = "SubItem_" + segmentId.ToString();
        PXView pxView2 = pxView1;
        views.Add(str, pxView2);
        PXDependToCacheAttribute.AddDependencies(pxView1, new Type[1]
        {
          typeof (InventoryItem)
        });
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        PXGraph.RowUpdatedEvents rowUpdated = cDisplayClass61.CS\u0024\u003C\u003E8__locals1.graph.RowUpdated;
        INSubItemSegmentValueList segmentValueList = this;
        // ISSUE: virtual method pointer
        PXRowUpdated pxRowUpdated = new PXRowUpdated((object) segmentValueList, __vmethodptr(segmentValueList, OnRowUpdated));
        rowUpdated.AddHandler<INSubItemSegmentValueList.SValue>(pxRowUpdated);
      }
      // ISSUE: reference to a compiler-generated field
      INSubItemSegmentValueList.GenerateDummyViews(cDisplayClass60.graph, utilizedSegmentNumbers);
    }
  }

  private static void GenerateDummyViews(PXGraph graph, List<int> utilizedSegmentNumbers)
  {
    for (int index = 1; index <= 10; ++index)
    {
      if (!utilizedSegmentNumbers.Contains(index))
        graph.Views.Add("SubItem_" + index.ToString(), new PXView(graph, false, BqlCommand.CreateInstance(new Type[1]
        {
          typeof (Select<SegmentValue, Where<True, Equal<False>>>)
        }))
        {
          AllowDelete = false,
          AllowInsert = false,
          AllowSelect = false
        });
    }
  }

  protected virtual void OnRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is INSubItemSegmentValueList.SValue row))
      return;
    INSubItemSegmentValue instance = (INSubItemSegmentValue) ((PXSelectBase) this).Cache.CreateInstance();
    ((PXSelectBase) this).Cache.SetDefaultExt<INSubItemSegmentValue.inventoryID>((object) instance);
    instance.SegmentID = row.SegmentID;
    instance.Value = row.Value;
    if (row.Active.GetValueOrDefault())
      ((PXSelectBase) this).Cache.Update((object) instance);
    else
      ((PXSelectBase) this).Cache.Delete((object) instance);
  }

  [PXHidden]
  public class SValue : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected short? _SegmentID;
    protected string _Value;
    protected string _Descr;
    protected bool? _Active;

    [PXDBShort(IsKey = true)]
    [PXUIField]
    public virtual short? SegmentID
    {
      get => this._SegmentID;
      set => this._SegmentID = value;
    }

    [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
    [PXDefault]
    [PXUIField]
    public virtual string Value
    {
      get => this._Value;
      set => this._Value = value;
    }

    [PXDBString(60, IsUnicode = true)]
    [PXUIField]
    public virtual string Descr
    {
      get => this._Descr;
      set => this._Descr = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField]
    public virtual bool? Active
    {
      get => this._Active;
      set => this._Active = value;
    }

    public abstract class segmentID : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      INSubItemSegmentValueList.SValue.segmentID>
    {
    }

    public abstract class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INSubItemSegmentValueList.SValue.value>
    {
    }

    public abstract class descr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INSubItemSegmentValueList.SValue.descr>
    {
    }

    public abstract class active : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      INSubItemSegmentValueList.SValue.active>
    {
    }
  }
}
