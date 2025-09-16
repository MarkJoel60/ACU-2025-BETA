// Decompiled with JetBrains decompiler
// Type: PX.Api.BPEventProviderType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Api;

public class BPEventProviderType : BqlType<IBqlString, string>.Constant<
#nullable disable
BPEventProviderType>
{
  internal const string BusinessProcessProviderName = "PX.DataSync.BusinessProcessSYProvider";

  public BPEventProviderType()
    : base("PX.DataSync.BusinessProcessSYProvider")
  {
  }
}
