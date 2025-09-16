// Decompiled with JetBrains decompiler
// Type: PX.Common.PXCultureScope
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Globalization;
using System.Threading;

#nullable disable
namespace PX.Common;

public class PXCultureScope : IDisposable
{
  protected CultureInfo previousThreadCulture;
  protected CultureInfo previousThreadUICulture;
  protected CultureInfo previousCulture;
  protected CultureInfo previousUICulture;

  protected PXCultureScope()
  {
  }

  public PXCultureScope(CultureInfo culture) => this.PutCulture(culture);

  protected void PutCulture(CultureInfo culture)
  {
    this.previousThreadCulture = Thread.CurrentThread.CurrentCulture;
    this.previousThreadUICulture = Thread.CurrentThread.CurrentUICulture;
    this.previousCulture = PXContext.PXIdentity.Culture;
    this.previousUICulture = PXContext.PXIdentity.UICulture;
    Thread.CurrentThread.CurrentCulture = culture;
    Thread.CurrentThread.CurrentUICulture = culture;
    PXContext.PXIdentity.Culture = culture;
    PXContext.PXIdentity.UICulture = culture;
  }

  public virtual void Dispose()
  {
    if (this.previousThreadCulture == null)
      return;
    Thread.CurrentThread.CurrentCulture = this.previousThreadCulture;
    Thread.CurrentThread.CurrentUICulture = this.previousThreadUICulture;
    PXContext.PXIdentity.Culture = this.previousCulture;
    PXContext.PXIdentity.UICulture = this.previousUICulture;
  }
}
