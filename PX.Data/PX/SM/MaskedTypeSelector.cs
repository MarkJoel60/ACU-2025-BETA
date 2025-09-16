// Decompiled with JetBrains decompiler
// Type: PX.SM.MaskedTypeSelector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;

#nullable disable
namespace PX.SM;

internal class MaskedTypeSelector : PXCustomSelectorAttribute
{
  public MaskedTypeSelector()
    : base(typeof (MaskedType.entityTypeName))
  {
    this.DescriptionField = typeof (MaskedType.text);
  }

  internal IEnumerable GetRecords() => RelationGroups.GetMaskedTypes();
}
