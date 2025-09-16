// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheExtension`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

[Serializable]
public class PXCacheExtension<Extension1, Table> : PXCacheExtension<Table>, IExtends<Extension1>
  where Extension1 : PXCacheExtension<Table>
  where Table : IBqlTable
{
  internal Extension1 _Base1;

  protected Extension1 Base1 => this._Base1;
}
