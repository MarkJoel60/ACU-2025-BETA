// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.BasicEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Mail;
using PX.Data;
using PX.Objects.CR;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.EP;

public abstract class BasicEmailProcessor : IEmailProcessor
{
  public void Process(EmailProcessEventArgs e)
  {
    BasicEmailProcessor.Package package = BasicEmailProcessor.Package.Extract(e);
    e.IsSuccessful = package != null && this.Process(package);
  }

  protected abstract bool Process(BasicEmailProcessor.Package package);

  protected void PersistRecord(BasicEmailProcessor.Package package, object record)
  {
    if (record == null)
      return;
    PXCache cach = package.Graph.Caches[record.GetType()];
    PXEntryStatus status = cach.GetStatus(record);
    try
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        switch (status - 1)
        {
          case 0:
            cach.Persist(record, (PXDBOperation) 1);
            break;
          case 1:
            cach.Persist(record, (PXDBOperation) 2);
            break;
          case 2:
            cach.Persist(record, (PXDBOperation) 3);
            break;
          default:
            throw new InvalidOperationException();
        }
        transactionScope.Complete();
      }
    }
    catch (Exception ex)
    {
      cach.Remove(record);
      throw;
    }
  }

  protected sealed class Package
  {
    private readonly PXGraph _graph;
    private readonly EMailAccount _account;
    private readonly CRSMEmail _message;
    private string _address;
    private string _description;
    private bool _isProcessed;

    private Package(PXGraph graph, EMailAccount account, CRSMEmail message)
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

    public PXGraph Graph => this._graph;

    public CRSMEmail Message => this._message;

    public string Address => this._address;

    public string Description => this._description;

    public EMailAccount Account => this._account;

    public bool IsProcessed => this._isProcessed;

    public static BasicEmailProcessor.Package Extract(EmailProcessEventArgs e)
    {
      PXGraph graph = e.Graph;
      EMailAccount account = e.Account;
      CRSMEmail message = e.Message;
      string str1;
      string str2;
      if (!Mailbox.TryParse(message.MailFrom, ref str1, ref str2) || str1 == null || str1.Trim().Length == 0)
        return (BasicEmailProcessor.Package) null;
      return new BasicEmailProcessor.Package(graph, account, message)
      {
        _address = str1,
        _description = str2,
        _isProcessed = e.IsSuccessful
      };
    }
  }
}
