// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ConversationEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.EP;

public class ConversationEmailProcessor : BasicEmailProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    CRSMEmail message = package.Message;
    EMailAccount account = package.Account;
    PXGraph graph = package.Graph;
    if (message.Ticket.HasValue || message.ResponseToNoteID.HasValue || account.EmailAccountType != "E" || !account.IncomingProcessing.GetValueOrDefault() || string.IsNullOrEmpty(message.MessageId) || string.IsNullOrEmpty(message.MessageReference))
      return false;
    string messageReference = message.MessageReference;
    string[] separator = new string[2]{ "> <", ">,<" };
    foreach (string str in messageReference.Split(separator, StringSplitOptions.RemoveEmptyEntries))
    {
      if (str.IndexOf('<') != 0)
        str = str.Insert(0, "<");
      if (str.LastIndexOf('>') != str.Length - 1)
        str = str.Insert(str.Length, ">");
      CRSMEmail crsmEmail = PXResultset<CRSMEmail>.op_Implicit(PXSelectBase<CRSMEmail, PXViewOf<CRSMEmail>.BasedOn<SelectFromBase<CRSMEmail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRSMEmail.messageId, IBqlString>.IsEqual<P.AsString>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
      {
        (object) str
      }));
      if (crsmEmail != null)
      {
        message.Ticket = crsmEmail.ID;
        return true;
      }
    }
    return false;
  }
}
