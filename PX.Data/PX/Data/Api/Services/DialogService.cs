// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Services.DialogService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Mobile;
using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Api.Services;

[PXInternalUseOnly]
public class DialogService : IDialogService
{
  public void SetAnswers(PXGraph graph)
  {
    if (PXContext.GetDialogAnswers() == null)
      return;
    foreach (KeyValuePair<string, string> dialogAnswer in PXContext.GetDialogAnswers())
    {
      (string ViewName, string Key) cachePrefix = PXDialogRequiredExceptionExtension.ParseCachePrefix(dialogAnswer.Key);
      WebDialogResult result;
      if (!Enum.TryParse<WebDialogResult>(dialogAnswer.Value, out result))
        throw new Exception("Unexpected answer " + dialogAnswer.Value);
      DialogManager.SetAnswer(graph, cachePrefix.ViewName, cachePrefix.Key, result);
    }
  }
}
