// Decompiled with JetBrains decompiler
// Type: PX.SM.TemplateLeftLike
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

/// <exclude />
public sealed class TemplateLeftLike : BqlType<IBqlString, string>.Constant<
#nullable disable
TemplateLeftLike>
{
  public TemplateLeftLike()
    : base("Template:" + PXDatabase.Provider.SqlDialect.WildcardAnything)
  {
  }
}
