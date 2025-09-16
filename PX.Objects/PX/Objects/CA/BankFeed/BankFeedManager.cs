// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.BankFeedManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.CA.BankFeed;

internal abstract class BankFeedManager
{
  public const string CategoriesSeparator = ":";

  public abstract (string, string)[] AvailableCorpCardFilters { get; }

  public abstract string[] AvailableTransactionFields { get; }

  public abstract bool AllowBatchDownloading { get; }

  public abstract int NumberOfAccountsForBatchDownloading { get; }

  public virtual DateTime? DefaultImportStartDate { get; }

  public virtual DateTime? MinimumImportStartDate { get; }

  public abstract System.Threading.Tasks.Task ConnectAsync(CABankFeed bankFeed);

  public abstract System.Threading.Tasks.Task UpdateAsync(CABankFeed bankFeed);

  public abstract System.Threading.Tasks.Task ProcessConnectResponseAsync(
    string responseStr,
    CABankFeed bankFeed);

  public abstract System.Threading.Tasks.Task ProcessUpdateResponseAsync(
    string responseStr,
    CABankFeed bankFeed);

  public abstract Task<IEnumerable<BankFeedCategory>> GetCategoriesAsync(CABankFeed bankFeed);

  public abstract Task<IEnumerable<BankFeedAccount>> GetAccountsAsync(CABankFeed bankFeed);

  public abstract Task<IEnumerable<BankFeedTransaction>> GetTransactionsAsync(
    LoadTransactionsData input,
    CABankFeed bankFeed);

  public abstract System.Threading.Tasks.Task DeleteAsync(CABankFeed bankFeed);

  public abstract LoadTransactionsData GetTransactionsFilterForTesting(DateTime loadingDate);

  protected virtual CABankFeedMaint GetBankFeedGraph(CABankFeed bankFeed)
  {
    CABankFeedMaint instance = PXGraph.CreateInstance<CABankFeedMaint>();
    if (bankFeed != null && bankFeed.BankFeedID != null)
      ((PXSelectBase<CABankFeed>) instance.BankFeed).Current = PXResultset<CABankFeed>.op_Implicit(PXSelectBase<CABankFeed, PXSelect<CABankFeed, Where<CABankFeed.bankFeedID, Equal<Required<CABankFeed.bankFeedID>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) bankFeed.BankFeedID
      }));
    return instance;
  }

  protected virtual CABankFeedMaint GetBankFeedGraph() => this.GetBankFeedGraph((CABankFeed) null);

  protected virtual T ParseResponse<T>(string responseStr)
  {
    try
    {
      return JsonSerializer.Deserialize<T>(responseStr, (JsonSerializerOptions) null);
    }
    catch (Exception ex)
    {
      throw new PXException("Could not parse the response from the hosted form. Please contact your Acumatica support provider.", ex);
    }
  }
}
