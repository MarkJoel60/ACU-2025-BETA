// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.PreProcessAssigmentProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Api.Reports;

internal class PreProcessAssigmentProcessor : CommandProcessor<PX.Api.Models.Field>
{
  private static readonly string _preProcessName = "PreProcess";

  public override bool CanExecute(Command cmd)
  {
    return string.Equals(cmd.FieldName, PreProcessAssigmentProcessor._preProcessName) && Convert.ToBoolean(cmd.Value.Replace("=", "").Replace("'", ""));
  }

  public override void Execute(PX.Api.Models.Field fieldCmd)
  {
    string str = PXContext.GetScreenID();
    if (!string.IsNullOrEmpty(str))
      str = str.Replace(".", "");
    string key = "SubmitReportKeyPreProcessIsActive$" + str;
    if (PXSharedUserSession.CurrentUser.ContainsKey(key))
      throw new PXException("The previous operation has not been completed yet.");
    PXSharedUserSession.CurrentUser.Add(key, (object) true);
  }
}
