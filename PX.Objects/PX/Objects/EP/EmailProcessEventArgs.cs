// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmailProcessEventArgs
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.EP;

public class EmailProcessEventArgs
{
  private readonly PXGraph _graph;
  private readonly EMailAccount _account;
  private readonly CRSMEmail _message;
  private bool _isSuccessful;

  public EmailProcessEventArgs(PXGraph graph, EMailAccount account, CRSMEmail message)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (account == null)
      throw new ArgumentNullException(nameof (account));
    if (message == null)
      throw new ArgumentNullException(nameof (message));
    this._graph = graph;
    this._account = account;
    this._message = message;
  }

  public CRSMEmail Message => this._message;

  public PXGraph Graph => this._graph;

  public EMailAccount Account => this._account;

  public bool IsSuccessful
  {
    get => this._isSuccessful;
    set => this._isSuccessful |= value;
  }
}
