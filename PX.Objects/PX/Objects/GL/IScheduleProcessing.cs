// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.IScheduleProcessing
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Represents a graph capable of processing recurring transactions
/// according to the specified schedule.
/// </summary>
public interface IScheduleProcessing
{
  void GenerateProc(Schedule schedule);

  void GenerateProc(Schedule schedule, short times, DateTime runDate);
}
