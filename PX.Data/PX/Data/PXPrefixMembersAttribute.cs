// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPrefixMembersAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
[Serializable]
public class PXPrefixMembersAttribute : Attribute
{
  protected readonly string _Prefix;

  public PXPrefixMembersAttribute(string prefix) => this._Prefix = (prefix ?? "").Replace("_", "");

  public string Prefix => this._Prefix;
}
