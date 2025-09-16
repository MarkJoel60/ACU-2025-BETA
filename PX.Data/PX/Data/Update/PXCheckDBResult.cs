// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXCheckDBResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Model;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Update;

public class PXCheckDBResult
{
  public bool NeedUpdate
  {
    get
    {
      if (!this.UpdateSupported)
        return false;
      return this.CompareResult == PXDBVersCompareResults.Early || this.CompareResult == PXDBVersCompareResults.Empty;
    }
  }

  public bool UpdateSupported { get; private set; }

  public PXDBVersCompareResults CompareResult { get; private set; }

  public Version AssemblyVersion { get; private set; }

  public IEnumerable<DataVersion> DatabaseVersions { get; private set; }

  public string HostName { get; private set; }

  public string InstanceID { get; private set; }

  public PXCheckDBResult(
    PXDBVersCompareResults compareResult,
    string hostName,
    string instanceID,
    bool updateSupported,
    Version assemblyVersion = null,
    IEnumerable<DataVersion> databaseVersion = null)
  {
    this.CompareResult = compareResult;
    this.UpdateSupported = updateSupported;
    this.HostName = hostName;
    this.InstanceID = instanceID;
    this.AssemblyVersion = assemblyVersion;
    this.DatabaseVersions = databaseVersion;
  }

  public override string ToString()
  {
    return $"UpdateSupported: {this.UpdateSupported}, vAssembly: {this.AssemblyVersion}, CompareResult: {this.CompareResult}, vDatabase: {(this.DatabaseVersions == null ? "(null)" : string.Join<DataVersion>("; ", this.DatabaseVersions))}, Host = {this.HostName}, NeedUpdate = {this.NeedUpdate}";
  }
}
