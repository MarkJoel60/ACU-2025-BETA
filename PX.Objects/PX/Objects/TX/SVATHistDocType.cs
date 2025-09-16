// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.SVATHistDocType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

public class SVATHistDocType
{
  public static readonly string[] Values = ((IEnumerable<string>) ARDocType.Values).Select<string, string>((Func<string, string>) (value => "AR" + value)).Concat<string>(((IEnumerable<string>) APDocType.Values).Select<string, string>((Func<string, string>) (value => "AP" + value))).Concat<string>(((IEnumerable<string>) CATranType.Values).Select<string, string>((Func<string, string>) (value => "CA" + value))).ToArray<string>();
  public static readonly string[] Labels = ((IEnumerable<string>) ARDocType.Labels).Concat<string>((IEnumerable<string>) APDocType.Labels).Concat<string>((IEnumerable<string>) CATranType.Labels).ToArray<string>();

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(SVATHistDocType.Values, SVATHistDocType.Labels)
    {
    }
  }
}
