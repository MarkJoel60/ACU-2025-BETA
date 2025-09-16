// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.SearchMailboxesCompletedEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Web.Services", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
public class SearchMailboxesCompletedEventArgs : AsyncCompletedEventArgs
{
  private object[] results;

  internal SearchMailboxesCompletedEventArgs(
    object[] results,
    Exception exception,
    bool cancelled,
    object userState)
    : base(exception, cancelled, userState)
  {
    this.results = results;
  }

  /// <remarks />
  public SearchMailboxesResponseType Result
  {
    get
    {
      this.RaiseExceptionIfNecessary();
      return (SearchMailboxesResponseType) this.results[0];
    }
  }
}
