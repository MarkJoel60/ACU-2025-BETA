// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheExtension`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

[Serializable]
public class PXCacheExtension<Extension2, Extension1, Table> : 
  PXCacheExtension<Extension1, Table>,
  IExtends<Extension2>
  where Extension2 : PXCacheExtension<Table>
  where Extension1 : PXCacheExtension<Table>
  where Table : IBqlTable
{
  internal Extension2 _Base2;

  protected Extension2 Base2 => this._Base2;
}
