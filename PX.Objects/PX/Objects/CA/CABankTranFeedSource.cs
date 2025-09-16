// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranFeedSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CA;

public sealed class CABankTranFeedSource : PXCacheExtension<
#nullable disable
CABankTran>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.bankFeedIntegration>();

  [PXUIField(DisplayName = "Source")]
  [CABankFeedType.List]
  [PXDBString(1, IsFixed = true)]
  public string Source { get; set; }

  /// <summary>
  /// Indicates that the bank transaction was retrieved from the bank feed account that relates to the bank feed in the multiple mapping mode.
  /// </summary>
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Bank Feed Account", Enabled = false)]
  [PXSelector(typeof (Search<CABankFeedAccountMapping.bankFeedAccountMapID>), new Type[] {typeof (CABankFeedAccountMapping.accountName)}, DescriptionField = typeof (CABankFeedAccountMapping.accountNameMask))]
  public Guid? BankFeedAccountMapID { get; set; }

  public static class FK
  {
    public class BankFeedAccountMappingRow : 
      PrimaryKeyOf<CABankFeedAccountMapping>.By<CABankFeedAccountMapping.bankFeedAccountMapID>.ForeignKeyOf<CABankFeedAccountMapping>.By<CABankTranFeedSource.bankFeedAccountMapID>
    {
    }
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranFeedSource.source>
  {
  }

  public abstract class bankFeedAccountMapID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABankTranFeedSource.bankFeedAccountMapID>
  {
  }
}
