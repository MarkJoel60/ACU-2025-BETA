// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DefaultPickPackShipModeByUser
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
namespace PX.Objects.SO;

public sealed class DefaultPickPackShipModeByUser : PXCacheExtension<
#nullable disable
UserPreferences>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.wMSFulfillment>();

  [PXDBString(4, IsFixed = true)]
  [DefaultPickPackShipModeByUser.pPSMode.List]
  [PXUIField(DisplayName = "Pick, Pack, and Ship")]
  [PXDefault("NONE")]
  public string PPSMode { get; set; }

  public abstract class pPSMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DefaultPickPackShipModeByUser.pPSMode>
  {
    public const string None = "NONE";
    public const string Pick = "PICK";
    public const string Pack = "PACK";
    public const string Ship = "SHIP";
    public const string Return = "CRTN";

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string None = "None";
      public const string Pick = "Pick";
      public const string Pack = "Pack";
      public const string Ship = "Ship";
      public const string Return = "Return";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[5]
        {
          PXStringListAttribute.Pair("NONE", "None"),
          PXStringListAttribute.Pair("PICK", "Pick"),
          PXStringListAttribute.Pair("PACK", "Pack"),
          PXStringListAttribute.Pair("SHIP", "Ship"),
          PXStringListAttribute.Pair("CRTN", "Return")
        })
      {
      }
    }
  }
}
