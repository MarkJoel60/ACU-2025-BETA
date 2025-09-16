// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraphExtension`7
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class PXGraphExtension<Extension6, Extension5, Extension4, Extension3, Extension2, Extension1, Graph> : 
  PXGraphExtension<Extension5, Extension4, Extension3, Extension2, Extension1, Graph>,
  IExtends<Extension6>
  where Extension6 : PXGraphExtension<Graph>
  where Extension5 : PXGraphExtension<Graph>
  where Extension4 : PXGraphExtension<Graph>
  where Extension3 : PXGraphExtension<Graph>
  where Extension2 : PXGraphExtension<Graph>
  where Extension1 : PXGraphExtension<Graph>
  where Graph : PXGraph
{
  internal Extension6 _Base6;

  protected Extension6 Base6 => this._Base6;
}
