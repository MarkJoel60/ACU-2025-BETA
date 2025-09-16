// Decompiled with JetBrains decompiler
// Type: PX.Data.InHelperWithIsNull
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class InHelperWithIsNull
{
  public static System.Type Create(System.Type fieldType, int count)
  {
    if (count < 1)
      throw new ArgumentException();
    List<System.Type> where = new List<System.Type>();
    where.Add(typeof (Where<,,>));
    InHelperWithIsNull.AddEqual(fieldType, (ICollection<System.Type>) where);
    for (int index = count - 1; index > 0; --index)
      InHelperWithIsNull.AddOr(fieldType, (ICollection<System.Type>) where);
    InHelperWithIsNull.AddOrIsNull(fieldType, (ICollection<System.Type>) where);
    return BqlCommand.Compose(where.ToArray());
  }

  private static void AddEqual(System.Type fieldType, ICollection<System.Type> where)
  {
    where.Add(fieldType);
    where.Add(typeof (Equal<>));
    where.Add(typeof (Required<>));
    where.Add(fieldType);
  }

  private static void AddEndOr(System.Type fieldType, ICollection<System.Type> where)
  {
    where.Add(typeof (Or<,>));
    InHelperWithIsNull.AddEqual(fieldType, where);
  }

  private static void AddOr(System.Type fieldType, ICollection<System.Type> where)
  {
    where.Add(typeof (Or<,,>));
    InHelperWithIsNull.AddEqual(fieldType, where);
  }

  private static void AddOrIsNull(System.Type fieldType, ICollection<System.Type> where)
  {
    where.Add(typeof (Or<,>));
    InHelperWithIsNull.AddIsNull(fieldType, where);
  }

  private static void AddIsNull(System.Type fieldType, ICollection<System.Type> where)
  {
    where.Add(fieldType);
    where.Add(typeof (IsNull));
  }
}
