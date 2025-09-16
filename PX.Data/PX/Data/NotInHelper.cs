// Decompiled with JetBrains decompiler
// Type: PX.Data.NotInHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class NotInHelper
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
    NotInHelper.AddEqual(fieldType, (ICollection<System.Type>) where);
    for (int index = count - 1; index > 0; --index)
    {
      if (index == 1)
        NotInHelper.AddEndAnd(fieldType, (ICollection<System.Type>) where);
      else
        NotInHelper.AddAnd(fieldType, (ICollection<System.Type>) where);
    }
    return BqlCommand.Compose(where.ToArray());
  }

  private static void AddEqual(System.Type fieldType, ICollection<System.Type> where)
  {
    where.Add(fieldType);
    where.Add(typeof (NotEqual<>));
    where.Add(typeof (Required<>));
    where.Add(fieldType);
  }

  private static void AddEndAnd(System.Type fieldType, ICollection<System.Type> where)
  {
    where.Add(typeof (And<,>));
    NotInHelper.AddEqual(fieldType, where);
  }

  private static void AddAnd(System.Type fieldType, ICollection<System.Type> where)
  {
    where.Add(typeof (And<,,>));
    NotInHelper.AddEqual(fieldType, where);
  }
}
