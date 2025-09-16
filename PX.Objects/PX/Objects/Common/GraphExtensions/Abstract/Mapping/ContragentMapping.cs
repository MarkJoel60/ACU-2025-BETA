// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.Mapping.ContragentMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.Mapping;

public class ContragentMapping : IBqlMapping
{
  /// <exclude />
  protected Type _table;

  /// <exclude />
  public Type Extension => typeof (Contragent);

  /// <exclude />
  public Type Table => this._table;

  public ContragentMapping(Type table) => this._table = table;
}
