// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.FinDocCopyPasteHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.Common;

public class FinDocCopyPasteHelper
{
  private const string BranchIDFieldName = "BranchID";
  private const string OriginalObjectName = "CurrentDocument";
  private const string DesiredObjectName = "Document";

  public FinDocCopyPasteHelper(PXGraph graph)
  {
    if (graph.PrimaryItemType == (Type) null)
      return;
    if (graph.PrimaryItemType.GetProperty("BranchID") == (PropertyInfo) null)
      throw new InvalidOperationException("The graph is not suitable for this helper because its primary do not have field BranchID");
    if (graph.GetType().GetField("CurrentDocument") == (FieldInfo) null)
      throw new InvalidOperationException("The graph is not suitable for this helper because it does not have view CurrentDocument");
    if (graph.GetType().GetField("Document") == (FieldInfo) null)
      throw new InvalidOperationException("The graph is not suitable for this helper because it does not have view Document");
  }

  public void SetBranchFieldCommandToTheTop(List<Command> script)
  {
    Command command = script.Single<Command>((Func<Command, bool>) (cmd => cmd.FieldName == "BranchID" && cmd.ObjectName == "CurrentDocument"));
    command.ObjectName = "Document";
    script.Remove(command);
    script.Insert(0, command);
  }
}
