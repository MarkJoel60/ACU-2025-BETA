// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.LicenseTypeGridFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class LicenseTypeGridFilter : FSLicenseType
{
  [PXDBString(15, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "License Type ID", Enabled = false)]
  public override 
  #nullable disable
  string LicenseTypeCD { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public override string Descr { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Mem_Selected { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "FromService")]
  public virtual bool? Mem_FromService { get; set; }

  /// <summary>
  /// Check if the given license type identifier is required for any service specified in the given service identifier list.
  /// </summary>
  /// <param name="graph">Context graph that will be used in the query execution.</param>
  /// <param name="licenseTypeID">License type identifier that will be use to check if the requirements are met.</param>
  /// <param name="serviceIDList">Service identifier list that will be consulted for the license type requirement.</param>
  /// <returns>Returns true if any of the services.</returns>
  public static bool IsThisLicenseTypeRequiredByAnyService(
    PXGraph graph,
    int? licenseTypeID,
    List<int?> serviceIDList)
  {
    if (serviceIDList.Count == 0 || !licenseTypeID.HasValue)
      return false;
    return SharedFunctions.GetItemWithList<FSServiceLicenseType, FSServiceLicenseType.serviceID, FSServiceLicenseType.licenseTypeID, Where<FSServiceLicenseType.licenseTypeID, Equal<Required<FSServiceLicenseType.licenseTypeID>>>>(graph, serviceIDList, (object) licenseTypeID).Count > 0;
  }

  public abstract class mem_Selected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LicenseTypeGridFilter.mem_Selected>
  {
  }

  public abstract class mem_FromService : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LicenseTypeGridFilter.mem_FromService>
  {
  }
}
