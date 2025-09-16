// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.Mapping.DocumentMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.Mapping;

public class DocumentMapping : IBqlMapping
{
  /// <exclude />
  protected Type _table;
  /// <exclude />
  public Type BranchID = typeof (Document.branchID);
  /// <exclude />
  public Type HeaderTranPeriodID = typeof (Document.headerTranPeriodID);
  /// <exclude />
  public Type HeaderDocDate = typeof (Document.headerDocDate);

  /// <exclude />
  public Type Extension => typeof (Document);

  /// <exclude />
  public Type Table => this._table;

  /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.Common.GraphExtensions.Abstract.DAC.Document" /> mapped cache extension to the specified table.</summary>
  /// <param name="table">A DAC.</param>
  public DocumentMapping(Type table) => this._table = table;
}
