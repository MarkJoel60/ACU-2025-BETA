// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraphExtension`9
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class PXGraphExtension<Extension8, Extension7, Extension6, Extension5, Extension4, Extension3, Extension2, Extension1, Graph> : 
  PXGraphExtension<Extension7, Extension6, Extension5, Extension4, Extension3, Extension2, Extension1, Graph>,
  IExtends<Extension8>
  where Extension8 : PXGraphExtension<Graph>
  where Extension7 : PXGraphExtension<Graph>
  where Extension6 : PXGraphExtension<Graph>
  where Extension5 : PXGraphExtension<Graph>
  where Extension4 : PXGraphExtension<Graph>
  where Extension3 : PXGraphExtension<Graph>
  where Extension2 : PXGraphExtension<Graph>
  where Extension1 : PXGraphExtension<Graph>
  where Graph : PXGraph
{
  internal Extension8 _Base8;

  protected Extension8 Base8 => this._Base8;
}
