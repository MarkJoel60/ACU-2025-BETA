// Decompiled with JetBrains decompiler
// Type: PX.Data.PXQueryParameters
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

public class PXQueryParameters
{
  private Func<PXView, object[]> fn;

  internal object[] GetParameters(PXView view) => this.fn(view);

  public static PXQueryParameters SelectBound(object[] currents, object[] parameters)
  {
    return new PXQueryParameters()
    {
      fn = (Func<PXView, object[]>) (v => v.PrepareParameters(currents, parameters))
    };
  }

  public static PXQueryParameters FromCurrents(params object[] currents)
  {
    return new PXQueryParameters()
    {
      fn = (Func<PXView, object[]>) (v => v.PrepareParameters(currents, (object[]) null))
    };
  }

  public static PXQueryParameters ExplicitParameters(params object[] queryParameters)
  {
    return new PXQueryParameters()
    {
      fn = (Func<PXView, object[]>) (v => queryParameters)
    };
  }

  public static PXQueryParameters ExtractFromRecord(IBqlTable row)
  {
    return new PXQueryParameters()
    {
      fn = (Func<PXView, object[]>) (v => v.GetParameterValues(row))
    };
  }

  public static PXQueryParameters ExtractFromRecord(PXResult row)
  {
    return new PXQueryParameters()
    {
      fn = (Func<PXView, object[]>) (v => v.GetParameterValues(row))
    };
  }
}
