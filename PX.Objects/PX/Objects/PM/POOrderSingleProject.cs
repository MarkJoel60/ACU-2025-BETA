// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POOrderSingleProject
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.PO;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// An extension of <see cref="T:PX.Objects.PO.POOrder" /> that contains auxiliary fields that are used to find out whether the document includes lines with different projects specified.
/// </summary>
[PXInternalUseOnly]
public sealed class POOrderSingleProject : PXCacheExtension<POOrder>, ISingleProjectExtension
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  /// <summary>The number of detail lines of the document.</summary>
  [PXDBInt]
  public int? DetailCount { get; set; }

  /// <summary>
  /// The sum of integer IDs of all projects in document's detail lines.
  /// </summary>
  [PXDBLong]
  public long? SumProjectID { get; set; }

  /// <summary>
  /// The sum of squares of integer IDs of all projects in document's detail lines.
  /// </summary>
  [PXDBLong]
  public long? SquareSumProjectID { get; set; }

  public abstract class detailCount : BqlType<IBqlInt, int>.Field<POOrderSingleProject.detailCount>
  {
  }

  public abstract class sumProjectID : 
    BqlType<IBqlLong, long>.Field<POOrderSingleProject.sumProjectID>
  {
  }

  public abstract class squareSumProjectID : 
    BqlType<IBqlLong, long>.Field<POOrderSingleProject.squareSumProjectID>
  {
  }
}
