// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.FieldAndParameterExt
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.ReferentialIntegrity;

internal static class FieldAndParameterExt
{
  public static System.Type ToWhere(this FieldAndParameter[] fieldsAndParameters)
  {
    FieldAndParameter fieldAndParameter1 = ((IEnumerable<FieldAndParameter>) fieldsAndParameters).First<FieldAndParameter>();
    List<System.Type> typeList = new List<System.Type>()
    {
      fieldsAndParameters.Length == 1 ? typeof (Where<,>) : typeof (Where<,,>)
    };
    typeList.AddRange((IEnumerable<System.Type>) fieldAndParameter1.EqualSequence);
    int num = 1;
    foreach (FieldAndParameter fieldAndParameter2 in ((IEnumerable<FieldAndParameter>) fieldsAndParameters).Skip<FieldAndParameter>(1))
    {
      bool flag = ++num == fieldsAndParameters.Length;
      typeList.Add(flag ? typeof (And<,>) : typeof (And<,,>));
      typeList.AddRange((IEnumerable<System.Type>) fieldAndParameter2.EqualSequence);
    }
    return BqlCommand.Compose(typeList.ToArray());
  }
}
