// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.Imc.IVCalendarFactory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Export.Imc;
using System.Collections;

#nullable disable
namespace PX.Objects.EP.Imc;

[PXInternalUseOnly]
public interface IVCalendarFactory
{
  vCalendar CreateVCalendar(IEnumerable events);

  vEvent CreateVEvent(object item);
}
