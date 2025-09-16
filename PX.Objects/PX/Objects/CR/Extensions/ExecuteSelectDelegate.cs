// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ExecuteSelectDelegate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections;

#nullable disable
namespace PX.Objects.CR.Extensions;

public delegate IEnumerable ExecuteSelectDelegate(
  string viewName,
  object[] parameters,
  object[] searches,
  string[] sortcolumns,
  bool[] descendings,
  PXFilterRow[] filters,
  ref int startRow,
  int maximumRows,
  ref int totalRows);
