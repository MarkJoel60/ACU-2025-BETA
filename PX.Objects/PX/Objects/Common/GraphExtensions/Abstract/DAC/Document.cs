// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.DAC.Document
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.DAC;

public class Document : PXMappedCacheExtension
{
  public virtual int? BranchID { get; set; }

  public virtual DateTime? HeaderDocDate { get; set; }

  public virtual string HeaderTranPeriodID { get; set; }

  public virtual string HeaderFinPeriodID { get; set; }

  /// <exclude />
  public abstract class branchID : IBqlField, IBqlOperand
  {
  }

  /// <exclude />
  public abstract class headerDocDate : IBqlField, IBqlOperand
  {
  }

  /// <exclude />
  public abstract class headerTranPeriodID : IBqlField, IBqlOperand
  {
  }

  /// <exclude />
  public abstract class headerFinPeriodID : IBqlField, IBqlOperand
  {
  }
}
