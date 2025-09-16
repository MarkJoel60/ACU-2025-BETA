// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.AffectedAvailability.DocumentStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.SO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.AffectedAvailability;

public class DocumentStatus
{
  public class ListAttribute : PXStringListAttribute
  {
    public static readonly (string, string)[] ValuesToLabels = ((IEnumerable<(string, string)>) SOOrderStatus.ListAttribute.ValuesToLabels).Select<(string, string), (string, string)>((Func<(string, string), (string, string)>) (p => (EntityType.SOOrder + p.Item1, p.Item2))).Union<(string, string)>(((IEnumerable<(string, string)>) SOShipmentStatus.ListAttribute.ValuesToLabels).Select<(string, string), (string, string)>((Func<(string, string), (string, string)>) (p => (EntityType.SOShipment + p.Item1, p.Item2)))).Union<(string, string)>(((IEnumerable<(string, string)>) INDocStatus.ListAttribute.ValuesToLabels).Select<(string, string), (string, string)>((Func<(string, string), (string, string)>) (p => (EntityType.INRegister + p.Item1, p.Item2)))).ToArray<(string, string)>();

    public ListAttribute()
      : base(DocumentStatus.ListAttribute.ValuesToLabels)
    {
    }
  }
}
