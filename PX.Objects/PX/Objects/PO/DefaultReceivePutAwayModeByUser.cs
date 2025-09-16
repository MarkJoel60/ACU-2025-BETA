// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.DefaultReceivePutAwayModeByUser
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.PO;

public sealed class DefaultReceivePutAwayModeByUser : PXCacheExtension<
#nullable disable
UserPreferences>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.wMSReceiving>();

  [PXDBString(4, IsFixed = true)]
  [DefaultReceivePutAwayModeByUser.rPAMode.List]
  [PXUIField(DisplayName = "Receive and Put Away")]
  [PXDefault("NONE")]
  public string RPAMode { get; set; }

  public abstract class rPAMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DefaultReceivePutAwayModeByUser.rPAMode>
  {
    public const string None = "NONE";
    public const string Receive = "RCPT";
    public const string ReceiveTransfer = "TRRC";
    public const string Return = "VRTN";
    public const string PutAway = "PTAW";

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string None = "None";
      public const string Receive = "Receive";
      public const string ReceiveTransfer = "Receive Transfer";
      public const string Return = "Return";
      public const string PutAway = "Put Away";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[5]
        {
          PXStringListAttribute.Pair("NONE", "None"),
          PXStringListAttribute.Pair("RCPT", "Receive"),
          PXStringListAttribute.Pair("TRRC", "Receive Transfer"),
          PXStringListAttribute.Pair("VRTN", "Return"),
          PXStringListAttribute.Pair("PTAW", "Put Away")
        })
      {
      }
    }
  }
}
