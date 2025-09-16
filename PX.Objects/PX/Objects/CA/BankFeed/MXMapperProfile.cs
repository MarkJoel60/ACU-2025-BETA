// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.MXMapperProfile
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using AutoMapper;
using PX.BankFeed.MX;
using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.CA.BankFeed;

internal class MXMapperProfile : Profile
{
  public MXMapperProfile()
  {
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    this.CreateMap<ConnectResponse, BankFeedFormResponse>().ForMember<string>((Expression<Func<BankFeedFormResponse, string>>) (d => d.ItemID), (Action<IMemberConfigurationExpression<ConnectResponse, BankFeedFormResponse, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<ConnectResponse, string>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ConnectResponse.get_MemberGuid))), parameterExpression1))));
    ParameterExpression parameterExpression2;
    ParameterExpression parameterExpression3;
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    this.CreateMap<MemberResponse, BankFeedFormResponse>().ForMember<string>((Expression<Func<BankFeedFormResponse, string>>) (d => d.InstitutionID), (Action<IMemberConfigurationExpression<MemberResponse, BankFeedFormResponse, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<MemberResponse, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MemberResponse.get_Member))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Member.get_InstitutionCode))), parameterExpression2)))).ForMember<string>((Expression<Func<BankFeedFormResponse, string>>) (d => d.InstitutionName), (Action<IMemberConfigurationExpression<MemberResponse, BankFeedFormResponse, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<MemberResponse, string>>((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MemberResponse.get_Member))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Member.get_Name))), parameterExpression3))));
    ParameterExpression parameterExpression4;
    ParameterExpression parameterExpression5;
    ParameterExpression parameterExpression6;
    ParameterExpression parameterExpression7;
    ParameterExpression parameterExpression8;
    ParameterExpression parameterExpression9;
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    this.CreateMap<Account, BankFeedAccount>().ForMember<string>((Expression<Func<BankFeedAccount, string>>) (d => d.AccountID), (Action<IMemberConfigurationExpression<Account, BankFeedAccount, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Account, string>>((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Account.get_Guid))), parameterExpression4)))).ForMember<string>((Expression<Func<BankFeedAccount, string>>) (d => d.Currency), (Action<IMemberConfigurationExpression<Account, BankFeedAccount, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Account, string>>((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Account.get_CurrencyCode))), parameterExpression5)))).ForMember<string>((Expression<Func<BankFeedAccount, string>>) (d => d.Mask), (Action<IMemberConfigurationExpression<Account, BankFeedAccount, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Account, string>>((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Account.get_AccountNumber))), parameterExpression6)))).ForMember<string>((Expression<Func<BankFeedAccount, string>>) (d => d.Name), (Action<IMemberConfigurationExpression<Account, BankFeedAccount, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Account, string>>((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Account.get_Name))), parameterExpression7)))).ForMember<string>((Expression<Func<BankFeedAccount, string>>) (d => d.Type), (Action<IMemberConfigurationExpression<Account, BankFeedAccount, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Account, string>>((Expression) Expression.Property((Expression) parameterExpression8, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Account.get_Type))), parameterExpression8)))).ForMember<string>((Expression<Func<BankFeedAccount, string>>) (d => d.Subtype), (Action<IMemberConfigurationExpression<Account, BankFeedAccount, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Account, string>>((Expression) Expression.Property((Expression) parameterExpression9, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Account.get_Subtype))), parameterExpression9))));
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
    ParameterExpression parameterExpression26;
    ParameterExpression parameterExpression27;
    ParameterExpression parameterExpression28;
    ParameterExpression parameterExpression29;
    ParameterExpression parameterExpression30;
    ParameterExpression parameterExpression31;
    ParameterExpression parameterExpression32;
    ParameterExpression parameterExpression33;
    ParameterExpression parameterExpression34;
    ParameterExpression parameterExpression35;
    ParameterExpression parameterExpression36;
    ParameterExpression parameterExpression37;
    ParameterExpression parameterExpression38;
    ParameterExpression parameterExpression39;
    ParameterExpression parameterExpression40;
    ParameterExpression parameterExpression41;
    ParameterExpression parameterExpression42;
    ParameterExpression parameterExpression43;
    ParameterExpression parameterExpression44;
    ParameterExpression parameterExpression45;
    ParameterExpression parameterExpression46;
    ParameterExpression parameterExpression47;
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
    this.CreateMap<Transaction, BankFeedTransaction>().ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.AccountID), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression10, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_AccountGuid))), parameterExpression10)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.PartnerAccountId), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression11, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_AccountId))), parameterExpression11)))).ForMember<Decimal?>((Expression<Func<BankFeedTransaction, Decimal?>>) (d => d.Amount), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, Decimal?>>) (m => m.MapFrom((IValueResolver<Transaction, BankFeedTransaction, Decimal?>) new MXMapperProfile.AmountResolver()))).ForMember<DateTime?>((Expression<Func<BankFeedTransaction, DateTime?>>) (d => d.Date), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, DateTime?>>) (m => m.MapFrom<DateTime>(Expression.Lambda<Func<Transaction, DateTime>>((Expression) Expression.Property((Expression) parameterExpression12, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Date))), parameterExpression12)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.IsoCurrencyCode), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression13, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_CurrencyCode))), parameterExpression13)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.Name), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression14, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Description))), parameterExpression14)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.TransactionID), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression15, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Guid))), parameterExpression15)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.Type), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression16, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Type))), parameterExpression16)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.CheckNumber), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression17, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_CheckNumberString))), parameterExpression17)))).ForMember<bool?>((Expression<Func<BankFeedTransaction, bool?>>) (d => d.Pending), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, bool?>>) (m => m.ConvertUsing<string>((IValueConverter<string, bool?>) new MXMapperProfile.PendingConverter(), Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression18, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Status))), parameterExpression18)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.Category), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom((IValueResolver<Transaction, BankFeedTransaction, string>) new MXMapperProfile.CategoryResolver()))).ForMember<DateTime?>((Expression<Func<BankFeedTransaction, DateTime?>>) (d => d.CreatedAt), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, DateTime?>>) (m => m.MapFrom<DateTime?>(Expression.Lambda<Func<Transaction, DateTime?>>((Expression) Expression.Property((Expression) parameterExpression19, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_CreatedAt))), parameterExpression19)))).ForMember<DateTime?>((Expression<Func<BankFeedTransaction, DateTime?>>) (d => d.PostedAt), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, DateTime?>>) (m => m.MapFrom<DateTime?>(Expression.Lambda<Func<Transaction, DateTime?>>((Expression) Expression.Property((Expression) parameterExpression20, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_PostedAt))), parameterExpression20)))).ForMember<DateTime?>((Expression<Func<BankFeedTransaction, DateTime?>>) (d => d.TransactedAt), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, DateTime?>>) (m => m.MapFrom<DateTime?>(Expression.Lambda<Func<Transaction, DateTime?>>((Expression) Expression.Property((Expression) parameterExpression21, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_TransactedAt))), parameterExpression21)))).ForMember<DateTime?>((Expression<Func<BankFeedTransaction, DateTime?>>) (d => d.UpdatedAt), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, DateTime?>>) (m => m.MapFrom<DateTime?>(Expression.Lambda<Func<Transaction, DateTime?>>((Expression) Expression.Property((Expression) parameterExpression22, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_UpdatedAt))), parameterExpression22)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.AccountStringId), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression23, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_AccountId))), parameterExpression23)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.CategoryGuid), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression24, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_CategoryGuid))), parameterExpression24)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.ExtendedTransactionType), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression25, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_ExtendedTransactionType))), parameterExpression25)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.Id), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression26, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Id))), parameterExpression26)))).ForMember<bool?>((Expression<Func<BankFeedTransaction, bool?>>) (d => d.IsBillPay), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, bool?>>) (m => m.MapFrom<bool?>(Expression.Lambda<Func<Transaction, bool?>>((Expression) Expression.Property((Expression) parameterExpression27, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_IsBillPay))), parameterExpression27)))).ForMember<bool?>((Expression<Func<BankFeedTransaction, bool?>>) (d => d.IsDirectDeposit), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, bool?>>) (m => m.MapFrom<bool?>(Expression.Lambda<Func<Transaction, bool?>>((Expression) Expression.Property((Expression) parameterExpression28, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_IsDirectDeposit))), parameterExpression28)))).ForMember<bool?>((Expression<Func<BankFeedTransaction, bool?>>) (d => d.IsExpense), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, bool?>>) (m => m.MapFrom<bool?>(Expression.Lambda<Func<Transaction, bool?>>((Expression) Expression.Property((Expression) parameterExpression29, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_IsExpense))), parameterExpression29)))).ForMember<bool?>((Expression<Func<BankFeedTransaction, bool?>>) (d => d.IsFee), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, bool?>>) (m => m.MapFrom<bool?>(Expression.Lambda<Func<Transaction, bool?>>((Expression) Expression.Property((Expression) parameterExpression30, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_IsFee))), parameterExpression30)))).ForMember<bool?>((Expression<Func<BankFeedTransaction, bool?>>) (d => d.IsIncome), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, bool?>>) (m => m.MapFrom<bool?>(Expression.Lambda<Func<Transaction, bool?>>((Expression) Expression.Property((Expression) parameterExpression31, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_IsIncome))), parameterExpression31)))).ForMember<bool?>((Expression<Func<BankFeedTransaction, bool?>>) (d => d.IsInternational), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, bool?>>) (m => m.MapFrom<bool?>(Expression.Lambda<Func<Transaction, bool?>>((Expression) Expression.Property((Expression) parameterExpression32, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_IsInternational))), parameterExpression32)))).ForMember<bool?>((Expression<Func<BankFeedTransaction, bool?>>) (d => d.IsOverdraftFee), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, bool?>>) (m => m.MapFrom<bool?>(Expression.Lambda<Func<Transaction, bool?>>((Expression) Expression.Property((Expression) parameterExpression33, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_IsOverdraftFee))), parameterExpression33)))).ForMember<bool?>((Expression<Func<BankFeedTransaction, bool?>>) (d => d.IsPayrollAdvance), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, bool?>>) (m => m.MapFrom<bool?>(Expression.Lambda<Func<Transaction, bool?>>((Expression) Expression.Property((Expression) parameterExpression34, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_IsPayrollAdvance))), parameterExpression34)))).ForMember<bool?>((Expression<Func<BankFeedTransaction, bool?>>) (d => d.IsRecurring), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, bool?>>) (m => m.MapFrom<bool?>(Expression.Lambda<Func<Transaction, bool?>>((Expression) Expression.Property((Expression) parameterExpression35, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_IsRecurring))), parameterExpression35)))).ForMember<bool?>((Expression<Func<BankFeedTransaction, bool?>>) (d => d.IsSubscription), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, bool?>>) (m => m.MapFrom<bool?>(Expression.Lambda<Func<Transaction, bool?>>((Expression) Expression.Property((Expression) parameterExpression36, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_IsSubscription))), parameterExpression36)))).ForMember<Decimal?>((Expression<Func<BankFeedTransaction, Decimal?>>) (d => d.Latitude), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, Decimal?>>) (m => m.MapFrom<Decimal?>(Expression.Lambda<Func<Transaction, Decimal?>>((Expression) Expression.Property((Expression) parameterExpression37, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Latitude))), parameterExpression37)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.LocalizedDescription), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression38, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_LocalizedDescription))), parameterExpression38)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.LocalizedMemo), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression39, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_LocalizedMemo))), parameterExpression39)))).ForMember<Decimal?>((Expression<Func<BankFeedTransaction, Decimal?>>) (d => d.Longitude), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, Decimal?>>) (m => m.MapFrom<Decimal?>(Expression.Lambda<Func<Transaction, Decimal?>>((Expression) Expression.Property((Expression) parameterExpression40, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Longitude))), parameterExpression40)))).ForMember<bool?>((Expression<Func<BankFeedTransaction, bool?>>) (d => d.MemberIsManagedByUser), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, bool?>>) (m => m.MapFrom<bool?>(Expression.Lambda<Func<Transaction, bool?>>((Expression) Expression.Property((Expression) parameterExpression41, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_MemberIsManagedByUser))), parameterExpression41)))).ForMember<int?>((Expression<Func<BankFeedTransaction, int?>>) (d => d.MerchantCategoryCode), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, int?>>) (m => m.MapFrom<int?>(Expression.Lambda<Func<Transaction, int?>>((Expression) Expression.Property((Expression) parameterExpression42, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_MerchantCategoryCode))), parameterExpression42)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.MerchantGuid), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression43, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_MerchantGuid))), parameterExpression43)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.MerchantLocationGuid), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression44, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_MerchantLocationGuid))), parameterExpression44)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.Metadata), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression45, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_Metadata))), parameterExpression45)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.OriginalDescription), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression46, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_OriginalDescription))), parameterExpression46)))).ForMember<string>((Expression<Func<BankFeedTransaction, string>>) (d => d.UserId), (Action<IMemberConfigurationExpression<Transaction, BankFeedTransaction, string>>) (m => m.MapFrom<string>(Expression.Lambda<Func<Transaction, string>>((Expression) Expression.Property((Expression) parameterExpression47, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Transaction.get_UserId))), parameterExpression47))));
  }

  private class PendingConverter : IValueConverter<string, bool?>
  {
    public bool? Convert(string source, ResolutionContext context)
    {
      return new bool?(source == "PENDING");
    }
  }

  private class AmountResolver : IValueResolver<Transaction, BankFeedTransaction, Decimal?>
  {
    public Decimal? Resolve(
      Transaction source,
      BankFeedTransaction destination,
      Decimal? destMember,
      ResolutionContext context)
    {
      return new Decimal?(source.Type == "CREDIT" ? source.Amount * -1M : source.Amount);
    }
  }

  private class CategoryResolver : IValueResolver<Transaction, BankFeedTransaction, string>
  {
    public string Resolve(
      Transaction source,
      BankFeedTransaction destination,
      string destMember,
      ResolutionContext context)
    {
      return string.IsNullOrEmpty(source.TopLevelCategory) || string.IsNullOrEmpty(source.Category) || !(source.TopLevelCategory != source.Category) ? source.Category : $"{source.TopLevelCategory}:{source.Category}";
    }
  }
}
