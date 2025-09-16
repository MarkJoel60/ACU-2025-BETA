// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.BoundaryYears
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FA;

[Serializable]
public class BoundaryYears : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The financial year starting from which the periods will be generated in the system.
  /// </summary>
  [PXString(4, IsFixed = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "From Year")]
  public virtual string FromYear { get; set; }

  /// <summary>
  /// The financial year till which the periods will be generated in the system.
  /// </summary>
  [PXString(4, IsFixed = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "To Year")]
  public virtual string ToYear { get; set; }

  public abstract class fromYear : IBqlField, IBqlOperand
  {
  }

  public abstract class toYear : IBqlField, IBqlOperand
  {
  }
}
