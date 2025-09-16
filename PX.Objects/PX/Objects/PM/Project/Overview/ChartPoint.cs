// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.ChartPoint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM.Project.Overview;

[PXHidden]
[ExcludeFromCodeCoverage]
[Serializable]
public class ChartPoint : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The unique identifier of the curve</summary>
  [PXString(IsKey = true)]
  public virtual 
  #nullable disable
  string GraphKey { get; set; }

  /// <summary>The Name of the point on the X axis</summary>
  [PXString]
  public virtual string PointName { get; set; }

  /// <summary>The point index</summary>
  [PXInt]
  public virtual int? PointIndex { get; set; }

  /// <summary>The point value (Y axis)</summary>
  [PXDecimal]
  public virtual Decimal? PointValue { get; set; }

  public abstract class graphKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ChartPoint.graphKey>
  {
  }

  public abstract class pointName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ChartPoint.pointName>
  {
  }

  public abstract class pointIndex : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ChartPoint.pointIndex>
  {
  }

  public abstract class pointValue : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ChartPoint.pointValue>
  {
  }
}
