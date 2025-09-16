// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.V2Converter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using AutoMapper;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers;

public class V2Converter
{
  private static readonly IMapper Mapper = new MapperConfiguration((Action<IMapperConfigurationExpression>) (cfg =>
  {
    cfg.CreateMap<CCTranType, CCTranType>().AfterMap((Action<CCTranType, CCTranType>) ((i, v2) =>
    {
      if (!Enum.IsDefined(v2.GetType(), (object) v2))
        throw new PXException("Invalid credit card transaction type: {0}. Please contact support service.", new object[1]
        {
          (object) i
        });
    }));
    cfg.CreateMap<CCTranType, CCTranType>().AfterMap((Action<CCTranType, CCTranType>) ((v2, o) =>
    {
      if (!Enum.IsDefined(o.GetType(), (object) o))
        throw new PXException("Invalid credit card transaction type: {0}. Please contact support service.", new object[1]
        {
          (object) v2
        });
    }));
    cfg.CreateMap<SettingsControlType, int?>().ConvertUsing((Expression<Func<SettingsControlType, int?>>) (i => V2Converter.convertSettingsControlType(i)));
    cfg.CreateMap<PluginSettingDetail, SettingsValue>();
    ParameterExpression parameterExpression1;
    cfg.CreateMap<SettingsDetail, PluginSettingDetail>().ForMember<string>((Expression<Func<PluginSettingDetail, string>>) (dst => dst.Value), (Action<IMemberConfigurationExpression<SettingsDetail, PluginSettingDetail, string>>) (opts => opts.MapFrom<string>(Expression.Lambda<Func<SettingsDetail, string>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SettingsDetail.get_DefaultValue))), parameterExpression1)))).ForMember<ICollection<KeyValuePair<string, string>>>((Expression<Func<PluginSettingDetail, ICollection<KeyValuePair<string, string>>>>) (dst => dst.ComboValuesCollection), (Action<IMemberConfigurationExpression<SettingsDetail, PluginSettingDetail, ICollection<KeyValuePair<string, string>>>>) (opts =>
    {
      opts.AllowNull();
      ParameterExpression parameterExpression2;
      opts.MapFrom<Dictionary<string, string>>(Expression.Lambda<Func<SettingsDetail, Dictionary<string, string>>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SettingsDetail.get_ComboValues))), parameterExpression2));
    }));
    cfg.CreateMap<HostedFormResponse, HostedFormResponse>();
  })).CreateMapper();

  public static CCTranType ConvertTranType(CCTranType v2TranType)
  {
    return V2Converter.Mapper.Map<CCTranType>((object) v2TranType);
  }

  public static CCCardType ConvertCardType(CCCardType v2TranType)
  {
    switch ((int) v2TranType)
    {
      case 0:
        return CCCardType.Other;
      case 1:
        return CCCardType.Visa;
      case 2:
        return CCCardType.MasterCard;
      case 3:
        return CCCardType.AmericanExpress;
      case 4:
        return CCCardType.Discover;
      case 5:
        return CCCardType.JCB;
      case 6:
        return CCCardType.DinersClub;
      case 7:
        return CCCardType.UnionPay;
      case 8:
        return CCCardType.Debit;
      case 9:
        return CCCardType.EFT;
      default:
        return CCCardType.Other;
    }
  }

  public static CCCardType ConvertCardType(CCCardType cardType)
  {
    switch (cardType)
    {
      case CCCardType.Other:
        return (CCCardType) 0;
      case CCCardType.Visa:
        return (CCCardType) 1;
      case CCCardType.MasterCard:
        return (CCCardType) 2;
      case CCCardType.AmericanExpress:
        return (CCCardType) 3;
      case CCCardType.Discover:
        return (CCCardType) 4;
      case CCCardType.JCB:
        return (CCCardType) 5;
      case CCCardType.DinersClub:
        return (CCCardType) 6;
      case CCCardType.UnionPay:
        return (CCCardType) 7;
      case CCCardType.Debit:
        return (CCCardType) 8;
      case CCCardType.EFT:
        return (CCCardType) 9;
      default:
        return (CCCardType) 0;
    }
  }

  public static HostedFormResponse ConvertHostedFormResponse(HostedFormResponse v2FormResponse)
  {
    return V2Converter.Mapper.Map<HostedFormResponse>((object) v2FormResponse);
  }

  public static CCTranType ConvertTranTypeToV2(CCTranType tranType)
  {
    return V2Converter.Mapper.Map<CCTranType>((object) tranType);
  }

  public static SettingsValue ConvertSettingDetailToV2(PluginSettingDetail detail)
  {
    return V2Converter.Mapper.Map<SettingsValue>((object) detail);
  }

  internal static CCTranStatus ConvertTranStatusToV2(CCTranStatus tranStatus)
  {
    return V2Converter.Mapper.Map<CCTranStatus>((object) tranStatus);
  }

  public static TranProcessingResult ConvertTranProcessingResult(ProcessingResult processingResult)
  {
    if (processingResult == null)
      throw new ArgumentNullException(nameof (processingResult));
    TranProcessingResult processingResult1 = new TranProcessingResult();
    processingResult1.AuthorizationNbr = processingResult.AuthorizationNbr;
    processingResult1.CcvVerificatonStatus = V2Converter.ConvertCvvStatus(processingResult.CcvVerificatonStatus);
    processingResult1.ExpireAfterDays = processingResult.ExpireAfterDays;
    processingResult1.Success = true;
    processingResult1.PCResponse = processingResult.ResponseText;
    processingResult1.PCResponseCode = processingResult.ResponseCode;
    processingResult1.PCResponseReasonCode = processingResult.ResponseReasonCode;
    processingResult1.PCResponseReasonText = processingResult.ResponseReasonText;
    processingResult1.PCTranNumber = processingResult.TransactionNumber;
    processingResult1.ResultFlag = CCResultFlag.None;
    processingResult1.TranStatus = CCTranStatus.Approved;
    processingResult1.CardType = V2Converter.ConvertCardType(processingResult.CardTypeCode);
    processingResult1.ProcCenterCardTypeCode = processingResult.CardType;
    processingResult1.Level3Support = processingResult.Level3Support;
    processingResult1.L3Status = V2Converter.GetL3TranStatus(processingResult.Level3Support ? "PEN" : "NA ");
    processingResult1.POSTerminalID = processingResult.POSTerminalID;
    processingResult1.LastDigits = !string.IsNullOrEmpty(processingResult.LastFourDigits) ? processingResult.LastFourDigits.Substring(processingResult.LastFourDigits.Length - 4) : string.Empty;
    CCTranType? tranType = processingResult.TranType;
    CCTranType? nullable;
    if (!tranType.HasValue)
    {
      nullable = new CCTranType?();
    }
    else
    {
      tranType = processingResult.TranType;
      nullable = new CCTranType?(V2Converter.ConvertTranType(tranType.Value));
    }
    processingResult1.TranType = nullable;
    processingResult1.TranDateTimeUTC = processingResult.TranDateTimeUTC;
    return processingResult1;
  }

  public static L3TranStatus GetL3TranStatus(string status)
  {
    if (status != null && status.Length == 3)
    {
      switch (status[1])
      {
        case 'A':
          if (status == "NA ")
            break;
          break;
        case 'E':
          switch (status)
          {
            case "PEN":
              return L3TranStatus.Pending;
            case "REJ":
              return L3TranStatus.Rejected;
          }
          break;
        case 'F':
          if (status == "RFL")
            return L3TranStatus.ResendFailed;
          break;
        case 'L':
          if (status == "FLD")
            return L3TranStatus.Failed;
          break;
        case 'N':
          if (status == "SNT")
            return L3TranStatus.Sent;
          break;
        case 'R':
          if (status == "RRJ")
            return L3TranStatus.ResendRejected;
          break;
      }
    }
    return L3TranStatus.NotApplicable;
  }

  public static string GetL3Status(L3TranStatus status)
  {
    switch (status)
    {
      case L3TranStatus.Pending:
        return "PEN";
      case L3TranStatus.Sent:
        return "SNT";
      case L3TranStatus.Failed:
        return "FLD";
      case L3TranStatus.Rejected:
        return "REJ";
      case L3TranStatus.ResendRejected:
        return "RRJ";
      case L3TranStatus.ResendFailed:
        return "RFL";
      default:
        return "NA ";
    }
  }

  public static PluginSettingDetail ConvertSettingsDetail(SettingsDetail detail)
  {
    return V2Converter.Mapper.Map<PluginSettingDetail>((object) detail);
  }

  public static int? ConvertSettingsControlType(SettingsControlType controlType)
  {
    return V2Converter.Mapper.Map<int?>((object) controlType);
  }

  public static CcvVerificationStatus ConvertCardVerificationStatus(CcvVerificationStatus status)
  {
    CcvVerificationStatus verificationStatus = V2Converter.Mapper.Map<CcvVerificationStatus>((object) status);
    if (!Enum.IsDefined(typeof (CcvVerificationStatus), (object) verificationStatus))
      verificationStatus = CcvVerificationStatus.Unknown;
    return verificationStatus;
  }

  internal static CcvVerificationStatus ConvertCardVerificationStatus(CcvVerificationStatus status)
  {
    CcvVerificationStatus verificationStatus = V2Converter.Mapper.Map<CcvVerificationStatus>((object) status);
    if (!Enum.IsDefined(typeof (CcvVerificationStatus), (object) verificationStatus))
      verificationStatus = (CcvVerificationStatus) 7;
    return verificationStatus;
  }

  public static CCTranStatus ConvertTranStatus(CCTranStatus status)
  {
    CCTranStatus ccTranStatus = V2Converter.Mapper.Map<CCTranStatus>((object) status);
    if (!Enum.IsDefined(typeof (CCTranStatus), (object) ccTranStatus))
      ccTranStatus = CCTranStatus.Unknown;
    return ccTranStatus;
  }

  public static CcvVerificationStatus ConvertCvvStatus(CcvVerificationStatus status)
  {
    return V2Converter.Mapper.Map<CcvVerificationStatus>((object) status);
  }

  private static int? convertSettingsControlType(SettingsControlType i)
  {
    switch ((int) i)
    {
      case 0:
        return new int?(1);
      case 1:
        return new int?(2);
      case 2:
        return new int?(3);
      default:
        return new int?();
    }
  }
}
