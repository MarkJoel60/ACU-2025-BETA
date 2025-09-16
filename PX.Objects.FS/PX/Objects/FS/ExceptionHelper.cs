// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ExceptionHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public static class ExceptionHelper
{
  public static Exception GetExceptionWithContextMessage(string contextMessage, Exception ex)
  {
    switch (ex)
    {
      case PXLockViolationException _ when ex.Message.Contains(typeof (FSPostRegister).Name):
        return (Exception) new PXException($"{contextMessage}{Environment.NewLine}Attempt to create duplicated Invoice.", ex);
      case PXOuterException _:
        return (Exception) ExceptionHelper.GetPXOuterExceptionWithContextMessage(contextMessage, (PXOuterException) ex);
      case PXException _:
        return (Exception) new PXException(contextMessage + Environment.NewLine + ex.Message, ex);
      default:
        return new Exception(contextMessage + Environment.NewLine + ex.Message, ex);
    }
  }

  private static PXOuterException GetPXOuterExceptionWithContextMessage(
    string contextMessage,
    PXOuterException ex)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    for (int index = 0; index < ex.InnerFields.Length; ++index)
      dictionary.Add(ex.InnerFields[index], ex.InnerMessages[index]);
    return new PXOuterException(dictionary, ex.GraphType, ex.Row, contextMessage + Environment.NewLine + ((Exception) ex).Message);
  }
}
