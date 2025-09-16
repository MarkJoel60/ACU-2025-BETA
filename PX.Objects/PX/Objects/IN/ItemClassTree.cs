// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.ItemClassTree
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Data.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN;

public class ItemClassTree : 
  DimensionTree<
  #nullable disable
  ItemClassTree, ItemClassTree.INItemClass, PX.Objects.IN.INItemClass.dimension, ItemClassTree.INItemClass.itemClassCD, ItemClassTree.INItemClass.itemClassID>
{
  public string GetFullItemClassDescription(string itemClassCD)
  {
    if (itemClassCD == null)
      return (string) null;
    itemClassCD = itemClassCD.TrimEnd();
    ItemClassTree.INItemClass nodeByCd = this.GetNodeByCD(itemClassCD);
    return nodeByCd == null ? (string) null : string.Join(" / ", this.GetParentsOf(itemClassCD).Reverse<ItemClassTree.INItemClass>().Append<ItemClassTree.INItemClass>(nodeByCd).Select<ItemClassTree.INItemClass, string>((Func<ItemClassTree.INItemClass, string>) (node => node.Descr ?? " ")));
  }

  protected virtual void PrepareElement(ItemClassTree.INItemClass original)
  {
    int num1 = 0;
    DimensionTree<PX.Objects.IN.INItemClass.dimension>.Segment[] segments = DimensionTree<PX.Objects.IN.INItemClass.dimension>.Segments;
    string str1 = DimensionTree<PX.Objects.IN.INItemClass.dimension>.PadKey(original.ItemClassCD, ((IEnumerable<DimensionTree<PX.Objects.IN.INItemClass.dimension>.Segment>) segments).Sum<DimensionTree<PX.Objects.IN.INItemClass.dimension>.Segment>((Func<DimensionTree<PX.Objects.IN.INItemClass.dimension>.Segment, int>) (s => (int) s.Length.Value)));
    foreach (DimensionTree<PX.Objects.IN.INItemClass.dimension>.Segment segment in ((IEnumerable<DimensionTree<PX.Objects.IN.INItemClass.dimension>.Segment>) segments).Take<DimensionTree<PX.Objects.IN.INItemClass.dimension>.Segment>(segments.Length - 1))
    {
      string str2 = str1;
      int num2 = num1;
      short? length = segment.Length;
      int num3 = (int) length.Value;
      int startIndex = num2 + num3;
      string separator = segment.Separator;
      str1 = str2.Insert(startIndex, separator);
      int num4 = num1;
      length = segment.Length;
      int num5 = (int) length.Value + segment.Separator.Length;
      num1 = num4 + num5;
    }
    original.SegmentedClassCD = $"{str1} {original.Descr}";
  }

  public class INItemClass : PX.Objects.IN.INItemClass
  {
    public virtual string SegmentedClassCD { get; set; }

    public new abstract class itemClassID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ItemClassTree.INItemClass.itemClassID>
    {
    }

    public new abstract class itemClassCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ItemClassTree.INItemClass.itemClassCD>
    {
    }

    public new abstract class stkItem : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ItemClassTree.INItemClass.stkItem>
    {
    }
  }
}
