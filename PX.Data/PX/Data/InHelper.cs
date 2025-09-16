// Decompiled with JetBrains decompiler
// Type: PX.Data.InHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class InHelper
{
  public static System.Type Create(System.Type fieldType, int count)
  {
    if (count < 1)
      throw new ArgumentException();
    List<System.Type> where = new List<System.Type>();
    if (count == 1)
      where.Add(typeof (Where<,>));
    else
      where.Add(typeof (Where<,,>));
    InHelper.AddEqual(fieldType, (ICollection<System.Type>) where);
    for (int index = count - 1; index > 0; --index)
    {
      if (index == 1)
        InHelper.AddEndOr(fieldType, (ICollection<System.Type>) where);
      else
        InHelper.AddOr(fieldType, (ICollection<System.Type>) where);
    }
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
    InHelper.AddEqual(fieldType, where);
  }

  private static void AddOr(System.Type fieldType, ICollection<System.Type> where)
  {
    where.Add(typeof (Or<,,>));
    InHelper.AddEqual(fieldType, where);
  }
}
