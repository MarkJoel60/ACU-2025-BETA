// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.DimensionTree`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Data.Maintenance;

public class DimensionTree<TDimension> where TDimension : 
#nullable disable
IConstant<string>, IBqlOperand, new()
{
  protected static DimensionTree<TDimension>.Segment[] Segments
  {
    get => DimensionTree<TDimension>.GetSegmentsSlot().Segments;
  }

  private static DimensionTree<TDimension>.SegmentsSlot GetSegmentsSlot()
  {
    return PXContext.GetSlot<DimensionTree<TDimension>.SegmentsSlot>() ?? PXContext.SetSlot<DimensionTree<TDimension>.SegmentsSlot>(DimensionTree<TDimension>.GetSegmentsFromDBSlot());
  }

  private static DimensionTree<TDimension>.SegmentsSlot GetSegmentsFromDBSlot()
  {
    return PXDatabase.GetSlot<DimensionTree<TDimension>.SegmentsSlot>(typeof (DimensionTree<TDimension>.SegmentsSlot).FullName, DimensionTree<TDimension>.GetTablesToWatch());
  }

  protected static System.Type[] GetTablesToWatch()
  {
    return new System.Type[2]
    {
      typeof (DimensionTree<TDimension>.Dimension),
      typeof (DimensionTree<TDimension>.Segment)
    };
  }

  protected static IEnumerable<T> CloneSequence<T>(IEnumerable<T> source)
  {
    return PXReflectionSerializer.CloneSequence<T>(source);
  }

  protected static T Clone<T>(T source) => PXReflectionSerializer.Clone<T>(source);

  public static string MakeWildcard(string key)
  {
    return !string.IsNullOrEmpty(key) ? DimensionTree<TDimension>.AppendSpaces(key?.TrimEnd()) + PXDatabase.Provider.SqlDialect.WildcardAnything : (string) null;
  }

  public static string AppendSpaces(string key)
  {
    if (string.IsNullOrEmpty(key))
      return key;
    int consumedLength = 0;
    foreach (DimensionTree<TDimension>.Segment segment in DimensionTree<TDimension>.Segments)
    {
      consumedLength += (int) segment.Length.GetValueOrDefault();
      if (key.Length <= consumedLength)
        break;
    }
    return DimensionTree<TDimension>.PadKey(key, consumedLength);
  }

  protected static string PadKey(string key, int consumedLength)
  {
    return key.Length < consumedLength ? key + new string(' ', consumedLength - key.Length) : key;
  }

  public DimensionTree<TDimension>.Segment[] GetSegments()
  {
    return DimensionTree<TDimension>.CloneSequence<DimensionTree<TDimension>.Segment>((IEnumerable<DimensionTree<TDimension>.Segment>) DimensionTree<TDimension>.Segments).ToArray<DimensionTree<TDimension>.Segment>();
  }

  public class Dimension : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  public class Segment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(15, IsUnicode = true, IsKey = true)]
    public virtual string DimensionID { get; set; }

    [PXDBShort(IsKey = true)]
    public virtual short? SegmentID { get; set; }

    [PXDBShort]
    public virtual short? Length { get; set; }

    [PXDBString(1, IsFixed = true)]
    public virtual string Separator { get; set; }

    public abstract class dimensionID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DimensionTree<TDimension>.Segment.dimensionID>
    {
    }

    public abstract class segmentID : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      DimensionTree<TDimension>.Segment.segmentID>
    {
    }

    public abstract class length : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      DimensionTree<TDimension>.Segment.length>
    {
    }

    public abstract class separator : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DimensionTree<TDimension>.Segment.separator>
    {
    }
  }

  public class SegmentsSlot : IPrefetchable, IPXCompanyDependent
  {
    public DimensionTree<TDimension>.Segment[] Segments { get; private set; }

    public void Prefetch()
    {
      this.Segments = DimensionTree<TDimension>.SegmentsSlot.SelectSegments();
    }

    private static DimensionTree<TDimension>.Segment[] SelectSegments()
    {
      return DimensionTree<TDimension>.SegmentsSlot.SelectSegments(new TDimension().Value);
    }

    private static DimensionTree<TDimension>.Segment[] SelectSegments(string dimension)
    {
      using (new PXConnectionScope())
        return PXDatabase.SelectRecords<DimensionTree<TDimension>.Segment>((PXDataField) new PXDataFieldValue<DimensionTree<TDimension>.Segment.dimensionID>((object) dimension)).ToArray<DimensionTree<TDimension>.Segment>();
    }
  }
}
