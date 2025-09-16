// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.RestrictSiteByBranchAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable disable
namespace PX.Objects.IN.Attributes;

public class RestrictSiteByBranchAttribute(Type branchField = null, Type where = null) : 
  RestrictorWithParametersAttribute(RestrictSiteByBranchAttribute.GetWhere(branchField, where), "The {0} branch specified for the {1} warehouse has other base currency than the {2} branch that is currently selected.", typeof (INSite.branchID), typeof (INSite.siteCD), typeof (Current2<>).MakeGenericType(branchField))
{
  private static Type GetWhere(Type branchField, Type where)
  {
    return where != (Type) null ? where : BqlTemplate.OfCondition<Where<Current2<BqlPlaceholder.A>, IsNull, Or<INSite.baseCuryID, EqualBaseCuryID<Current2<BqlPlaceholder.A>>>>>.Replace<BqlPlaceholder.A>(branchField).ToType();
  }
}
