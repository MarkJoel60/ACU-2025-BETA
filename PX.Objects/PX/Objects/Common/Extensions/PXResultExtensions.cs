// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.PXResultExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Extensions;

public static class PXResultExtensions
{
  public static Tuple<T0, T1> ToTuple<T0, T1>(this PXResult<T0, T1> result)
    where T0 : class, IBqlTable, new()
    where T1 : class, IBqlTable, new()
  {
    return Tuple.Create<T0, T1>(PXResult<T0, T1>.op_Implicit(result), PXResult<T0, T1>.op_Implicit(result));
  }

  public static Tuple<T0, T1, T2> ToTuple<T0, T1, T2>(this PXResult<T0, T1, T2> result)
    where T0 : class, IBqlTable, new()
    where T1 : class, IBqlTable, new()
    where T2 : class, IBqlTable, new()
  {
    return Tuple.Create<T0, T1, T2>(PXResult<T0, T1, T2>.op_Implicit(result), PXResult<T0, T1, T2>.op_Implicit(result), PXResult<T0, T1, T2>.op_Implicit(result));
  }
}
