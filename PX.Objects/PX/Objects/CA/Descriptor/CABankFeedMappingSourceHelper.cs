// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Descriptor.CABankFeedMappingSourceHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA.Descriptor;

public class CABankFeedMappingSourceHelper
{
  private Dictionary<string, string> _internalCommonMappings;
  private Dictionary<string, string> _internalMXMappings;
  private Dictionary<string, string> _internalPlaidMappings;
  private PXGraph _graph;

  public CABankFeedMappingSourceHelper(PXCache cache)
  {
    this._graph = cache.Graph;
    PXCache cach = cache.Graph.Caches[typeof (BankFeedTransaction)];
    this._internalCommonMappings = new Dictionary<string, string>()
    {
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.accountID>(cach),
        "accountID"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.amount>(cach),
        "amount"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.category>(cach),
        "category"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.checkNumber>(cach),
        "checkNumber"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.isoCurrencyCode>(cach),
        "isoCurrencyCode"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.date>(cach),
        "date"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.memo>(cach),
        "memo"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.name>(cach),
        "name"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.pending>(cach),
        "pending"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.pendingTransactionID>(cach),
        "pendingTransactionID"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.transactionID>(cach),
        "transactionID"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.type>(cach),
        "type"
      }
    };
    this._internalMXMappings = new Dictionary<string, string>()
    {
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.accountStringId>(cach),
        "accountStringId"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.categoryGuid>(cach),
        "categoryGuid"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.createdAt>(cach),
        "createdAt"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.extendedTransactionType>(cach),
        "extendedTransactionType"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.id>(cach),
        "id"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.isBillPay>(cach),
        "isBillPay"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.isDirectDeposit>(cach),
        "isDirectDeposit"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.isExpense>(cach),
        "isExpense"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.isFee>(cach),
        "isFee"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.isIncome>(cach),
        "isIncome"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.isInternational>(cach),
        "isInternational"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.isOverdraftFee>(cach),
        "isOverdraftFee"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.isPayrollAdvance>(cach),
        "isPayrollAdvance"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.isRecurring>(cach),
        "isRecurring"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.isSubscription>(cach),
        "isSubscription"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.latitude>(cach),
        "latitude"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.localizedDescription>(cach),
        "localizedDescription"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.localizedMemo>(cach),
        "localizedMemo"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.longitude>(cach),
        "longitude"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.memberIsManagedByUser>(cach),
        "memberIsManagedByUser"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.merchantCategoryCode>(cach),
        "merchantCategoryCode"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.merchantGuid>(cach),
        "merchantGuid"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.merchantLocationGuid>(cach),
        "merchantLocationGuid"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.metadata>(cach),
        "metadata"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.originalDescription>(cach),
        "originalDescription"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.postedAt>(cach),
        "postedAt"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.transactedAt>(cach),
        "transactedAt"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.updatedAt>(cach),
        "updatedAt"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.userId>(cach),
        "userId"
      }
    };
    this._internalPlaidMappings = new Dictionary<string, string>()
    {
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.accountOwner>(cach),
        "accountOwner"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.address>(cach),
        "address"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.authorizedDate>(cach),
        "authorizedDate"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.authorizedDatetime>(cach),
        "authorizedDatetime"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.byOrderOf>(cach),
        "byOrderOf"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.city>(cach),
        "city"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.country>(cach),
        "country"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.datetimeValue>(cach),
        "datetimeValue"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.merchantName>(cach),
        "merchantName"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.payee>(cach),
        "payee"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.payer>(cach),
        "payer"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.paymentChannel>(cach),
        "paymentChannel"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.paymentMethod>(cach),
        "paymentMethod"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.paymentProcessor>(cach),
        "paymentProcessor"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.personalFinanceCategory>(cach),
        "personalFinanceCategory"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.ppdId>(cach),
        "ppdId"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.postalCode>(cach),
        "postalCode"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.reason>(cach),
        "reason"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.referenceNumber>(cach),
        "referenceNumber"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.region>(cach),
        "region"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.storeNumber>(cach),
        "storeNumber"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.transactionCode>(cach),
        "transactionCode"
      },
      {
        PXUIFieldAttribute.GetDisplayName<BankFeedTransaction.unofficialCurrencyCode>(cach),
        "unofficialCurrencyCode"
      }
    };
  }

  public string GetFileFieldsForFormula(CABankFeed bankFeed)
  {
    return string.Join(";", (IEnumerable<string>) this.GetFileFieldListForFormula(bankFeed));
  }

  public List<string> GetFileFieldListForFormula(CABankFeed bankFeed)
  {
    List<string> fieldListForFormula = new List<string>();
    if (bankFeed != null)
      fieldListForFormula = GraphHelper.RowCast<SYProviderField>((IEnumerable) PXSelectBase<SYProviderField, PXSelectJoin<SYProviderField, InnerJoin<SYProviderObject, On<SYProviderObject.providerID, Equal<SYProviderField.providerID>, And<SYProviderObject.name, Equal<SYProviderField.objectName>>>>, Where<SYProviderObject.isActive, Equal<True>, And<SYProviderObject.lineNbr, Equal<Required<SYProviderObject.lineNbr>>, And<SYProviderObject.providerID, Equal<Required<SYProviderObject.providerID>>, And<SYProviderField.isActive, Equal<True>>>>>, OrderBy<Asc<SYProviderField.lineNbr>>>.Config>.Select(this._graph, new object[2]
      {
        (object) 1,
        (object) bankFeed.ProviderID
      })).Select<SYProviderField, string>((Func<SYProviderField, string>) (i => $"[{i.Name}]")).ToList<string>();
    return fieldListForFormula;
  }

  public string GetFieldsForFormula(string bankFeedType)
  {
    return string.Join(";", this.GetFieldListForFormula(bankFeedType).ToArray());
  }

  public List<string> GetFieldListForFormula(string bankFeedType)
  {
    List<string> fieldListForFormula = new List<string>();
    fieldListForFormula.AddRange((IEnumerable<string>) this._internalCommonMappings.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (kvp => $"[{kvp.Key}]")).ToList<string>());
    switch (bankFeedType)
    {
      case "M":
        fieldListForFormula.AddRange((IEnumerable<string>) this._internalMXMappings.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (kvp => $"[{kvp.Key}]")).ToList<string>());
        break;
      case "P":
      case "T":
        fieldListForFormula.AddRange((IEnumerable<string>) this._internalPlaidMappings.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (kvp => $"[{kvp.Key}]")).ToList<string>());
        break;
    }
    fieldListForFormula.Sort();
    return fieldListForFormula;
  }

  public string GetFieldNameByDisplayName(string displayName)
  {
    string empty = string.Empty;
    if (this._internalCommonMappings.TryGetValue(displayName, out empty) || this._internalMXMappings.TryGetValue(displayName, out empty))
      return empty;
    this._internalPlaidMappings.TryGetValue(displayName, out empty);
    return empty;
  }
}
