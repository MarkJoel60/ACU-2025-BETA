// Decompiled with JetBrains decompiler
// Type: PX.SM.emptyGuid
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

internal class emptyGuid : BqlType<IBqlString, string>.Constant<
#nullable disable
emptyGuid>
{
  public emptyGuid()
    : base(Guid.Empty.ToString())
  {
  }
}
