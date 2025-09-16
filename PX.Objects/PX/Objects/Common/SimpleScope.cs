// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.SimpleScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common;

public class SimpleScope : IDisposable
{
  private readonly Action _onClose;

  public SimpleScope(Action onOpen, Action onClose)
  {
    onOpen();
    this._onClose = onClose;
  }

  public void Dispose() => this._onClose();
}
