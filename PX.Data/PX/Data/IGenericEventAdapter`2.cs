// Decompiled with JetBrains decompiler
// Type: PX.Data.IGenericEventAdapter`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable enable
namespace PX.Data;

internal interface IGenericEventAdapter<TClassicEventArgs, TGenericDelegate>
  where TClassicEventArgs : EventArgs
  where TGenericDelegate : Delegate
{
  TGenericDelegate GenericHandler { get; }

  void Execute(PXCache cache, TClassicEventArgs args);
}
