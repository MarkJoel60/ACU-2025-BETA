// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.PaymentProcessing.Level3Processing
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.CCPaymentProcessing.Wrappers;
using PX.Objects.CA;
using PX.Objects.CC.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

#nullable disable
namespace PX.Objects.CC.PaymentProcessing;

public class Level3Processing
{
  private ICCPaymentProcessingRepository _repository;
  private Func<object, CCProcessingContext, ICardTransactionProcessingWrapper> _transactionProcessingWrapper;

  public Level3Processing()
    : this(CCPaymentProcessingRepository.GetCCPaymentProcessingRepository())
  {
  }

  public Level3Processing(ICCPaymentProcessingRepository repository)
  {
    this._repository = repository;
    this._transactionProcessingWrapper = (Func<object, CCProcessingContext, ICardTransactionProcessingWrapper>) ((plugin, context) => CardTransactionProcessingWrapper.GetTransactionProcessingWrapper(plugin, (ICardProcessingReadersProvider) new CardProcessingReadersProvider(context)));
  }

  public virtual TranOperationResult UpdateLevel3Data(PX.Objects.Extensions.PaymentTransaction.Payment payment, int? transactionId)
  {
    return this.UpdateL3Data(this.CopyTranProcessingToL3DataInput(payment.L3Data), transactionId, this.GetAndCheckProcessingCenterFromTransaction(payment.ProcessingCenterID, payment.PMInstanceID, payment.CuryID));
  }

  protected TranOperationResult UpdateL3Data(
    L3DataInput l3DataInput,
    int? transactionId,
    CCProcessingCenter processingCenter)
  {
    TranOperationResult tranOperationResult = new TranOperationResult()
    {
      TransactionId = transactionId
    };
    TranProcessingResult aRes = new TranProcessingResult();
    bool flag = false;
    ICardProcessingReadersProvider provider = (ICardProcessingReadersProvider) new CardProcessingReadersProvider(new CCProcessingContext()
    {
      callerGraph = this._repository.Graph,
      processingCenter = processingCenter
    });
    ICCLevel3Processor level3Processor = this.CreateLevel3Processor(processingCenter, provider);
    CCL3ProcessingException processingException = (CCL3ProcessingException) null;
    try
    {
      level3Processor.CreateOrUpdateLevel3Data(l3DataInput);
    }
    catch (CCL3ProcessingException ex)
    {
      flag = true;
      processingException = ex;
      switch ((int) ex.Reason)
      {
        case 0:
          aRes.L3Status = V2Converter.GetL3TranStatus("FLD");
          aRes.L3Error = ((Exception) ex).Message;
          break;
        case 1:
          aRes.L3Status = V2Converter.GetL3TranStatus("REJ");
          break;
        case 2:
          aRes.L3Status = V2Converter.GetL3TranStatus("RRJ");
          aRes.L3Error = ((Exception) ex).Message;
          break;
        case 3:
          aRes.L3Status = V2Converter.GetL3TranStatus("RFL");
          aRes.L3Error = ((Exception) ex).Message;
          break;
      }
      PXTrace.WriteInformation($"CCPaymentProcessing.UpdateL3Data.V2.CCL3ProcessingException. ErrorSource:{aRes.ErrorSource}; ErrorText:{aRes.ErrorText}");
    }
    catch (WebException ex)
    {
      flag = true;
      aRes.L3Status = V2Converter.GetL3TranStatus("FLD");
      aRes.L3Error = ex.Message;
      PXTrace.WriteInformation($"CCPaymentProcessing.UpdateL3Data.WebException. ErrorSource:{aRes.ErrorSource}; ErrorText:{aRes.ErrorText}");
    }
    catch (Exception ex)
    {
      flag = true;
      aRes.L3Status = V2Converter.GetL3TranStatus("FLD");
      aRes.L3Error = ex.Message;
      throw new PXException("Error during request processing. Transaction ID:{0}, Error:{1}", new object[2]
      {
        (object) l3DataInput.TransactionId,
        (object) ex.Message
      });
    }
    finally
    {
      transactionId = this._repository.GetCCProcTranByTranID(transactionId).First<CCProcTran>().TranNbr;
      aRes.Level3Support = true;
      if (!flag)
      {
        aRes.L3Status = V2Converter.GetL3TranStatus("SNT");
        aRes.L3Error = (string) null;
      }
      tranOperationResult.Success = true;
      aRes.TranStatus = CCTranStatus.Approved;
      this.UpdateExternalTransaction(transactionId.Value, aRes, "FIN");
      if (flag && processingException != null)
        throw processingException;
    }
    return tranOperationResult;
  }

  private ICCLevel3Processor CreateLevel3Processor(
    CCProcessingCenter processingCenter,
    ICardProcessingReadersProvider provider)
  {
    return processingCenter != null ? this.GetLevel3Processor(this.CreatePlugin(processingCenter), provider) : throw new PXException("Processing center can't be found");
  }

  private object CreatePlugin(CCProcessingCenter processingCenter)
  {
    try
    {
      return CCPluginTypeHelper.CreatePluginInstance(processingCenter);
    }
    catch (PXException ex)
    {
      throw;
    }
    catch
    {
      throw new PXException("Cannot instantiate processing object of {0} type for the processing center {1}.", new object[2]
      {
        (object) processingCenter.ProcessingTypeName,
        (object) processingCenter.ProcessingCenterID
      });
    }
  }

  private ICCLevel3Processor GetLevel3Processor(
    object pluginObject,
    ICardProcessingReadersProvider provider)
  {
    ICCLevel3Processor processor = CCProcessingHelper.IsV2ProcessingInterface(pluginObject).CreateProcessor<ICCLevel3Processor>(new V2SettingsGenerator(provider).GetSettings());
    if (processor != null)
      return processor;
    this.CreateAndThrowException(CCProcessingFeature.Level3);
    return processor;
  }

  private void CreateAndThrowException(CCProcessingFeature feature)
  {
    throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} feature is not supported by processing", new object[1]
    {
      (object) feature
    }));
  }

  private L3DataInput CopyTranProcessingToL3DataInput(TranProcessingL3DataInput input)
  {
    if (input == null)
      return (L3DataInput) null;
    return new L3DataInput()
    {
      CustomerVatRegistration = input.CustomerVatRegistration,
      DestinationCountryCode = input.DestinationCountryCode,
      DutyAmount = input.DutyAmount,
      FreightAmount = input.FreightAmount,
      LineItems = this.CopyTranProcessingLineItemsToL3DataInputLineItems(input.LineItems),
      MerchantVatRegistration = input.MerchantVatRegistration,
      NationalTax = input.NationalTax,
      OrderDate = input.OrderDate,
      SalesTax = input.SalesTax,
      ShipfromZipCode = input.ShipfromZipCode,
      ShiptoZipCode = input.ShiptoZipCode,
      SummaryCommodityCode = input.SummaryCommodityCode,
      TaxAmount = input.TaxAmount,
      TaxExempt = input.TaxExempt,
      TaxRate = input.TaxRate,
      TransactionId = input.TransactionId,
      UniqueVatRefNumber = input.UniqueVatRefNumber,
      CardType = V2Converter.ConvertCardType(input.CardType)
    };
  }

  private List<L3DataLineItemInput> CopyTranProcessingLineItemsToL3DataInputLineItems(
    List<TranProcessingL3DataLineItemInput> inputLineItems)
  {
    return inputLineItems == null ? (List<L3DataLineItemInput>) null : inputLineItems.Select<TranProcessingL3DataLineItemInput, L3DataLineItemInput>((Func<TranProcessingL3DataLineItemInput, L3DataLineItemInput>) (lineItem => new L3DataLineItemInput()
    {
      AlternateTaxId = lineItem.AlternateTaxId,
      CommodityCode = lineItem.CommodityCode,
      DebitCredit = lineItem.DebitCredit,
      Description = lineItem.Description,
      DiscountAmount = lineItem.DiscountAmount,
      DiscountRate = lineItem.DiscountRate,
      TaxAmount = lineItem.TaxAmount,
      OtherTaxAmount = lineItem.OtherTaxAmount,
      ProductCode = lineItem.ProductCode,
      Quantity = lineItem.Quantity,
      TaxRate = lineItem.TaxRate,
      TaxTypeApplied = lineItem.TaxTypeApplied,
      TaxTypeId = lineItem.TaxTypeId,
      UnitCode = lineItem.UnitCode,
      UnitCost = lineItem.UnitCost
    })).ToList<L3DataLineItemInput>();
  }

  public virtual CCProcessingCenter GetAndCheckProcessingCenterFromTransaction(
    string processingCenterID,
    int? pMInstanceID,
    string curyID)
  {
    CCProcessingCenter procCenter = string.IsNullOrEmpty(processingCenterID) ? this._repository.FindProcessingCenter(pMInstanceID, curyID) : CCProcessingCenter.PK.Find(this._repository.Graph, processingCenterID);
    this.CheckProcessingCenter(procCenter);
    return procCenter != null && !string.IsNullOrEmpty(procCenter.ProcessingTypeName) ? procCenter : throw new PXException("Processing center for this card type is not configured properly.");
  }

  private void CheckProcessingCenter(CCProcessingCenter procCenter)
  {
    CCPluginTypeHelper.GetPluginTypeWithCheck(procCenter);
  }

  protected ICardTransactionProcessingWrapper GetProcessingWrapper(CCProcessingContext context)
  {
    return this._transactionProcessingWrapper(this.GetProcessor(context.processingCenter), context);
  }

  protected object GetProcessor(CCProcessingCenter processingCenter)
  {
    if (processingCenter == null)
      throw new PXException("Processing center can't be found");
    try
    {
      return CCPluginTypeHelper.CreatePluginInstance(processingCenter);
    }
    catch (PXException ex)
    {
      throw;
    }
    catch
    {
      throw new PXException("Cannot instantiate processing object of {0} type for the processing center {1}.", new object[2]
      {
        (object) processingCenter.ProcessingTypeName,
        (object) processingCenter.ProcessingCenterID
      });
    }
  }

  protected virtual void UpdateExternalTransaction(
    int aTranID,
    TranProcessingResult aRes,
    string aProcStatus)
  {
    PX.Objects.AR.ExternalTransaction externalTransaction = PX.Objects.AR.ExternalTransaction.PK.Find(this._repository.Graph, CCProcTran.PK.Find(this._repository.Graph, new int?(aTranID)).TransactionID);
    Level3Helper.SetL3StatusExternalTransaction(externalTransaction, new L3TranStatus?(aRes.L3Status), aRes.L3Error);
    this._repository.UpdateExternalTransaction(externalTransaction);
    this._repository.Save();
  }
}
