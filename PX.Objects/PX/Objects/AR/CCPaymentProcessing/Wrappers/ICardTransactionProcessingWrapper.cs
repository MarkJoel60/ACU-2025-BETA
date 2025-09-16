// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.ICardTransactionProcessingWrapper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Objects.AR.CCPaymentProcessing.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers;

public interface ICardTransactionProcessingWrapper
{
  TranProcessingResult DoTransaction(CCTranType aTranType, TranProcessingInput inputData);

  CCError ValidateSettings(PluginSettingDetail setting);

  TransactionData GetTransaction(string transactionId);

  TransactionData FindTransaction(TransactionSearchParams searchParams);

  IEnumerable<TransactionData> GetTransactionsByBatch(string batchId);

  IEnumerable<TransactionData> GetTransactionsByTypedBatch(string batchId, BatchType batchType);

  IEnumerable<TransactionData> GetTransactionsByCustomer(
    string customerProfileId,
    TransactionSearchParams searchParams = null);

  IEnumerable<TransactionData> GetUnsettledTransactions(TransactionSearchParams searchParams = null);

  void TestCredentials(APIResponse apiResponse);

  void ExportSettings(IList<PluginSettingDetail> aSettings);

  IEnumerable<BatchData> GetSettledBatches(BatchSearchParams batchSearchParams);
}
