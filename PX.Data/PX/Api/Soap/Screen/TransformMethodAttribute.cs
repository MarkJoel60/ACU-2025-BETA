// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.Screen.TransformMethodAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Web.Services.Protocols;

#nullable disable
namespace PX.Api.Soap.Screen;

[AttributeUsage(AttributeTargets.Method)]
internal class TransformMethodAttribute : SoapExtensionAttribute
{
  public override Type ExtensionType => typeof (TransformMethodExtension);

  public override int Priority { get; set; }
}
