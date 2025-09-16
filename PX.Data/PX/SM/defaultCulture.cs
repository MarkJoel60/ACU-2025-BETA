// Decompiled with JetBrains decompiler
// Type: PX.SM.defaultCulture
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.SM;

internal class defaultCulture : BqlType<IBqlString, string>.Constant<
#nullable disable
defaultCulture>
{
  public defaultCulture()
    : base("en-US")
  {
  }

  public new static string Value => "en-US";
}
