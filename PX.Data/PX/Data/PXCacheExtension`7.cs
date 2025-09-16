// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheExtension`7
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

[Serializable]
public class PXCacheExtension<Extension6, Extension5, Extension4, Extension3, Extension2, Extension1, Table> : 
  PXCacheExtension<Extension5, Extension4, Extension3, Extension2, Extension1, Table>,
  IExtends<Extension6>
  where Extension6 : PXCacheExtension<Table>
  where Extension5 : PXCacheExtension<Table>
  where Extension4 : PXCacheExtension<Table>
  where Extension3 : PXCacheExtension<Table>
  where Extension2 : PXCacheExtension<Table>
  where Extension1 : PXCacheExtension<Table>
  where Table : IBqlTable
{
  internal Extension6 _Base6;

  protected Extension6 Base6 => this._Base6;
}
