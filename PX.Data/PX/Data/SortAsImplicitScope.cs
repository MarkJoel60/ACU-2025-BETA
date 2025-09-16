// Decompiled with JetBrains decompiler
// Type: PX.Data.SortAsImplicitScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

internal class SortAsImplicitScope : IDisposable
{
  private string[] _implicitSortColumns;

  internal SortAsImplicitScope(params string[] implicitSortColumns)
  {
    this._implicitSortColumns = implicitSortColumns ?? new string[0];
    PXContext.SetSlot<SortAsImplicitScope>(this);
  }

  internal static string[] GetSortColumns()
  {
    SortAsImplicitScope slot = PXContext.GetSlot<SortAsImplicitScope>();
    return slot != null ? slot._implicitSortColumns : new string[0];
  }

  public void Dispose()
  {
    PXContext.ClearSlot<SortAsImplicitScope>();
    this._implicitSortColumns = new string[0];
  }
}
