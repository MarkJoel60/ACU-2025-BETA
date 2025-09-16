// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.PlaidMapperProfile
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using AutoMapper;
using PX.BankFeed.Plaid;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.CA.BankFeed;

internal class PlaidMapperProfile : Profile
{
  public PlaidMapperProfile()
  {
    ParameterExpression parameterExpression1;
    ParameterExpression parameterExpression2;
    ParameterExpression parameterExpression3;
    ParameterExpression parameterExpression4;
    ParameterExpression parameterExpression5;
    ParameterExpression parameterExpression6;
    ParameterExpression parameterExpression7;
    ParameterExpression parameterExpression8;
    ParameterExpression parameterExpression9;
    ParameterExpression parameterExpression10;
    ParameterExpression parameterExpression11;
    ParameterExpression parameterExpression12;
    ParameterExpression parameterExpression13;
    ParameterExpression parameterExpression14;
    ParameterExpression parameterExpression15;
    ParameterExpression parameterExpression16;
    ParameterExpression parameterExpression17;
    ParameterExpression parameterExpression18;
    ParameterExpression parameterExpression19;
    ParameterExpression parameterExpression20;
    ParameterExpression parameterExpression21;
    ParameterExpression parameterExpression22;
    ParameterExpression parameterExpression23;
    ParameterExpression parameterExpression24;
    ParameterExpression parameterExpression25;
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    this.CreateMap<Transaction, BankFeedTransaction>().ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.Category), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.ConvertUsing<List<string>>((IValueConverter<List<string>, string>) new PlaidMapperProfile.CategoryConverter(), Expression.Lambda<Func<Transaction, List<string>>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Categories))), parameterExpression1)))).ForMember<DateTime?>((Expression<Func<BankFeedTransaction, DateTime?>>) (d => d.AuthorizedDate), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, DateTime?>>) (m => m.MapFrom<DateTime?>(Expression.Lambda<Func<Transaction, DateTime?>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_AuthorizedDate))), parameterExpression2)))).ForMember<DateTime?>((Expression<Func<BankFeedTransaction, DateTime?>>) (d => d.AuthorizedDatetime), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, DateTime?>>) (m => m.MapFrom<DateTime?>(Expression.Lambda<Func<Transaction, DateTime?>>((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_AuthorizedDatetime))), parameterExpression3)))).ForMember<DateTime?>((Expression<Func<BankFeedTransaction, DateTime?>>) (d => d.DatetimeValue), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, DateTime?>>) (m => m.MapFrom<DateTime?>(Expression.Lambda<Func<Transaction, DateTime?>>((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_DatetimeValue))), parameterExpression4)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.Address), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Location))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Location.get_Address))), parameterExpression5)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.City), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Location))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Location.get_City))), parameterExpression6)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.Country), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Location))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Location.get_Country))), parameterExpression7)))).ForMember<Decimal?>((Expression<Func<BankFeedTransaction, Decimal?>>) (d => d.Latitude), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, Decimal?>>) (m => m.MapFrom<Decimal?>(Expression.Lambda<Func<Transaction, Decimal?>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression8, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Location))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Location.get_Lat))), parameterExpression8)))).ForMember<Decimal?>((Expression<Func<BankFeedTransaction, Decimal?>>) (d => d.Longitude), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, Decimal?>>) (m => m.MapFrom<Decimal?>(Expression.Lambda<Func<Transaction, Decimal?>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression9, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Location))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Location.get_Lon))), parameterExpression9)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.PostalCode), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression10, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Location))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Location.get_PostalCode))), parameterExpression10)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.Region), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression11, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Location))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Location.get_Region))), parameterExpression11)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.StoreNumber), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression12, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Location))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Location.get_StoreNumber))), parameterExpression12)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.MerchantName), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression13, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_MerchantName))), parameterExpression13)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.PaymentChannel), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression14, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_PaymentChannel))), parameterExpression14)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.ByOrderOf), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression15, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_PaymentMetadata))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PaymentMetadata.get_ByOrderOf))), parameterExpression15)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.Payee), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression16, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_PaymentMetadata))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PaymentMetadata.get_Payee))), parameterExpression16)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.Payer), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression17, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_PaymentMetadata))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PaymentMetadata.get_Payer))), parameterExpression17)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.PaymentMethod), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression18, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_PaymentMetadata))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PaymentMetadata.get_PaymentMethod))), parameterExpression18)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.PaymentProcessor), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression19, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_PaymentMetadata))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PaymentMetadata.get_PaymentProcessor))), parameterExpression19)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.PpdId), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression20, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_PaymentMetadata))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PaymentMetadata.get_PpdId))), parameterExpression20)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.Reason), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression21, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_PaymentMetadata))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PaymentMetadata.get_Reason))), parameterExpression21)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.ReferenceNumber), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression22, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_PaymentMetadata))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PaymentMetadata.get_ReferenceNumber))), parameterExpression22)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.PersonalFinanceCategory), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.ConvertUsing<PersonalFinanceCategory>((IValueConverter<PersonalFinanceCategory, string>) new PlaidMapperProfile.PersonFinanceCategoryConverter(), Expression.Lambda<Func<Transaction, PersonalFinanceCategory>>((Expression) Expression.Property((Expression) parameterExpression23, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_PersonalFinanceCategory))), parameterExpression23)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.TransactionCode), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression24, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_TransactionCode))), parameterExpression24)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.UnofficialCurrencyCode), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression25, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_UnofficialCurrencyCode))), parameterExpression25))));
    ParameterExpression parameterExpression26;
    // ISSUE: method reference
    // ISSUE: method reference
    this.CreateMap<Account, BankFeedAccount>().ForMember<string>((Expression<Func<BankFeedAccount, string>>) (d => d.Currency), (Action<IMemberConfigurationExpression<Account, BankFeedAccount, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Account, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression26, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Account.get_Balances))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Balances.get_IsoCurrencyCode))), parameterExpression26))));
    this.CreateMap<ExchangeTokenResponse, BankFeedFormResponse>();
    ParameterExpression parameterExpression27;
    ParameterExpression parameterExpression28;
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    this.CreateMap<ConnectResponse, BankFeedFormResponse>().ForMember<string>((Expression<Func<BankFeedFormResponse, string>>) (d => d.InstitutionID), (Action<IMemberConfigurationExpression<ConnectResponse, BankFeedFormResponse, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<ConnectResponse, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression27, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ConnectResponse.get_Metadata))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (BankFeedMetadata.get_Institution))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Institution.get_InstitutionId))), parameterExpression27)))).ForMember<string>((Expression<Func<BankFeedFormResponse, string>>) (d => d.InstitutionName), (Action<IMemberConfigurationExpression<ConnectResponse, BankFeedFormResponse, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<ConnectResponse, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression28, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ConnectResponse.get_Metadata))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (BankFeedMetadata.get_Institution))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Institution.get_Name))), parameterExpression28))));
  }

  private class CategoryConverter : IValueConverter<List<string>, string>
  {
    public string Convert(List<string> source, ResolutionContext context)
    {
      return source == null ? (string) null : string.Join(":", (IEnumerable<string>) source);
    }
  }

  private class PersonFinanceCategoryConverter : IValueConverter<PersonalFinanceCategory, string>
  {
    public string Convert(
      PersonalFinanceCategory personalFinanceCategory,
      ResolutionContext context)
    {
      return personalFinanceCategory == null ? (string) null : $"{personalFinanceCategory.Primary},{personalFinanceCategory.Detailed}";
    }
  }
}
