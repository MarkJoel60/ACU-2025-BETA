// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.CleanerEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CR;
using PX.SM;

#nullable disable
namespace PX.Objects.EP;

public class CleanerEmailProcessor : BasicEmailProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    EMailAccount account = package.Account;
    if (!account.IncomingProcessing.GetValueOrDefault() || !account.DeleteUnProcessed.GetValueOrDefault() || account.TypeDelete.GetValueOrDefault() == 1 && package.IsProcessed || account.TypeDelete.GetValueOrDefault() == 2 && !package.IsProcessed)
      return false;
    CRSMEmail message = package.Message;
    package.Graph.Caches[((object) message).GetType()].Delete((object) message);
    return true;
  }
}
