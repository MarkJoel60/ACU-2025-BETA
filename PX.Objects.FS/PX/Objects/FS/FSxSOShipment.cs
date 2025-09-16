// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxSOShipment
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.FS;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
public class FSxSOShipment : PXCacheExtension<
#nullable disable
SOShipment>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXBool]
  [PXUnboundDefault(true)]
  public virtual bool? IsFSRelated { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Installed", Enabled = false)]
  [PXUIVisible(typeof (Where<FSxSOShipment.isFSRelated, Equal<True>>))]
  public virtual bool? Installed { get; set; }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  public abstract class isFSRelated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxSOShipment.isFSRelated>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  public abstract class installed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxSOShipment.installed>
  {
  }
}
