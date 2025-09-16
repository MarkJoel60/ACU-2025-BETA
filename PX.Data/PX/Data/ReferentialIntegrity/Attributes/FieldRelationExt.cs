// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.FieldRelationExt
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

internal static class FieldRelationExt
{
  public static System.Type ToWhere(
    this IFieldsRelation relation,
    bool forParentSelect = true,
    bool? autoParameters = true)
  {
    return ((IEnumerable<IFieldsRelation>) new IFieldsRelation[1]
    {
      relation
    }).ToWhere(forParentSelect, autoParameters);
  }

  public static System.Type ToWhere(
    this IEnumerable<IFieldsRelation> relations,
    bool forParentSelect = true,
    bool? autoParameters = true)
  {
    return relations.Select<IFieldsRelation, FieldAndParameter>((Func<IFieldsRelation, FieldAndParameter>) (r => new FieldAndParameter(forParentSelect ? r.FieldOfParentTable : r.FieldOfChildTable, forParentSelect ? r.FieldOfChildTable : r.FieldOfParentTable, !autoParameters.HasValue ? (System.Type) null : (autoParameters.Value ? typeof (Current<>) : typeof (Required<>))))).ToArray<FieldAndParameter>().ToWhere();
  }
}
