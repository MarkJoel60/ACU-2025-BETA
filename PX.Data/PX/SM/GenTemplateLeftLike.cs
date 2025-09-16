// Decompiled with JetBrains decompiler
// Type: PX.SM.GenTemplateLeftLike
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public sealed class GenTemplateLeftLike : BqlType<IBqlString, string>.Constant<
#nullable disable
GenTemplateLeftLike>
{
  public GenTemplateLeftLike()
    : base("GenTemplate:" + PXDatabase.Provider.SqlDialect.WildcardAnything)
  {
  }
}
