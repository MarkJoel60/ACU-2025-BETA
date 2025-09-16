// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.OrderedEmailProcessorsProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.AP.InvoiceRecognition;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

/// <summary>
/// Email processors provider. Accepts all <see cref="T:PX.Objects.EP.IEmailProcessor" />s registered with Autofac, orders them and returns ordered collection.
/// </summary>
internal class OrderedEmailProcessorsProvider : IEmailProcessorsProvider
{
  public const double DefaultOrder = 100000.0;
  private static readonly Dictionary<System.Type, double> _executionOrderByProcessorType = new Dictionary<System.Type, double>()
  {
    [typeof (ConversationEmailProcessor)] = 1.0,
    [typeof (ConfirmReceiptEmailProcessor)] = 2.0,
    [typeof (DefaultEmailProcessor)] = 3.0,
    [typeof (ExchangeEmailProcessor)] = 4.0,
    [typeof (CaseCommonEmailProcessor)] = 5.0,
    [typeof (RouterEmailProcessor)] = 6.0,
    [typeof (NewCaseEmailProcessor)] = 7.0,
    [typeof (ContactBAccountEmailProcessor)] = 8.0,
    [typeof (NotificationEmailProcessor)] = 9.0,
    [typeof (UnassignedEmailProcessor)] = 10.0,
    [typeof (AssignmentEmailProcessor)] = 11.0,
    [typeof (NewLeadEmailProcessor)] = 12.0,
    [typeof (ImageExtractorEmailProcessor)] = 13.0,
    [typeof (APInvoiceEmailProcessor)] = 14.0,
    [typeof (CleanerEmailProcessor)] = 200000.0
  };
  private readonly List<IEmailProcessor> _orderedEmailProcessors;

  public OrderedEmailProcessorsProvider(IEnumerable<IEmailProcessor> emailProcessors)
  {
    this._orderedEmailProcessors = emailProcessors.OrderBy<IEmailProcessor, double>(new Func<IEmailProcessor, double>(this.GetProcessorOrder)).ToList<IEmailProcessor>();
  }

  /// <summary>
  /// Gets <see cref="T:PX.Objects.EP.IEmailProcessor" /> execution order. A processor with a higher order will be executed after a processor with lower order.
  /// The order value DefaultOrder is assigned to custom email processors not dependent on the order of execution.
  /// </summary>
  /// <param name="emailProcessor">The email processor.</param>
  /// <returns>The processor's order.</returns>
  private double GetProcessorOrder(IEmailProcessor emailProcessor)
  {
    double num;
    return !OrderedEmailProcessorsProvider._executionOrderByProcessorType.TryGetValue(emailProcessor.GetType(), out num) ? 100000.0 : num;
  }

  /// <summary>
  /// Gets the collection of email processors. Order-dependent processors would be returned first followed by custom email processors.
  /// </summary>
  /// <returns />
  public IEnumerable<IEmailProcessor> GetEmailProcessors()
  {
    return (IEnumerable<IEmailProcessor>) this._orderedEmailProcessors;
  }
}
