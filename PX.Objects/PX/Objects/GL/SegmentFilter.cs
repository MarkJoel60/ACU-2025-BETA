// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.SegmentFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public sealed class SegmentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private 
  #nullable disable
  string _DimensionID;
  private short? _SegmentID;
  private bool? _ValidCombos;

  [PXDBString]
  [PXDefault("SUBACCOUNT")]
  public string DimensionID
  {
    get => this._DimensionID;
    set => this._DimensionID = value;
  }

  [PXDBShort]
  [PXDefault(typeof (Search<Segment.segmentID, Where<Segment.dimensionID, Equal<Current<SegmentFilter.dimensionID>>, And<Segment.validate, Equal<True>>>, OrderBy<Desc<Segment.segmentID>>>))]
  [PXSelector(typeof (Search<Segment.segmentID, Where<Segment.dimensionID, Equal<Current<SegmentFilter.dimensionID>>, And<Segment.validate, Equal<True>>>>), DescriptionField = typeof (Segment.descr))]
  [PXUIField(DisplayName = "Segment ID")]
  public short? SegmentID
  {
    get => this._SegmentID;
    set => this._SegmentID = value;
  }

  [PXBool]
  [PXUIField(Visible = false, Enabled = false)]
  public bool? ValidCombos
  {
    get => this._ValidCombos;
    set => this._ValidCombos = value;
  }

  public abstract class dimensionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SegmentFilter.dimensionID>
  {
  }

  public abstract class segmentID : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SegmentFilter.segmentID>
  {
  }

  public abstract class validCombos : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SegmentFilter.validCombos>
  {
  }
}
