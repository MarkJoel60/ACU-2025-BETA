// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXUpdateQuestion
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Update;

public class PXUpdateQuestion
{
  private System.Action<WebDialogResult> Handle;

  public string Question { get; private set; }

  public string DatabaseName { get; private set; }

  public string DatabaseVersion { get; private set; }

  public string AssemblyVersion { get; private set; }

  public PXUpdateQuestion(
    string databaseName,
    string databaseVersion,
    string assemblyVersion,
    System.Action<WebDialogResult> answerHandle)
  {
    this.Question = "The system should update your database. Click OK to continue.";
    this.DatabaseName = databaseName;
    this.DatabaseVersion = databaseVersion;
    this.AssemblyVersion = assemblyVersion;
    this.Handle = answerHandle;
  }

  internal void SetAnswer(WebDialogResult answer) => this.Handle(answer);
}
