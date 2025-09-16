// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProcessInfo`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class ProcessInfo<T> where T : class
{
  public int RecordIndex { get; set; }

  public List<T> Batches { get; private set; }

  public ProcessInfo(int index)
  {
    this.RecordIndex = index;
    this.Batches = new List<T>();
  }
}
