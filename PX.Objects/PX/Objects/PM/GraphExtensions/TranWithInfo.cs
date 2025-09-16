// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.TranWithInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.GL;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class TranWithInfo
{
  public PMTran Tran { get; private set; }

  public Account Account { get; private set; }

  public PMAccountGroup AccountGroup { get; private set; }

  public PMProject Project { get; private set; }

  public PMTask Task { get; private set; }

  public TranWithInfo(
    PMTran tran,
    Account account,
    PMAccountGroup accountGroup,
    PMProject project,
    PMTask task)
  {
    this.Tran = tran;
    this.Account = account;
    this.AccountGroup = accountGroup;
    this.Project = project;
    this.Task = task;
  }
}
