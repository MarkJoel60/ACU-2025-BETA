// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenNavigationActionStateCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUScreenNavigationActionStateCustomizedExtension : 
  PXCacheExtension<
  #nullable disable
  AUScreenNavigationActionState>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DestinationScreenIDCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? WindowModeCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public bool? IconCustomized { get; set; } = new bool?(false);

  public abstract class destinationScreenIDCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenNavigationActionStateCustomizedExtension.destinationScreenIDCustomized>
  {
  }

  public abstract class windowModeCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenNavigationActionStateCustomizedExtension.windowModeCustomized>
  {
  }

  public abstract class iconCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenNavigationActionStateCustomizedExtension.iconCustomized>
  {
  }
}
