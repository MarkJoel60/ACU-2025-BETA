// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.DAC;

public class DocumentLine : PXMappedCacheExtension
{
  /// <summary>The identifier of the branch associated with the document.</summary>
  public virtual int? BranchID { get; set; }

  public virtual DateTime? TranDate { get; set; }

  public virtual string FinPeriodID { get; set; }

  public virtual string TranPeriodID { get; set; }

  /// <exclude />
  public abstract class branchID : IBqlField, IBqlOperand
  {
  }

  public abstract class tranDate : IBqlField, IBqlOperand
  {
  }

  /// <exclude />
  public abstract class finPeriodID : IBqlField, IBqlOperand
  {
  }

  /// <exclude />
  public abstract class tranPeriodID : IBqlField, IBqlOperand
  {
  }
}
