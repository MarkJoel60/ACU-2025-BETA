// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.GroupTypes
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public static class GroupTypes
{
  public class ListAttribute : PX.Objects.PM.GroupTypes.ListAttribute
  {
    public const 
    #nullable disable
    string ServiceContract = "Service Contract";

    public ListAttribute()
    {
      Array.Resize<string>(ref this._AllowedValues, this._AllowedValues.Length + 1);
      this._AllowedValues[this._AllowedValues.Length - 1] = "Service Contract";
      Array.Resize<string>(ref this._AllowedLabels, this._AllowedLabels.Length + 1);
      this._AllowedLabels[this._AllowedLabels.Length - 1] = "Service Contract";
    }

    public class ServiceContractType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      GroupTypes.ListAttribute.ServiceContractType>
    {
      public ServiceContractType()
        : base("Service Contract")
      {
      }
    }
  }
}
